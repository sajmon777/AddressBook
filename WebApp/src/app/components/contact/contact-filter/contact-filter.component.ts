import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ContactFilter } from 'src/app/model/contact';

@Component({
  selector: 'app-contact-filter',
  templateUrl: './contact-filter.component.html'
})
export class ContactFilterComponent implements OnInit {

  @Output() filterChange = new EventEmitter<ContactFilter>();

  contactFilter: ContactFilter;
  constructor() { }

  ngOnInit() {
    this.contactFilter = { firstName: '', lastName: '', address: '', telephoneNumber: '' };
  }

  filter(): void {
    this.filterChange.emit(this.contactFilter);
  }
}
