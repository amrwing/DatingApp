import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../models/member';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  private route = inject(ActivatedRoute);
  private memberService = inject(MembersService);
  member? : Member;
  ngOnInit(): void {
  this.loadMember();
}
loadMember(){
  const username = this.route.snapshot.paramMap.get("username");
  if(!username) return;
  this.memberService.getMember(username).subscribe({
    next:(memberParam)=> this.member = memberParam,
  });
}
}
