namespace BrightSword.RoslynWrapper
[<AutoOpen>]
module Literals =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory

    let ``true`` = SF.LiteralExpression SyntaxKind.TrueLiteralExpression
    let ``false`` = SF.LiteralExpression SyntaxKind.FalseLiteralExpression
    let ``null``  = SF.LiteralExpression SyntaxKind.NullLiteralExpression

    // http://nut-cracker.azurewebsites.net/blog/2011/11/15/typeclasses-for-fsharp/
    type Literal = Literal with
        static member ($) (_ : Literal, x : string) = (SyntaxKind.StringLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : char)   = (SyntaxKind.CharacterLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : decimal) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : float) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : int) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : int64) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : float32) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : uint32) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
        static member ($) (_ : Literal, x : uint64) = (SyntaxKind.NumericLiteralExpression, (SF.Literal x)) |> SF.LiteralExpression 
    
    let inline literal s = Literal $ s 
