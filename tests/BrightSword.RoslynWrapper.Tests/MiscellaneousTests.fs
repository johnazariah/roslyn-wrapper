namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module MiscellaneousTests =
    [<Test>]
    let ``type name with 1 part``() =
        let t = ``type`` "string"
        let s = ``new`` t ``(`` [ ] ``)``
        let m = return_from_arrow_method (``type name`` t) s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal string Host() => new string();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``type name with 2 parts``() =
        let t = ``type`` ["System"; "String"]
        let s = ``new`` t ``(`` [ ] ``)``
        let m = return_from_arrow_method (``type name`` t) s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal System.String Host() => new System.String();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``type name with 0 parts``() =
        let f = fun () -> ``type`` []|> ignore
        let exn = Assert.Throws<System.Exception>(TestDelegate f)
        Assert.AreEqual ("cannot get qualified name of empty list", exn.Message)


    [<Test>]
    let ``type name with >2 parts``() =
        let t = ``type`` ["System"; "Collections"; "Generic"; "List<T>"]
        let s = ``new`` t ``(`` [ ] ``)``
        let m = return_from_arrow_method (``type name`` t) s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal System.Collections.Generic.List<T> Host() => new System.Collections.Generic.List<T>();
    }
}"
        are_equal expected actual

