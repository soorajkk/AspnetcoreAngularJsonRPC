import { Injectable } from 'angular2/core';
import { IProduct } from './product';
import { Http, Response } from 'angular2/http';
import { Observable } from 'rxjs/Observable';
//import {JsonRPC} from '../shared/jsonrpc.component';



@Injectable()
export class ProductService {
    private _productUrl = 'http://localhost:31819/api/jsonrpc/GetProductListReqV1/CatogoryID/1';

    constructor(private _http: Http/*, private _JsonRPC: JsonRPC*/ ) { }

    getProducts(): Observable<IProduct[]> {
        return this._http.get(this._productUrl)
            .map((response: Response) => <IProduct[]>response.json().result.productList)
            .do(data => console.log("All: " +  JSON.stringify(data)))
            .catch(this.handleError);
            
        //return this._jsonRPC.post("GetProductListReqV1", { CatogoryID: '1' })
        //     .map((response: Response) => <IProduct[]>response.json().result.productList)
        //    .do(data => console.log("All: " +  JSON.stringify(data)))
        //    .catch(this.handleError);          
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

}