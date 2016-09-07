import {Component} from 'angular2/core';

@Component({
    selector: 'my-app',
    template: '<p>{{pageTitle}}</p>'
})
export class AppComponent {
  
   public pageTitle: string ;
    constructor() {
     
        this.pageTitle = "Title changed";    
    }
}