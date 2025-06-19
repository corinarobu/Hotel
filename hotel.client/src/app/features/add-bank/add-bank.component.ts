import { Component, OnInit } from '@angular/core';
import { BankAccountService } from '../../services/bank-account.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-bank',
  templateUrl: './add-bank.component.html',
  styleUrl: './add-bank.component.css'
})
export class AddBankComponent implements OnInit {
  iban: string = '';
  bankName: string = '';
  message: string | null = null;
  isLoading: boolean = false;
  returnUrl: string = '';
  showRedirectMessage: boolean = false;
  messageType: 'success' | 'error' | 'info' | null = null;

  constructor(private bankAccountService: BankAccountService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params['returnUrl'] || null;

      if (this.returnUrl) {
        setTimeout(() => {
          this.message = 'You were redirected from another page to add a new bank account.';
          this.messageType = 'info';
        }, 0);
      }
    });
  }

  addBankAccount() {
    if (this.iban && this.bankName) {
      this.isLoading = true;
      this.bankAccountService.addBankAccount(this.iban, this.bankName).subscribe(
        () => {
          this.message = 'Bank account added successfully.';
          this.messageType = 'success';

          if (this.returnUrl) {
            this.showRedirectMessage = true;
            setTimeout(() => {
              this.isLoading = false;
              this.router.navigate([this.returnUrl]);
            }, 3000);
          } else {
            this.isLoading = false;
          }
        },
        err => {
          console.error('Failed to add bank account', err);
          this.message = 'Failed to add bank account. Please try again.';
          this.messageType = 'error';
          this.isLoading = false;
        }
      );
    } else {
      this.message = 'Please fill in both fields.';
      this.messageType = 'error';
    }
  }
}
