(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/BrightSword.RoslynWrapper"

(**
Roslyn Wrapper Tutorial
========================

## Introduction

[Roslyn](https://github.com/dotnet/roslyn) replaces CodeDom (amongst other things) as a way to emit code in C# (or VB).

Consider the following C# class, which we would like to generate:

    public class Some : Maybe<T>, IEquatable<Some>
    {
        public Some(T value)
        {
            Value = value;
        }

        private T Value
        {
            get;
        }

        public override bool Equals(object other) => this.Equals(other as Some<T>);

        // other functions removed for brevity
    }

The (fully-inlined) sequence of API calls to generate this class is pretty formidable.

Use the [Roslyn Quoter](https://roslynquoter.azurewebsites.net/) to see the call-tree (150 lines long)

    CompilationUnit()
    .WithMembers(
        SingletonList<MemberDeclarationSyntax>(
            ClassDeclaration("Some")
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithBaseList(
                BaseList(
                    SeparatedList<BaseTypeSyntax>(
                        new SyntaxNodeOrToken[]{
                            SimpleBaseType(
                                GenericName(
                                    Identifier("Maybe"))
                                .WithTypeArgumentList(
                                    TypeArgumentList(
                                        SingletonSeparatedList<TypeSyntax>(
                                            IdentifierName("T"))))),
                            Token(SyntaxKind.CommaToken),
                            SimpleBaseType(
                                GenericName(
                                    Identifier("IEquatable"))
                                .WithTypeArgumentList(
                                    TypeArgumentList(
                                        SingletonSeparatedList<TypeSyntax>(
                                            IdentifierName("Some")))))})))
            .WithMembers(
                List<MemberDeclarationSyntax>(
                    new MemberDeclarationSyntax[]{
                        ConstructorDeclaration(
                            Identifier("Some"))
                        .WithModifiers(
                            TokenList(
                                Token(SyntaxKind.PublicKeyword)))
                        .WithParameterList(
                            ParameterList(
                                SingletonSeparatedList<ParameterSyntax>(
                                    Parameter(
                                        Identifier("value"))
                                    .WithType(
                                        IdentifierName("T")))))
                        ...
                        ...
                        ...
                        .WithExpressionBody(
                            ArrowExpressionClause(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName("Equals")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SingletonSeparatedList<ArgumentSyntax>(
                                            Argument(
                                                BinaryExpression(
                                                    SyntaxKind.AsExpression,
                                                    IdentifierName("other"),
                                                    GenericName(
                                                        Identifier("Some"))
                                                    .WithTypeArgumentList(
                                                        TypeArgumentList(
                                                            SingletonSeparatedList<TypeSyntax>(
                                                                IdentifierName("T")))))))))))
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken))}))))
    .NormalizeWhitespace()

We would like to have a more tractable (if less powerful) way to define and compose our code definition elements. 

In this tutorial, we will use the functions in this library to define this class in a more visually appealing way.

*)
#r "BrightSword.RoslynWrapper.dll"
#r "Microsoft.CodeAnalysis"
#r "Microsoft.CodeAnalysis.CSharp"
open BrightSword.RoslynWrapper

(**

## Structure Definition

We can define a class using the ``class`` function

*)

let c = 
    ``class`` "Some" ``<<`` [ "T" ] ``>>``
        ``:`` (Some "Maybe<T>") ``,`` [ "IEquatable<Some<T>>" ]
        [``public``]
        ``{``
            [
                // ctor
                ``constructor`` "Some" ``(`` [ ("value", (``type`` "T")) ] ``)``
                    ``:`` []
                    [``public``]
                    ``{`` 
                        [
                            statement ((ident "Value") <-- (ident "value"))
                        ]
                    ``}``

                // private T Value { get; }
                ``propg`` "T" "Value"
                    [``public``]

                // public override bool Equals(object other) => this.Equals(other as Some<T>);
                ``arrow_method`` "bool" "Equals" ``<<`` [] ``>>`` ``(`` [ ("other", ``type`` "object")] ``)``
                    [``public``; ``override``]
                    (Some (``=>`` (``invoke`` (ident "this" <.> ident "Equals") ``(`` [ (ident "other") |~> "Some<T>"  ] ``)``)))
            ]
        ``}``

(**

## Code Generation Mechanics

### CompilationUnit
At the root of the code-generation object hierarchy is the `CompilationUnit` object. This object allows us to generate code for a collection of objects.

Our library contains the ``compilation unit`` function to create one of these.
*)

let cu = 
    ``compilation unit`` 
        [ c ]

(**

### Generation

Use the `generateCodeToString` function to generate code from a compilation unit.

_An empty compilation generates no code, but this is how you use it:_  
*)

(*** define-output: code ***)
generateCodeToString cu

(**
which evaluates to the following string value:
*)

(*** include-it: code ***)
