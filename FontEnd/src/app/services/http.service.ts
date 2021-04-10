import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

	constructor(private http: HttpClient) {
		
	}

	getRequest(params?) {
		return this.http
				.get('http://localhost:5000/api/filemanager/get', { params: params })
				.pipe(catchError(this.handleError));
	}

    upload(fileToUpload: File): Observable<Object> {
        const formData: FormData = new FormData();
        formData.append('fileKey', fileToUpload, fileToUpload.name);
        return this.http.post('http://localhost:5000/api/filemanager/upload', formData);
    }
    
    downloadFile(fileName: string): Observable<Blob> {
        return this.http.get('http://localhost:5000/api/filemanager/download/' + fileName, {
            responseType: 'blob'
        })
    }

    deleteFile(fileName: string) {
        return this.http.get('http://localhost:5000/api/filemanager/delete/' + fileName);
    }
  
	private handleError(error: HttpErrorResponse) {
		if(error && error.status == 401)
			return throwError({ error: 'Tu sesión ha expirado o no tienes permisos para realizar esta acción.' });
		else if(error && error.status == 404)
			return throwError({ error: 'No se encontro el servicio solicitado' });
		else if(error && error.status == 500)
			return throwError({ error: 'Ocurrió un problema con el servidor.' });
		else
			return throwError(error);
	}
}
