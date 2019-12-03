import { Component } from '@angular/core';
import { LightbulbService } from './lightbulb.service';

import { Lightbulb } from './lightbulb.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  lightbulb: Lightbulb;
  numOfLightbulbs: number = 100;
  people: number = 100;

  constructor(private lightbulbService: LightbulbService) { }
  OnBlurMethod() {
    this.lightbulbService.getLightBulbs(this.numOfLightbulbs,this.people).subscribe(lightbulb => this.lightbulb = lightbulb);
    this.scrollToBottom();
  }

  scrollToBottom(): void {
    try {
        window.scrollTo(0,document.body.scrollHeight);
    } catch(err) { }    
  }

  getOpenBulbs() {
    var arr: number[] = [];
    this.lightbulb.bulbs.forEach( (x, i) => {
      if (x) 
        arr.push(i);
    });
    return arr;
  }

  title = 'LightBulbs';
}
