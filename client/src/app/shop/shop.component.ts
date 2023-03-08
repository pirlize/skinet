import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Brand } from '../shared/models/Brand';
import { Product } from '../shared/models/Product';
import { ProductType } from '../shared/models/ProductType';
import { ShopParams } from '../shared/models/ShopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm?: ElementRef;
  public products: Product[] = [];
  public brands: Brand[] = [];
  public productTypes: ProductType[] = [];
  public shopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc' },
    { name: 'Price: High to low', value: 'priceDesc' },
  ];
  public totalCount = 0;

  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getProductTypes();
    this.getBrands();
  }
  private getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      //we pass the optional Ids in the service's method
      next: (response) => {
        this.products = response.data;
        this.shopParams.pageIndex = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: (error) => console.log(error),
    });
  }
  private getBrands() {
    this.shopService.getBrands().subscribe({
      next: (response) => (this.brands = [{ id: 0, name: 'All' }, ...response]), //this means we creating a brand with id 0 and name all and then concat the rest of the response
      error: (error) => console.log(error),
    });
  }
  private getProductTypes() {
    this.shopService.getProductTypes().subscribe({
      next: (response) =>
        (this.productTypes = [{ id: 0, name: 'All' }, ...response]),
      error: (error) => console.log(error),
    });
  }

  public onBrandIdSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  public onProductTypeIdSelected(productTypeIdSelected: number) {
    this.shopParams.typeId = productTypeIdSelected;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  public onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  public onPageChanged(event: any) {
    if (this.shopParams.pageIndex !== event) {
      this.shopParams.pageIndex = event;
      this.getProducts();
    }
  }

  public onSearch() {
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  public onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
