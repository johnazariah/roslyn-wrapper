#### 1.0.2 - December 31 2016
* Clean-up and Document
    * The following inline functions generate expressions as follows:
        * <&&> - logical AND
        * <||> - logical OR
        * <^> - exclusive OR
        * <==> - equality
        * <!=> - inequality
        * ! - logical NOT
        * <|.|> - dot access to fields
        * <.> - dot access to methods
        * <\??> - null coalesce
        * <|?.|> - conditional dot access to fields
        * <\?.> - conditional dot access to methods
        * |~> - inline form of ``as``

    * The following functions generate expressions and statements as follows:
        * ``as`` ... : ( _ as _ )
        * ``is`` ... : ( _ is _ )
        * ``cast`` ... : (cast) ...
        * ``await`` ... : await
        * ``new`` ... : new ...
        * ``=>`` ... : arrow expression
        * ``:=`` ... : assignment expression
        * ``return`` ... : return ...
        * ``throw`` ... : throw ...
        * ``_ =>`` ... : lambda expression with one parameter
        * ``() =>`` ... : lambda expression with multiple parameters
        * ident ... : define an identifier
        * ``type`` ... : define a type

    * The following functions generate syntax declarations - along with associated keywords
        * ``namespace`` ... : namespace
        * ``using`` ... : using
        * ``interface`` ... : interface
        * ``class`` ... : class
        * ``constructor`` ... : constructor
        * ``field`` ... : field
        * ``method`` ... : method
        * ``prop`` ... : read-write property
        * ``propg`` ... : read-only property
        * ``var`` ... : local variable declaration
        * ``operator ==`` : equality operator
        * ``operator !=`` : inequality operator
        * ``explicit operator`` : explicit conversion operator
        * ``implicit operator`` : implicit conversion operator

    * The following literal types are supported
        * string
        * char
        * int
        * long
        * double
        * float
        * decimal
        * uint32
        * uint64
        * ``null`` : literal null
        * ``false`` : literal false
        * ``true`` : literal true

#### 1.0.1 - December 31 2016    
* Added more expressions
* Initial set of code, tests & docs
* Changed name from fsharp-project-scaffold to BrightSword.RoslynWrapper
* Initial release
