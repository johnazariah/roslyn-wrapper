namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>compilation unit</code>
/// </summary>
[<AutoOpen>]
module CompilationUnit =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    let private addMembers members (cu : CompilationUnitSyntax) =
        members
        |> (Seq.toArray >> SyntaxFactory.List)
        |> cu.WithMembers

    ///<summary>
    /// This function creates a 'compilation unit' given a sequence of 'namespace' objects
    ///</summary>
    let ``compilation unit`` namespaces =
        SyntaxFactory.CompilationUnit()
        |> addMembers namespaces
        |> (fun cu -> cu.NormalizeWhitespace())
