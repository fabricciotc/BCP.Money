import { Component, OnInit } from '@angular/core';
import { ITipocambio } from './tipocambio';
import { TipocambioService } from './tipocambio.service';

@Component({
  selector: 'app-tipocambio',
  templateUrl: './tipocambio.component.html',
  styleUrls: ['./tipocambio.component.css']
})
export class TipocambioComponent implements OnInit {


  personas: ITipocambio[]=[];
  constructor(private tipoCambioServices: TipocambioService) { }

  ngOnInit() {
    this.cargarData();
  }
  delete(persona: ITipocambio) {
    this.tipoCambioServices.deleteTipoCambio(persona.tipo_Cambio_Id.toString()).subscribe(
      ok => this.cargarData(),
      error => console.error(error)
    );
  }
  cargarData() {
    this.tipoCambioServices.getTipoCambios().subscribe(
      personasWS => { this.personas = personasWS; console.log(personasWS) },
      error => console.error(error)
    );
  }

}
