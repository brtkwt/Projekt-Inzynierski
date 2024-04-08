import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShopComponent } from '../shop/shop.component';
import { RouterModule } from '@angular/router';
import { ErrorNotFoundComponent } from './error-not-found/error-not-found.component';
import { ErrorInternalServerComponent } from './error-internal-server/error-internal-server.component';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [
    NavigationBarComponent,
    ErrorNotFoundComponent,
    ErrorInternalServerComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    ToastrModule.forRoot({
      preventDuplicates: true,
      positionClass: 'toast-bottom-center',
    })
  ],
  exports: [
    NavigationBarComponent,
  ]
})
export class CoreModule { }
