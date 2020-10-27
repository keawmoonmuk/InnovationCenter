import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { release } from 'os';


@Injectable({
  providedIn: 'root'
})
export class DataconfigService {

  baseUrl_base: string =  "https://localhost:44350/api/Patients/patientall";
  constructor(private httpCliet: HttpClient) { }

  getRepo() {

    return this.httpCliet.get(this.baseUrl_base);

  }


}
