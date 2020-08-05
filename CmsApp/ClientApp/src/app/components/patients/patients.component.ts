import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss'],
  animations: [fadeInOut]
})
export class PatientsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
