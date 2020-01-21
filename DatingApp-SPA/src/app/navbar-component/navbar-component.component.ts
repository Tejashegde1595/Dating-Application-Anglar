import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-navbar-component',
  templateUrl: './navbar-component.component.html',
  styleUrls: ['./navbar-component.component.css']
})
export class NavbarComponentComponent implements OnInit {
  model: any = {} 
  constructor(public authService : AuthService,private alertify : AlertifyService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next=>{
      this.alertify.success("successfully logged in");
    },
    error=>{
      this.alertify.error(error);
    });
  }

  loggedin() {
    return this.authService.loggedIn();
  }

  loggedout(){
    localStorage.removeItem('token');
    this.alertify.message("logged out");
  }
}
