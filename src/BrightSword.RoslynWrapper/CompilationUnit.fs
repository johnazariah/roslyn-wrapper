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

    /// This function creates a 'compilation unit' given a sequence of members. 
    let ``compilation unit`` members =
        SyntaxFactory.CompilationUnit()
        |> addMembers members
        |> (fun cu -> cu.NormalizeWhitespace())
