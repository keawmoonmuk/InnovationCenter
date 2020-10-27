
import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations
import { ConfigurationService } from '../../services/configuration.service';    //import configuration service
import { DataconfigService } from '../../services/dataconfig.service';   //import data config
import { config } from 'rxjs';
import { Patient } from '../../models/patient.model';
import { error } from '@angular/compiler/src/util';


@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss'],
  animations: [fadeInOut]
})
export class PatientsComponent implements OnInit {

  loading: boolean = false;
  errorMessage;

  userNamePatinet: string = 'patientall'

  patientrepo: any = [];

  constructor(private dataservice: DataconfigService, private configurationservice: ConfigurationService) { }


  ngOnInit(): void {

    //this.getdataPatient();
  };

  //getdataPatient() {
  //  this.dataservice.getRepo()
  //    .subscribe(data => {
  //      for (const d of (data as any)) {
  //        this.patientrepo.push({
  //          patientIDcard: d.patientIDcard,
  //          patientFirstname: d.patientFirstname
  //        });
  //      }
  //      console.log(this.patientrepo);
  //    });
  //}

}
