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
        |> Seq.map (ident >> (fun t -> t :> TypeSyntax))
        |> (SF.SeparatedList >> SF.TypeArgumentList)
        |> gn.WithTypeArgumentList
 
    let private simpleType parts =
        let rec toQualifiedNameImpl = function
            | [] -> failwith "cannot get qualified name of empty list"
            | [ n ] -> n |> ident :> NameSyntax
            | [ p1 ; p2 ] -> (p2, p1) |> mapTuple2 ident |> SF.QualifiedName :> NameSyntax
            | p1 :: rest -> (toQualifiedNameImpl rest, p1 |> ident) |> SF.QualifiedName :> NameSyntax
        in parts |> List.rev |> toQualifiedNameImpl

    type SimpleType = SimpleType with
        static member ($) (_ : SimpleType, x : string) = simpleType [x]
        static member ($) (_ : SimpleType, xs : string list) = simpleType xs

    let inline ``type`` s = SimpleType $ s 

    let ``generic type`` typeName ``<<`` typeArguments ``>>`` =
        typeName
        |> (SF.Identifier >> SF.GenericName)
        |> setTypeArgumentList typeArguments

    let ``type name`` (typeSyntax : NameSyntax) = typeSyntax.ToFullString ()