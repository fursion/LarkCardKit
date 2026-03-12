export class TemplateParameterFiller {
  private _parameters: Map<string, unknown> = new Map();

  setParameter(key: string, value: unknown): void {
    this._parameters.set(key, value);
  }

  setParameters(params: Record<string, unknown>): void {
    for (const [key, value] of Object.entries(params)) {
      this._parameters.set(key, value);
    }
  }

  getValue(key: string): unknown {
    return this._parameters.get(key);
  }

  fillString(template: string): string {
    if (!template || typeof template !== 'string') {
      return template;
    }
    
    return template.replace(/\$\{([^}:]+)(?::([^}]*))?\}/g, (match, key, defaultValue) => {
      const value = this._parameters.get(key);
      if (value !== undefined && value !== null) {
        return String(value);
      }
      return defaultValue !== undefined ? defaultValue : match;
    });
  }

  hasParameters(): boolean {
    return this._parameters.size > 0;
  }

  clear(): void {
    this._parameters.clear();
  }
}
