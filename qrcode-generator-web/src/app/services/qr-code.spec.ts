import { TestBed } from '@angular/core/testing';
import { QrCodeService } from './qr-code';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';

describe('QrCodeService', () => {
  let service: QrCodeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(QrCodeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should send a POST request and return a PNG', () => {
    const url = "https://google.com";
    const fakePng = new Blob(['fake png'], {
      type: 'image/png'
    });

    let result: Blob | undefined;

    service.generateQrCode(url).subscribe(blob => {
      result = blob;
    });

    const req = httpMock.expectOne('/api/qrcode');

    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ url });
    expect(req.request.responseType).toBe('blob');

    req.flush(fakePng);

    expect(result).toBe(fakePng);
  });
});
