using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C0 RID: 704
	internal class XmlTypeMapMemberList : XmlTypeMapMemberElement
	{
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0009C13C File Offset: 0x0009A33C
		public XmlTypeMapping ListTypeMapping
		{
			get
			{
				return ((XmlTypeMapElementInfo)base.ElementInfo[0]).MappedType;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0009C154 File Offset: 0x0009A354
		public string ElementName
		{
			get
			{
				return ((XmlTypeMapElementInfo)base.ElementInfo[0]).ElementName;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0009C16C File Offset: 0x0009A36C
		public string Namespace
		{
			get
			{
				return ((XmlTypeMapElementInfo)base.ElementInfo[0]).Namespace;
			}
		}
	}
}
