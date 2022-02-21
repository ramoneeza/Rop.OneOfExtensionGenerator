using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rop.OneOfExtensionGenerator
{
    public class PartialClassToAugment
    {
        public string Identifier { get; set; }
        public string FileName { get; set; }
        public string Namespace { get; set; }
        public List<(string name, string sentence)> Usings { get; set; }

        public PartialClassToAugment(ClassDeclarationSyntax classToAugment)
        {
            Identifier = classToAugment.Identifier.ToString();
            var stfp = Path.GetFileNameWithoutExtension(classToAugment.SyntaxTree.FilePath);
            FileName = (string.IsNullOrEmpty(stfp)) ? Identifier : stfp;
            Usings = classToAugment.SyntaxTree.GetRoot().ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(u => (u.Name.ToString(), u.ToString())).ToList();
            Namespace = classToAugment.SyntaxTree.GetRoot().ChildNodes().OfType<NamespaceDeclarationSyntax>()
                .First().Name.ToString();
        }

        public IEnumerable<string> GetHeader()
        {
            foreach (var u in Usings)
            {
                yield return u.sentence;
            }

            yield return $"namespace {Namespace}";
            yield return "{";
            yield return $"\tpublic static partial class {Identifier}";
            yield return "\t{";
        }

        public IEnumerable<string> GetFooter()
        {
            yield return "\t}";
            yield return "}";
        }
    }
}
