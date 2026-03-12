export function toJson(obj: unknown, indented: boolean = false): string {
  if (indented) {
    return JSON.stringify(obj, (key, value) => {
      if (value === null || value === undefined) {
        return undefined;
      }
      return value;
    }, 2);
  }
  return JSON.stringify(obj, (key, value) => {
    if (value === null || value === undefined) {
      return undefined;
    }
    return value;
  });
}

export function toJsonWithNull(obj: unknown, indented: boolean = false): string {
  if (indented) {
    return JSON.stringify(obj, null, 2);
  }
  return JSON.stringify(obj);
}
