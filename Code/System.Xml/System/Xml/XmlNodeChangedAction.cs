using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node change.</summary>
	// Token: 0x02000105 RID: 261
	public enum XmlNodeChangedAction
	{
		/// <summary>A node is being inserted in the tree.</summary>
		// Token: 0x0400052F RID: 1327
		Insert,
		/// <summary>A node is being removed from the tree.</summary>
		// Token: 0x04000530 RID: 1328
		Remove,
		/// <summary>A node value is being changed.</summary>
		// Token: 0x04000531 RID: 1329
		Change
	}
}
