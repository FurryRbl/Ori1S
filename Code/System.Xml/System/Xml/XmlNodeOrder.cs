using System;

namespace System.Xml
{
	/// <summary>Describes the document order of a node compared to a second node.</summary>
	// Token: 0x0200010D RID: 269
	public enum XmlNodeOrder
	{
		/// <summary>The current node of this navigator is before the current node of the supplied navigator.</summary>
		// Token: 0x04000544 RID: 1348
		Before,
		/// <summary>The current node of this navigator is after the current node of the supplied navigator.</summary>
		// Token: 0x04000545 RID: 1349
		After,
		/// <summary>The two navigators are positioned on the same node.</summary>
		// Token: 0x04000546 RID: 1350
		Same,
		/// <summary>The node positions cannot be determined in document order, relative to each other. This could occur if the two nodes reside in different trees.</summary>
		// Token: 0x04000547 RID: 1351
		Unknown
	}
}
