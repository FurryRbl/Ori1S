using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides enumeration functionality for the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection. This class cannot be inherited. </summary>
	// Token: 0x0200000E RID: 14
	public sealed class CryptographicAttributeObjectEnumerator : IEnumerator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000040B4 File Offset: 0x000022B4
		internal CryptographicAttributeObjectEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000040C8 File Offset: 0x000022C8
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000040D8 File Offset: 0x000022D8
		public CryptographicAttributeObject Current
		{
			get
			{
				return (CryptographicAttributeObject)this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumeration to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>true if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object; false if the enumerator is at the end of the enumeration.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x000040EC File Offset: 0x000022EC
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Resets the enumeration to the first <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		// Token: 0x06000041 RID: 65 RVA: 0x000040FC File Offset: 0x000022FC
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x04000039 RID: 57
		private IEnumerator enumerator;
	}
}
