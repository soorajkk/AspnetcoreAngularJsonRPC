import {Component} from 'angular2/core';
//import { Component } from 'angular2/core';
import { HTTP_PROVIDERS } from 'angular2/http';
import 'rxjs/Rx';   // Load all features
import { ROUTER_PROVIDERS, RouteConfig, ROUTER_DIRECTIVES } from 'angular2/router';

import { ProductListComponent } from './product/product-list.component';
import { ProductService } from './product/product.service';
import { WelcomeComponent } from './home/welcome.component';
import { ProductDetailComponent } from './product/product-detail.component';

@Component({
    selector: 'my-app',
    template: `
        <div>
        <nav class='navbar navbar-default'>
            <div class='container-fluid'>
                <a class='navbar-brand'>{{pageTitle}}</a>
                <ul class='nav navbar-nav'>
                    <li><a [routerLink]="['Welcome']">Home</a></li>
                    <li><a [routerLink]="['Product']">Product List</a></li>
                </ul>
            </div>
        </nav>
        <div class='container'>
            <router-outlet></router-outlet>
        </div>
     </div>
     `,
    directives: [ROUTER_DIRECTIVES],
    providers: [ProductService,
        HTTP_PROVIDERS,
        ROUTER_PROVIDERS]
})
@RouteConfig([
    { path: '/welcome', name: 'Welcome', component: WelcomeComponent, useAsDefault: true },
    { path: '/product', name: 'Product', component: ProductListComponent },
    { path: '/product/:id', name: 'ProductDetail', component: ProductDetailComponent }
])
export class AppComponent {

    public pageTitle: string;
    constructor() {

        this.pageTitle = "Title changed";
    }
}