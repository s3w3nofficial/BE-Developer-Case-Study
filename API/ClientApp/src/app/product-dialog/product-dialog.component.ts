import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Product } from '../models/product';

export interface ProductDialogData {
  product: Partial<Product>;
  enableDelete: boolean;
}

export interface ProductDialogResult {
  product: Product;
  delete?: boolean;
}

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.sass']
})
export class ProductDialogComponent implements OnInit {

  private backupTask: Partial<Product> = { ...this.data.product };

  constructor(
    public dialogRef: MatDialogRef<ProductDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ProductDialogData
  ) {}

  cancel(): void {
    this.data.product.name = this.backupTask.name;
    this.data.product.description = this.backupTask.description;
    this.dialogRef.close(this.data);
  }

  ngOnInit(): void {
    
  }

}
