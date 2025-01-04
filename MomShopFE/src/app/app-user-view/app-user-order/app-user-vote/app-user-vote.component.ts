import { Component, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { ModalDirective } from 'src/directive/modal.directive';
import { FeedbackDto } from 'src/models/user/FeedbackDto';
import { UserFeedBackService } from 'src/services/user-feedback.service';

@Component({
  selector: 'app-user-vote',
  templateUrl: './app-user-vote.component.html',
  styleUrls: ['./app-user-vote.component.scss']
})
export class AppUserVoteComponent {

  data: FeedbackDto = new FeedbackDto;
  @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
  active: boolean = false;
  saving: boolean = false;
  constructor(public userFeedBack: UserFeedBackService, public toartr: ToastrService) {
  }
  show(order) {
    this.data.customerName = JSON.parse(localStorage.getItem('user'))?.fullName;
    this.data.orderCode = order.orderCode;
    this.data.orderId = order.id;
    this.data.createdDate = moment().add(1, 'days').toDate();
    this.data.email = JSON.parse(localStorage.getItem('user'))?.email;
    this.modal.show();
  }
  close() {
    this.modal.hide();
  }
  save() {
    this.data.customerName = JSON.parse(localStorage.getItem('user'))?.fullName;
    this.userFeedBack.addFeedbackUser(this.data).subscribe(() => {
      this.toartr.success("Đánh giá đơn hàng thành công", "Thông báo", { timeOut: 3000 });
      this.modal.hide();
    })
  }
  changeColorIconStar(number?) {
    this.data.rating = number;
    var star = document.getElementsByClassName('icon-vote');
    for (var i = 0; i < star.length; i++) {
      star[i].classList.remove('active');
      if (i < number) {
        star[i].classList.add('active');
      }
    };
  }
}
