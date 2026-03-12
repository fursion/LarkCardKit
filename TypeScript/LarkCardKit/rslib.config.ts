import { defineConfig } from '@rslib/core';

export default defineConfig({
  lib: [
    {
      format: 'esm',
      dts: true,
      output: {
        distPath: {
          root: './dist/esm',
        },
      },
    },
    {
      format: 'cjs',
      output: {
        distPath: {
          root: './dist/cjs',
        },
      },
    },
  ],
  source: {
    entry: {
      index: './src/index.ts',
    },
  },
  output: {
    target: 'node',
  },
});
