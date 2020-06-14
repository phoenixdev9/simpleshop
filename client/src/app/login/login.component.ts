import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  returnUrl: string;
  constructor(
    private accountService: AccountService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl;
  }

  readonly emailOnly = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
  isValidateTextTrue(data: any) {
    if (data.touched && data.valid) {
      return true;
    } else {
      return false;
    }
  }
  isValidateTextFalse(data: any) {
    if (data.touched && data.invalid) {
      return true;
    } else {
      return false;
    }
  }
  submitFunc(data: any, event: Event) {
    event.preventDefault();
    this.accountService.login(data.value).subscribe(
      () => {
        this.router.navigateByUrl(this.returnUrl);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
