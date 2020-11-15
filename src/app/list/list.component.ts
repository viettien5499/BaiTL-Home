import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../lib/base-component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent extends BaseComponent implements OnInit {
  list: any;
  page: any;
  pageSize: any;
  totalItems:any;
  Maloai:any;
  menus:any;
  spkm:any;
  public totalRecords: any;

  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this.list = [];
    this.page = 1;
    this.pageSize = 12;
    this._route.params.subscribe(params => {
      this.Maloai = params['id'];
      this._api.post('/api/sanpham/search', { page: this.page, pageSize: this.pageSize, Maloai: this.Maloai}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list = res.data;
        this.totalItems = res.totalItems;
        setTimeout(() => {
          this.loadScripts();
        },);
        }, err => { }); 
        this._api
        .get('/api/loaisanpham/getloaisp')
        .takeUntil(this.unsubscribe)
        .subscribe(res => {
          this.menus = res;
        });       
   });
   Observable.combineLatest(
    this._api.get('/api/sanpham/get-spkm'),
  ).takeUntil(this.unsubscribe).subscribe(res => {
    this.spkm = res[0];
    setTimeout(() => {
      this.loadScripts();
    });
  }, err => { });   
  }
  loadPage(page) { 
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.post('/api/sanpham/search', { page: page, pageSize: this.pageSize, Maloai: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  } 
  addToCart(it) { 
    this._cart.addToCart(it);
    alert('Thêm thành công!'); 
  }
}

