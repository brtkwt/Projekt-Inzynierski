import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  licznik = 0;

  constructor(private spinnerService: NgxSpinnerService) {}

  toLoading(){
    this.licznik = this.licznik + 1;
    this.spinnerService.show(undefined, {
      type: "ball-clip-rotate",
      size: "large",
      color: "#ff851b",
      bdColor: "rgba(0, 0, 0, 0.8)"
    })
  }

  toFinished(){
    this.licznik = this.licznik - 1;

    if( this.licznik <= 0 ){
      this.licznik = 0;
      this.spinnerService.hide();
      
    }

  }
}
