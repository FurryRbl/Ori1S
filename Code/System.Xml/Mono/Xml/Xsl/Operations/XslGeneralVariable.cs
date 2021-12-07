using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000070 RID: 112
	internal abstract class XslGeneralVariable : XslCompiledElement, IXsltContextVariable
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x00019878 File Offset: 0x00017A78
		public XslGeneralVariable(Compiler c) : base(c)
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00019884 File Offset: 0x00017A84
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			this.var = new XslVariableInformation(c);
		}

		// Token: 0x060003B2 RID: 946
		public abstract override void Evaluate(XslTransformProcessor p);

		// Token: 0x060003B3 RID: 947
		protected abstract object GetValue(XslTransformProcessor p);

		// Token: 0x060003B4 RID: 948 RVA: 0x000198BC File Offset: 0x00017ABC
		public object Evaluate(XsltContext xsltContext)
		{
			object value = this.GetValue(((XsltCompiledContext)xsltContext).Processor);
			if (value is XPathNodeIterator)
			{
				return new WrapperIterator(((XPathNodeIterator)value).Clone(), xsltContext);
			}
			return value;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000198FC File Offset: 0x00017AFC
		public XmlQualifiedName Name
		{
			get
			{
				return this.var.Name;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0001990C File Offset: 0x00017B0C
		public XPathResultType VariableType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003B7 RID: 951
		public abstract bool IsLocal { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003B8 RID: 952
		public abstract bool IsParam { get; }

		// Token: 0x040002A2 RID: 674
		protected XslVariableInformation var;
	}
}
