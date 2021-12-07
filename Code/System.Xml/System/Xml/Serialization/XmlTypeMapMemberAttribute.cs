using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BE RID: 702
	internal class XmlTypeMapMemberAttribute : XmlTypeMapMember
	{
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0009BE5C File Offset: 0x0009A05C
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0009BE64 File Offset: 0x0009A064
		public string AttributeName
		{
			get
			{
				return this._attributeName;
			}
			set
			{
				this._attributeName = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0009BE70 File Offset: 0x0009A070
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0009BE78 File Offset: 0x0009A078
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0009BE84 File Offset: 0x0009A084
		public string DataTypeNamespace
		{
			get
			{
				if (this._mappedType == null)
				{
					return "http://www.w3.org/2001/XMLSchema";
				}
				return this._mappedType.Namespace;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0009BEA4 File Offset: 0x0009A0A4
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x0009BEAC File Offset: 0x0009A0AC
		public XmlSchemaForm Form
		{
			get
			{
				return this._form;
			}
			set
			{
				this._form = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0009BEB8 File Offset: 0x0009A0B8
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x0009BEC0 File Offset: 0x0009A0C0
		public XmlTypeMapping MappedType
		{
			get
			{
				return this._mappedType;
			}
			set
			{
				this._mappedType = value;
			}
		}

		// Token: 0x04000BBE RID: 3006
		private string _attributeName;

		// Token: 0x04000BBF RID: 3007
		private string _namespace = string.Empty;

		// Token: 0x04000BC0 RID: 3008
		private XmlSchemaForm _form;

		// Token: 0x04000BC1 RID: 3009
		private XmlTypeMapping _mappedType;
	}
}
