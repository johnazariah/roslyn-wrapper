namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>object instantiation</code>
/// </summary>
[<AutoOpen>]
module ObjectCreation = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    let private setArguments arguments (oce : ObjectCreationExpressionSyntax) = 
        arguments 
        |> Seq.map (SF.Argument) 
        |> (SF.SeparatedList >> SF.ArgumentList)
        |> oce.WithArgumentList
            
    let ``new`` nameparts ``(`` arguments ``)`` = 
        nameparts
        |> toQualifiedName
        |> SF.ObjectCreationExpression
        |> setArguments arguments

