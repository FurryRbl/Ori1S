using System;
using System.Security.Cryptography;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B4 RID: 180
	internal class TlsClientFinished : HandshakeMessage
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x000252B0 File Offset: 0x000234B0
		public TlsClientFinished(Context context, byte[] buffer) : base(context, HandshakeType.Finished, buffer)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000252BC File Offset: 0x000234BC
		protected override void ProcessAsSsl3()
		{
			HashAlgorithm hashAlgorithm = new SslHandshakeHash(base.Context.MasterSecret);
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.HandshakeMessages.ToArray());
			tlsStream.Write(1129074260);
			hashAlgorithm.TransformFinalBlock(tlsStream.ToArray(), 0, (int)tlsStream.Length);
			tlsStream.Reset();
			byte[] buffer = base.ReadBytes((int)this.Length);
			byte[] hash = hashAlgorithm.Hash;
			if (!HandshakeMessage.Compare(buffer, hash))
			{
				throw new TlsException(AlertDescription.DecryptError, "Decrypt error.");
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002534C File Offset: 0x0002354C
		protected override void ProcessAsTls1()
		{
			byte[] buffer = base.ReadBytes((int)this.Length);
			HashAlgorithm hashAlgorithm = new MD5SHA1();
			byte[] array = base.Context.HandshakeMessages.ToArray();
			byte[] data = hashAlgorithm.ComputeHash(array, 0, array.Length);
			byte[] buffer2 = base.Context.Current.Cipher.PRF(base.Context.MasterSecret, "client finished", data, 12);
			if (!HandshakeMessage.Compare(buffer, buffer2))
			{
				throw new TlsException(AlertDescription.DecryptError, "Decrypt error.");
			}
		}
	}
}
