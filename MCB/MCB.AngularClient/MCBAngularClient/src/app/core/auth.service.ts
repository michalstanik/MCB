import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { UserManager, User, WebStorageStateStore } from 'oidc-client';
import { environment } from 'src/environments/environment';



@Injectable()
export class AuthService {
    private _userManager: UserManager;
    private _user: User;

    constructor(private httpClient: HttpClient) {
        var config = {
            authority: environment.stsAuthority,
            client_id: environment.clientId,
            redirect_uri: `${environment.clientRoot}assets/oidc-login-redirect.html`,
            scope: 'openid tripwithmeapi profile role',
            response_type: 'id_token token',
            post_logout_redirect_uri: `${environment.clientRoot}?postLogout=true`,
            userStore: new WebStorageStateStore({ store: window.localStorage })
        };
        this._userManager = new UserManager(config);
        this._userManager.getUser().then(user => {
            if (user && !user.expired) {
                this._user = user;
            }
        });
    }

    login(): Promise<any> {
        return this._userManager.signinRedirect();
    }

    logout(): Promise<any> {
        return this._userManager.signoutRedirect();
    }

    isLoggedIn(): boolean {
        return this._user && this._user.access_token && !this._user.expired;
    }

    getAccessToken(): string {
        return this._user ? this._user.access_token : '';
    }

    signoutRedirectCallback(): Promise<any> {
        return this._userManager.signoutRedirectCallback();
    }
}
