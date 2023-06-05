import { Injectable } from '@angular/core';
import { filter, Observable, Subject } from "rxjs";
import { Notify } from '../models/notify';
import { NgTemplateOutlet } from '@angular/common';
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private subject = new Subject<Notify>();
  notify$ = this.subject.asObservable();
  onNotify(notify: Notify) {
    this.subject.next(notify)
  }
  constructor() { }
}
