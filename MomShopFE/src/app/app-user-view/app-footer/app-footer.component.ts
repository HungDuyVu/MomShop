import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './app-footer.component.html',
  styleUrls: ['./app-footer.component.scss']
})
export class AppFooterComponent {
  constructor(private router: Router) {

  }
  routerLink(option) {
    if(option == 2){
      this.router.navigateByUrl('/exchange-policy')
    }
    if(option == 3){
      this.router.navigateByUrl('/')
    }
    if(option == 4){
      this.router.navigateByUrl('/')
    }
    if(option == 5){
      this.router.navigateByUrl('/')
    }
    if(option == 6){
      this.router.navigateByUrl('/general-trading-terms')
    }
  }
}
