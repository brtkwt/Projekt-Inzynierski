import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private ruter: Router, private tost: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((blad:HttpErrorResponse) => {
        if(blad != null){

          // Bad Request
          if(blad.status == 400){
            if(blad.error.errors){
              throw blad.error;
            }
            else
              this.tost.error(blad.error.message, String(blad.status) );    // " "
          }

          // Unauthorized
          if(blad.status == 401){
            this.tost.error(blad.error.message, String(blad.status) );
          }

          // Not-found
          if(blad.status == 404){
            this.ruter.navigateByUrl('/nie-znaleziono');
          }

          // Server-error
          if(blad.status == 500){
            const extraInfo: NavigationExtras = {state: {error: blad.error}}

            this.ruter.navigateByUrl('/blad-servera', extraInfo);
          }
        }
        
        return throwError(() => new Error(blad.message));

      })
    )
  }
}
