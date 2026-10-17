import { Component, inject, OnDestroy, signal } from '@angular/core';
import { QrCodeService } from './services/qr-code';

@Component({
  imports: [],
  selector: 'app-root',
  styleUrl: './app.scss',
  templateUrl: './app.html',
})
export class App implements OnDestroy {

  private readonly qrCodeService = inject(QrCodeService);

  url = signal('');
  imageUrl = signal<string | null>(null);
  loading = signal(false);
  error = signal('')

  updateUrl(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.url.set(input.value);
  }

  generate(event: Event): void {
    event.preventDefault();

    if (!this.url().trim() || this.loading())
      return;

    this.loading.set(true);
    this.error.set('');

    this.qrCodeService.generateQrCode(this.url()).subscribe(
      {
        next: (blob) => {
          this.clearImage();
          this.imageUrl.set(URL.createObjectURL(blob));
          this.loading.set(false);
        },
        error: () => {
          this.error.set('Unable to generate QR code.');
          this.loading.set(false);
        }
      }
    )
  }

  private clearImage(): void {
    const currentUrl = this.imageUrl();

    if (currentUrl) {
      URL.revokeObjectURL(currentUrl);
      this.imageUrl.set(null);
    }
  }

  ngOnDestroy(): void {
    this.clearImage();
  }

}
