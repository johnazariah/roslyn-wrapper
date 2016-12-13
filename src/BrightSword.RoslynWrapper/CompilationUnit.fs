namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>compilation unit</code>
/// </summary>
[<AutoOpen>]
module CompilationUnit =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory
    
    let private addMembers members (cu : CompilationUnitSyntax) =
        members
        |> (Seq.toArray >> SF.List)
        |> cu.WithMembers

    let ``compilation unit`` namespaces =
        SF.CompilationUnit()
        |> addMembers namespaces
        |> (fun cu -> cu.NormalizeWhitespace())
