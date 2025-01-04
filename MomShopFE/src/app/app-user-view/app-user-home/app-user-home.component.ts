import { Component } from '@angular/core';

@Component({
  selector: 'app-user-home',
  templateUrl: './app-user-home.component.html',
  styleUrls: ['./app-user-home.component.scss']
})
export class AppUserHomeComponent {
  count = 0;
  constructor() {
  }
  translate(option) {
    var blockProduct = document.querySelector<HTMLElement>(".block-product");
    var offsetY = blockProduct.getBoundingClientRect();
    var itemBlock = document.querySelectorAll<HTMLElement>(".item-product");
    if (option == 1) {
      console.log('test');
      this.count += 1;
      for (let i = 0; i < itemBlock.length; i++) {
        var offsetItem = itemBlock[i].getBoundingClientRect();
        if (offsetItem.top <= offsetY.bottom) {
          itemBlock[i].style.transform = `translateX(-${(blockProduct.offsetWidth / 3) * this.count}px)`
        }
      }
    }
  }
}
