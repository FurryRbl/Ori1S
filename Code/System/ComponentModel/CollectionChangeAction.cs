using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the collection is changed.</summary>
	// Token: 0x020000D8 RID: 216
	public enum CollectionChangeAction
	{
		/// <summary>Specifies that an element was added to the collection.</summary>
		// Token: 0x0400027A RID: 634
		Add = 1,
		/// <summary>Specifies that an element was removed from the collection.</summary>
		// Token: 0x0400027B RID: 635
		Remove,
		/// <summary>Specifies that the entire collection has changed. This is caused by using methods that manipulate the entire collection, such as <see cref="M:System.Collections.CollectionBase.Clear" />.</summary>
		// Token: 0x0400027C RID: 636
		Refresh
	}
}
