import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './models/Pagination';
import { Product } from './models/Product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Skinet';
  products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    //httpGet has 14 overloads, in<> you pass return response type
    this.http
      .get<Pagination<Product[]>>(
        'https://localhost:5001/api/products?PageSize=50'
      )
      .subscribe({
        next: (response) => (this.products = response.data),
        error: (error) => console.log(error),
        complete: () => {
          console.log('request completed');
          console.log('extra statement');
        },
      });
  }
}

// this.http.get('https://localhost:5001/api/products?PageSize=50').subscribe({
//   next: (response: any) => (this.products = response.data),
//   error: (error) => console.log(error),
//   complete: () => {
//     console.log('request completed');
//     console.log('extra statement');
//   },
// });
