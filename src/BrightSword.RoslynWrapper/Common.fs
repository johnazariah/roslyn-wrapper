namespace BrightSword.RoslynWrapper

[<AutoOpen>]
module Common =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    let (?+) option list =
        option |> Option.fold (fun l o -> o :: l) list

    let mapTuple2 f (t1, t2)= (f t1, f t2)

    let ident s = (SF.Identifier >> SF.IdentifierName) s
       
    let ``:=`` expression = expression |> SF.EqualsValueClause 
    let ``=>`` expression = expression |> SF.ArrowExpressionClause    

    let ``param`` paramName ``of`` paramType =
        let p = 
            paramName 
            |> (SF.Identifier >> SF.Parameter)
        p.WithType paramType
