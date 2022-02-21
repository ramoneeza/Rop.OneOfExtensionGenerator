using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rop.OneOfExtensionGenerator
{
    public static class SyntaxHelper
    {
        public static bool IsDecoratedWith(this TypeDeclarationSyntax item, string attname)
        {
            return item.AttributeLists.SelectMany(a => a.Attributes).Any(a => a.Name.ToString().Equals(attname));
        }
        public static bool IsDecoratedWith(this MemberDeclarationSyntax item, string attname)
        {
            return item.AttributeLists.SelectMany(a => a.Attributes).Any(a => a.Name.ToString().Equals(attname));
        }

        public static void AppendLines(this StringBuilder sb, params string[] lines)
        {
            AppendLines(sb, lines as IEnumerable<string>);
        }
        public static void AppendLines(this StringBuilder sb, IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                sb.AppendLine(line);
            }
        }
    }
}
