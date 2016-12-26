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
    
    let private setModifiers modifiers (co : ConversionOperatorDeclarationSyntax) =
        modifiers
        |> Set.ofSeq
        |> (fun s -> s.Add ``static``) 
        |> Seq.map SF.Token
        |> SF.TokenList 
        |> co.WithModifiers
        
    let private addClosingSemicolon (co : ConversionOperatorDeclarationSyntax) =
        SyntaxKind.SemicolonToken |> SF.Token 
        |> co.WithSemicolonToken

    let ``explicit operator`` target ``(`` source ``)`` initializer =
        (SyntaxKind.ExplicitKeyword |> SF.Token, target |> toIdentifierName)
        |> SF.ConversionOperatorDeclaration
        |> (fun co -> co.WithParameterList <| toParameterList [ ("value", source) ])
        |> setModifiers [``public``; ``static``]
        |> (fun co -> co.WithExpressionBody initializer)
        |> addClosingSemicolon

    let ``implicit operator`` target ``(`` source ``)`` modifiers initializer =
        (SyntaxKind.ImplicitKeyword |> SF.Token, target |> toIdentifierName)
        |> SF.ConversionOperatorDeclaration
        |> (fun co -> co.WithParameterList <| toParameterList [ ("value", source) ])
        |> setModifiers modifiers
        |> (fun co -> co.WithExpressionBody initializer)
        |> addClosingSemicolon

