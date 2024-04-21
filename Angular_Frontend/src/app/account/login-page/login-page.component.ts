import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {

  constructor(private ruter: Router, private acService: AccountService) {}

  newFormForLogging = new FormGroup({
    emailAddress: new FormControl("", [Validators.email, Validators.required]),
    password: new FormControl("", Validators.required),
  })


  onDataSent(){
    this.acService.login(this.newFormForLogging.value).subscribe({
      next: () => this.ruter.navigateByUrl("/sklep")
    })
  }

}
