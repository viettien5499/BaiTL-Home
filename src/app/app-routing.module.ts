import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogComponent } from './blog/blog.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { ContactComponent } from './contact/contact.component';
import { DetailComponent } from './detail/detail.component';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './share/layout/layout.component';

const routes: Routes = [
  {
    path:'', 
    redirectTo:'home', 
    pathMatch:'full',
  },
  {
    path: '', component: LayoutComponent, children: [
      { path: 'home', component: HomeComponent},
      { path: 'detail/:id', component: DetailComponent, },
      { path: 'list/:id', component: ListComponent, },
      { path: 'home', component: HomeComponent, },
      { path: 'contact', component: ContactComponent, },
      { path: 'blog', component: BlogComponent, },
      { path: 'login', component: LoginComponent, },
      { path: 'cart', component: CartComponent, },
      { path: 'checkout', component: CheckoutComponent, },
    ]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
