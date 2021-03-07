import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.sass']
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  id!: number;
  private _sub: any;
  product!: Product;

  constructor(private _route: ActivatedRoute, private _http: HttpClient) {}

  ngOnInit(): void {
    this._sub = this._route.params.subscribe(params => {
      this.id = +params['id'];
      this._http.get<Product>(`/api/v1/products/${this.id}`)
        .subscribe(product => this.product = product);
   });
  }

  ngOnDestroy(): void {
    this._sub.unsubscribe();
  }

}
