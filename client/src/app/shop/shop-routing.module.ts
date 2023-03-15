import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';

const routes: Routes = [
  { path: '', component: ShopComponent },
  {
    path: ':id',
    component: ProductDetailsComponent,
    data: { breadcrumb: { alias: 'productDetails' } },
  }, //pass parameter
];

@NgModule({
  declarations: [],
  imports: [
    // CommonModule   we dont need common module, we pass child routes and export router module
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
})
export class ShopRoutingModule {}
