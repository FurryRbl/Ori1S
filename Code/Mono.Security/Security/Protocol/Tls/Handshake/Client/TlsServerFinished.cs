using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000AE RID: 174
	internal class TlsServerFinished : HandshakeMessage
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x00024770 File Offset: 0x00022970
		public TlsServerFinished(Context context, byte[] buffer) : base(context, HandshakeType.Finished, buffer)
		{
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00024794 File Offset: 0x00022994
		public override void Update()
		{
			base.Update();
			base.Context.HandshakeState = HandshakeState.Finished;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000247A8 File Offset: 0x000229A8
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			byte[] array = base.Context.HandshakeMessages.ToArray();
			hashAlgorithm.TransformBlock(array, 0, array.Length, array, 0);
			hashAlgorithm.TransformBlock(TlsServerFinished.Ssl3Marker, 0, TlsServerFinished.Ssl3Marker.Length, TlsServerFinished.Ssl3Marker, 0);
			hashAlgorithm.TransformFinalBlock(CipherSuite.EmptyArray, 0, 0);
			byte[] buffer = base.ReadBytes((int)this.Length);
			byte[] hash = hashAlgorithm.Hash;
			if (!HandshakeMessage.Compare(hash, buffer))
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Invalid ServerFinished message received.");
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002483C File Offset: 0x00022A3C
		protected override void ProcessAsTls1()
		{
			byte[] buffer = base.ReadBytes((int)this.Length);
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] data = hashAlgorithm.ComputeHash(array, 0, array.Length);
			byte[] buffer2 = base.Context.Current.Cipher.PRF(base.Context.MasterSecret, "server finished", data, 12);
			if (!HandshakeMessage.Compare(buffer2, buffer))
			{
				throw new TlsException("Invalid ServerFinished message received.");
			}
		}

		// Token: 0x04000322 RID: 802
		private static byte[] Ssl3Marker = new byte[]
		{
			83,
			82,
			86,
			82
		};
	}
}
