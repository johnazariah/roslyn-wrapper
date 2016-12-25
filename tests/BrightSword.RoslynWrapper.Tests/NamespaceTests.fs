namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module NamespaceTests = 

    [<Test>]
    let ``namespace: empty``() =
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

    [<Test>]
    let ``namespace: with usings``() =
        let n = 
            ``namespace`` "Foo"
                ``{`` 
                    [ "System.Collections" ]
                    []
                ``}``
        let actual = to_namespace_code n
        let expected = @"namespace Foo
{
    using System;
    using System.Collections;
}"
        are_equal expected actual

    [<Test>]
    let ``namespace: with usings and classes``() =
        let c =
            ``class`` "C" ``<<`` [] ``>>``
                ``:`` None ``,`` []
                [``public``]
                ``{``
                    []
                ``}``
        let n = 
            ``namespace`` "Foo"
                ``{`` 
                    [ "System.Collections" ]
                    [ c ]
                ``}``
        let actual = to_namespace_code n
        let expected = @"namespace Foo
{
    using System;
    using System.Collections;

    public class C
    {
    }
}"
        are_equal expected actual



