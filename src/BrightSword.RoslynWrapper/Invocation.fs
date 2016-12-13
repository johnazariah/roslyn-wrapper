namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>method invocations</code>
/// </summary>
[<AutoOpen>]
module Invocation =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory
    
    let private setArguments (methodArguments : ArgumentSyntax seq) (ie : InvocationExpressionSyntax) =
        methodArguments
        |> (SF.SeparatedList >> SF.ArgumentList)
        |> ie.WithArgumentList
        
    let ``invoke`` m ``(`` methodArguments ``)`` =
        m
        |> SF.InvocationExpression
        |> setArguments methodArguments
