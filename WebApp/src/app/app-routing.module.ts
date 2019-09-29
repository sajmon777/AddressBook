import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContactListComponent } from './components/contact/contact-list/contact-list.component';
import { ContactManageComponent } from './components/contact/contact-manage/contact-manage.component';



const routes: Routes = [
  { path: 'contacts', component: ContactListComponent },
  { path: 'contact', component: ContactManageComponent },
  { path: 'contact/:id', component: ContactManageComponent },
  { path: '**', redirectTo: 'contacts' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
