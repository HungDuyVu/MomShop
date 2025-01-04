import { ToastrService } from 'ngx-toastr';
import { UserDto } from 'src/models/user';
import { Component } from '@angular/core';
import { UserService } from 'src/services/user.service';
import * as moment from 'moment';
@Component({
  selector: 'app-app-user-profile',
  templateUrl: './app-user-profile.component.html',
  styleUrls: ['./app-user-profile.component.scss']
})
export class AppUserProfileComponent {
  user: UserDto = new UserDto;
  userProfile:UserDto= new UserDto;

  constructor(private userService: UserService, private toastr: ToastrService) {
    this.updateUserInfo();
    // const userId = JSON.parse(localStorage.getItem('user'));
    //  this.userService.findUserById(userId).subscribe((result: UserDto)=>{
    //   this.userProfile = result;
    //  })
  }
  updateUserInfo() {
    this.userService.findUser().subscribe((result: UserDto) => {
      this.user = result;
      this.user.birthDay = moment(result.birthDay).add(1, 'days').toDate();
    })
  }
  saveProfile() {
    if (this.user.phone.length > 11) {
      this.toastr.warning("Số điện thoại không được lớn hơn 11 ký tự", "Thông báo",
        { timeOut: 2000 }); return
    }
    this.userService.updateInforUser(this.user).subscribe(result => {
      if (result == 'duplicate') {
        this.toastr.warning("Tên đăng nhập đã tồn tại", "Thông báo",
          { timeOut: 2000 })
      }
      if (result == 'success') {
        this.toastr.success("Cập nhật thông tin thành công", "Thông báo",
          { timeOut: 2000 });
        this.updateUserInfo();
      }
    })
  }
}
