import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class QrCodeService {
    private readonly http = inject(HttpClient);

    generateQrCode(url: string): Observable<Blob> {
        return this.http.post('/api/qrcode',
            { url },
            {responseType: 'blob'}
        );
    }
}