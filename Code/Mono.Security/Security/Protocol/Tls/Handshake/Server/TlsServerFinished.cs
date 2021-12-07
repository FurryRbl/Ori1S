using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B9 RID: 185
	internal class TlsServerFinished : HandshakeMessage
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0002594C File Offset: 0x00023B4C
		public TlsServerFinished(Context context) : base(context, HandshakeType.Finished)
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00025970 File Offset: 0x00023B70
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			byte[] array = base.Context.HandshakeMessages.ToArray();
			hashAlgorithm.TransformBlock(array, 0, array.Length, array, 0);
			hashAlgorithm.TransformBlock(TlsServerFinished.Ssl3Marker, 0, TlsServerFinished.Ssl3Marker.Length, TlsServerFinished.Ssl3Marker, 0);
			hashAlgorithm.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
			base.Write(hashAlgorithm.Hash);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000259E4 File Offset: 0x00023BE4
		protected override void ProcessAsTls1()
		{
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] data = hashAlgorithm.ComputeHash(array, 0, array.Length);
			base.Write(base.Context.Current.Cipher.PRF(base.Context.MasterSecret, "server finished", data, 12));
		}

		// Token: 0x0400032E RID: 814
		private static byte[] Ssl3Marker = new byte[]
		{
			83,
			82,
			86,
			82
		};
	}
}
