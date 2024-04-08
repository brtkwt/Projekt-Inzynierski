import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-error-internal-server',
  templateUrl: './error-internal-server.component.html',
  styleUrls: ['./error-internal-server.component.scss']
})
export class ErrorInternalServerComponent {
  blad:any;

  constructor(private ruter: Router){
    const info = this.ruter.getCurrentNavigation();

    this.blad = info?.extras?.state?.['error'];
  }

}
