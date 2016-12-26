namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module StatementTests = 
    let host_in_method ss = 
        ``method`` "void" "Host" ``<<`` [] ``>>`` ``(`` [] ``)``
            [``protected``; ``internal`` ]
            ``{``
                ss
            ``}``

    let return_from_arrow_method t s = 
        ``arrow_method`` t "Host" ``<<`` [] ``>>`` ``(`` [] ``)``
            [``protected``; ``internal`` ]
            (Some <| ``=>`` s)

    [<Test>]
    let ``statement: new()``() =
        let s = ``new`` [ "List<int>" ] ``(`` [ ] ``)``
        let m = return_from_arrow_method "List<int>" s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal List<int> Host() => new List<int>();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``statement: new() with args``() =
        let s = ``new`` ["System"; "String" ] ``(`` [ literalString "A" ] ``)``
        let m = return_from_arrow_method "String" s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal String Host() => new System.String(""A"");
    }
}"
        are_equal expected actual


