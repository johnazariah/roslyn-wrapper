namespace BrightSword.RoslynWrapper.Tests

open Xunit

open BrightSword.RoslynWrapper

module CompilationUnitTests =
    [<Fact>]
    let ``compilation-unit : empty``() =
        let input = ``compilation unit`` [ ]
        let actual = generateCodeToString input
        let expected = @""
        are_equal expected actual
                 
    [<Fact>]
    let ``compilation-unit : single namespace``() =
        let n = 
            ``namespace`` "Foo"
                ``{`` 
                    []
                    []
                ``}``
        let actual = to_namespace_code n
        let expected = @"namespace Foo
{
    using System;
}"
        are_equal expected actual

