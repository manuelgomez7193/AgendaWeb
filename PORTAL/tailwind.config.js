/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js}"],
  theme: {
    extend: {
      height: {
        '10': '10%',
        '90': '90%',
      }
    }
  },
  postcssOptions: { plugins: [] }
}
