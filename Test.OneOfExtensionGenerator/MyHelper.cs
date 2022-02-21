using OneOf;
using Rop.OneOfExtension.Annotations;
namespace Test.OneOfExtensionGenerator
{
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

}
