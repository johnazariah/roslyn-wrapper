namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module ArrayCreationTests =

    [<Test>]
    let ``array: new empty array``() =
        let s = ``var`` "a" (``:=`` (``new-array`` "int" [ ]))
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

        
    [<Test>]
    let ``array: new initialized array with ids``() =
        
        let elems = 
            [
                ``ident`` "a"
                ``ident`` "b"
                ``ident`` "c"
            ]
        let s = ``var`` "a" (``:=`` (``new-array`` "Test" elems))
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

        
    [<Test>]
    let ``array: new initialized array with constants``() =
        
        let elems = 
            [
                ``literal`` 1
                ``literal`` 2
                ``literal`` 3
            ]
        let s = ``array`` "int" "a" (Some (``:=`` (``new-array`` "int" elems)))
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            int[] a = new int[] { 1, 2, 3 };
        }
    }
}"
        are_equal expected actual