import { Component, OnInit, Input } from '@angular/core';
import { RuleViolation } from 'src/app/infrastructure/error-info';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html'
})
export class ErrorModalComponent implements OnInit {

  constructor(public activeModal: NgbActiveModal) { }

  @Input() title: string;
  @Input() message: string;

  ngOnInit() {
  }

  close() {
    this.activeModal.close();
  }
}
