import { Component, OnInit } from '@angular/core';
import { _client_home } from '../_constVars/_client_consts';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss'],
})
export class ServerErrorComponent implements OnInit {
  home: string = _client_home;
  constructor() {}

  ngOnInit(): void {}
}
