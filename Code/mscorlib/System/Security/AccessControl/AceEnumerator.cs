using System;
using System.Collections;

namespace System.Security.AccessControl
{
	/// <summary>Provides the ability to iterate through the access control entries (ACEs) in an access control list (ACL). </summary>
	// Token: 0x02000559 RID: 1369
	public sealed class AceEnumerator : IEnumerator
	{
		// Token: 0x06003599 RID: 13721 RVA: 0x000B1380 File Offset: 0x000AF580
		internal AceEnumerator(GenericAcl owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000B1398 File Offset: 0x000AF598
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection. This property gets the type-friendly version of the object. </summary>
		/// <returns>The current element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</returns>
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000B13A0 File Offset: 0x000AF5A0
		public GenericAce Current
		{
			get
			{
				return (this.current >= 0) ? this.owner[this.current] : null;
			}
		}

		/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600359C RID: 13724 RVA: 0x000B13C8 File Offset: 0x000AF5C8
		public bool MoveNext()
		{
			if (this.current + 1 == this.owner.Count)
			{
				return false;
			}
			this.current++;
			return true;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.AccessControl.GenericAce" /> collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x0600359D RID: 13725 RVA: 0x000B13F4 File Offset: 0x000AF5F4
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x0400168E RID: 5774
		private GenericAcl owner;

		// Token: 0x0400168F RID: 5775
		private int current = -1;
	}
}
