import { Component, OnInit } from '@angular/core';

import { AuthService } from '../core/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {


  constructor(
    private _authService: AuthService
  ) {}


  ngOnInit() {

  }

  login() {
    this._authService.login();
  }

  isLoggedIn() {
    return this._authService.isLoggedIn();
  }
}
