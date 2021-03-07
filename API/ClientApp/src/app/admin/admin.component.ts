import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Product } from '../models/product';
import {
  ProductDialogComponent,
  ProductDialogResult,
} from '../product-dialog/product-dialog.component';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],
})
export class AdminComponent implements OnInit {
  @Output() edit = new EventEmitter<Product>();

  displayedColumns: string[] = [
    'id',
    'name',
    'imgUri',
    'price',
    'edit',
    'delete',
  ];
  dataSource: Product[] = [];

  constructor(private _http: HttpClient, private _dialog: MatDialog) {}

  ngOnInit(): void {
    this._http
      .get<Product[]>('/api/v1/products/')
      .subscribe((products) => (this.dataSource = products));
  }

  editProduct(product: Product): void {
    const dialogRef = this._dialog.open(ProductDialogComponent, {
      width: '270px',
      data: {
        product: product,
      },
    });
    dialogRef.afterClosed().subscribe((result: ProductDialogResult) => {
      if (result !== undefined) {
        const headers = new HttpHeaders().set(
          'Content-Type',
          'application/json'
        );
        this._http
          .put<any>(`/api/v1/products/${result.product.id}`, result.product, {
            headers,
          })
          .subscribe((res) => console.log(res));
      }
    });
  }

  newProduct(): void {
    const dialogRef = this._dialog.open(ProductDialogComponent, {
      width: '270px',
      data: {
        product: {},
      },
    });
    dialogRef.afterClosed().subscribe((result: ProductDialogResult) => {
      if (result !== undefined) {
        const headers = new HttpHeaders().set(
          'Content-Type',
          'application/json'
        );
        this._http
          .post<any>('/api/v1/products/', result.product, { headers })
          .subscribe((res) => console.log(res));
      }
    });
  }

  deleteProduct(product: Product): void {
    this._http.delete(`/api/v1/products/${product.id}`).subscribe((_) => {
      console.log('deleted');
      this._http
        .get<Product[]>('/api/v1/products/')
        .subscribe((products) => (this.dataSource = products));
    });
  }
}
