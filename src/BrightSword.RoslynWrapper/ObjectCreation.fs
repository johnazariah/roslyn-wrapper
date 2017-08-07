namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>object instantiation</code>
/// </summary>
[<AutoOpen>]
module ObjectCreation = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    let private setArguments arguments (oce : ObjectCreationExpressionSyntax) = 
        arguments 
        |> Seq.map (SyntaxFactory.Argument) 
        |> (SyntaxFactory.SeparatedList >> SyntaxFactory.ArgumentList)
        |> oce.WithArgumentList
            
    let ``new`` genericName ``(`` arguments ``)`` = 
        genericName
        |> SyntaxFactory.ObjectCreationExpression
        |> setArguments arguments

        
    let ``new array`` arrayType arrayElements = 
        let array = arrayType |> ``array type`` :?> ArrayTypeSyntax
        let elems =
            arrayElements 
            |> List.map (fun x -> x :> ExpressionSyntax)
            |> SyntaxFactory.SeparatedList
            |> (SyntaxFactory.InitializerExpression SyntaxKind.ArrayInitializerExpression).WithExpressions
        (array,elems)
        |>SyntaxFactory.ArrayCreationExpression 
