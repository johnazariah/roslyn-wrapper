namespace BrightSword.RoslynWrapper.Tests

open Xunit

open BrightSword.RoslynWrapper

module AutoPropertyTests =

    [<Fact>]
    let ``property: read``() =
        let m = 
            ``property-get`` "string" "Name" [ ``public`` ] 
                ``get``
                [
                    ``return`` (Some (literal ""))
                ]

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public string Name
        {
            get
            {
                return """";
            }
        }
    }
}"
        are_equal expected actual

        
    [<Fact>]
    let ``property: readwrite``() =
        let m = 
            ``property`` "string" "Name" [ ``public`` ] 
                ``get``
                    [
                        ``return`` (Some (literal "hardcoded"))
                    ]
                ``set``           
                    [
                        statement ((ident "test") <-- (ident "value"))
                    ]

        let actual = to_class_members_code [m]
        let expected = @"namespace N
{
    using System;

    public class C
    {
        public string Name
        {
            get
            {
                return ""hardcoded"";
            }

            set
            {
                test = value;
            }
        }
    }
}"
        are_equal expected actual

    [<Fact>]
    let ``auto property: read-only``() =
        let m = ``propg`` "string" "Name" [ ``public`` ]

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

    [<Fact>]
    let ``auto property: read-write``() =
        let m = ``prop`` "string" "Name" [ ``public`` ]

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
