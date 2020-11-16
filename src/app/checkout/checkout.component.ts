
import { Component, Injector, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseComponent } from '../lib/base-component';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent extends BaseComponent implements OnInit {
  thanhtoan:any;
  tongtientt:any;
  tongmuctt:any;
   id:any;
  public hoadonForm: FormGroup;
    constructor(injector:Injector) {
      super(injector);
    }
  
    ngOnInit(): void {
      this.hoadonForm = new FormGroup({
        hoten: new FormControl('', Validators.required),
        gioitinh: new FormControl(''),
        email: new FormControl('', Validators.required),
        sdt:new FormControl('', Validators.required),
        diachi:new FormControl(''),
        password:new FormControl(''),
        thanhtoan:new FormControl(''),
        ghichu:new FormControl('')
      });
  
  
      this._cart.items.subscribe((res) => {
        this.thanhtoan = res;
        console.log(this.thanhtoan);
        this.tongtientt = 0;
        this.tongmuctt=this.thanhtoan.length;
        for(let x of this.thanhtoan){
          x.money = x.quantity * x.giatien;
          this.tongtientt += x.quantity * x.giatien;
        }
        setTimeout(() => {
          this.loadScripts();
        },);
      });
    }
  themdonhang(input){
  
  let user={
  hoten:input.hoten,
  ngaysinh:null,
  diachi:input.diachi,
  gioitinh:null,
  email:input.email,
  taikhoan:null,
  matkhau:null,
  role:"User",
  image_url:null,
  };
  this._api.post('/api/Users/create-user',user).takeUntil(this.unsubscribe).subscribe(res => {
  
    this.id=res.user_id;
  }, err => { });
  let tg=[];
  this.thanhtoan.forEach(element => {
    let kq=
    {
      "Macthdb":Number.parseInt(element.id),
      "Mahdb":element.Mahdb,
      "Masp":element.masp,
      "Soluongban":Number.parseInt(element.quantity),
      "Giaban":Number.parseInt(element.giatien)   
    };
    tg.push(kq);
    console.log(kq);
  });
  console.log(this.id);
let order={makh:this.id,hoten: input.hoten,sdt:input.sdt.toString(), diachi:input.diachi,email:input.email,tongtien:Number.parseFloat(this.tongtientt),thanhtoan:input.thanhtoan,ghichu:input.ghichu,listjson_chitiet: this.thanhtoan };
console.log(order);
this._api.post('/api/hoadonban/create-hoadonban',order).takeUntil(this.unsubscribe).subscribe(res => {
alert("đã thêm đơn hàng, cảm ơn bạn đã ủng hộ!");
this._cart.clearCart();
window.location.replace('home');
   }, err => { });
}
}
  

//     items:any;
//   total:any;
//   public hoadonForm: FormGroup;
//   _cart: any;
//   constructor(injector: Injector) { 
//     super(injector);
//   }

//   onSubmit(value: any) {
//     let hoadon = {Ngayban: value.Ngayban, Tongtien:value.Tongtien, Trangthai:value.Trangthai, Ghichu:value.Ghichu, listjson_chitiet:this.items};
//     this._api.post('/api/hoadonban/create-hoa-don', hoadon).takeUntil(this.unsubscribe).subscribe(res => {
//       alert('Tạo thành công');
//        }, err => { });      
 
//   }

//   ngOnInit(): void {
//     this.hoadonForm = new FormGroup({
//       Ngayban: new FormControl('', Validators.required),
//       Tongtien: new FormControl(''),
//       Trangthai: new FormControl(''),   
//       Ghichu: new FormControl('')                  
//     });

//     this._cart.items.subscribe((res) => {
//       this.items = res;
//       this.total = 0;
//       for(let x of this.items){ 
//         x.so_luong = +x.quantity;
//         x.money = x.quantity * x.Giatien;
//         this.total += x.quantity * x.Giatien;
//       } 
//     });

//   }

// }
