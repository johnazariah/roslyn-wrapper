namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module FieldTests =
    [<Test>]
    let ``field: uninitialized`` () =
        let m = 
            ``field`` "string" "m_Name" 
                [ ``private`` ] 
                None

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        private string m_Name;
    }
}"
        are_equal expected actual

    [<Test>]
    let ``field: initialized`` () =
        let e = ``:=`` <| literal "John"
        let m = 
            ``field`` "string" "m_Name" 
                [ ``private`` ] 
                (Some e)

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        private string m_Name = ""John"";
    }
}"
        are_equal expected actual

