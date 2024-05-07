import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { AbstractControl, AsyncValidator, AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent {

  errors: string[] | null = null;

  constructor(private ruter: Router, private acService: AccountService) {}

  toughPassword = "(?=^.{6,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

  newFormForRegistration = new FormGroup({

    emailAddress: new FormControl("", [Validators.email, Validators.required], [this.emailCheck()]),
    givenName: new FormControl("", [Validators.required]),

    password: new FormControl("", [Validators.required, Validators.pattern(this.toughPassword)]),

  })

  dataSent(){
    this.acService.registration(this.newFormForRegistration.value).subscribe({
      next: () => this.ruter.navigateByUrl("/"),
      error: blad => this.errors = blad.errors
    })
  }

  emailCheck(): AsyncValidatorFn{
    return (ctrl : AbstractControl) => {
      return ctrl.valueChanges.pipe(

        debounceTime(1200),
        take(1),
        switchMap( ()=> {

          return this.acService.mailExistanceCheck(ctrl.value).pipe(
            map(response => response ? {emailAddressTaken: true}: null)
    
          )
        })
      )
    }
  }

}
