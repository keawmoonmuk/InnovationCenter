
export class Patient {

  constructor(patientIDcard?: string, patientHN?: string, patientprefix?: string, patientFirstname?: string, patientLastname?: string, dateOfbrith?: string, age?: string) {

    this.patientIDcard = patientIDcard;
    this.patientHN = patientHN;
    this.patientprefix = patientprefix;
    this.patientFirstname = patientFirstname;
    this.patientLastname = patientLastname;
    this.dateOfbrith = dateOfbrith;
    this.age = age;
  }


  public patientIDcard: string;
  public patientHN: string;
  public fullName: string;
  public patientprefix: string;
  public patientFirstname: string;
  public patientLastname: string;
  public dateOfbrith: string;
  public age: string;
  //public isLockedOut: boolean;
  //public roles: string[];
}
