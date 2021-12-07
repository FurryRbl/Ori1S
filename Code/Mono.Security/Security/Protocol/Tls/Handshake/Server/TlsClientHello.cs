using System;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B5 RID: 181
	internal class TlsClientHello : HandshakeMessage
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x000253D0 File Offset: 0x000235D0
		public TlsClientHello(Context context, byte[] buffer) : base(context, HandshakeType.ClientHello, buffer)
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x000253DC File Offset: 0x000235DC
		public override void Update()
		{
			base.Update();
			this.selectCipherSuite();
			this.selectCompressionMethod();
			base.Context.SessionId = this.sessionId;
			base.Context.ClientRandom = this.random;
			base.Context.ProtocolNegotiated = true;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002542C File Offset: 0x0002362C
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00025434 File Offset: 0x00023634
		protected override void ProcessAsTls1()
		{
			this.processProtocol(base.ReadInt16());
			this.random = base.ReadBytes(32);
			this.sessionId = base.ReadBytes((int)base.ReadByte());
			this.cipherSuites = new short[(int)(base.ReadInt16() / 2)];
			for (int i = 0; i < this.cipherSuites.Length; i++)
			{
				this.cipherSuites[i] = base.ReadInt16();
			}
			this.compressionMethods = new byte[(int)base.ReadByte()];
			for (int j = 0; j < this.compressionMethods.Length; j++)
			{
				this.compressionMethods[j] = base.ReadByte();
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000254E0 File Offset: 0x000236E0
		private void processProtocol(short protocol)
		{
			SecurityProtocolType securityProtocolType = base.Context.DecodeProtocolCode(protocol);
			if ((securityProtocolType & base.Context.SecurityProtocolFlags) == securityProtocolType || (base.Context.SecurityProtocolFlags & SecurityProtocolType.Default) == SecurityProtocolType.Default)
			{
				base.Context.SecurityProtocol = securityProtocolType;
				base.Context.SupportedCiphers.Clear();
				base.Context.SupportedCiphers = null;
				base.Context.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(securityProtocolType);
				return;
			}
			throw new TlsException(AlertDescription.ProtocolVersion, "Incorrect protocol version received from server");
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00025574 File Offset: 0x00023774
		private void selectCipherSuite()
		{
			for (int i = 0; i < this.cipherSuites.Length; i++)
			{
				int index;
				if ((index = base.Context.SupportedCiphers.IndexOf(this.cipherSuites[i])) != -1)
				{
					base.Context.Negotiating.Cipher = base.Context.SupportedCiphers[index];
					break;
				}
			}
			if (base.Context.Negotiating.Cipher == null)
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Insuficient Security");
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00025604 File Offset: 0x00023804
		private void selectCompressionMethod()
		{
			base.Context.CompressionMethod = SecurityCompressionType.None;
		}

		// Token: 0x0400032A RID: 810
		private byte[] random;

		// Token: 0x0400032B RID: 811
		private byte[] sessionId;

		// Token: 0x0400032C RID: 812
		private short[] cipherSuites;

		// Token: 0x0400032D RID: 813
		private byte[] compressionMethods;
	}
}
