import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { dataNewEvent } from 'src/app/interfaces/dataNewEvent';
import { typeEvents } from 'src/app/interfaces/typeEvents';

@Component({
  selector: 'app-new-event',
  templateUrl: './new-event.component.html',
  styleUrls: ['./new-event.component.scss'],
})
export class NewEventComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<NewEventComponent>, @Inject(MAT_DIALOG_DATA) public data: dataNewEvent, public translateService: TranslateService) {
    const lenguageCode: string = data.lenguageCode;
    this.translateService.use(lenguageCode);
  }
  
  ngOnInit(): void {
  }
  
  form: FormGroup = new FormGroup({
    nombre: new FormControl(''),
  });

  typeEvents: typeEvents[] = [
    { name: 'Evento', color: 'accent' },
    { name: 'Tarea', color: 'warn' },
    { name: 'Cumpleaños', color: 'primary' },
    { name: 'Otro', color: 'default' },
  ];

  event: any = {
    title: '',
    start: '',
    end: '',
    allDay: false,
    color: '',
  };
}