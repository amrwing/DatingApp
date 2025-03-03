import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent,MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  membersService = inject(MembersService);
  pageNumber = 1;
  pageSize = 5;
  members: Member[] = [];
  ngOnInit(): void {
    if (!this.membersService.paginatedResult()) {      
   this.loadMembers();
  }
  }
  loadMembers(){
    this.membersService.getMembers(this.pageNumber, this.pageSize);  }

}
