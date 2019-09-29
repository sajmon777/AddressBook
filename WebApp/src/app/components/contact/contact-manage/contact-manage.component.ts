import { Component, OnInit } from '@angular/core';
import { Contact } from 'src/app/model/contact';
import { ContactService } from 'src/app/services/contact.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ErrorDetails } from 'src/app/infrastructure/error-info';

@Component({
  selector: 'app-contact-manage',
  templateUrl: './contact-manage.component.html'
})
export class ContactManageComponent implements OnInit {

  contact: Contact;
  isReadOnly = true;
  modalDelete: NgbModalRef;
  errorDetails: ErrorDetails;

  constructor(private router: Router, private route: ActivatedRoute, private contactService: ContactService,
              private modalService: NgbModal) { }

  ngOnInit() {
    this.loadData();
  }

  loadData(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    if (id === 0) {
      this.isReadOnly = false;
      this.contact = { id: null, firstName: '', lastName: '', address: '', telephoneNumber: '' };
    } else {
      this.contactService.getContact(id)
        .subscribe(contact => this.contact = contact);
    }
  }
  edit() {
    this.isReadOnly = false;
  }

  save() {
    if (this.contact.id) {
      this.contactService.updateContact(this.contact).subscribe(() => {
        this.router.navigate(['Contacts']);
      });
    } else {
      this.contactService.addContact(this.contact).subscribe(contact => {
        this.contact.id = contact.id;
        this.router.navigate(['Contacts']);
      });
    }
  }

  delete() {
    this.contactService.deleteContact(this.contact.id).subscribe(x => {
      this.router.navigate(['Contacts']);
      this.modalDelete.close();
    });
  }

  openDeleteModal(content) {
    this.modalDelete = this.modalService.open(content);
  }
}
