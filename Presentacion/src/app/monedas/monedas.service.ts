import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IMoneda } from './moneda';

@Injectable({
  providedIn: 'root'
})
export class MonedasService {

  private apiUREL = "api/moneda";

  constructor(private http: HttpClient) { }


  getMonedas(): Observable<IMoneda[]> {
    return this.http.get<IMoneda[]>(this.apiUREL);
  }
  getMoneda(monedaId: string): Observable<IMoneda> {
    return this.http.get<IMoneda>(this.apiUREL + '/' + monedaId);
  }
  createMoneda(moneda: IMoneda): Observable<IMoneda> {
    return this.http.post<IMoneda>(this.apiUREL, moneda);
  }
  editMoneda(moneda: IMoneda): Observable<IMoneda> {
    var h = moneda.monedaId.toString();
    return this.http.put<IMoneda>(this.apiUREL + '/' + h, moneda);
  }
  deleteMoneda(monedaID: string): Observable<IMoneda> {
    return this.http.delete<IMoneda>(this.apiUREL + "/" + monedaID);
  }
}