# Tactical refactoring

Taken from the [Large Scale Refactoring Mural](https://app.mural.co/t/sqlclone3118/m/sqlclone3118/1607695975998/3457612ff8b529d112da9eeaa13171e67d0639a9), we have a collection of post-its that have been grouped under the heading of "tactical refactorings". However, they're all short descriptions of a solution to a currently undefined problem and need to be fleshed out before we can decide how (or if) to build course content around them.

## Reduce stringly typing

- What?
  - Consider where different concepts are represented using basic primitive types with a high probability of confusion (e.g. where everything is represented as a string), and modify the representation to use types that more closely match their associated.
- Why?
  - This takes advantage of typesafety to eliminate confusion and avoid common mistakes. For example, consider a method that posts the contents of a file to an http endpoint. Instead of `void UploadFile(string filePath, string url, IDictionary<string, string> headers)` you could use `void UploadFile(PathInfo filePath, Uri url, HttpRequestHeaders headers)` to provide a whole host of benefits (the file must exist, the url must pass basic validation, the headers type provides assistance in creating a valid set of headers).
- Risks?
  - Effort is required ensure the code is still easy to use.
  - Care needs to be taken to avoid a proliferation of incompatible DTO types, with lots of painful conversion between similar though distinct types. Careful thought about shareable domain objects is required.

## Extract projects

- What?
- Why?
- Risks?

## Make a class immutable

- What?
- Why?
- Risks?

## Introduce Factory / Factory methods

- What?
- Why?
- Risks?

## Encapsulating external dependencies
- e.g. Replacing wide ranging references to Serilog/Log4Net with our internal interfaces.

## Introduce Builder

- What?
- Why?
- Risks?

## Combine projects

- What?
- Why?
- Risks?

## Extract class

- What?
- Why?
- Risks?

## Extract Interface

- What?
- Why?
- Risks?

## Kill the Singleton

- What?
- Why?
- Risks?

## Break down "big bucket of gubbins" - projects with too many classes

- What?
- Why?
- Risks?

## Strategy pattern

- What?
- Why?
- Risks?

## Template pattern

- What?
- Why?
- Risks?

## Immutability as a strategy

- What?
- Why?
- Risks?

## Separate API from delivery

- What?
- Why?
- Risks?

## Multiple-specific case constructors

- What?
- Why?
- Risks?

## Organise

- What?
- Why?
- Risks?

## Extract API Module (Client library) Interfaces and DTOs

- What?
- Why?
- Risks?

## Unify cut and pastes

- What?
- Why?
- Risks?

## Breakout Method Object

- What?
- Why?
- Risks?
