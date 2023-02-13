import { Injectable } from '@angular/core';
import { AES, enc } from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {
  private key = 'PIidFn3vxYpe9iDlP6Ebj7L4UKOP7vVa';

  encrypt(toEncrypt: string): string {
    return AES.encrypt(toEncrypt, this.key).toString();
  }

  decrypt(toDecrypt: string): string {
    return toDecrypt ? AES.decrypt(toDecrypt, this.key).toString(enc.Utf8) : null;
  }
}
