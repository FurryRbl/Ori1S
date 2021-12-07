using System;
using System.Collections;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x0200001C RID: 28
	internal class XsdIdentitySelector
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00007B48 File Offset: 0x00005D48
		public XsdIdentitySelector(XmlSchemaXPath selector)
		{
			this.selectorPaths = selector.CompiledExpression;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00007B68 File Offset: 0x00005D68
		public XsdIdentityPath[] Paths
		{
			get
			{
				return this.selectorPaths;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007B70 File Offset: 0x00005D70
		public void AddField(XsdIdentityField field)
		{
			this.cachedFields = null;
			this.fields.Add(field);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007B88 File Offset: 0x00005D88
		public XsdIdentityField[] Fields
		{
			get
			{
				if (this.cachedFields == null)
				{
					this.cachedFields = (this.fields.ToArray(typeof(XsdIdentityField)) as XsdIdentityField[]);
				}
				return this.cachedFields;
			}
		}

		// Token: 0x040000EA RID: 234
		private XsdIdentityPath[] selectorPaths;

		// Token: 0x040000EB RID: 235
		private ArrayList fields = new ArrayList();

		// Token: 0x040000EC RID: 236
		private XsdIdentityField[] cachedFields;
	}
}
