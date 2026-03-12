import type { CardLink } from '../models';

export class CardLinkBuilder {
  private _link: CardLink = { url: '' };

  url(url: string): this {
    this._link.url = url;
    return this;
  }

  pcUrl(url: string): this {
    this._link.pc_url = url;
    return this;
  }

  iosUrl(url: string): this {
    this._link.ios_url = url;
    return this;
  }

  androidUrl(url: string): this {
    this._link.android_url = url;
    return this;
  }

  build(): CardLink {
    return this._link;
  }
}
