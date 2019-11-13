import { Component, OnInit } from '@angular/core';
import { NgbTooltipConfig } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  tipContent = 'test ';

  constructor() { }

  currentRate = 0;

  ngOnInit() {

  }

  getToolTip(index: number): string {

    if (index > 3) {
      return 'Biba';
    }
    return 'Test';

  }

  rateChange() {
    console.log(this.currentRate);

  }

}
