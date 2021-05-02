import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class CowinService {

    data: any;

    constructor(private http: HttpClient) {
    }

    getCowinData(api: string): Promise<any> {
        return this.http.get(api).pipe(map(d => {
            this.data = d;
            return d;
        })).toPromise();
    }

    getData<T>(api: string): Promise<T> {
        return this.http.get<T>(api).toPromise();
    }
}