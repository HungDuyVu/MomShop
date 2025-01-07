import { ToastrService } from 'ngx-toastr';
import { UserDto } from 'src/models/user';
import { Component } from '@angular/core';
import { UserService } from 'src/services/user.service';
import * as moment from 'moment';
import { ChangePasswordUserDto } from 'src/models/user/ChangePasswordUserDto';


@Component({
  selector: 'app-app-user-profile',
  templateUrl: './app-user-profile.component.html',
  styleUrls: ['./app-user-profile.component.scss']
})
export class AppUserProfileComponent {
  user: UserDto = new UserDto();
  userProfile: UserDto = new UserDto();
  changePasswordDto: ChangePasswordUserDto = new ChangePasswordUserDto();
  showChangePasswordForm: boolean = false;

  constructor(private userService: UserService, private toastr: ToastrService) {
    this.updateUserInfo();
  }

  updateUserInfo() {
    this.userService.findUser().subscribe((result: UserDto) => {
      this.user = result;
      this.changePasswordDto.email = this.user.email;
      this.user.birthDay = moment(result.birthDay).add(1, 'days').toDate();
    });
  }

  saveProfile() {
    if (this.user.phone.length > 11) {
      this.toastr.warning("Số điện thoại không được lớn hơn 11 ký tự", "Thông báo", { timeOut: 2000 });
      return;
    }
    this.userService.updateInforUser(this.user).subscribe(result => {
      if (result === 'duplicate') {
        this.toastr.warning("Tên đăng nhập đã tồn tại", "Thông báo", { timeOut: 2000 });
      }
      if (result === 'success') {
        this.toastr.success("Cập nhật thông tin thành công", "Thông báo", { timeOut: 2000 });
        this.updateUserInfo();
      }
    });
  }

  changePassword() {
    this.userService.changePassword(this.changePasswordDto).subscribe(response => {
      if (response === 'success') {
        this.toastr.success("Đổi mật khẩu thành công", "Thông báo", { timeOut: 2000 });
        this.showChangePasswordForm = false;
      } else {
        this.toastr.error(response, "Thông báo", { timeOut: 2000 });
      }
    });
  }

  cancelChangePassword() {
    this.showChangePasswordForm = false;
    this.changePasswordDto.password = '';
    this.changePasswordDto.newPassword = '';
  }

  toggleChangePasswordForm() {
    this.showChangePasswordForm = !this.showChangePasswordForm;
    if (this.showChangePasswordForm) {
      this.changePasswordDto.password = '****';
      this.changePasswordDto.newPassword = '';
    }
  }
}