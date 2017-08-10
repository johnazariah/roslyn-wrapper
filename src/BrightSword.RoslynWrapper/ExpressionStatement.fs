namespace BrightSword.RoslynWrapper

[<AutoOpen>]
module Expressions = 
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    // target = source
    let (<--) target source =
         SyntaxFactory.AssignmentExpression (SyntaxKind.SimpleAssignmentExpression, target, source)

    // (targetType) expression        
    let ``cast`` targetType expression = 
        SyntaxFactory.CastExpression (ident targetType, expression) :> ExpressionSyntax

    // expression as targetType
    let ``as`` targetType expression = 
        SyntaxFactory.BinaryExpression (SyntaxKind.AsExpression, expression, ident targetType)

    /// alias for the ``as`` function
    let (|~>) expression targetType = ``as`` targetType expression

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

    // left ?? right
    let (<??>) left right =
        SyntaxFactory.BinaryExpression (SyntaxKind.CoalesceExpression, left, right)
        :> ExpressionSyntax
        
    // left.right
    let (<|.|>) left right = 
        SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, left, right)
        :> ExpressionSyntax

    // left.right(args)
    let (<.>) left (right, args) = 
        ``invoke`` (left <|.|> right) ``(`` args ``)``

    // left?.right
    let (<|?.|>) left right = 
        let member_binding_expr = SyntaxFactory.MemberBindingExpression right
        SyntaxFactory.ConditionalAccessExpression (left, member_binding_expr)
        :> ExpressionSyntax

    // left?.right(args)
    let (<?.>) left (right, args) =
        let member_binding_expr = 
            SyntaxFactory.MemberBindingExpression right :> ExpressionSyntax

        let target = ``invoke`` member_binding_expr ``(`` args ``)``
        SyntaxFactory.ConditionalAccessExpression (left, target)
        :> ExpressionSyntax
        
    // left + right
    let (<+>) left right =
        (SyntaxKind.AddExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left - right
    let (<->) left right =
        (SyntaxKind.SubtractExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left * right
    let (<*>) left right =
        (SyntaxKind.MultiplyExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left / right
    let (</>) left right =
        (SyntaxKind.DivideExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax
        
    // left % right
    let (<%>) left right =
        (SyntaxKind.ModuloExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left == right
    let (<==>) left right =
        (SyntaxKind.EqualsExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left != right
    let (<!=>) left right =
        (SyntaxKind.NotEqualsExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left ^ right
    let (<^>) left right =
        (SyntaxKind.ExclusiveOrExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left && right
    let (<&&>) left right =
        (SyntaxKind.LogicalAndExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // left || right
    let (<||>) left right =
        (SyntaxKind.LogicalOrExpression, left, right) |> SyntaxFactory.BinaryExpression
        :> ExpressionSyntax

    // ! (expr)
    let (!) expr =
        (SyntaxKind.LogicalNotExpression, SyntaxFactory.ParenthesizedExpression expr) |> SyntaxFactory.PrefixUnaryExpression
        :> ExpressionSyntax

    // expr is target
    let ``is`` targetType expression = 
        SyntaxFactory.BinaryExpression (SyntaxKind.IsExpression, expression, ident targetType)
        :> ExpressionSyntax

    // ( expr )
    let ``))`` = None
    let ``((`` expr ``))`` = 
        SyntaxFactory.ParenthesizedExpression expr
        :> ExpressionSyntax

    // single line comment(s)
    let ``//`` comments node =
        comments
        |> List.map  (fun l -> "// " + l)
        |> List.map  SyntaxFactory.Comment
        |> List.map  (fun c -> [c; (SyntaxFactory.EndOfLine "")])
        |> List.fold (fun all syntax -> all @ syntax) []
        |> node.WithLeadingTrivia
        
    let private setArrayArguments (methodArguments : ArgumentSyntax seq) (ie : ElementAccessExpressionSyntax) =
        methodArguments
        |> (SyntaxFactory.SeparatedList >> SyntaxFactory.BracketedArgumentList)
        |> ie.WithArgumentList
        
    // name[args]
    let ``item`` name args =     
        name
        |> (``ident`` >> SyntaxFactory.ElementAccessExpression)
        |> setArrayArguments (args |> Seq.map SyntaxFactory.Argument)
        :> ExpressionSyntax

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
