using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000062 RID: 98
	public sealed class SHA512CryptoServiceProvider : SHA512
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x00018648 File Offset: 0x00016848
		[SecurityCritical]
		public SHA512CryptoServiceProvider()
		{
			this.hash = new SHA512Managed();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001866C File Offset: 0x0001686C
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001867C File Offset: 0x0001687C
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018690 File Offset: 0x00016890
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000186C8 File Offset: 0x000168C8
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000162 RID: 354
		private static byte[] Empty = new byte[0];

		// Token: 0x04000163 RID: 355
		private SHA512 hash;
	}
}
