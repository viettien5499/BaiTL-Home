import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../lib/base-component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent extends BaseComponent implements OnInit {
  items:any;
  total:any;
  _cart: any;
  constructor(injector: Injector) { 
    super(injector);
  }

  ngOnInit(): void {
    this._cart.items.subscribe((res) => {
      this.items = res;
      this.total = 0;
      for(let x of this.items){ 
        x.money = x.quantity * x.item_price;
        this.total += x.quantity * x.item_price;
      } 
    });
  } 
  clearCart() { 
    this._cart.clearCart();
    alert('Xóa thành công');
  }
  addQty(item, quantity){ 
    item.quantity =  quantity;
    item.money =  Number.parseInt(item.quantity) *  item.item_price;
    this._cart.addQty(item);
  }
}

