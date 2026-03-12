import type { ImgCombination } from '../models/elements';

export class ImgCombinationBuilder {
  private _img: ImgCombination = { tag: 'img_combination', img_key: '' };

  imgKey(imgKey: string): this {
    this._img.img_key = imgKey;
    return this;
  }

  mode(mode: string): this {
    this._img.mode = mode;
    return this;
  }

  combinationId(id: string): this {
    this._img.combination_id = id;
    return this;
  }

  elementId(id: string): this {
    this._img.element_id = id;
    return this;
  }

  margin(margin: string): this {
    this._img.margin = margin;
    return this;
  }

  build(): ImgCombination {
    return this._img;
  }
}
