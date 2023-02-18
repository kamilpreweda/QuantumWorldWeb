import { Component } from '@angular/core';
import { ResearchType, User } from 'src/app/models/user';
import { DisplayHelperService } from 'src/app/services/display-helper.service';
import { ResearchService } from 'src/app/services/research.service';
import { UserService } from 'src/app/services/user.service'

@Component({
  selector: 'app-research',
  templateUrl: './research.component.html',
  styleUrls: ['./research.component.css']
})
export class ResearchComponent {
  user: User;
  users: User[] = [];
  type: ResearchType;
  email: string = "string";


  constructor(private userService: UserService, public displayHelper: DisplayHelperService, private researchService: ResearchService) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe((result: User[]) => { this.users = result; this.user = this.users[0] });
  }

  discover(type: ResearchType): void {
    this.researchService.upgradeResearch(type, this.email).subscribe(() => {
      window.location.reload();
    });
  }
}
