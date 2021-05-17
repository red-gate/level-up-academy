- Modules  
  https://www.typescriptlang.org/docs/handbook/2/modules.html

  - import, export, default

    - https://www.typescriptlang.org/docs/handbook/2/modules.html#modules-in-typescript
    - Adding `import` or `export` to a typescript file makes it a module
    - Modules are namespaced
      - Typescript will complain about duplicate definitions between files that are not modules
      - You can add `export {}` if you just want the namespace without exporting something
    - Export styles:  
      `export default function hello[...]` is used like `import hello from './hello'`  
      `export function hello[...] ` or `function hello[...]; export { hello }` is used like `import { hello } from './hello'`  
      Style choice: export each item individually, or put one big `export { ... }` statement at the bottom of the file.
    - Typescript can also import and export types. This has no impact on the compiled result, but provides namespacing for types.
      - Note: typescript can sometimes be confusing - the same name might refer to both a value and a type. This happens most often with class declarations, but can happen with other types too. Types and values have separate namespaces (TODO: check details here)

  - require
    - https://www.typescriptlang.org/docs/handbook/2/modules.html#es-module-syntax-with-commonjs-behavior
    - Typescript also supports the commonjs / node syntax for modules, using `module.exports = { ... }` and `const foo = require(...)`
  - TypeScript Module Output Options
    - https://www.typescriptlang.org/docs/handbook/2/modules.html#typescripts-module-output-options

- Classes and modules (type modules)
  - readonly
- Object types
- Interfaces
  - extending
- Assigning to interface/class/type
