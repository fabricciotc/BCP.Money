import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserInfo } from './user-info';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
    // private apiURL = this.baseUrl + 'api/usuario';

    // @Inject(String) private url: string, private http: Http

    constructor(private http: HttpClient) { }
    create(userInfo: IUserInfo): Observable<any> {
        return this.http.post<any>("api/usuario/registrar", userInfo);

    }
  login(userInfo: IUserInfo): Observable<any> {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
        return this.http.post<any>("api/usuario/login", userInfo);
    }

    obtenerToken(): string|null {
        var token=localStorage.getItem("token");
        return token;
    }

    obtenerExpiracionToken(): string|null {
        return localStorage.getItem("tokenExpiration");
    }

    logout() {
        localStorage.removeItem("token");
        localStorage.removeItem("tokenExpiration");
    }

    estaLogueado(): boolean {

        var exp = this.obtenerExpiracionToken();

        if (!exp) {
            // el token no existe
            return false;
        }

        var now = new Date().getTime();
        var dateExp = new Date(exp);

        if (now >= dateExp.getTime()) {
            // ya expiró el token
            localStorage.removeItem('token');
            localStorage.removeItem('tokenExpiration');
            return false;
        } else {
            return true;
        }

    }
}
