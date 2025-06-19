import { Component } from '@angular/core';
import { AuthService } from '../../../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  loginError: string = '';
  showPassword: boolean = false;


  constructor(public authService: AuthService, private router: Router, private formbuilder: FormBuilder, private toastr: ToastrService) {
    this.loginForm = this.formbuilder.group({
      email : ['', [Validators.required]],
      password : ['', [Validators.required]],
      rememberMe: [false]
    });
  }
  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  get email() {
    return this.loginForm.get('email');
  }
  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: _ => {
          window.location.href = '/';
        },
        error: () => {
          this.loginError = 'Invalid login attempt.';
        }
      });
    }
  }

}
