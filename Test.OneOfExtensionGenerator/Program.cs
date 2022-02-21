// See https://aka.ms/new-console-template for more information

using OneOf;
using Test.OneOfExtensionGenerator;


var a=new A();
var b=new B();
var c=new C();
Console.WriteLine(a.WriteValueABC());
Console.WriteLine(b.WriteValueABC());
Console.WriteLine(c.WriteValueABC());
Console.WriteLine(a.WriteValueAB());
Console.WriteLine(b.WriteValueAB());
