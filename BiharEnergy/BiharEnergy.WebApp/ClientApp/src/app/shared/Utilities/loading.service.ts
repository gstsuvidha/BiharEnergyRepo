import { Subscription } from 'rxjs/Subscription';
import { Injectable } from '@angular/core';

@Injectable()
export class LoadingService {

  constructor() { }
  busy:Subscription;
}
