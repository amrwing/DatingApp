import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { FormsModule } from '@angular/forms';
import { UserParams } from '../../_models/userParams';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent, PaginationModule, FormsModule],
    templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  private accountService = inject(AccountService);
  membersService = inject(MembersService);
  userParams = new UserParams(this.accountService.currentUser());
  genderList = [{value: "female", display: "Females"}, {value: "male", display: "Males"}]
  members: Member[] = [];
  ngOnInit(): void {
    if (!this.membersService.paginatedResult()) {      
   this.loadMembers();
  }
  }
  resetFilters() {
    this.userParams = new UserParams(this.accountService.currentUser());
    this.loadMembers();
  }
  loadMembers(){
    this.membersService.getMembers(this.userParams);
   }
    pageChanged(event: any) {
      if (this.userParams.pageNumber !== event.page) {
        this.userParams.pageNumber = event.page;
        this.loadMembers();
      }
    }
}
