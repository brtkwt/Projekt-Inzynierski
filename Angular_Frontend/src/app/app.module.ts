import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { ContactModule } from './contact/contact.module';
import { HomeModule } from './home/home.module';
import { SharedModule } from './shared/shared.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { LoadingPageInterceptor } from './core/interceptors/loading-page.interceptor';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { OrderInfoComponent } from './order/order-info/order-info.component';
import { OrderModule } from './order/order.module';

@NgModule({
  declarations: [
    AppComponent,
    OrderInfoComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    ShopModule,
    SharedModule,
    FormsModule,
    ContactModule,
    HomeModule,
    TabsModule.forRoot(),
    OrderModule,
  ],
  
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingPageInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
