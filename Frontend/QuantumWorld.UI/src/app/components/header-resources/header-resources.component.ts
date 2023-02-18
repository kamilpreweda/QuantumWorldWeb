import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { ResearchType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { UserService } from 'src/app/services/user.service'
import { decrement, increment, reset } from 'src/app/state/app.actions';

@Component({
  selector: 'app-header-resources',
  templateUrl: './header-resources.component.html',
  styleUrls: ['./header-resources.component.css']
})
export class HeaderResourcesComponent {
  user: User;
  users: User[] = [];
  carbonFiberResource: number;

  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private store: Store<{app: {carbonFiberResource: number}}>) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
    this.store.select('app').subscribe(data => {
      this.carbonFiberResource = data.carbonFiberResource;
    })
  }

  onIncrement(){
    this.store.dispatch(increment());
  }

  onDecrement() {
    this.store.dispatch(decrement());
  }

  onReset() {
    this.store.dispatch(reset());
  }
}




