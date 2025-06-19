import { Component } from '@angular/core';
import { AuthService, RegisterDto } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup
  showPassword: boolean = false
  showConfirmPassword: boolean = false;

  constructor(private authService: AuthService, private router: Router, private formbuilder: FormBuilder, private toastr: ToastrService) {
    this.registerForm = this.formbuilder.group({
      fullname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required,Validators.minLength(8)]],
      confirmPassword: ['',[Validators.required]]
    }, {validator:this.passwordMatch});
  }
  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
  toggleConfirmPasswordVisibility(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
  get fullname() {
    return this.registerForm.get('fullname');
  }
  get email() {
    return this.registerForm.get('email');
  }
  get password() {
    return this.registerForm.get('password');
  }
  get confirmPassword() {
    return this.registerForm.get('confirmPassword');
  }
  passwordMatch(group: FormGroup): ValidationErrors | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const registerDto: RegisterDto = {
       
        fullname: this.registerForm.value.fullname,
        email: this.registerForm.value.email,
        password: this.registerForm.value.password
      };
      this.authService.register(registerDto).subscribe({
        next: user => {
          console.log("Registration Successful:", user);
          window.location.href = '/login';
        },
        error: error => {
          console.log("Registration Error:", error);
          if (error.error && typeof error.error === 'string') {
            this.toastr.error(error.error);
          } else {
            this.toastr.error('An error occurred');
          }
        }
      });
    } else {
      console.log('Form is invalid:', this.registerForm.errors);
    }
  }
}
