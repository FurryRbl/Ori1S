using System;
using System.IO;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x0200006A RID: 106
	internal abstract class XslOperation
	{
		// Token: 0x0600039C RID: 924
		public abstract void Evaluate(XslTransformProcessor p);

		// Token: 0x0600039D RID: 925 RVA: 0x00018C5C File Offset: 0x00016E5C
		public virtual string EvaluateAsString(XslTransformProcessor p)
		{
			StringWriter stringWriter = new StringWriter();
			Outputter outputter = new TextOutputter(stringWriter, true);
			p.PushOutput(outputter);
			this.Evaluate(p);
			p.PopOutput();
			outputter.Done();
			return stringWriter.ToString();
		}

		// Token: 0x04000290 RID: 656
		public const string XsltNamespace = "http://www.w3.org/1999/XSL/Transform";
	}
}
