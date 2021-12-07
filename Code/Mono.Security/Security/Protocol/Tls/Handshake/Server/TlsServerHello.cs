using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000BA RID: 186
	internal class TlsServerHello : HandshakeMessage
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x00025A44 File Offset: 0x00023C44
		public TlsServerHello(Context context) : base(context, HandshakeType.ServerHello)
		{
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00025A50 File Offset: 0x00023C50
		public override void Update()
		{
			base.Update();
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(this.unixTime);
			tlsStream.Write(this.random);
			base.Context.ServerRandom = tlsStream.ToArray();
			tlsStream.Reset();
			tlsStream.Write(base.Context.ClientRandom);
			tlsStream.Write(base.Context.ServerRandom);
			base.Context.RandomCS = tlsStream.ToArray();
			tlsStream.Reset();
			tlsStream.Write(base.Context.ServerRandom);
			tlsStream.Write(base.Context.ClientRandom);
			base.Context.RandomSC = tlsStream.ToArray();
			tlsStream.Reset();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00025B0C File Offset: 0x00023D0C
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00025B14 File Offset: 0x00023D14
		protected override void ProcessAsTls1()
		{
			base.Write(base.Context.Protocol);
			this.unixTime = base.Context.GetUnixTime();
			base.Write(this.unixTime);
			this.random = base.Context.GetSecureRandomBytes(28);
			base.Write(this.random);
			if (base.Context.SessionId == null)
			{
				this.WriteByte(0);
			}
			else
			{
				this.WriteByte((byte)base.Context.SessionId.Length);
				base.Write(base.Context.SessionId);
			}
			base.Write(base.Context.Negotiating.Cipher.Code);
			this.WriteByte((byte)base.Context.CompressionMethod);
		}

		// Token: 0x0400032F RID: 815
		private int unixTime;

		// Token: 0x04000330 RID: 816
		private byte[] random;
	}
}
