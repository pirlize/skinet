import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/Product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent {
  @Input() product?: Product;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute, //activated route accesses the params from the route we going to
    private bcService: BreadcrumbService
  ) {
    this.bcService.set('@productDetails', ' '); //fix not showing Id before getting name in breadcrumb
  }
  ngOnInit(): void {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
    this.getProduct();
  }

  private getProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id'); //access the id from the product we accessing via route
    if (id)
      // if condition to avoid null error
      this.shopService.getProduct(+id).subscribe({
        // convert to number with + & subscribe to get method to load product and put it on Init
        next: (product) => {
          (this.product = product),
            this.bcService.set('@productDetails', product.name); //breadcrumb get name
        },
        error: (err) => console.log(err),
      });
  }
}
