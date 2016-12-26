namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for <code>explicit</code> and <code>implicit</code> conversion operators
/// </summary>
[<AutoOpen>]
module Conversion =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    let private setParameterList parameters (co : ConversionOperatorDeclarationSyntax) =
        parameters  
        |> Seq.map (fun (paramName, paramType) -> ``param`` paramName ``of`` paramType)
        |> (SF.SeparatedList >> SF.ParameterList)
        |> co.WithParameterList
    
    let private setModifiers modifiers (co : ConversionOperatorDeclarationSyntax) =
        modifiers
        |> Set.ofSeq
        |> (fun s -> s.Add ``static``) 
        |> Seq.map SF.Token
        |> SF.TokenList 
        |> co.WithModifiers
    
    let private setExpressionBody body (co : ConversionOperatorDeclarationSyntax) =
        co.WithExpressionBody body
        
    let private addClosingSemicolon (co : ConversionOperatorDeclarationSyntax) =
        SyntaxKind.SemicolonToken |> SF.Token 
        |> co.WithSemicolonToken

    let ``explicit operator`` target ``(`` source ``)`` initializer =
        (SyntaxKind.ExplicitKeyword |> SF.Token, target |> ident)
        |> SF.ConversionOperatorDeclaration
        |> setParameterList [ ("value", source) ]
        |> setModifiers [``public``; ``static``]
        |> setExpressionBody initializer
        |> addClosingSemicolon

    let ``implicit operator`` target ``(`` source ``)`` modifiers initializer =
        (SyntaxKind.ImplicitKeyword |> SF.Token, target |> ident)
        |> SF.ConversionOperatorDeclaration
        |> setParameterList [ ("value", source) ]
        |> setModifiers modifiers
        |> setExpressionBody initializer
        |> addClosingSemicolon

