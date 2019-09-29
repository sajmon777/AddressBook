import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ErrorHandlerService } from './services/error-handler.service';
import { ContactManageComponent } from './components/contact/contact-manage/contact-manage.component';
import { ContactListComponent } from './components/contact/contact-list/contact-list.component';
import { HttpClientModule } from '@angular/common/http';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ContactFilterComponent } from './components/contact/contact-filter/contact-filter.component';
import { ValidationErrorModalComponent } from './components/validation-error-modal/validation-error-modal.component';
import { ErrorModalComponent } from './components/error-modal/error-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactListComponent,
    ContactManageComponent,
    ContactFilterComponent,
    ValidationErrorModalComponent,
    ErrorModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule
  ],
  providers: [
    ErrorHandlerService,
    { provide: ErrorHandler, useClass: ErrorHandlerService },

  ],
  bootstrap: [AppComponent],
  entryComponents: [
    ValidationErrorModalComponent,
    ErrorModalComponent
  ]
})
export class AppModule { }
