import { Component, OnInit } from 'angular2/core';
import { ROUTER_DIRECTIVES } from 'angular2/router';

import { IProduct } from './product'
import { ProductFilterPipe } from './product-filter.pipe';
import { StarComponent } from '../shared/star.component';
import { ProductService } from './product.service';

@Component({
    templateUrl: 'app/product/product-list.component.html',
    styleUrls: ['app/product/product-list.component.css'],
    pipes: [ProductFilterPipe],
    directives: [StarComponent, ROUTER_DIRECTIVES]
})
export class ProductListComponent implements OnInit {
    pageTitle: string = 'Product List';
    imageWidth: number = 50;
    imageMargin: number = 2;
    showImage: boolean = false;
    listFilter: string;
    errorMessage: string;

    products: IProduct[];

    constructor(private _productService: ProductService) {

    }

    toggleImage(): void {
        this.showImage = !this.showImage;
    }

    ngOnInit(): void {

        console.log("jajajajajaj" + String( this.wrap("GetProductListReqV1", { CatogoryID: '1' })));
        this._productService.getProducts()
            .subscribe(
            products => this.products = products,
            error => this.errorMessage = <any>error);
    }

    onRatingClicked(message: string): void {
        this.pageTitle = 'Product List: ' + message;
    }

    private wrap(method, params) {
        return {
            jsonrpc: '2.0',
            method: method,
            params: params,
            id: this.guid()
        };
    }

    //REF: http://stackoverflow.com/a/2117523
    private guid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });

    }
}