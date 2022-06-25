import { IMoneda } from "../monedas/moneda";

export interface ITipocambio {
        tipo_Cambio_Id:string,
        descripcion:string,
        conversion: number,
        monedaOrigenId: string,
        monedaDestinoId: string,
        monedaDestino: IMoneda,
        monedaOrigen: IMoneda,
        fechaActualizacion:Date,
        fechaCreacion: Date,
        abreviacion:string
}
