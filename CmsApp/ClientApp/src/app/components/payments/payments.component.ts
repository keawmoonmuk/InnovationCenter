
import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations  

@Component({
  selector: 'app-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.scss'],
  animations: [fadeInOut]
})
export class PaymentsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
