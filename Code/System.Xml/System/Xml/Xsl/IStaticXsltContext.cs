using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020001B5 RID: 437
	internal interface IStaticXsltContext
	{
		// Token: 0x060011EA RID: 4586
		Expression TryGetVariable(string nm);

		// Token: 0x060011EB RID: 4587
		Expression TryGetFunction(XmlQualifiedName nm, FunctionArguments args);

		// Token: 0x060011EC RID: 4588
		XmlQualifiedName LookupQName(string s);

		// Token: 0x060011ED RID: 4589
		string LookupNamespace(string prefix);
	}
}
