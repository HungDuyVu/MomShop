import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private showMenuSubject = new BehaviorSubject<boolean>(false);
  showMenu$ = this.showMenuSubject.asObservable();

  setShowMenu(showMenu: boolean) {
    this.showMenuSubject.next(showMenu);
  }
}
