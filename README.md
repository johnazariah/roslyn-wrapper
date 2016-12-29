# roslyn-wrapper

## Code Generation
[Roslyn](https://github.com/dotnet/roslyn) replaces CodeDom (amongst other things) as a way to emit code in C# (or VB).

There is a rich library which somewhat resembles CodeDom in its approach to building and generating C# code, but the C# syntax used to call these libraries tends to result in deeply nested function structures.

This 'Roslyn-Wrapper' library simplifies the usage of the code-generation libraries by wrapping these APIs with more visually appealing, composable F# functions.

## Build Status

Mono | .NET
---- | ----
[![Mono CI Build Status](https://img.shields.io/travis/johnazariah/roslyn-wrapper/master.svg)](https://travis-ci.org/johnazariah/roslyn-wrapper) | [![.NET Build Status](https://img.shields.io/appveyor/ci/johnazariah/roslyn-wrapper/master.svg)](https://ci.appveyor.com/project/johnazariah/roslyn-wrapper)

## Maintainer(s)

- [@johnazariah](https://github.com/johnazariah)
