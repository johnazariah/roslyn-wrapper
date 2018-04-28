namespace BrightSword.RoslynWrapper.Tests

open Xunit

open BrightSword.RoslynWrapper

module ArrayCreationTests =
    open Microsoft.CodeAnalysis.CSharp.Syntax

    [<Fact>]
    let ``array: new empty array``() =
        let s = ``var`` "a" (``:=`` (``new array`` "int" [ ]))
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            var a = new int[] { };
        }
    }
}"
        are_equal expected actual

        
    [<Fact>]
    let ``array: new initialized array with ids``() =
        
        let elems = 
            [
                ``ident`` "a"
                ``ident`` "b"
                ``ident`` "c"
            ]
        let s = ``var`` "a" (``:=`` (``new array`` "Test" elems))
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            var a = new Test[] { a, b, c };
        }
    }
}"
        are_equal expected actual

        
    [<Fact>]
    let ``array: new initialized array with constants``() =
        
        let elems = 
            [
                ``literal`` 1
                ``literal`` 2
                ``literal`` 3
            ]
        let s = ``var`` "a" (``:=`` (``new array`` "int" elems))
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            var a = new int[] { 1, 2, 3 };
        }
    }
}"
        are_equal expected actual

        
        
    [<Fact>]
    let ``array: new array as argument``() =
        
        let elems = 
            [
                ``literal`` 1
                ``literal`` 2
                ``literal`` 3
            ]
        let arg1 = (``new array`` "int" elems) :> ExpressionSyntax
        let arg2 = (``ident`` "p2") :> ExpressionSyntax
        let s =  statement (``invoke`` (``ident`` "Apply") ``(`` [arg1;arg2] ``)``)
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            Apply(new int[] { 1, 2, 3 }, p2);
        }
    }
}"
        are_equal expected actual