import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-cus-input',
  template: `
    <div class="loading-input-container">
      <input
        *ngIf="!isLoading; else loadingTemplate"
        [value]="value"
        (input)="onChange($event)"
        (blur)="onTouched()"
      />
      <ng-template #loadingTemplate>
        <div class="loading-spinner"></div>
      </ng-template>
    </div>
  `,
  styles: [
    `
      .loading-input-container {
        position: relative;
      }

      .loading-spinner {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        // Add styles for the loading spinner, such as a background or a font-awesome spinner
      }
    `,
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CusInputComponent),
      multi: true,
    },
  ],
})
export class CusInputComponent implements ControlValueAccessor {
  @Input() isLoading = false;
  value: string ='';
constructor() {
  this.value = '';
}
  onChange: any = () => {};
  onTouched: any = () => {};
  writeValue(val: string): void {
    if (val !== null) {
      this.value = val;
    }
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    throw new Error('Method not implemented.');
  }

}
