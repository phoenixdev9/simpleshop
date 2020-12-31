import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Input() pageNumber: number;
  @Output() pageChanged = new EventEmitter<number>();
  @Input() totalPages: number;

  constructor() {}

  ngOnInit() {}
  getArrayData() {
    return Array.from({ length: this.totalPages }, (v, i) => i + 1);
  }
  onPagerChange(page: number) {
    if (page > 0 && page <= this.totalPages && page !== this.pageNumber) {
      console.log('page', page);
      this.pageChanged.emit(page);
    }
  }
}
