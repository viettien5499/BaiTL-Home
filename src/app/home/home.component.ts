import { BaseComponent } from './../lib/base-component';
import { Component, OnInit,Injector } from '@angular/core';
import { Observable} from 'rxjs';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/takeUntil';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseComponent implements OnInit {
  list_item:any;
  spkm:any;
  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    Observable.combineLatest(
      this._api.get('/api/sanpham/get-allsp'),
    ).takeUntil(this.unsubscribe).subscribe(res => {
      this.list_item = res[0];
      setTimeout(() => {
        this.loadScripts();
      });
    }, err => { });
    Observable.combineLatest(
      this._api.get('/api/sanpham/get-spkm'),
    ).takeUntil(this.unsubscribe).subscribe(res => {
      this.spkm = res[0];
      setTimeout(() => {
        this.loadScripts();
      });
    }, err => { });
  }
}
