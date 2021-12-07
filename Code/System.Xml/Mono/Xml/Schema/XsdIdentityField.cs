using System;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200001D RID: 29
	internal class XsdIdentityField
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00007BBC File Offset: 0x00005DBC
		public XsdIdentityField(XmlSchemaXPath field, int index)
		{
			this.index = index;
			this.fieldPaths = field.CompiledExpression;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public XsdIdentityPath[] Paths
		{
			get
			{
				return this.fieldPaths;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x040000ED RID: 237
		private XsdIdentityPath[] fieldPaths;

		// Token: 0x040000EE RID: 238
		private int index;
	}
}
