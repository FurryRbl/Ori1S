using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents the enumerator for <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects in a <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.</summary>
	// Token: 0x0200060A RID: 1546
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x06003AE8 RID: 15080 RVA: 0x000CA05C File Offset: 0x000C825C
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		/// <summary>Gets the current object in the collection.</summary>
		/// <returns>The current object in the collection.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000CA070 File Offset: 0x000C8270
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		/// <summary>Gets the current entry in the collection.</summary>
		/// <returns>The current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed before first calling the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method. The cursor is located before the first object in the collection.-or- The <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> property is accessed after a call to the <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> method returns false, which indicates that the cursor is located after the last object in the collection. </exception>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000CA080 File Offset: 0x000C8280
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this.e.Current;
			}
		}

		/// <summary>Moves to the next element in the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06003AEB RID: 15083 RVA: 0x000CA094 File Offset: 0x000C8294
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000CA0A4 File Offset: 0x000C82A4
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x0400199C RID: 6556
		private IEnumerator e;
	}
}
