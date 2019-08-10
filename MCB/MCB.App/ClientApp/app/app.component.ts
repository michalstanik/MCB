import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    firstLogin = false;
    constructor(
        private _authService: AuthService,
        private _router: Router
    ) { }

    ngOnInit() {

    }

    login() {
        this._authService.login();
    }

    isLoggedIn() {
        console.log("IsLoggedIn AppComponent");
        return this._authService.isLoggedIn();
    }
}
