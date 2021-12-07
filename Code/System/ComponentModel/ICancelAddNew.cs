using System;

namespace System.ComponentModel
{
	/// <summary>Adds transactional capability when adding a new item to a collection.</summary>
	// Token: 0x02000151 RID: 337
	public interface ICancelAddNew
	{
		/// <summary>Discards a pending new item from the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection. </param>
		// Token: 0x06000C64 RID: 3172
		void CancelNew(int itemIndex);

		/// <summary>Commits a pending new item to the collection.</summary>
		/// <param name="itemIndex">The index of the item that was previously added to the collection. </param>
		// Token: 0x06000C65 RID: 3173
		void EndNew(int itemIndex);
	}
}
