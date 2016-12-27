namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>class or interface method</code>
/// </summary>
[<AutoOpen>]
module MethodDeclaration =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax

    type SF = SyntaxFactory

    let private setModifiers modifiers (md : MethodDeclarationSyntax) =
        modifiers 
        |> Seq.map SF.Token
        |> SF.TokenList 
        |> md.WithModifiers 

    let private setParameterList parameters (md : MethodDeclarationSyntax) =
        parameters 
        |> Seq.map (fun (paramName, paramType) -> ``param`` paramName ``of`` paramType)
        |> (SF.SeparatedList >> SF.ParameterList)
        |> md.WithParameterList
    
    let private setExpressionBody methodBody (md : MethodDeclarationSyntax) =
        methodBody 
        |> Option.fold (fun (_md : MethodDeclarationSyntax) _mb -> _md.WithExpressionBody _mb) md

    let private setBodyBlock bodyBlockStatements (md : MethodDeclarationSyntax) =
        bodyBlockStatements
        |> (Seq.toArray >> SF.Block)
        |> md.WithBody

    let private setTypeParameters typeParameters (md : MethodDeclarationSyntax) =
        if typeParameters |> Seq.isEmpty then md
        else
        typeParameters 
        |> Seq.map (SF.Identifier >> SF.TypeParameter)
        |> (SF.SeparatedList >> SF.TypeParameterList)
        |> md.WithTypeParameterList

    let private addClosingSemicolon (md : MethodDeclarationSyntax) =
        SyntaxKind.SemicolonToken |> SF.Token 
        |> md.WithSemicolonToken

    let ``arrow_method`` methodType methodName ``<<`` methodTypeParameters ``>>``
            ``(`` methodParams ``)``
            modifiers
            methodBodyExpression =
        (methodType |> ident,  methodName |> SF.Identifier) |> SF.MethodDeclaration
        |> setTypeParameters methodTypeParameters
        |> setModifiers modifiers
        |> setParameterList methodParams
        |> setExpressionBody methodBodyExpression
        |> addClosingSemicolon

    let ``method`` methodType methodName ``<<`` methodTypeParameters ``>>``
            ``(`` methodParams ``)``
            modifiers
            ``{`` 
                bodyBlockStatements
            ``}`` =
        (methodType |> ident,  methodName |> SF.Identifier) |> SF.MethodDeclaration
        |> setTypeParameters methodTypeParameters
        |> setModifiers modifiers
        |> setParameterList methodParams
        |> setBodyBlock bodyBlockStatements
