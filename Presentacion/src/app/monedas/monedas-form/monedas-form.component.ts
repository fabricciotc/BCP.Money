import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IMoneda } from '../moneda';
import { MonedasService } from '../monedas.service';

@Component({
  selector: 'app-monedas-form',
  templateUrl: './monedas-form.component.html',
  styleUrls: ['./monedas-form.component.css']
})
export class MonedasFormComponent implements OnInit {

  constructor(private fb: FormBuilder, private monedasService: MonedasService, private router: Router,
    private router2: ActivatedRoute) {
  }



  modoEdicion: boolean = false;
  formGroup!: FormGroup;
  monedaID!: string;



  ngOnInit() {
    this.formGroup = this.fb.group({
      abreviacion: '',
      descripcion: ''
    });
    this.router2.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.modoEdicion = true;
      this.monedaID = params["id"];
      this.monedasService.getMoneda(this.monedaID.toString()).subscribe(persona => this.cargarFormulario(persona),
        error => console.error(error),
        () => console.log(this.monedaID));
    });
  }


  save() {
    let persona: IMoneda = Object.assign({}, this.formGroup.value);
    console.table(persona);
    if (this.modoEdicion) {
      // editar el registro
      persona.monedaId = this.monedaID;
      persona.fechaActualizacion = new Date()
      this.monedasService.editMoneda(persona)
        .subscribe(persona => this.onSaveSF(),
          error => console.error(error));
    } else {
      // agregar el registro
      persona.fechaActualizacion = new Date()
      persona.fechaCreacion = new Date()
      this.monedasService.createMoneda(persona)
        .subscribe(persona => this.onSaveSF(),
          error => console.error(error));
    }
  }
  cargarFormulario(person: IMoneda) {
    var dp = new DatePipe("en-IN");
    var format = 'yyyy-MM-dd';
    this.formGroup.patchValue(
      {
        abreviacion: person.abreviacion,
        descripcion: person.descripcion
      });
  }
  onSaveSF() {
    this.router.navigate(["/monedas"]);
  }

}
