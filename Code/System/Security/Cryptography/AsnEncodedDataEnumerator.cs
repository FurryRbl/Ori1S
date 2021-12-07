using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000433 RID: 1075
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x00077E04 File Offset: 0x00076004
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection collection)
		{
			this._collection = collection;
			this._position = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x00077E1C File Offset: 0x0007601C
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

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in the collection.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x00077E44 File Offset: 0x00076044
		public AsnEncodedData Current
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

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060026AE RID: 9902 RVA: 0x00077E6C File Offset: 0x0007606C
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
		// Token: 0x060026AF RID: 9903 RVA: 0x00077EB0 File Offset: 0x000760B0
		public void Reset()
		{
			this._position = -1;
		}

		// Token: 0x040017C3 RID: 6083
		private AsnEncodedDataCollection _collection;

		// Token: 0x040017C4 RID: 6084
		private int _position;
	}
}
