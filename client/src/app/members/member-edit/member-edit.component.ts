import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule } from '@angular/forms';
import { Gallery, GalleryModule } from 'ng-gallery';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule,GalleryModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  
ngOnInit(): void {
this.loadMember();
}
member?: Member;
private accountService = inject(AccountService);
private membersService = inject(MembersService);
private toastr = inject(ToastrService);
loadMember(){
  const user = this.accountService.currentUser();
  if(!user) return;
  this.membersService.getMember(user.username).subscribe({
    next: member => this.member = member
  });
} 
updateMember(){
console.log(this.member);
this.toastr.success("Profile updated");
}
}

