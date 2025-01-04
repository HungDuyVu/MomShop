import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-main',
  templateUrl: './app-user-main.component.html',
  styleUrls: ['./app-user-main.component.less'],
})
export class AppUserMainComponent implements OnInit {
  ngOnInit(): void {
    if(sessionStorage.getItem("adminLogout")){
      location.reload();
      sessionStorage.clear();
    }

    console.log(localStorage.getItem('user'));
  }
}
