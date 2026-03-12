import type { Image } from '../models/elements';
import { ImageSize } from '../enums';

export class ImageBuilder {
  private _image: Image;

  constructor(imgKey: string) {
    this._image = { tag: 'img', img_key: imgKey };
  }

  alt(alt: string): this {
    this._image.alt = { tag: 'plain_text', content: alt };
    return this;
  }

  size(size: ImageSize | string): this {
    this._image.size = typeof size === 'string' ? size : size;
    return this;
  }

  mode(mode: string): this {
    this._image.mode = mode;
    return this;
  }

  width(width: string): this {
    this._image.width = width;
    return this;
  }

  height(height: string): this {
    this._image.height = height;
    return this;
  }

  preview(preview: boolean = true): this {
    this._image.preview = preview;
    return this;
  }

  hoverTips(tips: string): this {
    this._image.hover_tips = { tag: 'plain_text', content: tips };
    return this;
  }

  clickUrl(url: string): this {
    this._image.click_url = url;
    return this;
  }

  elementId(id: string): this {
    this._image.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._image.margin = margin;
    return this;
  }

  build(): Image {
    return this._image;
  }
}
