import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './share/layout/layout.component';
import { ListComponent } from './list/list.component';
import { DetailComponent } from './detail/detail.component';
import { BlogComponent } from './blog/blog.component';
import { ContactComponent } from './contact/contact.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { GioithieuComponent } from './gioithieu/gioithieu.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LayoutComponent,
    ListComponent,
    DetailComponent,
    BlogComponent,
    ContactComponent,
    LoginComponent,
    LogoutComponent,
    CartComponent,
    CheckoutComponent,
    GioithieuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    NgModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
