import { Component, inject, input, OnInit, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { JsonPipe, NgIf } from '@angular/common';
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, JsonPipe, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  validationErrors: string[] | undefined;

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    };
}
  initializeForm() {
    
    this.registerForm = this.fb.group({
      gender: ["female"],
      username: ["", Validators.required],
      knownAs: ["", Validators.required],
      birthDay: ["", Validators.required],
      city: ["", Validators.required],
      country: ["", Validators.required],
      password: ["", [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ["", [Validators.required, this.matchValues("password")]]

    });
    this.registerForm.controls["password"].valueChanges.subscribe({
      next: () => this.registerForm.controls["confirmPassword"].updateValueAndValidity()
    });
  }
  
  private accountService = inject(AccountService);
  //usersFromHomeComponent = input.required<any>();
  cancelRegister = output<boolean>();
  private toastr = inject(ToastrService);
  maxDate = new Date();
  model: any = {};
  registerForm: FormGroup = new FormGroup({});

  register(): void {
    const bd = this.getDateOnly(this.registerForm.get("birthDay")?.value);
    this.registerForm.patchValue({ birthDay: bd });
    this.accountService.register(this.model).subscribe({
      next: () => this.router.navigateByUrl("/members"),
      error: (error) => this.validationErrors = error
    });

  }
  private getDateOnly(birthDay: string | undefined) {
    if (!birthDay) return;
    return new Date(birthDay).toISOString().slice(0, 10);
  }
  cancel(): void{
   this.cancelRegister.emit(false);
  }
}
