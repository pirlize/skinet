import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'Skinet';
}

// this.http.get('https://localhost:5001/api/products?PageSize=50').subscribe({
//   next: (response: any) => (this.products = response.data),
//   error: (error) => console.log(error),
//   complete: () => {
//     console.log('request completed');
//     console.log('extra statement');
//   },
// });
