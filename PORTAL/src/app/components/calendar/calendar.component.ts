import { Component } from '@angular/core';
import { CalendarOptions, EventClickArg, EventSourceInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import interactionPlugin, { DateClickArg } from '@fullcalendar/interaction';
import bootstrapPlugin from '@fullcalendar/bootstrap';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { NewEventComponent } from '../new-event/new-event.component';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
  constructor(public dialog: MatDialog, public translateService: TranslateService, private http: HttpClient) { }

  ngOnInit(): void {
    this.test();
  }

  // Initialize calendar with options
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    headerToolbar: {
      left: 'prev,next,today',
      center: 'title',
      right: 'timeGridDay,timeGridWeek,dayGridMonth,agendaView',
    },
    views: {
      dayGrid: {
        dayHeaderFormat: {
          weekday: 'long',
        },
      },
      timeGrid: {
        dayHeaderFormat: {
          weekday: 'long',
          day: 'numeric',
        },
        slotEventOverlap: true,
      },
      agendaView: {
        type: 'list',
        duration: { days: 31 },
        buttonText: 'Agenda',
      }
    },
    plugins: [interactionPlugin, dayGridPlugin, timeGridPlugin, listPlugin, bootstrapPlugin],
    eventClick: this.handleEventClick.bind(this),
    dateClick: this.handleDateClick.bind(this),
  };

  // Fetch events from API
  eventsPromise: Promise<EventSourceInput> = new Promise((resolve) => {
    setTimeout(() => {
      resolve([
        { title: 'event 1', date: '2024-01-27' },
        { title: 'event 2', date: '2024-01-27' }
        ]);
    }
    , 2000);
  });

  // Handle event click
  handleEventClick(arg: EventClickArg) {
    alert('event click! ' + arg.event.title);
  }

  handleDateClick(arg: DateClickArg) {
    let config: MatDialogConfig = {
      data: {
        lenguageCode: this.translateService.currentLang
      }
    }

    const dialogRef = this.dialog.open(NewEventComponent,config);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  test(){
    const proxyUrl = 'http://localhost:8888/api/proxy'; // Reemplaza con la URL y puerto correctos

    this.http.get(proxyUrl).subscribe({
      next: (responseData) => {
        let data = responseData;
        console.log('Respuesta del servidor:', data);
      },
      error: (error) => {
        console.error('Error al hacer la solicitud:', error);
      }
    });
  }

}
