namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>class or interface property</code>
/// </summary>
[<AutoOpen>]
module PropertyDeclaration =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax

    let private setModifiers modifiers (pd : PropertyDeclarationSyntax) =
        modifiers
        |> Seq.map SyntaxFactory.Token
        |> SyntaxFactory.TokenList
        |> pd.WithModifiers

    let private setGetAccessor (pd : PropertyDeclarationSyntax) =
        SyntaxKind.GetAccessorDeclaration
        |> SyntaxFactory.AccessorDeclaration
        |> (fun ad -> ad.WithSemicolonToken(SyntaxKind.SemicolonToken |> SyntaxFactory.Token))
        |> (fun ad -> pd.AddAccessorListAccessors ad)

    let private setSetAccessor (pd : PropertyDeclarationSyntax) =
        SyntaxKind.SetAccessorDeclaration
        |> SyntaxFactory.AccessorDeclaration
        |> (fun ad -> ad.WithSemicolonToken(SyntaxKind.SemicolonToken |> SyntaxFactory.Token))
        |> (fun ad -> pd.AddAccessorListAccessors ad)

    let ``prop`` propertyType propertyName modifiers =
        (propertyType |> ident, propertyName |> SyntaxFactory.Identifier)
        |> SyntaxFactory.PropertyDeclaration
        |> setModifiers modifiers
        |> setGetAccessor
        |> setSetAccessor

    let ``propg`` propertyType propertyName modifiers =
        (propertyType |> ident, propertyName |> SyntaxFactory.Identifier)
        |> SyntaxFactory.PropertyDeclaration
        |> setModifiers modifiers
        |> setGetAccessor
