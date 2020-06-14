import { Component, OnInit } from '@angular/core';
import { _client_home } from '../_constVars/_client_consts';

@Component({
  selector: 'app-notfound',
  templateUrl: './notfound.component.html',
  styleUrls: ['./notfound.component.scss'],
})
export class NotfoundComponent implements OnInit {
  home: string = _client_home;
  constructor() {}

  ngOnInit(): void {}
}
