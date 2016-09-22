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
var JsonRPC = (function () {
    function JsonRPC(_http) {
        this._http = _http;
        this._productUrl = 'http://localhost:31819/api/jsonrpc/';
    }
    JsonRPC.prototype.post = function (method, param) {
        // let body = JSON.stringify({ balance, amount, user_id, paymethod_id });
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        return this._http.post(this._productUrl, String(this.wrap(method, param)), headers)
            .map(function (response) { return response.json().result; })
            .do(function (data) { return console.log("All: " + JSON.stringify(data)); })
            .catch(this.handleError);
    };
    //getProducts(): Observable<IProduct[]> {
    //    return this._http.get(this._productUrl)
    //        .map((response: Response) => <IProduct[]>response.json().result.productList)
    //        .do(data => console.log("All: " + JSON.stringify(data)))
    //        .catch(this.handleError);
    //}
    JsonRPC.prototype.handleError = function (error) {
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    //REF: http://stackoverflow.com/a/2117523
    JsonRPC.prototype.guid = function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };
    JsonRPC.prototype.wrap = function (method, params) {
        return {
            jsonrpc: '2.0',
            method: method,
            params: params,
            id: this.guid()
        };
    };
    JsonRPC = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [http_1.Http])
    ], JsonRPC);
    return JsonRPC;
}());
exports.JsonRPC = JsonRPC;
//# sourceMappingURL=jsonrpc.component.js.map