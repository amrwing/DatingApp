import { CommonModule} from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from "./nav/nav.component";
import { RouterOutlet } from '@angular/router';
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";
import { User } from './models/user';
import { Observable } from 'rxjs';
import { NgxSpinner, NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent, HomeComponent,NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);
  title = 'Date me';
  ngOnInit(): void {

    this.setCurrentUser();
  }
  setCurrentUser(){
    const userString = localStorage.getItem("user");
    if(!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }

}

