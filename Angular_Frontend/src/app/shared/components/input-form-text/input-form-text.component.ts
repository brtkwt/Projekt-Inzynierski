import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';

@Component({
  selector: 'app-input-form-text',
  templateUrl: './input-form-text.component.html',
  styleUrls: ['./input-form-text.component.scss']
})
export class InputFormTextComponent  implements ControlValueAccessor{
  @Input() inputType = "text"

  @Input() inputLabel = ""


  constructor(@Self() public controlDir: NgControl) { 
    this.controlDir.valueAccessor = this

  }

  get control(): FormControl{
    return this.controlDir.control as FormControl

  }

  writeValue(obj: any): void { }
  registerOnChange(fn: any): void { }
  registerOnTouched(fn: any): void { }

}
