using System;
using System.Globalization;
using System.IO;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.Protocol.Tls.Handshake.Client;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000084 RID: 132
	internal class ClientRecordProtocol : RecordProtocol
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public ClientRecordProtocol(Stream innerStream, ClientContext context) : base(innerStream, context)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
		public override HandshakeMessage GetMessage(HandshakeType type)
		{
			return this.createClientHandshakeMessage(type);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001D30C File Offset: 0x0001B50C
		protected override void ProcessHandshakeMessage(TlsStream handMsg)
		{
			HandshakeType handshakeType = (HandshakeType)handMsg.ReadByte();
			int num = handMsg.ReadInt24();
			byte[] array = null;
			if (num > 0)
			{
				array = new byte[num];
				handMsg.Read(array, 0, num);
			}
			HandshakeMessage handshakeMessage = this.createServerHandshakeMessage(handshakeType, array);
			if (handshakeMessage != null)
			{
				handshakeMessage.Process();
			}
			base.Context.LastHandshakeMsg = handshakeType;
			if (handshakeMessage != null)
			{
				handshakeMessage.Update();
				base.Context.HandshakeMessages.WriteByte((byte)handshakeType);
				base.Context.HandshakeMessages.WriteInt24(num);
				if (num > 0)
				{
					base.Context.HandshakeMessages.Write(array, 0, array.Length);
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		private HandshakeMessage createClientHandshakeMessage(HandshakeType type)
		{
			switch (type)
			{
			case HandshakeType.CertificateVerify:
				return new TlsClientCertificateVerify(this.context);
			case HandshakeType.ClientKeyExchange:
				return new TlsClientKeyExchange(this.context);
			default:
				if (type == HandshakeType.ClientHello)
				{
					return new TlsClientHello(this.context);
				}
				if (type != HandshakeType.Certificate)
				{
					throw new InvalidOperationException("Unknown client handshake message type: " + type.ToString());
				}
				return new TlsClientCertificate(this.context);
			case HandshakeType.Finished:
				return new TlsClientFinished(this.context);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001D44C File Offset: 0x0001B64C
		private HandshakeMessage createServerHandshakeMessage(HandshakeType type, byte[] buffer)
		{
			ClientContext clientContext = (ClientContext)this.context;
			switch (type)
			{
			case HandshakeType.Certificate:
				return new TlsServerCertificate(this.context, buffer);
			case HandshakeType.ServerKeyExchange:
				return new TlsServerKeyExchange(this.context, buffer);
			case HandshakeType.CertificateRequest:
				return new TlsServerCertificateRequest(this.context, buffer);
			case HandshakeType.ServerHelloDone:
				return new TlsServerHelloDone(this.context, buffer);
			default:
				switch (type)
				{
				case HandshakeType.HelloRequest:
					if (clientContext.HandshakeState != HandshakeState.Started)
					{
						clientContext.HandshakeState = HandshakeState.None;
					}
					else
					{
						base.SendAlert(AlertLevel.Warning, AlertDescription.NoRenegotiation);
					}
					return null;
				case HandshakeType.ServerHello:
					return new TlsServerHello(this.context, buffer);
				}
				throw new TlsException(AlertDescription.UnexpectedMessage, string.Format(CultureInfo.CurrentUICulture, "Unknown server handshake message received ({0})", new object[]
				{
					type.ToString()
				}));
			case HandshakeType.Finished:
				return new TlsServerFinished(this.context, buffer);
			}
		}
	}
}
