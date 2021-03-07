import { DecimalPipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.sass']
})
export class ProductsComponent implements OnInit {

  products: Product[] = [];

  constructor(private _http: HttpClient) { }

  ngOnInit(): void {
    this._http.get<Product[]>('/api/v1/products/')
      .subscribe(products => this.products = products)
  }

}
