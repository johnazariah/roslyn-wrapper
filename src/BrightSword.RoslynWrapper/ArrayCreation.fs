namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>new array instantiation</code>
/// </summary>
[<AutoOpen>]
module ArrayCreation = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax    

    let private setRank (at:ArrayTypeSyntax) =
        [
            [ (SyntaxFactory.OmittedArraySizeExpression()) :> ExpressionSyntax ]
            |> SyntaxFactory.SeparatedList
            |> SyntaxFactory.ArrayRankSpecifier
        ]
        |> SyntaxFactory.List
        |> at.WithRankSpecifiers                
        
    let ``new-array`` arrayType arrayElements = 
        let array =
            arrayType
            |> ident
            |> SyntaxFactory.ArrayType
            |> setRank    
        let elems =
            arrayElements 
            |> List.map (fun x -> x :> ExpressionSyntax)
            |> SyntaxFactory.SeparatedList
            |> (SyntaxFactory.InitializerExpression SyntaxKind.ArrayInitializerExpression).WithExpressions
        SyntaxFactory.ArrayCreationExpression (array,elems)
        
