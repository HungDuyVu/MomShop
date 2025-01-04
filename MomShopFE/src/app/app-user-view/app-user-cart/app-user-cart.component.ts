import { Component, EventEmitter, Output } from "@angular/core";

@Component({
  selector: "app-user-cart",
  templateUrl: "./app-user-cart.component.html",
  styleUrls: ["./app-user-cart.component.scss"],
})
export class AppUserCartComponent {
  @Output() backGoHome: EventEmitter<any> = new EventEmitter<any>();
  cart;
  quantityProduct = 1;
  minusQuantityProduct() {
    if(this.quantityProduct === 1){
      return;
    }
    var quantity = document.getElementsByClassName("text-quantity")[0];
    this.quantityProduct--;
    quantity.textContent = this.quantityProduct.toString();
  }
  addQuantityProduct() {
    var quantity = document.getElementsByClassName("text-quantity")[0];
    this.quantityProduct++;
    quantity.textContent = this.quantityProduct.toString();
  }
  deleteProduct(){
    document.getElementsByClassName("fa-trash")[0].parentElement.parentElement.parentElement.remove();
  }
  backHome(){
    this.backGoHome.emit(null);
  }
}
