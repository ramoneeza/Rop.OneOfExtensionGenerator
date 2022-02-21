using System;

namespace Rop.OneOfExtension.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OneOfExtensionAttribute:Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class OneOfSplitAttribute:Attribute
    {

    }
}
