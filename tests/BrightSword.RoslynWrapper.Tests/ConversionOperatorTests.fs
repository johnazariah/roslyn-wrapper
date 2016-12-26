namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module ConversionOperatorTests =
    [<Test>]
    let ``conversion operator: implicit`` () =
        let m = 
            ``implicit operator`` "string" ``(`` "C" ``)`` 
                [ ``public``; ``static`` ] 
                (``=>`` (``invoke`` ("value.ToString" |> toIdentifierName) ``(`` [] ``)``))

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public static implicit operator string(C value) => value.ToString();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``conversion operator: implicit with forced static`` () =
        let m = 
            ``implicit operator`` "string" ``(`` "C" ``)`` 
                [ ``public`` ] 
                (``=>`` (``invoke`` ("value.ToString" |> toIdentifierName) ``(`` [] ``)``))

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public static implicit operator string(C value) => value.ToString();
    }
}"
        are_equal expected actual

    [<Test>]
    let ``conversion operator: explicit`` () =
        let m = 
            ``explicit operator`` "string" ``(`` "C" ``)`` 
                (``=>`` (``invoke`` ("value.ToString" |> toIdentifierName) ``(`` [] ``)``))

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public static explicit operator string(C value) => value.ToString();
    }
}"
        are_equal expected actual



    [<Test>]
    let ``conversion operator: explicit from-string`` () =
        let m = 
            ``explicit operator`` "C" ``(`` "string" ``)`` 
                (``=>`` (``new`` ["C"] ``(`` ["value"] ``)``))

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public static explicit operator C(string value) => new C(value);
    }
}"
        are_equal expected actual


