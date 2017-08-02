namespace BrightSword.RoslynWrapper.Tests

open NUnit.Framework

open BrightSword.RoslynWrapper

module AutoPropertyTests =
    [<Test>]
    let ``auto property: read-only``() =
        let m = ``auto-propg`` "string" "Name" [ ``public`` ]

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public string Name
        {
            get;
        }
    }
}"
        are_equal expected actual

    [<Test>]
    let ``auto property: read-write``() =
        let m = ``auto-prop`` "string" "Name" [ ``public`` ]

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public string Name
        {
            get;
            set;
        }
    }
}"
        are_equal expected actual


