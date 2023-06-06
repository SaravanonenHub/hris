import { EventEmitter, Injectable } from '@angular/core';
import { Message } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class PgmessageService {
  message!:Message[];
  emitter:EventEmitter<Message> = new EventEmitter<Message>();
  constructor() { }
  getEmitter(){
    return this.emitter;
  }
  setEmitter(event:any){
    this.emitter.emit(event);
  }
}
