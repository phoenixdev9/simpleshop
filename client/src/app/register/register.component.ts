import { AccountService } from './../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  signUpForm: FormGroup;
  returnUrl: string;
  constructor(
    private as: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl ?? '/';
  }
  private readonly emailOnly = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
  ngOnInit(): void {
    this.signUpForm = new FormGroup({
      displayName: new FormControl('', [Validators.required]),
      email: new FormControl('', [
        Validators.pattern(this.emailOnly),
        Validators.required,
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.maxLength(15),
        Validators.minLength(8),
      ]),
    });
  }
  submitFunc() {
    this.as.register(this.signUpForm.value).subscribe(
      () => {
        this.router.navigate([this.returnUrl]);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
