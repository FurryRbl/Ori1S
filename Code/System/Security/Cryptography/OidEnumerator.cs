using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000436 RID: 1078
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x00078460 File Offset: 0x00076660
		internal OidEnumerator(OidCollection collection)
		{
			this._collection = collection;
			this._position = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00078478 File Offset: 0x00076678
		object IEnumerator.Current
		{
			get
			{
				if (this._position < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._collection[this._position];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x000784A0 File Offset: 0x000766A0
		public Oid Current
		{
			get
			{
				if (this._position < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this._collection[this._position];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060026CB RID: 9931 RVA: 0x000784C8 File Offset: 0x000766C8
		public bool MoveNext()
		{
			if (++this._position < this._collection.Count)
			{
				return true;
			}
			this._position = this._collection.Count - 1;
			return false;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060026CC RID: 9932 RVA: 0x0007850C File Offset: 0x0007670C
		public void Reset()
		{
			this._position = -1;
		}

		// Token: 0x040017DF RID: 6111
		private OidCollection _collection;

		// Token: 0x040017E0 RID: 6112
		private int _position;
	}
}
