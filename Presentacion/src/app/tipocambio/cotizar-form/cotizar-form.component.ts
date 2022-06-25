import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IMoneda } from 'src/app/monedas/moneda';
import { MonedasService } from 'src/app/monedas/monedas.service';
import { ICotizar } from '../Cotizar';
import { ITipocambio } from '../tipocambio';
import { TipocambioService } from '../tipocambio.service';

@Component({
  selector: 'app-cotizar-form',
  templateUrl: './cotizar-form.component.html',
  styleUrls: ['./cotizar-form.component.css']
})
export class CotizarFormComponent implements OnInit {


  constructor(private fb: FormBuilder, private monedaService: MonedasService,
    private tipoCambioServices: TipocambioService) {
  }
  // final: number = 0;
  formGroup!: FormGroup;
  tipocambioID!: string;
  mensaje!: string;
  tipCam!: ITipocambio;
  monedas: IMoneda[] = [];
  tipocambios: ITipocambio[] = [];
  cargarDatos() {
    this.monedaService.getMonedas().subscribe(
      monedasWS => this.monedas = monedasWS,
      error => console.error(error)
    );
    this.tipoCambioServices.getTipoCambios().subscribe(
      monedasWS => this.tipocambios = monedasWS,
      error => console.error(error)
    );
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      tipo_Cambio_Id: '',
      montoInicial: 0,
    });
    this.cargarDatos();
 
  }


  save() {
    let persona: ICotizar = Object.assign({}, this.formGroup.value);
    console.log(persona);
    this.tipoCambioServices.getTipoCambio(persona.tipo_Cambio_Id).subscribe(
      monedasWS => {
        this.tipCam = monedasWS
        console.log(this.tipCam);
        persona.abreviacionOrigen = this.tipCam.monedaOrigen.abreviacion;
        persona.abreviacionDestino = this.tipCam.monedaDestino.abreviacion;
        console.table(persona);
        this.tipoCambioServices.cotizar(persona)
          .subscribe(persona => {
            console.log(persona)
            this.mensaje =persona.montoInicial +" "+ persona.abreviacionOrigen + " -> " + persona.montoFinal +" "+ persona.abreviacionDestino
              // this.final = persona.montoFinal
          },
            error => console.error(error));
      },
      error => console.error(error)
    );
    
  }
 



}
