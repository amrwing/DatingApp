import { Component, input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from '../../models/member';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent implements OnInit{
  ngOnInit(): void {
    console.log("member:"+this.member);
  }
  member = input.required<Member>();
  
}
