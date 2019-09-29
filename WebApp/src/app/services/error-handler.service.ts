import { ErrorHandler, Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { throwError, Observable, empty, of } from 'rxjs';
import { ErrorDetails, RuleViolation } from '../infrastructure/error-info';
import { HttpErrorResponse } from '@angular/common/http';
import { ValidationErrorModalComponent } from '../components/validation-error-modal/validation-error-modal.component';
import { ErrorModalComponent } from '../components/error-modal/error-modal.component';

@Injectable({
    providedIn: 'root',
})
export class ErrorHandlerService implements ErrorHandler {

    constructor(private modalService: NgbModal) { }

    handleHttpError = (error) => {
        if (error instanceof HttpErrorResponse) {
            if (error.status === 400) {
                if (error.error.ValidationErrors && error.error.ValidationErrors.length > 0) {
                    this.openValidationErrorModal(error.error.ValidationErrors);
                }
            } else if (error.status === 500) {
                this.openErrorModal('Server Error', error.error.Message);
            } else {
                this.openErrorModal('Server Error', error.message);
            }
        }
        return throwError(error);
    }

    handleError(error) {
    }

    private openValidationErrorModal(validationErrors: RuleViolation[]) {
        const modalRef = this.modalService.open(ValidationErrorModalComponent);
        modalRef.componentInstance.validationErrors = validationErrors;
    }

    private openErrorModal(title: string, message: string) {
        const modalRef = this.modalService.open(ErrorModalComponent);
        modalRef.componentInstance.title = title;
        modalRef.componentInstance.message = message;
    }
}
