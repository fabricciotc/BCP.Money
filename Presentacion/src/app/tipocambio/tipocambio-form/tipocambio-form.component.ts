import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IMoneda } from 'src/app/monedas/moneda';
import { MonedasService } from 'src/app/monedas/monedas.service';
import { ITipocambio } from '../tipocambio';
import { TipocambioService } from '../tipocambio.service';

@Component({
  selector: 'app-tipocambio-form',
  templateUrl: './tipocambio-form.component.html',
  styleUrls: ['./tipocambio-form.component.css']
})
export class TipocambioFormComponent implements OnInit {


  constructor(private fb: FormBuilder, private monedaService: MonedasService,
    private tipoCambioServices: TipocambioService, private router: Router,
    private router2: ActivatedRoute) {
  }
  modoEdicion: boolean = false;
  formGroup!: FormGroup;
  tipocambioID!: string;
  monedas: IMoneda[] = [];
  cargarDataMonedas() {
    this.monedaService.getMonedas().subscribe(
      monedasWS => this.monedas = monedasWS,
      error => console.error(error)
    );
  }

  ngOnInit() {
    this.formGroup = this.fb.group({
      tipo_Cambio_Id: '',
      descripcion: '',
      conversion: 0,
      monedaOrigenId: '',
      monedaDestinoId: '',
      monedaDestino: {},
      monedaOrigen: {},
      fechaActualizacion: Date,
      fechaCreacion: Date,
      abreviacion: {}
    });
    this.cargarDataMonedas();
    this.router2.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.modoEdicion = true;
      this.tipocambioID = params["id"];
      this.tipoCambioServices.getTipoCambio(this.tipocambioID.toString()).subscribe(persona => this.cargarFormulario(persona),
        error => console.error(error),
        () => console.log(this.tipocambioID));
    });
  }


  save() {
    let persona: ITipocambio = Object.assign({}, this.formGroup.value);
    console.table(persona);
    if (this.modoEdicion) {
      // editar el registro
      persona.tipo_Cambio_Id = this.tipocambioID;
      this.tipoCambioServices.editTipoCambio(persona)
        .subscribe(persona => this.onSaveSF(),
          error => console.error(error));
    } else {
      // agregar el registro

      this.tipoCambioServices.createTipoCambio(persona)
        .subscribe(persona => this.onSaveSF(),
          error => alert(error));
    }
  }
  cargarFormulario(person: ITipocambio) {
    var dp = new DatePipe("en-IN");
    var format = 'yyyy-MM-dd';
    this.formGroup.patchValue(
      {
        descripcion: person.descripcion,
        conversion: person.conversion,
        monedaOrigenId: person.monedaOrigenId,
        monedaDestinoId: person.monedaDestinoId,
        fechaActualizacion: new Date,
      });
  }
  onSaveSF() {
    this.router.navigate(["/tipocambio"]);
  }





}
