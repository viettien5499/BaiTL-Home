import { Component, OnInit, Injector } from '@angular/core';
import { takeUntil } from 'rxjs/operators';
import { BaseComponent } from './../lib/base-component';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent extends BaseComponent implements OnInit {
  item:any;
  constructor(injector: Injector) { 
    super(injector);
  }
  ngOnInit(): void {
    this.item = {};
    this._route.params.subscribe(params => {
      let id = params['id'];
      this._api.get('/api/item/get-by-id/'+id).pipe(takeUntil(this.unsubscribe)).subscribe(res => {
        this.item = res;
        setTimeout(() => {
          this.loadScripts();
        });
      }); 
    });

  }
}

