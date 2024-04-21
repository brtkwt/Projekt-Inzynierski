import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinalizationComponent } from './finalization.component';
import { FinalizationRoutingModule } from './finalization-routing.module';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    FinalizationComponent
  ],
  imports: [
    CommonModule,
    FinalizationRoutingModule,
    TabsModule,
    SharedModule,
  ]
})

export class FinalizationModule { }
