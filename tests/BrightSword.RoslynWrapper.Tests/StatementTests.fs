namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module StatementTests = 
    [<Test>]
    let ``expression: new``() =
        let t = ``generic type`` "List" ``<<`` [ "int" ] ``>>``
        let s = ``new`` t ``(`` [ ] ``)``
        let m = return_from_arrow_method (``type name`` t) s
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
    let ``expression: new() with args``() =
        let s = ``new`` (``type`` ["System"; "String" ]) ``(`` [ literal "A" ] ``)``
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

    [<Test>]
    let ``statement: empty return``() =
        let s = ``return`` None
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            return;
        }
    }
}"
        are_equal expected actual


    [<Test>]
    let ``statement: return value``() =
        let s = ``return`` (Some <| literal 42)
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            return 42;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: assignment``() =
        let target = ident "a"
        let source = literal 42
        let s = statement (target <-- source)
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            a = 42;
        }
    }
}"
        are_equal expected actual


    [<Test>]
    let ``statement: empty throw``() =
        let s = ``throw`` None
        let m = host_in_method "void" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            throw;
        }
    }
}"
        are_equal expected actual


    [<Test>]
    let ``statement: throw exception``() =
        let newException = ``new`` (``type`` ["System"; "Exception"]) ``(`` [] ``)``
        let s = ``throw`` <| Some newException
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            throw new System.Exception();
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: cast``() =
        let expr = ``cast`` "float" (literal 42)
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = (float)42;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: as``() =
        let expr = ``as`` "float" (ident "b")
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b as float;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: |~> ``() =
        let expr = (ident "b") |~> "float" 
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b as float;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: is``() =
        let expr = ``is`` "float" (ident "b")
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b is float;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: ==``() =
        let expr = (ident "b") <==> (literal 12)
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b == 12;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: &&``() =
        let expr = (ident "b") <&&> (ident "c")
        let s = ((ident "a") <-- expr) |> statement
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b && c;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: ||``() =
        let expr = (ident "b") <||> (ident "c")
        let s = ((ident "a") <-- expr) |> statement
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b || c;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: !=``() =
        let expr = (ident "b") <!=> (literal 12)
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b != 12;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: ^``() =
        let expr = (ident "b") <^> (literal 12)
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b ^ 12;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: ??``() =
        let expr = (ident "b") <??> ``false``
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = b ?? false;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: paranthesize``() =
        let expr = ``((`` ((ident "b") <^> (literal 12)) ``))``
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = (b ^ 12);
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: !``() =
        let expr = ! ((ident "b") <^> (literal 12))
        let s = ((ident "a") <-- expr) |> statement 
        let m = host_in_method "int" [s]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            a = !(b ^ 12);
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: member.access``() =
        let ma = (ident "System") <|.|> (ident "Console") <.> (ident "WriteLine", [ literal "Hello, World!" ])
        let m = host_in_method "int" [ statement ma ]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            System.Console.WriteLine(""Hello, World!"");
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: member?.access``() =
        let ma = (ident "System") <|?.|> (ident "Console") <?.> (ident "WriteLine", [ literal "Hello, World!" ])
        let m = host_in_method "int" [ statement ma ]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            System?.Console?.WriteLine(""Hello, World!"");
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: async - await``() =
        let ma = (ident "System") <|.|> (ident "Console") <.> (ident "WriteLine", [ literal "Hello, World!" ])
        let s = ``await`` ma |> statement
        let m =
            ``method`` "int" "Host" ``<<`` [] ``>>`` ``(`` [] ``)``
                [``protected``; ``internal``; ``async`` ]
                ``{``
                    [s]
                ``}``
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal async int Host()
        {
            await System.Console.WriteLine(""Hello, World!"");
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: member.access<generic>``() =
        let gen1 = ``generic type`` "Task" ``<<`` [ "int" ] ``>>``
        let gen2 = ``generic type`` "Run" ``<<`` [ "string" ] ``>>``
        let ma = (ident "System") <|.|> gen1 <.> (gen2, [ literal "Hello, World!" ])
        let m = host_in_method "int" [ statement ma ]
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal int Host()
        {
            System.Task<int>.Run<string>(""Hello, World!"");
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: literals``() =
        let ss = 
            [
                (ident "a") <-- (literal "Hello World")
                (ident "a") <-- (literal 'c')
                (ident "a") <-- (literal 42)
                (ident "a") <-- (literal 3.14159)
                (ident "a") <-- (literal 100M)
                (ident "a") <-- (literal 2147483647u)
                (ident "a") <-- (literal 2147483647L)
                (ident "a") <-- (literal 9223372036854775807UL)
                (ident "a") <-- (literal 1.0f)
                (ident "a") <-- ``true``
                (ident "a") <-- ``false``
                (ident "a") <-- ``null``
            ]
            |> Seq.map statement

        let m = host_in_method "void" ss
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal void Host()
        {
            a = ""Hello World"";
            a = 'c';
            a = 42;
            a = 3.14159;
            a = 100M;
            a = 2147483647U;
            a = 2147483647L;
            a = 9223372036854775807UL;
            a = 1F;
            a = true;
            a = false;
            a = null;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``expression: single param lambda``() =
        let expr = ``as`` "float" (ident "b")
        let s = ``_ =>`` "b" expr 
        let m = return_from_arrow_method "Func<int, float>" s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal Func<int, float> Host() => b => b as float;
    }
}"
        are_equal expected actual


    [<Test>]
    let ``expression: multi param lambda``() =
        let expr = ``as`` "float" (ident "b")
        let s = ``() =>`` ["a"; "b"] expr 
        let m = return_from_arrow_method "Func<int, float>" s
        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        protected internal Func<int, float> Host() => (a, b) => b as float;
    }
}"
        are_equal expected actual
