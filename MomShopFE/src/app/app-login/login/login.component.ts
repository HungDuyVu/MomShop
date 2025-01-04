import { Component, EventEmitter, Output } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, finalize } from "rxjs";
import { LoginDto } from "src/models/login";
import { UserDto } from "src/models/user";
import { UserService } from "src/services/user.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent {
  user: LoginDto = new LoginDto
  constructor(
    private router: Router,
    private _user: UserService,
    public toastr: ToastrService
  ) { }
  @Output() register: EventEmitter<any> = new EventEmitter<any>();
  @Output() goPage: EventEmitter<any> = new EventEmitter<any>();
  @Output() openFormRegister: EventEmitter<any> = new EventEmitter<any>();
  passlogin: boolean = true;
  checkEmailValid:boolean = false;
  checkLogin = false;
  routerPage() {
    if(this.user.email?.trim() == null || this.user.password?.trim() == null){
      this.toastr.warning("Phải nhập đầy đủ thông tin đăng nhập",
      "Thông báo",
      { timeOut: 3000 })
      return;
    }
    if (this.user.email && this.user.email != "admin") {
      // Kiểm tra định dạng email
      const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
      if (!emailRegex.test(this.user.email)) {
        this.toastr.warning("Email phải nhập đúng định dạng",
        "Thông báo",
        { timeOut: 3000 })
        return;
      }
    }
    this._user.login(this.user).subscribe((result:UserDto) => {
      console.log(result)
      if (result == null) {
        this.toastr.warning(
          "Tên tài khoản hoặc mật khẩu chưa chính xác",
          "Thông báo",
          { timeOut: 3000 }
        );
        return;
      } else {
        localStorage.setItem('user', JSON.stringify(result));
        sessionStorage.setItem('userType', JSON.stringify(result.userType))
        if(result.userType == 1){
          this.router.navigateByUrl('admin/dashboard')
          sessionStorage.setItem('userType', JSON.stringify(result.userType))
        }
        else this.router.navigateByUrl('/view')
        this.toastr.success(
          "Đăng nhập thành công",
          "Thông báo",
          { timeOut: 3000 }
        );
      }
    });
  }
  routerRegister() {
    this.router.navigateByUrl('register');
    this.register.emit(null);
  } 
}
