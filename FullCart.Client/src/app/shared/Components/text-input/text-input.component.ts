import { Component, ElementRef, Input, Self, ViewChild } from '@angular/core';
import { NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent {
  @ViewChild('input', { static: true }) input?: ElementRef;
  @Input() type = 'text';
  @Input() label?: string;
  constructor(@Self() public ControlDir: NgControl) {
    this.ControlDir.valueAccessor = this;
  }
  ngOnInit(): void {
    const control = this.ControlDir.control;
    const validators = control?.validator ? [control.validator] : [];
    const asyncValidators = control?.asyncValidator ? [control.asyncValidator] : [];

    control?.setValidators(validators);
    control?.setAsyncValidators(asyncValidators);
    control?.updateValueAndValidity();

  }


  onChange(event: Event) {
  }
  onTouched() { }

  writeValue(obj: any): void {
    this.input!.nativeElement.value = obj || '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;

  }
}
