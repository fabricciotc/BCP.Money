import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICotizar } from './Cotizar';
import { ITipocambio } from './tipocambio';

@Injectable({
  providedIn: 'root'
})
export class TipocambioService {

  private apiUREL = "api/tipocambio";
  private apiCOTI = "api/cotizar";

  constructor(private http: HttpClient) { }


  getTipoCambios(): Observable<ITipocambio[]> {
    return this.http.get<ITipocambio[]>(this.apiUREL);
  }
  getTipoCambio(tipoCambioId: string): Observable<ITipocambio> {
    return this.http.get<ITipocambio>(this.apiUREL + '/' + tipoCambioId);
  }
  createTipoCambio(moneda: ITipocambio): Observable<ITipocambio> {
    return this.http.post<ITipocambio>(this.apiUREL, moneda);
  }
  cotizar(moneda: ICotizar): Observable<ICotizar> {
    return this.http.post<ICotizar>(this.apiCOTI, moneda);
  }
  editTipoCambio(moneda: ITipocambio): Observable<ITipocambio> {
    var h = moneda.tipo_Cambio_Id.toString();
    return this.http.put<ITipocambio>(this.apiUREL + '/' + h, moneda);
  }
  deleteTipoCambio(monedaID: string): Observable<ITipocambio> {
    return this.http.delete<ITipocambio>(this.apiUREL + "/" + monedaID);
  }
}