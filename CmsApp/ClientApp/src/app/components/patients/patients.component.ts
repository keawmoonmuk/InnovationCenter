
import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations
import { ConfigurationService } from '../../services/configuration.service';


@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss'],
  animations: [fadeInOut]
})
export class PatientsComponent implements OnInit {

  constructor(private configurations : ConfigurationService)
  { }



  ngOnInit(): void {
  }
  get firstname(): string {
  
    return this.configurations.baseUrl + '/api/Patients/patientall';
  }
}
