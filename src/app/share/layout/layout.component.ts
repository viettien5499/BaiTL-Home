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
}
