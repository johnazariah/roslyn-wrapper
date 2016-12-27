namespace BrightSword.RoslynWrapper
[<AutoOpen>]
module Expressions = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    // target = source
    let (<--) target source =
         SyntaxFactory.AssignmentExpression (SyntaxKind.SimpleAssignmentExpression, target, source)

    // a.b
    let (<.>) a b = 
        SyntaxFactory.MemberAccessExpression (SyntaxKind.SimpleMemberAccessExpression, a, b)

    // (targetType) expression        
    let ``cast`` targetType expression = 
        SyntaxFactory.CastExpression (ident targetType, expression)

    // expression as targetType
    let ``as`` targetType expression = 
        SyntaxFactory.BinaryExpression (SyntaxKind.AsExpression, expression, ident targetType)

    // await expr
    let ``await`` =
        SyntaxFactory.AwaitExpression

    let ``() =>`` parameters expression = 
        expression 
        |> SyntaxFactory.ParenthesizedLambdaExpression 
        |> (fun ple -> ple.AddParameterListParameters (parameters |> Seq.map (SyntaxFactory.Identifier >> SyntaxFactory.Parameter) |> Seq.toArray))

    let ``_ =>`` parameterName expression = 
        SyntaxFactory.SimpleLambdaExpression (parameterName |> (SyntaxFactory.Identifier >> SyntaxFactory.Parameter), expression)

    // make a statement from an expression
    let statement s = 
        SyntaxFactory.ExpressionStatement s
        :> Syntax.StatementSyntax

[<AutoOpen>]
module Statements = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SyntaxFactory = SyntaxFactory

    // throw;
    // throw s;
    let ``throw``  eOpt = eOpt |> Option.fold(fun _ e -> SyntaxFactory.ThrowStatement e)  (SyntaxFactory.ThrowStatement ())

    // return;
    // return s;
    let ``return`` eOpt = eOpt |> Option.fold(fun _ e -> SyntaxFactory.ReturnStatement e) (SyntaxFactory.ReturnStatement ())
