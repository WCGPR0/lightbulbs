import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Lightbulb } from './lightbulb.model';

@Injectable({
  providedIn: 'root'
})
export class LightbulbService {
  url = '/api/LightBulb/GetLightBulbs';
  constructor(private http: HttpClient) { }

  getLightBulbs(numOfLightbulbs: number, people: number): Observable<Lightbulb> {
    const params = new HttpParams().set('numOfLightbulbs', numOfLightbulbs.toString()).set('people', people.toString());
    return this.http.get<Lightbulb>(this.url, { params: params });
  }
}
