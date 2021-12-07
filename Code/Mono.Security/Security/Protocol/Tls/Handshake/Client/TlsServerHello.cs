using System;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x020000AF RID: 175
	internal class TlsServerHello : HandshakeMessage
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x000248BC File Offset: 0x00022ABC
		public TlsServerHello(Context context, byte[] buffer) : base(context, HandshakeType.ServerHello, buffer)
		{
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000248C8 File Offset: 0x00022AC8
		public override void Update()
		{
			base.Update();
			base.Context.SessionId = this.sessionId;
			base.Context.ServerRandom = this.random;
			base.Context.Negotiating.Cipher = this.cipherSuite;
			base.Context.CompressionMethod = this.compressionMethod;
			base.Context.ProtocolNegotiated = true;
			int num = base.Context.ClientRandom.Length;
			int num2 = base.Context.ServerRandom.Length;
			int num3 = num + num2;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(base.Context.ClientRandom, 0, array, 0, num);
			Buffer.BlockCopy(base.Context.ServerRandom, 0, array, num, num2);
			base.Context.RandomCS = array;
			byte[] array2 = new byte[num3];
			Buffer.BlockCopy(base.Context.ServerRandom, 0, array2, 0, num2);
			Buffer.BlockCopy(base.Context.ClientRandom, 0, array2, num2, num);
			base.Context.RandomSC = array2;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000249CC File Offset: 0x00022BCC
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000249D4 File Offset: 0x00022BD4
		protected override void ProcessAsTls1()
		{
			this.processProtocol(base.ReadInt16());
			this.random = base.ReadBytes(32);
			int num = (int)base.ReadByte();
			if (num > 0)
			{
				this.sessionId = base.ReadBytes(num);
				ClientSessionCache.Add(base.Context.ClientSettings.TargetHost, this.sessionId);
				base.Context.AbbreviatedHandshake = HandshakeMessage.Compare(this.sessionId, base.Context.SessionId);
			}
			else
			{
				base.Context.AbbreviatedHandshake = false;
			}
			short code = base.ReadInt16();
			if (base.Context.SupportedCiphers.IndexOf(code) == -1)
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Invalid cipher suite received from server");
			}
			this.cipherSuite = base.Context.SupportedCiphers[code];
			this.compressionMethod = (SecurityCompressionType)base.ReadByte();
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00024AB4 File Offset: 0x00022CB4
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

		// Token: 0x04000323 RID: 803
		private SecurityCompressionType compressionMethod;

		// Token: 0x04000324 RID: 804
		private byte[] random;

		// Token: 0x04000325 RID: 805
		private byte[] sessionId;

		// Token: 0x04000326 RID: 806
		private CipherSuite cipherSuite;
	}
}
