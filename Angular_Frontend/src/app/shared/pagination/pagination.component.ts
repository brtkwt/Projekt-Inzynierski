import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent {
[x: string]: any;
  @Input() pageSize?: number;
  @Input() objectCount?: number;

  @Output() pageClicked = new EventEmitter<number>();

  onPageClickedEmit(event: any){
    this.pageClicked.emit(event.page);
  }
}
