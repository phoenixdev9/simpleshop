import { IAddress } from './../../_models/IAddress';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss'],
})
export class AddressComponent implements OnInit {
  AddressForm: FormGroup;
  constructor(private as: AccountService, private router: Router) {
    this.setupForm({
      name: '',
      street: '',
      city: '',
      province: '',
      postalCode: '',
      country: '',
    });
    this.getUserAddress();
  }
  ngOnInit(): void {}
  setupForm(address: IAddress | null) {
    this.AddressForm = new FormGroup({
      Name: new FormControl(address.name ?? '', [Validators.required]),
      Street: new FormControl(address.street ?? '', [Validators.required]),
      City: new FormControl(address.city ?? '', [Validators.required]),
      Province: new FormControl(address.province ?? '', [Validators.required]),
      PostalCode: new FormControl(address.postalCode ?? '', [
        Validators.required,
      ]),
      Country: new FormControl(address.country ?? '', [Validators.required]),
    });
  }
  getUserAddress() {
    this.as.getUserAddress().subscribe(
      (e: IAddress) => this.setupForm(e),
      (err) => console.log('err', err)
    );
  }
  submitFunc() {
    this.as.updateUserAddress(this.AddressForm.value).subscribe(
      () => {},
      (error) => console.log(error)
    );
  }
}
