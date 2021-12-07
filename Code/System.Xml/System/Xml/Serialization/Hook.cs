using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000263 RID: 611
	[XmlType("hook")]
	internal class Hook
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x000843B8 File Offset: 0x000825B8
		public string GetCode(HookAction action)
		{
			if (action == HookAction.InsertBefore)
			{
				return this.InsertBefore;
			}
			if (action == HookAction.InsertAfter)
			{
				return this.InsertAfter;
			}
			return this.Replace;
		}

		// Token: 0x04000A56 RID: 2646
		[XmlAttribute("type")]
		public HookType HookType;

		// Token: 0x04000A57 RID: 2647
		[XmlElement("select")]
		public Select Select;

		// Token: 0x04000A58 RID: 2648
		[XmlElement("insertBefore")]
		public string InsertBefore;

		// Token: 0x04000A59 RID: 2649
		[XmlElement("insertAfter")]
		public string InsertAfter;

		// Token: 0x04000A5A RID: 2650
		[XmlElement("replace")]
		public string Replace;
	}
}
