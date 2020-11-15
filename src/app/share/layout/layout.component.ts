import { Component, Injector, OnInit } from '@angular/core';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';
import { BaseComponent } from 'src/app/lib/base-component';


@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent extends BaseComponent implements OnInit {
  menus:any;
  public items: any;
  public item: any;
  public page = 1;
  public pageSize = 3;
  public totalRecords: any;
  public formsearch: any;
  public formdata: any;
  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this._api
    .get('/api/loaisanpham/getloaisp')
    .takeUntil(this.unsubscribe)
    .subscribe(res => {
      this.menus = res;
    }); 
  }
  loadPage(page) {
    //debugger;
    this._api.post('/api/sanpham/search1', { page: page, pageSize: this.pageSize }).takeUntil(this.unsubscribe).subscribe(res => {
      this.items = res.data;
      this.totalRecords = res.totalItems;
      this.pageSize = res.pageSize;
    });
  }

  search() {
    // debugger;
    this.page = 1;
    this.pageSize = 5;
    this._api.post('/api/sanpham/search1', { page: this.page, pageSize: this.pageSize, Tensp: this.formsearch.get('Tensp').value }).takeUntil(this.unsubscribe).subscribe(res => {
      this.items = res.data;
      this.totalRecords = res.totalItems;
      this.pageSize = res.pageSize;
    });
  }
}
