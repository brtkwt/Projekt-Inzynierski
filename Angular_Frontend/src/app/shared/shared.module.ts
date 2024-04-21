import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationComponent } from './pagination/pagination.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { InputFormTextComponent } from './components/input-form-text/input-form-text.component';



@NgModule({
  declarations: [
    PaginationComponent,
    InputFormTextComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
  ],

  exports:[
    PaginationModule,
    PaginationComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    InputFormTextComponent,
    
  ]
})
export class SharedModule { }
