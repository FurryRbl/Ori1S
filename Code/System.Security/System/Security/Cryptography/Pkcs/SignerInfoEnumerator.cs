using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface. </summary>
	// Token: 0x0200002D RID: 45
	public sealed class SignerInfoEnumerator : IEnumerator
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00006190 File Offset: 0x00004390
		internal SignerInfoEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>Retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000061A4 File Offset: 0x000043A4
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000061B4 File Offset: 0x000043B4
		public SignerInfo Current
		{
			get
			{
				return (SignerInfo)this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.MoveNext" /> method advances the enumeration to the next   <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool value that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object, the method returns true. If the enumeration moved past the last item in the enumeration, it returns false.</returns>
		// Token: 0x0600011B RID: 283 RVA: 0x000061C8 File Offset: 0x000043C8
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		// Token: 0x0600011C RID: 284 RVA: 0x000061D8 File Offset: 0x000043D8
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x04000096 RID: 150
		private IEnumerator enumerator;
	}
}
