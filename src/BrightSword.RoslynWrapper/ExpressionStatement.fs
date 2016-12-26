namespace BrightSword.RoslynWrapper
[<AutoOpen>]
module Expressions = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory
    
    // target = source
    let (<--) target source =
         SF.AssignmentExpression (SyntaxKind.SimpleAssignmentExpression, target, source)

    // a.b
    let (<.>) a b = 
        SF.MemberAccessExpression (SyntaxKind.SimpleMemberAccessExpression, a, b)

    // (targetType) expression        
    let ``cast`` targetType expression = 
        SF.CastExpression (ident targetType, expression)

    // expression as targetType
    let ``as`` targetType expression = 
        SF.BinaryExpression (SyntaxKind.AsExpression, expression, ident targetType)

    // await expr
    let ``await`` =
        SF.AwaitExpression

    let ``() =>`` parameters expression = 
        expression 
        |> SF.ParenthesizedLambdaExpression 
        |> (fun ple -> ple.AddParameterListParameters (parameters |> Seq.map (SF.Identifier >> SF.Parameter) |> Seq.toArray))

    let ``_ =>`` parameterName expression = 
        SF.SimpleLambdaExpression (parameterName |> (SF.Identifier >> SF.Parameter), expression)

    // make a statement from an expression
    let statement s = 
        SF.ExpressionStatement s
        :> Syntax.StatementSyntax

[<AutoOpen>]
module Statements = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    // throw;
    // throw s;
    let ``throw``  eOpt = eOpt |> Option.fold(fun _ e -> SF.ThrowStatement e)  (SF.ThrowStatement ())

    // return;
    // return s;
    let ``return`` eOpt = eOpt |> Option.fold(fun _ e -> SF.ReturnStatement e) (SF.ReturnStatement ())
