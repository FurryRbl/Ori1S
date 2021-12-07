using System;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000AA RID: 170
	internal class TlsClientHello : HandshakeMessage
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x00023CC0 File Offset: 0x00021EC0
		public TlsClientHello(Context context) : base(context, HandshakeType.ClientHello)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00023CCC File Offset: 0x00021ECC
		public override void Update()
		{
			ClientContext clientContext = (ClientContext)base.Context;
			base.Update();
			clientContext.ClientRandom = this.random;
			clientContext.ClientHelloProtocol = base.Context.Protocol;
			this.random = null;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00023D10 File Offset: 0x00021F10
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00023D18 File Offset: 0x00021F18
		protected override void ProcessAsTls1()
		{
			base.Write(base.Context.Protocol);
			TlsStream tlsStream = new TlsStream();
			tlsStream.Write(base.Context.GetUnixTime());
			tlsStream.Write(base.Context.GetSecureRandomBytes(28));
			this.random = tlsStream.ToArray();
			tlsStream.Reset();
			base.Write(this.random);
			base.Context.SessionId = ClientSessionCache.FromHost(base.Context.ClientSettings.TargetHost);
			if (base.Context.SessionId != null)
			{
				base.Write((byte)base.Context.SessionId.Length);
				if (base.Context.SessionId.Length > 0)
				{
					base.Write(base.Context.SessionId);
				}
			}
			else
			{
				base.Write(0);
			}
			base.Write((short)(base.Context.SupportedCiphers.Count * 2));
			for (int i = 0; i < base.Context.SupportedCiphers.Count; i++)
			{
				base.Write(base.Context.SupportedCiphers[i].Code);
			}
			base.Write(1);
			base.Write((byte)base.Context.CompressionMethod);
		}

		// Token: 0x0400031E RID: 798
		private byte[] random;
	}
}
