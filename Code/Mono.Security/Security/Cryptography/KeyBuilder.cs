using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000025 RID: 37
	public sealed class KeyBuilder
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000B4AC File Offset: 0x000096AC
		private KeyBuilder()
		{
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B4B4 File Offset: 0x000096B4
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

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B4F0 File Offset: 0x000096F0
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x040000AE RID: 174
		private static RandomNumberGenerator rng;
	}
}
