
import { Component, Inject } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations
import { ConfigurationService } from '../../services/configuration.service';    //import configuration service
//import { DataconfigService } from '../../services/dataconfig.service';   //import data config

//import { Patient } from '../../models/patient.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss'],
  animations: [fadeInOut]
})
export class PatientsComponent {

  public forPatinet: Patients[];
  
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Patients[]>(baseUrl + 'api/Patients/patientall').subscribe(result => {
      this.forPatinet = result;
    }, error => console.error(error));
    console.log("this for patients ==> " + this.forPatinet);
   
  }

}

interface Patients {

   patientIDcard: string;
   patientHN: string;
   fullName: string;
   patientprefix: string;
   patientFirstname: string;
   patientLastname: string;
   dateOfbrith: string;
   age: number;

}
