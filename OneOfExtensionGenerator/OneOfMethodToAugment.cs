namespace Rop.OneOfExtensionGenerator
{
    public class OneOfMethodToAugment
    {
        public string Identifier { get; set; }
        public string ReturnType { get; set; }
        public string FirstParameterName { get; set; }
        public string[] TypesToAugment { get; set; }
        public string[] RestOfParameters { get; set; }
        public string[] RestOfVariables { get; set; }
        public string ConstraintClauses { get; set; }
        public string TypeParameterList { get; set; }
    }
}