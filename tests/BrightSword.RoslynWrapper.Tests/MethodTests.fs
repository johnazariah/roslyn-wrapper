namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module MethodTests =
    [<Test>]
    let ``arrow method: class abstract definition``() =
        let m = 
            ``arrow_method`` "void" "M" ``<<`` [] ``>>`` ``(`` [] ``)`` 
                [``public``; ``abstract``]
                None

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public abstract void M();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: interface method declaration``() =
        let m = 
            ``arrow_method`` "void" "M" ``<<`` [] ``>>`` ``(`` [] ``)`` 
                []
                None

        let actual = to_interface_members_code [m]
        let expected = @"namespace N
{
    using System;

    public interface I
    {
        void M();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: generic``() =
        let m = 
            ``arrow_method`` "void" "M" ``<<`` ["T"] ``>>`` ``(`` [] ``)`` 
                [``public``; ``abstract``]
                None

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public abstract void M<T>();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: with parameter``() =
        let m = 
            ``arrow_method`` "void" "M" ``<<`` ["T"] ``>>`` ``(`` [ ("thing", (``type`` "object")) ] ``)`` 
                [``public``; ``abstract``]
                None

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public abstract void M<T>(object thing);
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: with expression``() =
        let e = ``=>`` ("thing" |> ident)
        let m = 
            ``arrow_method`` "object" "M" ``<<`` ["T"] ``>>`` ``(`` [ ("thing", (``type`` "object")) ] ``)`` 
                [``public``; ``virtual``]
                (Some e)

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public virtual object M<T>(object thing) => thing;
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: with array parameter``() =
        let m = 
            ``arrow_method`` "void" "M" ``<<`` ["T"] ``>>`` ``(`` [ ("thing", (``array type`` "object")) ] ``)`` 
                [``public``; ``abstract``]
                None

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public abstract void M<T>(object[] thing);
    }
}"
        are_equal expected actual

    [<Test>]
    let ``arrow method: with two parameters``() =
        let p1 = ("p1", (``type`` "string")) 
        let p2 = ("p2", (``array type`` "object"))
        let m = 
            ``//`` "Method comment" <|
            ``arrow_method`` "void" "M" ``<<`` ["T"] ``>>`` ``(`` [ p1;p2 ] ``)`` 
                [``public``; ``abstract``]
                None
                
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        // Method comment
        public abstract void M<T>(string p1, object[] p2);
    }
}"
        are_equal expected actual

    [<Test>]
    let ``method: with body``() =
        let m = 
            ``method`` "void" "M" ``<<`` ["T"] ``>>`` ``(`` [ ("thing", (``type`` "object")) ] ``)`` 
                [``private``; ``static``]
                ``{``
                    [
                        ``//`` "First line" <| (
                            ``//`` "Second line" <|
                            ``return`` None)
                    ]
                ``}``

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        private static void M<T>(object thing)
        {
            // First line
            // Second line
            return;
        }
    }
}"
        are_equal expected actual

