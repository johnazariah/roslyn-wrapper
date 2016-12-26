namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>class or interface property</code>
/// </summary>
[<AutoOpen>]
module PropertyDeclaration =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax

    type SF = SyntaxFactory
        
    let private setModifiers modifiers (pd : PropertyDeclarationSyntax) =
        modifiers 
        |> Seq.map SF.Token
        |> SF.TokenList 
        |> pd.WithModifiers
        
    let private setGetAccessor (pd : PropertyDeclarationSyntax) =
        SyntaxKind.GetAccessorDeclaration 
        |> SF.AccessorDeclaration 
        |> (fun ad -> ad.WithSemicolonToken(SyntaxKind.SemicolonToken |> SF.Token))
        |> (fun ad -> pd.AddAccessorListAccessors ad)

    let private setSetAccessor (pd : PropertyDeclarationSyntax) =
        SyntaxKind.SetAccessorDeclaration 
        |> SF.AccessorDeclaration 
        |> (fun ad -> ad.WithSemicolonToken(SyntaxKind.SemicolonToken |> SF.Token))
        |> (fun ad -> pd.AddAccessorListAccessors ad)

    let ``prop`` propertyType propertyName modifiers =
        (propertyType |> toIdentifierName, propertyName |> SF.Identifier)
        |> SF.PropertyDeclaration
        |> setModifiers modifiers
        |> setGetAccessor
        |> setSetAccessor

    let ``propg`` propertyType propertyName modifiers =
        (propertyType |> toIdentifierName, propertyName |> SF.Identifier)
        |> SF.PropertyDeclaration
        |> setModifiers modifiers
        |> setGetAccessor
    

