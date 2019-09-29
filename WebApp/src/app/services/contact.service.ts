import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { ErrorHandlerService } from './error-handler.service';
import { catchError, map, tap } from 'rxjs/operators';
import { Contact, ContactFilter } from '../model/contact';
import { ErrorDetails } from '../infrastructure/error-info';
import { GridInfo, GridResult } from '../infrastructure/grid-info';

@Injectable({
    providedIn: 'root',
})
export class ContactService {

    private contactUrl = '/api/contact/';

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    constructor(private http: HttpClient, private errorService: ErrorHandlerService) { }

    getContact(id: number): Observable<Contact> {
        const url = `${this.contactUrl}/${id}`;
        return this.http.get<Contact>(url).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    getContactList(): Observable<Contact[]> {
        const url = `${this.contactUrl}`;
        return this.http.get<Contact[]>(url).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    getContactGrid(gridInfo: GridInfo<ContactFilter>): Observable<GridResult<Contact>> {
        return this.http.post<GridResult<Contact>>(this.contactUrl + 'grid', gridInfo, this.httpOptions).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    addContact(contact: Contact): Observable<Contact> {
        return this.http.post<Contact>(this.contactUrl, contact, this.httpOptions).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    updateContact(contact: Contact): Observable<any> {
        return this.http.put(this.contactUrl, contact, this.httpOptions).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    deleteContact(id: number): Observable<object> {
        const url = `${this.contactUrl}/${id}`;
        return this.http.delete(url, this.httpOptions).pipe(
            catchError(this.errorService.handleHttpError)
        );
    }

    // private handleError(error) {
    //    // this.errorService.openFormModal();
    //     let errorMessage = '';
    //     if (error.error instanceof ErrorEvent) {
    //         // client-side error
    //         errorMessage = `Error: ${error.error.message}`;
    //     } else {
    //         // server-side error
    //         const toDo = error.error as ErrorDetails;

    //         errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    //         //return throwError(toDo);
    //     }
    //     window.alert(errorMessage);
    //     return throwError(errorMessage);
    // }
}
