import { Component, inject } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatAnchor } from "@angular/material/button";
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filters-dialog',
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatAnchor,
    FormsModule
],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss',
})
export class FiltersDialogComponent {

  shopService = inject(ShopService)

  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>)
  data = inject(MAT_DIALOG_DATA);
  
  selectedBrand: string[] = this.data.selectedBrands;
  selecteTypes: string[] = this.data.selectedTypes; 

  applyFilters() 
  {
    this.dialogRef.close({
      selectedBrand:this.selectedBrand,
      selectedTypes:this.selecteTypes
    })
  }
}
