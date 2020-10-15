import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../lib/base-component';

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
  item_group_id:any;
  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this.list = [];
    this.page = 1;
    this.pageSize = 6;
    this._route.params.subscribe(params => {
      this.item_group_id = params['id'];
      this._api.post('/api/item/search', { page: this.page, pageSize: this.pageSize, item_group_id: this.item_group_id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  }
  loadPage(page) { 
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.post('/api/item/search', { page: page, pageSize: this.pageSize, item_group_id: id}).takeUntil(this.unsubscribe).subscribe(res => {
        this.list = res.data;
        this.totalItems = res.totalItems;
        }, err => { });       
   });   
  } 
}

