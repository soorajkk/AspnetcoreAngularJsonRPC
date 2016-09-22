"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('angular2/core');
var http_1 = require('angular2/http');
var Observable_1 = require('rxjs/Observable');
//import {JsonRPC} from '../shared/jsonrpc.component';
var ProductService = (function () {
    function ProductService(_http /*, private _JsonRPC: JsonRPC*/) {
        this._http = _http;
        this._productUrl = 'http://localhost:31819/api/jsonrpc/GetProductListReqV1/CatogoryID/1';
    }
    ProductService.prototype.getProducts = function () {
        return this._http.get(this._productUrl)
            .map(function (response) { return response.json().result.productList; })
            .do(function (data) { return console.log("All: " + JSON.stringify(data)); })
            .catch(this.handleError);
        //return this._jsonRPC.post("GetProductListReqV1", { CatogoryID: '1' })
        //     .map((response: Response) => <IProduct[]>response.json().result.productList)
        //    .do(data => console.log("All: " +  JSON.stringify(data)))
        //    .catch(this.handleError);          
    };
    ProductService.prototype.handleError = function (error) {
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    ProductService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], ProductService);
    return ProductService;
}());
exports.ProductService = ProductService;
//# sourceMappingURL=product.service.js.map