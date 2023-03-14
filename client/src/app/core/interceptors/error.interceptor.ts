import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastrService: ToastrService) {} //we pass router so we navigate somewhere when response comes back
  //we can make use of the intercept method cuz we implement the httpInterceptor class
  //we pass request we can either do something with it when it goes out or after it comes back
  //what comes from the httpclient is an Observable of <HttpEvent>
  //we use pipe operator so we can do something with the observable when it comes back
  //we use pipe to manipulate the observable before we pass it on the component
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          if (error.status === 400) {
            if (error.error.errors) {
              throw error.error;
            } else {
              this.toastrService.error(
                error.error.message,
                error.status.toString()
              );
            }
          }
          if (error.status === 401) {
            this.toastrService.error(
              error.error.message,
              error.status.toString()
            );
          }
          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }
          if (error.status === 500) {
            //we pass state to route
            const navigationExtras: NavigationExtras = {
              state: { error: error.error },
            };
            this.router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(() => new Error(error.message));
      })
    );
  }
}
