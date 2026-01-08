import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { ShopService } from '../../../core/services/shop.service';
import { MatAnchor, MatButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatInput, MatFormField, MatLabel } from "@angular/material/input";
import { MatDivider } from "@angular/material/divider";
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product-details',
  imports: [MatAnchor, MatIcon, MatInput, MatFormField, MatDivider, MatLabel, MatFormField, CurrencyPipe, MatButton, ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
})
export class ProductDetailsComponent implements OnInit {

  private shopService = inject(ShopService); 
  private activeedRoute = inject(ActivatedRoute); 
  product?: Product;


  ngOnInit(): void {
      this.loadProduct();
  }

  loadProduct() {

    const id = this.activeedRoute.snapshot.paramMap.get("id");
    if (!id) return;
    this.shopService.getProduct(+id).subscribe({
      next: product => this.product = product,
      error: error => console.log(error)
      
      
    })
  }

}


