import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { RouterModule } from '@angular/router';
import { ErrorNotFoundComponent } from './error-not-found/error-not-found.component';
import { ErrorInternalServerComponent } from './error-internal-server/error-internal-server.component';
import { ToastrModule } from 'ngx-toastr';
import { HeaderComponent } from './header/header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    NavigationBarComponent,
    ErrorNotFoundComponent,
    ErrorInternalServerComponent,
    HeaderComponent,
  ],
  imports: [
    RouterModule,
    CommonModule,
    ToastrModule.forRoot({
      preventDuplicates: true,
      positionClass: 'toast-bottom-center',
    }),
    BreadcrumbModule,
    NgxSpinnerModule,
    SharedModule,
    
  ],
  exports: [
    NavigationBarComponent,
    HeaderComponent,
    NgxSpinnerModule,
  ]
})
export class CoreModule { }
