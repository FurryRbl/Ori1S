using System;

namespace System.Xml
{
	/// <summary>Specifies how the <see cref="T:System.Xml.XmlTextReader" /> or <see cref="T:System.Xml.XmlValidatingReader" /> handle entities.</summary>
	// Token: 0x020000D7 RID: 215
	public enum EntityHandling
	{
		/// <summary>Expands all entities and returns the expanded nodes.</summary>
		// Token: 0x04000471 RID: 1137
		ExpandEntities = 1,
		/// <summary>Expands character entities and returns general entities as <see cref="F:System.Xml.XmlNodeType.EntityReference" /> nodes. </summary>
		// Token: 0x04000472 RID: 1138
		ExpandCharEntities
	}
}
