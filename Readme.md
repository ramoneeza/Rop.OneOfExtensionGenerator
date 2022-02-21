﻿# Rop.OneOfExtensionGenerator

Features
--------

Rop.OneOfExtensionGenerator is a source generator solution to use OneOf variables with static extension classes

`Rop.OneOfExtension.Annotations` 
------

Interfaces to decorate the static helper classes as a class with OneOf methods.

```csharp
    [AttributeUsage(AttributeTargets.Class)]
    public class OneOfExtensionAttribute:Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class OneOfSplitAttribute:Attribute
    {

    }
```

`OneOfExtension` decorate the static partial class
`OneOfSplit` decorate the OneOf Methods to split in final methods (one for each oneof generic type )

`Rop.OneOfExtensionGenerator`
------

The source generator that create the helper methods.
Must be included as:

* OutputItemType="Analyzer" 
* ReferenceOutputAssembly="false"

`Test.OneOfExtensionGenerator`
------

An example to test the generator.

1) Create a partial static class decorated as "OneOfExtension" and with private methods with first type as "this OneOf<A,B,...>"

```csharp
[OneOfExtension]
    public static partial class MyHelper{
        [OneOfSplit]
        private static string _WriteValueABC(this OneOf<A, B, C> abc)
        {
            return abc.Match(a => a.WriteValueA, b => b.WriteValueB, c => c.WriteValueC);
        }
        [OneOfSplit]
        private static string _WriteValueAB(this OneOf<A, B> ab)
        {
            return ab.Match(a => a.WriteValueA, b => b.WriteValueB);
        }
    }
```

2) Source Generator create Proxy Methods as public static methods for each generic type in the "OneOf" parameter

```csharp
// Autogenerated code for spliting OneOf Extensions
using OneOf;
using Rop.OneOfExtension.Annotations;
namespace Test.OneOfExtensionGenerator
{
	public static partial class MyHelper
	{
		public static string WriteValueABC(this A abc)
		{
			return _WriteValueABC(abc);
		}
		public static string WriteValueABC(this B abc)
		{
			return _WriteValueABC(abc);
		}
		public static string WriteValueABC(this C abc)
		{
			return _WriteValueABC(abc);
		}
		public static string WriteValueAB(this A ab)
		{
			return _WriteValueAB(ab);
		}
		public static string WriteValueAB(this B ab)
		{
			return _WriteValueAB(ab);
		}
	}
}
```

3) Use new extensions methods

```csharp
var a=new A();
var b=new B();
var c=new C();
Console.WriteLine(a.WriteValueABC());
Console.WriteLine(b.WriteValueABC());
Console.WriteLine(c.WriteValueABC());
Console.WriteLine(a.WriteValueAB());
Console.WriteLine(b.WriteValueAB());
 ```


 ------
 (C)2022 Ramón Ordiales Plaza