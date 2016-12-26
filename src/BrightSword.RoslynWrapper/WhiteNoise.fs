namespace BrightSword.RoslynWrapper

[<AutoOpen>]
module WhiteNoise =
    open Microsoft.CodeAnalysis
    open Microsoft.CodeAnalysis.CSharp
    open Microsoft.CodeAnalysis.CSharp.Syntax
    
    type SF = SyntaxFactory
    
    let ``:`` = None
    let ``,`` = None
    let ``}`` = None
    let ``{`` = None
    let ``<<`` = None
    let ``>>`` = None
    let ``(`` = None
    let ``)`` = None

    let ``of`` = None

    let ``private`` = SyntaxKind.PrivateKeyword
    let ``protected`` = SyntaxKind.ProtectedKeyword
    let ``internal`` = SyntaxKind.InternalKeyword
    let ``public`` = SyntaxKind.PublicKeyword
    let ``partial`` = SyntaxKind.PartialKeyword
    let ``abstract`` = SyntaxKind.AbstractKeyword    
    let ``async`` = SyntaxKind.AsyncKeyword
    let ``virtual`` = SyntaxKind.VirtualKeyword
    let ``override`` = SyntaxKind.OverrideKeyword
    let ``static`` = SyntaxKind.StaticKeyword
    let ``readonly`` = SyntaxKind.ReadOnlyKeyword
    