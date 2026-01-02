import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatAnchor, MatIconButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatMenu, MatMenuTrigger } from "@angular/material/menu";
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { shopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatAnchor,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule,
    MatIconButton
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss',
})
export class ShopComponent {


  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog)
  products? : Pagination<Product>;
  sortOption = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price : Low-High', value: 'priceAsc'},
    {name: 'Price : High-Low', value: 'priceDesc'}
  ]

shopParams = new shopParams();
pageSizeOptions = [5,10,15,20]
  
  

  ngOnInit(): void {
      this.shopService.getProducts(this.shopParams).subscribe({
        next:response => this.products = response,
        error: error => console.log(error),
    
        
      })

      this.initializeShop();
  }

   initializeShop() {

    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProduct();
      
  }

  getProduct(){
    this.shopService.getProducts(this.shopParams).subscribe({
        next:response => this.products = response,
        error: error => console.log(error),
    
        
      })
  }

  onSearchChange(){
    this.shopParams.pageNumber = 1;
    this.getProduct();
  }


  handlePageEvent(event: PageEvent)
  {
    this.shopParams.pageNumber = event.pageIndex + 1; 
    this.shopParams.pagesize = event.pageSize;
    this.getProduct();


  }

  onSortChange(event: MatSelectionListChange){
    const selectedOption = event.options[0];

    if(selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1 ; 
      this.getProduct();
    }
  }

  


 openFilterDialog() {
  const dialogref = this.dialogService.open(FiltersDialogComponent, {
    minWidth: '500px',
    data: {
      selectedBrands: this.shopParams.brands,   
      selectedTypes: this.shopParams.types
    }
  });


  dialogref.afterClosed().subscribe({
    next: result => {
      if (result) {
        this.shopParams.brands = result.selectedBrand;
        this.shopParams.types = result.selectedTypes;
        this.shopParams.pageNumber = 1 ; 
        this.getProduct();
      }
    }
  });
}
}


