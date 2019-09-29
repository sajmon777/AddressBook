import { Component, OnInit, Input } from '@angular/core';
import { Contact, ContactFilter } from 'src/app/model/contact';
import { ContactService } from 'src/app/services/contact.service';
import { ActivatedRoute } from '@angular/router';
import { GridInfo } from 'src/app/infrastructure/grid-info';

@Component({
  selector: 'app-list-detail',
  templateUrl: './contact-list.component.html'
})
export class ContactListComponent implements OnInit {

  constructor(private route: ActivatedRoute, private contactService: ContactService) { }

  pageSize = 10;
  page = 1;
  collectionSize: number;
  contacts: Contact[];
  filter: ContactFilter;

  ngOnInit() {
    this.lodaData();
  }

  lodaData(): void {
    this.contactService.getContactGrid({ page: this.page, pageSize: this.pageSize, filter: this.filter } as GridInfo<ContactFilter>)
      .subscribe(gridResult => {
        this.contacts = gridResult.data;
        this.collectionSize = gridResult.rowCount;
      });
  }

  filterChange(filter: ContactFilter) {
    this.filter = filter;
    this.lodaData();
  }

  pageChange(page: number) {
    this.page = page;
    this.lodaData();
  }

  pageSizeChange() {
    this.lodaData();
  }
}
