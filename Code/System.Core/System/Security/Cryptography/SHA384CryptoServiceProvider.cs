using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000060 RID: 96
	public sealed class SHA384CryptoServiceProvider : SHA384
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00018520 File Offset: 0x00016720
		[SecurityCritical]
		public SHA384CryptoServiceProvider()
		{
			this.hash = new SHA384Managed();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018544 File Offset: 0x00016744
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018554 File Offset: 0x00016754
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018568 File Offset: 0x00016768
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000185A0 File Offset: 0x000167A0
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015E RID: 350
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015F RID: 351
		private SHA384 hash;
	}
}
