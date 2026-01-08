import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BusyService {

  loading = false; 
  busyReqestCount = 0 ; 


  busy(){
    this.busyReqestCount++; 
    this.loading = true;
  }

  idle(){
    this.busyReqestCount--;
    if(this.busyReqestCount <= 0 ){
      this.busyReqestCount = 0;
      this.loading = false;
    }
  }
  
}
