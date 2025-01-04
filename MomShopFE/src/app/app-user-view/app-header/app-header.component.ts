import { Component, EventEmitter, HostListener, Output } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-header",
  templateUrl: "./app-header.component.html",
  styleUrls: ["./app-header.component.scss"],
})
export class AppHeaderComponent {
  isAccount: boolean = false;
  @Output() modalSaves: EventEmitter<any> = new EventEmitter<any>();
  @Output() backHome: EventEmitter<any> = new EventEmitter<any>();
  //@HostListener('window:beforeunload', ['$event'])
  // beforeunloadHandler(event: Event) {
  //   // Xóa dữ liệu trong LocalStorage
  //   localStorage.clear();
  // }

  accountUser;
  constructor(private router: Router, private toastr: ToastrService) {
    this.accountUser = JSON.parse(localStorage.getItem('user'));
    if (this.accountUser) {
      this.isAccount = true;
    }
    else {
      this.isAccount = false;
    }
  }
  routerToCart() {
    this.modalSaves.emit(null);
  }
  backToHome() {
    this.backHome.emit(null);
  }
  logout() {
    //location.reload();
    this.toastr.success("Đăng xuất thành công", "Thông báo", { timeOut: 2000 });
    localStorage.clear();
    this.router.navigateByUrl('login');
  }
}
