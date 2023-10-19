import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  constructor() { 
    this.estadoSpinner = new BehaviorSubject<boolean>(false);
  }

  private estadoSpinner:BehaviorSubject<boolean>;

  get getEstadoSpinner():Observable<boolean>{
    return this.estadoSpinner.asObservable();
  }

  mostrarSpinner():void{
    this.estadoSpinner.next(true);
  }

  ocultarSpinner():void{
    this.estadoSpinner.next(false);
  }

}
