import { Component, computed, inject, input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from '../../_models/member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_services/likes.service';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent implements OnInit{
  hasLiked = computed(() => this.likesService.likeIds().includes(this.member().id));
  private likesService = inject(LikesService);
  ngOnInit(): void {
    console.log("member:"+this.member);
  }
  member = input.required<Member>();
  
}
