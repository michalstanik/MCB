import { Component, OnInit } from '@angular/core';

import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(
    private authService: AuthService
  ) {}

  ngOnInit() {}

  login() {
    this.authService.login();
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }
}
