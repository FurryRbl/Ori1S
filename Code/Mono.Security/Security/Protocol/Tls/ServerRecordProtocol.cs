using System;
using System.Globalization;
using System.IO;
using Mono.Security.Protocol.Tls.Handshake;
using Mono.Security.Protocol.Tls.Handshake.Server;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000097 RID: 151
	internal class ServerRecordProtocol : RecordProtocol
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x0001F92C File Offset: 0x0001DB2C
		public ServerRecordProtocol(Stream innerStream, ServerContext context) : base(innerStream, context)
		{
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001F938 File Offset: 0x0001DB38
		public override HandshakeMessage GetMessage(HandshakeType type)
		{
			return this.createServerHandshakeMessage(type);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001F950 File Offset: 0x0001DB50
		protected override void ProcessHandshakeMessage(TlsStream handMsg)
		{
			HandshakeType handshakeType = (HandshakeType)handMsg.ReadByte();
			int num = handMsg.ReadInt24();
			byte[] array = new byte[num];
			handMsg.Read(array, 0, num);
			HandshakeMessage handshakeMessage = this.createClientHandshakeMessage(handshakeType, array);
			handshakeMessage.Process();
			base.Context.LastHandshakeMsg = handshakeType;
			if (handshakeMessage != null)
			{
				handshakeMessage.Update();
				base.Context.HandshakeMessages.WriteByte((byte)handshakeType);
				base.Context.HandshakeMessages.WriteInt24(num);
				base.Context.HandshakeMessages.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001F9DC File Offset: 0x0001DBDC
		private HandshakeMessage createClientHandshakeMessage(HandshakeType type, byte[] buffer)
		{
			switch (type)
			{
			case HandshakeType.CertificateVerify:
				return new TlsClientCertificateVerify(this.context, buffer);
			case HandshakeType.ClientKeyExchange:
				return new TlsClientKeyExchange(this.context, buffer);
			default:
				if (type == HandshakeType.ClientHello)
				{
					return new TlsClientHello(this.context, buffer);
				}
				if (type != HandshakeType.Certificate)
				{
					throw new TlsException(AlertDescription.UnexpectedMessage, string.Format(CultureInfo.CurrentUICulture, "Unknown server handshake message received ({0})", new object[]
					{
						type.ToString()
					}));
				}
				return new TlsClientCertificate(this.context, buffer);
			case HandshakeType.Finished:
				return new TlsClientFinished(this.context, buffer);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		private HandshakeMessage createServerHandshakeMessage(HandshakeType type)
		{
			switch (type)
			{
			case HandshakeType.Certificate:
				return new TlsServerCertificate(this.context);
			case HandshakeType.ServerKeyExchange:
				return new TlsServerKeyExchange(this.context);
			case HandshakeType.CertificateRequest:
				return new TlsServerCertificateRequest(this.context);
			case HandshakeType.ServerHelloDone:
				return new TlsServerHelloDone(this.context);
			default:
				switch (type)
				{
				case HandshakeType.HelloRequest:
					this.SendRecord(HandshakeType.ClientHello);
					return null;
				case HandshakeType.ServerHello:
					return new TlsServerHello(this.context);
				}
				throw new InvalidOperationException("Unknown server handshake message type: " + type.ToString());
			case HandshakeType.Finished:
				return new TlsServerFinished(this.context);
			}
		}
	}
}
