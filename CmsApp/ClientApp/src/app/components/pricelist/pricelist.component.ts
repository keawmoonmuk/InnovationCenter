
import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';     //import service animations

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.scss'],
  animations: [fadeInOut]
})
export class PricelistComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
