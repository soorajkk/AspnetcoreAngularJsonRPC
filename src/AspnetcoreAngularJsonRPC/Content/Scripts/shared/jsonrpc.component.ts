import { Injectable } from 'angular2/core';
import { Http, Response, Headers } from 'angular2/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class JsonRPC {

    private _productUrl = 'http://localhost:31819/api/jsonrpc/';

    constructor(private _http: Http) {
    
    }
    
    post(method: string, param ): Observable<any> {
       // let body = JSON.stringify({ balance, amount, user_id, paymethod_id });
        var headers = new Headers();
        headers.append('Content-Type', 'application/json');
        return this._http.post(this._productUrl, String(this.wrap(method, param)), headers)
            .map((response: Response) => <any>response.json().result)
            .do(data => console.log("All: " + JSON.stringify(data)))
            .catch(this.handleError);

    }



    //getProducts(): Observable<IProduct[]> {
    //    return this._http.get(this._productUrl)
    //        .map((response: Response) => <IProduct[]>response.json().result.productList)
    //        .do(data => console.log("All: " + JSON.stringify(data)))
    //        .catch(this.handleError);
    //}

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

    //REF: http://stackoverflow.com/a/2117523
    private guid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });

    }

    private wrap(method: string, params: string) {
        return  {
            jsonrpc: '2.0',
            method: method,
            params: params,
            id: this.guid()
        };
    }

   
}