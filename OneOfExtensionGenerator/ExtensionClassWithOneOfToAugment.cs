using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Rop.OneOfExtensionGenerator
{
    public class ExtensionClassWithOneOfToAugment
    {
        public PartialClassToAugment ClassToAugment { get; set; }
        public List<OneOfMethodToAugment> MethodsToAugment { get; private set; } = new List<OneOfMethodToAugment>();
        public ExtensionClassWithOneOfToAugment(ClassDeclarationSyntax classToAugment)
        {
            ClassToAugment = new PartialClassToAugment(classToAugment);
            foreach (var member in classToAugment.Members.OfType<MethodDeclarationSyntax>())
            {
                if (member.IsDecoratedWith("OneOfSplit")) addMethod(member);
            }
        }

        private void addMethod(MethodDeclarationSyntax member)
        {
            var id = member.Identifier.ToString();
            var r = member.ReturnType;
            var p = member.ParameterList.Parameters.ToList();
            //CheckHasParameters
            var to = p.FirstOrDefault();
            if (to == null) return;
            //CheckFirstParameter is generic
            if (!(to.ChildNodes().FirstOrDefault() is GenericNameSyntax gn)) return;
            //CheckFirstParameterType is OneOf
            if (gn.Identifier.ToString() != "OneOf") return;
            //
            //Evertything is OK. Add Method.
            //
            var firstparametername = to.Identifier.ToString();
            var typesToAugment = gn.TypeArgumentList.Arguments.Select(a => a.ToString()).ToArray();
            var mta = new OneOfMethodToAugment()
            {
                Identifier = id,
                ReturnType = r.ToString(),
                FirstParameterName = firstparametername,
                RestOfParameters = p.Skip(1).Select(pp => pp.ToString()).ToArray(),
                RestOfVariables = p.Skip(1).Select(pp => pp.Identifier.ToString()).ToArray(),
                TypesToAugment = typesToAugment,
                ConstraintClauses = member.ConstraintClauses.ToString()??"",
                TypeParameterList = member.TypeParameterList?.ToString()??""
            };
            MethodsToAugment.Add(mta);
        }
    }
}