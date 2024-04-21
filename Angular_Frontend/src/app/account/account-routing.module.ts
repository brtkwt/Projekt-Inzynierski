import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';

const routing: Routes =[
  {path: 'logowanie', component: LoginPageComponent},
  {path: 'rejestracja', component: RegisterPageComponent},
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routing),
  ],

  exports:[
    RouterModule,
  ]


})
export class AccountRoutingModule { }
