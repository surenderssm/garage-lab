# TypeScript

## Notes

- `npm install -g typescript`

  - This installs typescript compiler globally. The same can be verified by running `tsc --version`

- `tsc hello.ts`

  - `tsc` compiles/transforms it into a plain JavaScript file.

- `tsc --target es2015 hello.ts`

  - By default TypeScript targets ES3, old version of ECMAScript. Running with -target es2015 changes TypeScript to target ECMAScript 2015.

- Types

  - JavaScript has three very commonly used primitive types : string, number and boolean.
  - Arrays, for example : number[], string[], `Array<number>`
  - TypeScript has special type,`any` that you can use whenever you don't want a particular value to cause typechecking errors.

- `const`, `var`, `let`

  - TODO

- Functions

  - Functions are the primary means of passing data around in JavaScript.
  - Basic building block

- Object Types

  - Object : JavaScript value with properties.
  - Optional properties : add a `?` after thh property name.

- Union Types

  - Treat types as sets
  - id: number | string

- Type Aliases

  - A type alias is exactly that - a name for any type.For example :
    `type Point = { x: number; y: number; };`
  - Type alias can name a union type, for example : `type ID = string | number;`

- Interfaces

  - An interface declaration is another way to name an object type.
  - TypeScript is only concerned with the structure of the value. It only cares that it has the expected properties. Being concerned only with the structure and capabilities of types is why we call TypeScript a structurally typed type system.
  - Type cannot be re-opened to add new properties, whereas an interface is always extendable.
  - Prefer interface until you need features from `type`.

- Literal Types

  - Variable declaration
  - in JavaScript both `var` and `let` allow for changing what is held inside the variable, and `const` does not.

- `null` and `undefined`

  - JavaScript has two primitive values used to signal absent or uninitialized value: null and
    undefined. Same goes for TypeScript.
  - With StrictNullChecks off, null and undefined can be assigned to any property of any type. Recommendation is to keep this flag on.
  - In JavaScript, constructs like `if` first force their conditions to booleans to make sense of them, and then choose their branches depending on whether the result is true or false.
  - Value like below evaluate to `false`, and other values `true`
    - `0`
    - `NaN`
    - `""` (the empty string)
    - `0n` (the bigint version of zero)
    - `null`
    - `undefined`

- `object`

  - object refers to any value that isn't the primitive
  - object is not Object :)

- Rest Parameters

  - Rest parameters helps in defining functions that takes unbounded
  - Appears after all the parameters, and uses the `...` syntax.

- Classes

  - TypeScript offers full support for the `class` keyword introduced in ES2015.
  - A field declaration creates a public writeable property on a class.
  - Fields may be prefixed with the readonly modifier. This prevents assignments to the field outside
    of the constructor.
  - Class constructors are very similar to function.
  - Super Calls : Call `super();` in your constructor body before using any `this.` members.
  - Methods : A function property on a class is called a method.
  - Inside a method body, it is still mandatory to access fields and other methods via this. An unqualified name in a method body will always refer to something in the enclosing scope.
  - Member Visibility
    - public : default visibility of class member.
    - protected : visible to subclasses of the class they're declared in.
    - private : only visible in the class where it is defined.
    - Like other aspects of TypeScript's type system, private and protected are only enforced during type checking. This means that JavaScript runtime constructs like in or simple property lookup can still access a private or protected member.
  - The value of `this` inside a function depends on how the function was called.
  - Classes in TypeScript are compared structurally

-Modules

- Format to Modularizing code : ES Modules
- In TypeScript, just as in ECMAScript 2015, any file containing a top-level import or export is considered a module.
- A file without any top-level import/export declaration is treated as a script whose contents are available in the global scope, and yes even to the modules.
- Modules are executed wuthin their own scope, not in the global scope.
- This means that variables,functions, classes, etc. declared in a module are not visible outside the module unless they are explicitly exported using one of the export forms.
- To consume a variable, function, class,interface, etc. exported from a different module, it has to be imported using one of the import forms.
- The JavaScript specification declares that any JavaScript files without an export or top-level await should be considered a script and not a module.
- Examples:
  - `export var pi = 3.14;` // file Name : Modules.ts
  - `import { pi, helloWorld } from "./Modules"` // file Name : Module consumer
  - `import {pi as Pie} from "./Modules`
- You can take all of the exported objects and put them into a single namespace using _ as name : `import _ as module1 from "./Modules.ts"`;
- You can import a file and not include any variables into your current module via import "./file". In this case, the import does nothing. However, all of the code in maths.ts was evaluated, which could trigger side-effects which affect other objects.
- TypeScript has extended the import syntax with import type which is an import which can only import types. This syntax allows a non-TypeScript transpiler like Babel, swc or esbuild to know what imports can be safely removed.
- ES Module Syntax with CommonJS Behavior
  - TypeScript has ES Module syntax which directly correlates to a CommonJS and AMD require. Imports using ES Module are for most cases the same as the require from those environments, but this syntax ensures you have a 1 to 1 match in your TypeScript file with the CommonJS output
  - `import fs = require("fs"); const code = fs.readFileSync("hello.ts", "utf8");`
- CommonJS Syntax
  - CommonJS is the format which most modules on npm are delivered in. Even if you are writing using the ES Modules syntax above, having a brief understanding of how CommonJS syntax works will help you debug easier.
  - Identifiers are exported via setting the exports property on a global called module, for example
    ` module.exports = { pi: 3.14 };`
  - Then these files can be imported via a require statement.
    `const maths = require("maths");`
    Or you can simplify a bit using the destructuring feature in JavaScript.
    `const { pi } = require("maths");`
  - TypeScript includes two resolution strategies: Classic and Node
- All communication between modules happens via a module loader, the compiler flag module determines which one is used.

## Resources

- https://www.typescriptlang.org/docs/
- https://www.typescriptlang.org/assets/typescript-handbook.pdf

