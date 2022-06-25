import { Component, OnInit } from '@angular/core';
import { IMoneda } from './moneda';
import { MonedasService } from './monedas.service';

@Component({
  selector: 'app-monedas',
  templateUrl: './monedas.component.html',
  styleUrls: ['./monedas.component.css']
})
export class MonedasComponent implements OnInit {

  monedas: IMoneda[]=[];
  constructor(private monedaService: MonedasService) { }

  ngOnInit() {
    this.cargarData();
  }
  delete(moneda: IMoneda) {
    this.monedaService.deleteMoneda(moneda.monedaId.toString()).subscribe(
      ok => this.cargarData(),
      error => console.error(error)
    );
  }
  cargarData() {
    this.monedaService.getMonedas().subscribe(
      monedasWS => this.monedas = monedasWS,
      error => console.error(error)
    );
  }

}
