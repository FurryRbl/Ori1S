using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x020000B4 RID: 180
	internal sealed class KeyBuilder
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A61C File Offset: 0x0002881C
		private KeyBuilder()
		{
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002A624 File Offset: 0x00028824
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A640 File Offset: 0x00028840
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A660 File Offset: 0x00028860
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x0400024E RID: 590
		private static RandomNumberGenerator rng;
	}
}
