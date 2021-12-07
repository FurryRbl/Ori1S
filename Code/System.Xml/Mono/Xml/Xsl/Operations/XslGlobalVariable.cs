using System;
using System.Collections;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000071 RID: 113
	internal class XslGlobalVariable : XslGeneralVariable
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00019910 File Offset: 0x00017B10
		public XslGlobalVariable(Compiler c) : base(c)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00019928 File Offset: 0x00017B28
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			Hashtable globalVariableTable = p.globalVariableTable;
			if (!globalVariableTable.Contains(this))
			{
				globalVariableTable[this] = XslGlobalVariable.busyObject;
				globalVariableTable[this] = this.var.Evaluate(p);
				return;
			}
			if (globalVariableTable[this] == XslGlobalVariable.busyObject)
			{
				throw new XsltException("Circular dependency was detected", null, p.CurrentNode);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000199A8 File Offset: 0x00017BA8
		protected override object GetValue(XslTransformProcessor p)
		{
			this.Evaluate(p);
			return p.globalVariableTable[this];
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000199C0 File Offset: 0x00017BC0
		public override bool IsLocal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000199C4 File Offset: 0x00017BC4
		public override bool IsParam
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040002A3 RID: 675
		private static object busyObject = new object();
	}
}
