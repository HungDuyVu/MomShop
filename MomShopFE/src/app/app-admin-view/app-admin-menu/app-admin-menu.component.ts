import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MenuService } from 'src/services/menuService';

@Component({
  selector: 'app-admin-menu',
  templateUrl: './app-admin-menu.component.html',
  styleUrls: ['./app-admin-menu.component.scss']
})
export class AppAdminMenuComponent implements OnInit {
  @Input() isExpanded: boolean = false;
  @Output() toggleSidebar: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private route: Router, private menuService: MenuService, public toastr: ToastrService, private router: Router) {
    if (sessionStorage.getItem('userType')) {
      location.reload();
      sessionStorage.clear();
    }
  }

  ngOnInit(): void {
  }

  handleSidebarToggle = () => this.toggleSidebar.emit(!this.isExpanded);
  ClickMenu(option) {
    this.menuService.setShowMenu(!this.menuService.showMenu$);
    var menuItem = document.querySelectorAll(".item");
    var menuActive = document.getElementsByClassName("active")[0];
    menuActive?.classList.remove("active");
    menuItem?.forEach((item) => {
      item?.addEventListener("click", function () {
        item.classList.add("active");
      })
    })
    if (option == 0) {
      this.route.navigateByUrl("admin/dashboard");
    }
    if (option == 1) {
      this.route.navigateByUrl("admin/customer");
    }
    if (option == 2) {
      this.route.navigateByUrl("admin/product-management/product");
    }
    if (option == 3) {
      this.route.navigateByUrl("admin/order-management/order");
    }
    if (option == 4) {
      this.route.navigateByUrl("admin/received-order/order");
    }
    if (option == 5) {
      this.route.navigateByUrl("admin/feedback");
    }
    if (option == 6) {
      this.route.navigateByUrl("admin/collection-management/collection");
    }
  }

  logout() {
    localStorage.setItem('user', null);
    this.router.navigateByUrl('/view');
    sessionStorage.setItem("adminLogout", "true");
  }
}
