namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>namespace</code>
/// </summary>
[<AutoOpen>]
module NamespaceDeclaration =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    let private setUsings usings (nd : NamespaceDeclarationSyntax) =
        usings @ ["System"]
        |> Set.ofList |> Set.toSeq
        |> Seq.map (ident >> SyntaxFactory.UsingDirective) 
        |> SyntaxFactory.List
        |> nd.WithUsings

    let private setMembers members (nd : NamespaceDeclarationSyntax) =
        members 
        |> (Seq.toArray >> SyntaxFactory.List)
        |> nd.WithMembers

    /// <summary>
    /// This function creates a 'namespace' with the given name and contents
    /// </summary>
    /// <param name="namespaceName">A string representing the name of the namespace to be created</param>
    /// <param name="usings">A sequence of strings representing the namespaces to include within this namespace</param>
    /// <param name="members">A sequence of `MemberDeclaration`s the members of this namespace. Typically `class` and `interface`.</param>
    let ``namespace`` namespaceName 
            ``{`` 
                usings
                members
            ``}`` =
        namespaceName 
        |> (ident >> SyntaxFactory.NamespaceDeclaration)
        |> setUsings usings
        |> setMembers members

