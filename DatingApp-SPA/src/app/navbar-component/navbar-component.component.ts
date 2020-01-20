import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-navbar-component',
  templateUrl: './navbar-component.component.html',
  styleUrls: ['./navbar-component.component.css']
})
export class NavbarComponentComponent implements OnInit {
  model: any = {} 
  constructor(private authService : AuthService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next=>{
      console.log("successfully logged in");
    },
    error=>{
      console.log("login errored out");
    });
  }

  loggedin() {
    const token=localStorage.getItem('token');
     return !!token;
  }

  loggedout(){
    localStorage.removeItem('token');
    console.log("logged out");
  }
}
