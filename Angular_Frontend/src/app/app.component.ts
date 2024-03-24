import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Backcountry Gear';
  products: Product[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get< Pagination< Product[] >>('https://localhost:5002/api/product?PageSize=100').subscribe({
      next: response => this.products = response.objectList, // what to do next with the response
      error: error => console.log(error), // what to do if error occurs
      complete: () => {   // what to do after completed
        console.log("get request compleated");
      }
    })
  }

}
