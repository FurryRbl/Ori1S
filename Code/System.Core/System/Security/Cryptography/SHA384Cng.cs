using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200005F RID: 95
	public sealed class SHA384Cng : SHA384
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x0001848C File Offset: 0x0001668C
		[SecurityCritical]
		public SHA384Cng()
		{
			this.hash = new SHA384Managed();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000184B0 File Offset: 0x000166B0
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000184C0 File Offset: 0x000166C0
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000184D4 File Offset: 0x000166D4
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001850C File Offset: 0x0001670C
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015C RID: 348
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015D RID: 349
		private SHA384 hash;
	}
}
