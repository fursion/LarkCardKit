import type { Fallback } from '../models';

export class FallbackBuilder {
  private _fallback: Fallback = {};

  title(title: string): this {
    this._fallback.title = { tag: 'plain_text', content: title };
    return this;
  }

  content(content: string): this {
    this._fallback.content = { tag: 'plain_text', content: content };
    return this;
  }

  build(): Fallback {
    return this._fallback;
  }
}
