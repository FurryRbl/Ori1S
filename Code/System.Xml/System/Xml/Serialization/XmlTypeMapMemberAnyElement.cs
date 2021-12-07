using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C3 RID: 707
	internal class XmlTypeMapMemberAnyElement : XmlTypeMapMemberExpandable
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x0009C1C4 File Offset: 0x0009A3C4
		public bool IsElementDefined(string name, string ns)
		{
			foreach (object obj in base.ElementInfo)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
				if (xmlTypeMapElementInfo.IsUnnamedAnyElement)
				{
					return true;
				}
				if (xmlTypeMapElementInfo.ElementName == name && xmlTypeMapElementInfo.Namespace == ns)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0009C26C File Offset: 0x0009A46C
		public bool IsDefaultAny
		{
			get
			{
				foreach (object obj in base.ElementInfo)
				{
					XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)obj;
					if (xmlTypeMapElementInfo.IsUnnamedAnyElement)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0009C2EC File Offset: 0x0009A4EC
		public bool CanBeText
		{
			get
			{
				return base.ElementInfo.Count > 0 && ((XmlTypeMapElementInfo)base.ElementInfo[0]).IsTextElement;
			}
		}
	}
}
