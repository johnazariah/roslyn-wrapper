namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module CompilationUnitTests =
    [<Test>]
    let ``compilation-unit : empty``() =
        let input = ``compilation unit`` [ ]
        let actual = generateCodeToString input
        let expected = @""
        are_equal expected actual
                 
    [<Test>]
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

