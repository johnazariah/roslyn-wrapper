namespace BrightSword.RoslynWrapper

/// <summary>
/// Use this module to specify the syntax for a <code>generic type</code> name
/// </summary>
[<AutoOpen>]
module GenericName = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    let private setTypeArgumentList typeArguments (gn : GenericNameSyntax) =
        typeArguments
        |> Seq.map (toIdentifierName >> (fun t -> t :> TypeSyntax))
        |> (SF.SeparatedList >> SF.TypeArgumentList)
        |> gn.WithTypeArgumentList
 
    let ``generic type`` typeName ``<<`` typeArguments ``>>`` =
        typeName
        |> (SF.Identifier >> SF.GenericName)
        |> setTypeArgumentList typeArguments