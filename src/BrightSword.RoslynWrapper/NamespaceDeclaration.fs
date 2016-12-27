﻿namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>namespace</code>
/// </summary>
[<AutoOpen>]
module NamespaceDeclaration =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory
    
    let private setUsings usings (nd : NamespaceDeclarationSyntax) =
        usings @ ["System"]
        |> Set.ofList |> Set.toSeq
        |> Seq.map (ident >> SF.UsingDirective) 
        |> SF.List
        |> nd.WithUsings

    let private setMembers members (nd : NamespaceDeclarationSyntax) =
        members 
        |> (Seq.toArray >> SF.List)
        |> nd.WithMembers

    let ``namespace`` namespaceName 
            ``{`` 
                usings
                members
            ``}`` =
        namespaceName 
        |> (ident >> SF.NamespaceDeclaration)
        |> setUsings usings
        |> setMembers members

