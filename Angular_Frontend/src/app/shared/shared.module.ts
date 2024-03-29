import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationComponent } from './pagination/pagination.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    PaginationComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    FormsModule,
  ],

  exports:[
    PaginationModule,
    PaginationComponent,
  ]
})
export class SharedModule { }
