var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { AuthService } from './core/auth.service';
import { Router } from '@angular/router';
var AppComponent = /** @class */ (function () {
    function AppComponent(_authService, _router) {
        this._authService = _authService;
        this._router = _router;
        this.firstLogin = false;
    }
    AppComponent.prototype.ngOnInit = function () {
    };
    AppComponent.prototype.login = function () {
        this._authService.login();
    };
    AppComponent.prototype.isLoggedIn = function () {
        console.log("IsLoggedIn AppComponent");
        return this._authService.isLoggedIn();
    };
    AppComponent = __decorate([
        Component({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [AuthService,
            Router])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map