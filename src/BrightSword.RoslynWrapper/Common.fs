namespace BrightSword.RoslynWrapper

[<AutoOpen>]
module Common =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax

    let (?+) option list =
        option |> Option.fold (fun l o -> o :: l) list

    let mapTuple2 f (t1, t2)= (f t1, f t2)

    let ident s = (SyntaxFactory.Identifier >> SyntaxFactory.IdentifierName) s

    let ``:=`` expression = expression |> SyntaxFactory.EqualsValueClause
    let ``=>`` expression = expression |> SyntaxFactory.ArrowExpressionClause

    let ``param`` paramName ``of`` paramType =
        let p =
            paramName
            |> (SyntaxFactory.Identifier >> SyntaxFactory.Parameter)
        p.WithType paramType
