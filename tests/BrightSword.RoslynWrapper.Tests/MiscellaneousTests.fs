namespace BrightSword.RoslynWrapper.Tests

open Xunit

open BrightSword.RoslynWrapper

module MiscellaneousTests =
    [<Fact>]
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

    [<Fact>]
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

    [<Fact>]
    let ``type name with 0 parts``() =
        match Record.Exception(fun () -> ``type`` []|> ignore) with
        | exn when exn = null -> Assert.False (true, "exception not thrown as expected")
        | exn -> Assert.Equal ("cannot get qualified name of empty list", exn.Message)

    [<Fact>]
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

