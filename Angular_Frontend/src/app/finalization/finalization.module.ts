import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FinalizationComponent } from './finalization.component';
import { FinalizationRoutingModule } from './finalization-routing.module';



@NgModule({
  declarations: [
    FinalizationComponent
  ],
  imports: [
    CommonModule,
    FinalizationRoutingModule
  ]
})

export class FinalizationModule { }
