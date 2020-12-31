import { Component, OnInit } from '@angular/core';
import { _client_order_ } from '../../shared/_constVars/_client_consts';

@Component({
  selector: 'app-thankyou',
  templateUrl: './thankyou.component.html',
  styleUrls: ['./thankyou.component.scss'],
})
export class ThankyouComponent implements OnInit {
  order: string = `/${_client_order_}/`;
  constructor() {}

  ngOnInit(): void {}
}
