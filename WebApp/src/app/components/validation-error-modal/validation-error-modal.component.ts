import { Component, OnInit, Input } from '@angular/core';
import { RuleViolation } from 'src/app/infrastructure/error-info';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-validation-error-modal',
  templateUrl: './validation-error-modal.component.html'
})
export class ValidationErrorModalComponent implements OnInit {

  constructor(public activeModal: NgbActiveModal) { }

  @Input() validationErrors: RuleViolation[];

  ngOnInit() {
  }

  close() {
    this.activeModal.close();
  }
}
