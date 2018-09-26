import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { map, filter, tap, catchError } from 'rxjs/operators';
import { Response } from '@angular/http';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';



export abstract class ServiceBase<T> {

  public baseUrl: string;
  private entities: T[];

  constructor(protected http: HttpClient, baseUrl: string) {
    this.baseUrl = baseUrl
  }

  getAll() {
    return this.http.get<T[]>(this.baseUrl);
  }

  getOne(id: number): Observable<T> {
    if (id == 0) {
      return of(this.initializeObject());
    };
    return this.http.get<T>(`${this.baseUrl}/${id}`);
  }

  save(entity: T, id: number): Observable<T> {

    if (id == 0) {
      return this.create(entity);
    }
    return this.update(entity, id);
  }

  private create(entity: T): Observable<T> {
    return this.http.post<T>(this.baseUrl, entity)
      .pipe(
      tap(data => console.log('created: ' + JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  private update(entity: T, id: number): Observable<T> {
    const url = `${this.baseUrl}/${id}`;

    return this.http.put<T>(url, entity)
      .pipe(
      tap(data => console.log('updated: ' + JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  delete(id: number): Observable<any> {
 
    const url = `${this.baseUrl}/${id}`;

    return this.http.delete<T>(url)
    .pipe(
    tap(data => {
      console.log('deleted' + JSON.stringify(data));
  }),
    catchError(this.handleError)
    );
  }

  abstract initializeObject(): T;

  private handleError(err: HttpErrorResponse): ErrorObservable {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}, body was: ${err.error}`;
    }
    console.error(err);
    return new ErrorObservable(errorMessage);
  }

}
