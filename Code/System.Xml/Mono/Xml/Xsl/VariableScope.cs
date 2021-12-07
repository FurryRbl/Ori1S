using System;
using System.Collections;
using System.Xml;
using Mono.Xml.Xsl.Operations;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000079 RID: 121
	internal class VariableScope
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		public VariableScope(VariableScope parent)
		{
			this.parent = parent;
			if (parent != null)
			{
				this.nextSlot = parent.nextSlot;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001AD04 File Offset: 0x00018F04
		internal void giveHighTideToParent()
		{
			if (this.parent != null)
			{
				this.parent.highTide = Math.Max(this.VariableHighTide, this.parent.VariableHighTide);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001AD40 File Offset: 0x00018F40
		public int VariableHighTide
		{
			get
			{
				return Math.Max(this.highTide, this.nextSlot);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0001AD54 File Offset: 0x00018F54
		public VariableScope Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001AD5C File Offset: 0x00018F5C
		public int AddVariable(XslLocalVariable v)
		{
			if (this.variables == null)
			{
				this.variableNames = new ArrayList();
				this.variables = new Hashtable();
			}
			this.variables[v.Name] = v;
			int num = this.variableNames.IndexOf(v.Name);
			if (num >= 0)
			{
				return num;
			}
			this.variableNames.Add(v.Name);
			return this.nextSlot++;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001ADDC File Offset: 0x00018FDC
		public XslLocalVariable ResolveStatic(XmlQualifiedName name)
		{
			for (VariableScope variableScope = this; variableScope != null; variableScope = variableScope.Parent)
			{
				if (variableScope.variables != null)
				{
					XslLocalVariable xslLocalVariable = variableScope.variables[name] as XslLocalVariable;
					if (xslLocalVariable != null)
					{
						return xslLocalVariable;
					}
				}
			}
			return null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001AE28 File Offset: 0x00019028
		public XslLocalVariable Resolve(XslTransformProcessor p, XmlQualifiedName name)
		{
			for (VariableScope variableScope = this; variableScope != null; variableScope = variableScope.Parent)
			{
				if (variableScope.variables != null)
				{
					XslLocalVariable xslLocalVariable = variableScope.variables[name] as XslLocalVariable;
					if (xslLocalVariable != null && xslLocalVariable.IsEvaluated(p))
					{
						return xslLocalVariable;
					}
				}
			}
			return null;
		}

		// Token: 0x040002C9 RID: 713
		private ArrayList variableNames;

		// Token: 0x040002CA RID: 714
		private Hashtable variables;

		// Token: 0x040002CB RID: 715
		private VariableScope parent;

		// Token: 0x040002CC RID: 716
		private int nextSlot;

		// Token: 0x040002CD RID: 717
		private int highTide;
	}
}
