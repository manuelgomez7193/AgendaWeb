using System.Net;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            int proxyPort = 8888;
            string proxyPrefix = $"http://localhost:{proxyPort}/";

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(proxyPrefix);
            listener.Start();
            Console.WriteLine($"Proxy escuchando en el puerto {proxyPort}...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                _ = HandleContextAsync(context);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Se produjo una excepción: " + ex.Message);
        }
    }

    static async Task HandleContextAsync(HttpListenerContext context)
    {
        try
        {
            // Habilita CORS en el contexto de la solicitud
            context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200"); // Reemplaza con la URL de tu frontend

            // Obtiene la solicitud HTTP del cliente.
            HttpListenerRequest request = context.Request;

            // Verifica si la solicitud se realiza en la ruta /api/proxy.
            if (request.Url.AbsolutePath.Equals("/api/proxy", StringComparison.OrdinalIgnoreCase))
            {
                // Procesa la solicitud en la ruta /api/proxy.

                string serviciosUrl = "https://localhost:44379/WeatherForecast";

                // Crea una solicitud al servidor de destino (tu controlador).
                HttpWebRequest proxyRequest = (HttpWebRequest)WebRequest.Create(serviciosUrl);

                proxyRequest.Method = "GET"; // O el método que corresponda a tu caso.

                // Envía la solicitud al servidor de destino.
                using (HttpWebResponse proxyResponse = (HttpWebResponse)await proxyRequest.GetResponseAsync())
                {
                    // Copia la respuesta del servidor de destino al cliente.
                    context.Response.StatusCode = (int)proxyResponse.StatusCode;
                    context.Response.StatusDescription = proxyResponse.StatusDescription;

                    foreach (string headerName in proxyResponse.Headers.AllKeys)
                    {
                        context.Response.Headers.Add(headerName, proxyResponse.Headers[headerName]);
                    }

                    using (var responseStream = proxyResponse.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(context.Response.OutputStream);
                    }
                }
            }
            else
            {
                // Maneja otras rutas o solicitudes aquí si es necesario.
                // Puedes agregar lógica adicional para manejar diferentes rutas.
            }

            context.Response.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Se produjo una excepción: " + ex.Message);
        }
    }
}
