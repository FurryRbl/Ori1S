using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash value for the input data using the implementation provided by the cryptographic service provider (CSP). This class cannot be inherited. </summary>
	// Token: 0x020005DB RID: 1499
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> class.</summary>
		// Token: 0x0600394F RID: 14671 RVA: 0x000C4338 File Offset: 0x000C2538
		public SHA1CryptoServiceProvider()
		{
			this.sha = new SHA1Internal();
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000C434C File Offset: 0x000C254C
		~SHA1CryptoServiceProvider()
		{
			this.Dispose(false);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000C4388 File Offset: 0x000C2588
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000C4394 File Offset: 0x000C2594
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this.State = 1;
			this.sha.HashCore(rgb, ibStart, cbSize);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000C43AC File Offset: 0x000C25AC
		protected override byte[] HashFinal()
		{
			this.State = 0;
			return this.sha.HashFinal();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003954 RID: 14676 RVA: 0x000C43C0 File Offset: 0x000C25C0
		public override void Initialize()
		{
			this.sha.Initialize();
		}

		// Token: 0x040018D5 RID: 6357
		private SHA1Internal sha;
	}
}
