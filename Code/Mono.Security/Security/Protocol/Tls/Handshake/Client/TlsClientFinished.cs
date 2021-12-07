using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000A9 RID: 169
	internal class TlsClientFinished : HandshakeMessage
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public TlsClientFinished(Context context) : base(context, HandshakeType.Finished)
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00023BDC File Offset: 0x00021DDC
		public override void Update()
		{
			base.Update();
			base.Reset();
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00023BEC File Offset: 0x00021DEC
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			byte[] array = base.Context.HandshakeMessages.ToArray();
			hashAlgorithm.TransformBlock(array, 0, array.Length, array, 0);
			hashAlgorithm.TransformBlock(TlsClientFinished.Ssl3Marker, 0, TlsClientFinished.Ssl3Marker.Length, TlsClientFinished.Ssl3Marker, 0);
			hashAlgorithm.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
			base.Write(hashAlgorithm.Hash);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00023C60 File Offset: 0x00021E60
		protected override void ProcessAsTls1()
		{
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] data = hashAlgorithm.ComputeHash(array, 0, array.Length);
			base.Write(base.Context.Write.Cipher.PRF(base.Context.MasterSecret, "client finished", data, 12));
		}

		// Token: 0x0400031D RID: 797
		private static byte[] Ssl3Marker = new byte[]
		{
			67,
			76,
			78,
			84
		};
	}
}
