import { BaseComponent } from './../lib/base-component';
import { Component, OnInit, Injector } from '@angular/core';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent extends BaseComponent implements OnInit {
  detail:any;
  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this.detail = {};
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('/api/sanpham/get-by-id/'+id).takeUntil(this.unsubscribe).subscribe(res => {
        this.detail = res;
        setTimeout(() => {
          this.loadScripts();
        });
      }); 
    });

  }
}


