using System;
using System.Security.Cryptography;
using Mono.Security.Protocol.Tls.Handshake;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000088 RID: 136
	internal abstract class Context
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x0001DA80 File Offset: 0x0001BC80
		public Context(SecurityProtocolType securityProtocolType)
		{
			this.SecurityProtocol = securityProtocolType;
			this.compressionMethod = SecurityCompressionType.None;
			this.serverSettings = new TlsServerSettings();
			this.clientSettings = new TlsClientSettings();
			this.handshakeMessages = new TlsStream();
			this.sessionId = null;
			this.handshakeState = HandshakeState.None;
			this.random = RandomNumberGenerator.Create();
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001DADC File Offset: 0x0001BCDC
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		public bool AbbreviatedHandshake
		{
			get
			{
				return this.abbreviatedHandshake;
			}
			set
			{
				this.abbreviatedHandshake = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public bool ProtocolNegotiated
		{
			get
			{
				return this.protocolNegotiated;
			}
			set
			{
				this.protocolNegotiated = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001DB04 File Offset: 0x0001BD04
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001DB60 File Offset: 0x0001BD60
		public SecurityProtocolType SecurityProtocol
		{
			get
			{
				if ((this.securityProtocol & SecurityProtocolType.Tls) == SecurityProtocolType.Tls || (this.securityProtocol & SecurityProtocolType.Default) == SecurityProtocolType.Default)
				{
					return SecurityProtocolType.Tls;
				}
				if ((this.securityProtocol & SecurityProtocolType.Ssl3) == SecurityProtocolType.Ssl3)
				{
					return SecurityProtocolType.Ssl3;
				}
				throw new NotSupportedException("Unsupported security protocol type");
			}
			set
			{
				this.securityProtocol = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public SecurityProtocolType SecurityProtocolFlags
		{
			get
			{
				return this.securityProtocol;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001DB74 File Offset: 0x0001BD74
		public short Protocol
		{
			get
			{
				SecurityProtocolType securityProtocolType = this.SecurityProtocol;
				if (securityProtocolType != SecurityProtocolType.Default)
				{
					if (securityProtocolType != SecurityProtocolType.Ssl2)
					{
						if (securityProtocolType == SecurityProtocolType.Ssl3)
						{
							return 768;
						}
						if (securityProtocolType == SecurityProtocolType.Tls)
						{
							return 769;
						}
					}
					throw new NotSupportedException("Unsupported security protocol type");
				}
				return 769;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		public byte[] SessionId
		{
			get
			{
				return this.sessionId;
			}
			set
			{
				this.sessionId = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0001DBE8 File Offset: 0x0001BDE8
		public SecurityCompressionType CompressionMethod
		{
			get
			{
				return this.compressionMethod;
			}
			set
			{
				this.compressionMethod = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		public TlsServerSettings ServerSettings
		{
			get
			{
				return this.serverSettings;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public TlsClientSettings ClientSettings
		{
			get
			{
				return this.clientSettings;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001DC04 File Offset: 0x0001BE04
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		public HandshakeType LastHandshakeMsg
		{
			get
			{
				return this.lastHandshakeMsg;
			}
			set
			{
				this.lastHandshakeMsg = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001DC18 File Offset: 0x0001BE18
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public HandshakeState HandshakeState
		{
			get
			{
				return this.handshakeState;
			}
			set
			{
				this.handshakeState = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0001DC34 File Offset: 0x0001BE34
		public bool ReceivedConnectionEnd
		{
			get
			{
				return this.receivedConnectionEnd;
			}
			set
			{
				this.receivedConnectionEnd = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001DC40 File Offset: 0x0001BE40
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0001DC48 File Offset: 0x0001BE48
		public bool SentConnectionEnd
		{
			get
			{
				return this.sentConnectionEnd;
			}
			set
			{
				this.sentConnectionEnd = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001DC54 File Offset: 0x0001BE54
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		public CipherSuiteCollection SupportedCiphers
		{
			get
			{
				return this.supportedCiphers;
			}
			set
			{
				this.supportedCiphers = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001DC68 File Offset: 0x0001BE68
		public TlsStream HandshakeMessages
		{
			get
			{
				return this.handshakeMessages;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001DC70 File Offset: 0x0001BE70
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001DC78 File Offset: 0x0001BE78
		public ulong WriteSequenceNumber
		{
			get
			{
				return this.writeSequenceNumber;
			}
			set
			{
				this.writeSequenceNumber = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001DC84 File Offset: 0x0001BE84
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public ulong ReadSequenceNumber
		{
			get
			{
				return this.readSequenceNumber;
			}
			set
			{
				this.readSequenceNumber = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001DC98 File Offset: 0x0001BE98
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		public byte[] ClientRandom
		{
			get
			{
				return this.clientRandom;
			}
			set
			{
				this.clientRandom = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		public byte[] ServerRandom
		{
			get
			{
				return this.serverRandom;
			}
			set
			{
				this.serverRandom = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		public byte[] RandomCS
		{
			get
			{
				return this.randomCS;
			}
			set
			{
				this.randomCS = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001DCD4 File Offset: 0x0001BED4
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		public byte[] RandomSC
		{
			get
			{
				return this.randomSC;
			}
			set
			{
				this.randomSC = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001DCF0 File Offset: 0x0001BEF0
		public byte[] MasterSecret
		{
			get
			{
				return this.masterSecret;
			}
			set
			{
				this.masterSecret = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001DCFC File Offset: 0x0001BEFC
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001DD04 File Offset: 0x0001BF04
		public byte[] ClientWriteKey
		{
			get
			{
				return this.clientWriteKey;
			}
			set
			{
				this.clientWriteKey = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001DD10 File Offset: 0x0001BF10
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001DD18 File Offset: 0x0001BF18
		public byte[] ServerWriteKey
		{
			get
			{
				return this.serverWriteKey;
			}
			set
			{
				this.serverWriteKey = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001DD24 File Offset: 0x0001BF24
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public byte[] ClientWriteIV
		{
			get
			{
				return this.clientWriteIV;
			}
			set
			{
				this.clientWriteIV = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001DD38 File Offset: 0x0001BF38
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0001DD40 File Offset: 0x0001BF40
		public byte[] ServerWriteIV
		{
			get
			{
				return this.serverWriteIV;
			}
			set
			{
				this.serverWriteIV = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0001DD54 File Offset: 0x0001BF54
		public RecordProtocol RecordProtocol
		{
			get
			{
				return this.recordProtocol;
			}
			set
			{
				this.recordProtocol = value;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public int GetUnixTime()
		{
			return (int)((DateTime.UtcNow.Ticks - 621355968000000000L) / 10000000L);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		public byte[] GetSecureRandomBytes(int count)
		{
			byte[] array = new byte[count];
			this.random.GetNonZeroBytes(array);
			return array;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public virtual void Clear()
		{
			this.compressionMethod = SecurityCompressionType.None;
			this.serverSettings = new TlsServerSettings();
			this.clientSettings = new TlsClientSettings();
			this.handshakeMessages = new TlsStream();
			this.sessionId = null;
			this.handshakeState = HandshakeState.None;
			this.ClearKeyInfo();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001DDFC File Offset: 0x0001BFFC
		public virtual void ClearKeyInfo()
		{
			if (this.masterSecret != null)
			{
				Array.Clear(this.masterSecret, 0, this.masterSecret.Length);
				this.masterSecret = null;
			}
			if (this.clientRandom != null)
			{
				Array.Clear(this.clientRandom, 0, this.clientRandom.Length);
				this.clientRandom = null;
			}
			if (this.serverRandom != null)
			{
				Array.Clear(this.serverRandom, 0, this.serverRandom.Length);
				this.serverRandom = null;
			}
			if (this.randomCS != null)
			{
				Array.Clear(this.randomCS, 0, this.randomCS.Length);
				this.randomCS = null;
			}
			if (this.randomSC != null)
			{
				Array.Clear(this.randomSC, 0, this.randomSC.Length);
				this.randomSC = null;
			}
			if (this.clientWriteKey != null)
			{
				Array.Clear(this.clientWriteKey, 0, this.clientWriteKey.Length);
				this.clientWriteKey = null;
			}
			if (this.clientWriteIV != null)
			{
				Array.Clear(this.clientWriteIV, 0, this.clientWriteIV.Length);
				this.clientWriteIV = null;
			}
			if (this.serverWriteKey != null)
			{
				Array.Clear(this.serverWriteKey, 0, this.serverWriteKey.Length);
				this.serverWriteKey = null;
			}
			if (this.serverWriteIV != null)
			{
				Array.Clear(this.serverWriteIV, 0, this.serverWriteIV.Length);
				this.serverWriteIV = null;
			}
			this.handshakeMessages.Reset();
			if (this.securityProtocol != SecurityProtocolType.Ssl3)
			{
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001DF78 File Offset: 0x0001C178
		public SecurityProtocolType DecodeProtocolCode(short code)
		{
			if (code == 768)
			{
				return SecurityProtocolType.Ssl3;
			}
			if (code != 769)
			{
				throw new NotSupportedException("Unsupported security protocol type");
			}
			return SecurityProtocolType.Tls;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public void ChangeProtocol(short protocol)
		{
			SecurityProtocolType securityProtocolType = this.DecodeProtocolCode(protocol);
			if ((securityProtocolType & this.SecurityProtocolFlags) == securityProtocolType || (this.SecurityProtocolFlags & SecurityProtocolType.Default) == SecurityProtocolType.Default)
			{
				this.SecurityProtocol = securityProtocolType;
				this.SupportedCiphers.Clear();
				this.SupportedCiphers = null;
				this.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(securityProtocolType);
				return;
			}
			throw new TlsException(AlertDescription.ProtocolVersion, "Incorrect protocol version received from server");
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0001E028 File Offset: 0x0001C228
		public SecurityParameters Current
		{
			get
			{
				if (this.current == null)
				{
					this.current = new SecurityParameters();
				}
				if (this.current.Cipher != null)
				{
					this.current.Cipher.Context = this;
				}
				return this.current;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001E068 File Offset: 0x0001C268
		public SecurityParameters Negotiating
		{
			get
			{
				if (this.negotiating == null)
				{
					this.negotiating = new SecurityParameters();
				}
				if (this.negotiating.Cipher != null)
				{
					this.negotiating.Cipher.Context = this;
				}
				return this.negotiating;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public SecurityParameters Read
		{
			get
			{
				return this.read;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001E0B0 File Offset: 0x0001C2B0
		public SecurityParameters Write
		{
			get
			{
				return this.write;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		public void StartSwitchingSecurityParameters(bool client)
		{
			if (client)
			{
				this.write = this.negotiating;
				this.read = this.current;
			}
			else
			{
				this.read = this.negotiating;
				this.write = this.current;
			}
			this.current = this.negotiating;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001E10C File Offset: 0x0001C30C
		public void EndSwitchingSecurityParameters(bool client)
		{
			SecurityParameters securityParameters;
			if (client)
			{
				securityParameters = this.read;
				this.read = this.current;
			}
			else
			{
				securityParameters = this.write;
				this.write = this.current;
			}
			if (securityParameters != null)
			{
				securityParameters.Clear();
			}
			this.negotiating = securityParameters;
		}

		// Token: 0x04000263 RID: 611
		internal const short MAX_FRAGMENT_SIZE = 16384;

		// Token: 0x04000264 RID: 612
		internal const short TLS1_PROTOCOL_CODE = 769;

		// Token: 0x04000265 RID: 613
		internal const short SSL3_PROTOCOL_CODE = 768;

		// Token: 0x04000266 RID: 614
		internal const long UNIX_BASE_TICKS = 621355968000000000L;

		// Token: 0x04000267 RID: 615
		private SecurityProtocolType securityProtocol;

		// Token: 0x04000268 RID: 616
		private byte[] sessionId;

		// Token: 0x04000269 RID: 617
		private SecurityCompressionType compressionMethod;

		// Token: 0x0400026A RID: 618
		private TlsServerSettings serverSettings;

		// Token: 0x0400026B RID: 619
		private TlsClientSettings clientSettings;

		// Token: 0x0400026C RID: 620
		private SecurityParameters current;

		// Token: 0x0400026D RID: 621
		private SecurityParameters negotiating;

		// Token: 0x0400026E RID: 622
		private SecurityParameters read;

		// Token: 0x0400026F RID: 623
		private SecurityParameters write;

		// Token: 0x04000270 RID: 624
		private CipherSuiteCollection supportedCiphers;

		// Token: 0x04000271 RID: 625
		private HandshakeType lastHandshakeMsg;

		// Token: 0x04000272 RID: 626
		private HandshakeState handshakeState;

		// Token: 0x04000273 RID: 627
		private bool abbreviatedHandshake;

		// Token: 0x04000274 RID: 628
		private bool receivedConnectionEnd;

		// Token: 0x04000275 RID: 629
		private bool sentConnectionEnd;

		// Token: 0x04000276 RID: 630
		private bool protocolNegotiated;

		// Token: 0x04000277 RID: 631
		private ulong writeSequenceNumber;

		// Token: 0x04000278 RID: 632
		private ulong readSequenceNumber;

		// Token: 0x04000279 RID: 633
		private byte[] clientRandom;

		// Token: 0x0400027A RID: 634
		private byte[] serverRandom;

		// Token: 0x0400027B RID: 635
		private byte[] randomCS;

		// Token: 0x0400027C RID: 636
		private byte[] randomSC;

		// Token: 0x0400027D RID: 637
		private byte[] masterSecret;

		// Token: 0x0400027E RID: 638
		private byte[] clientWriteKey;

		// Token: 0x0400027F RID: 639
		private byte[] serverWriteKey;

		// Token: 0x04000280 RID: 640
		private byte[] clientWriteIV;

		// Token: 0x04000281 RID: 641
		private byte[] serverWriteIV;

		// Token: 0x04000282 RID: 642
		private TlsStream handshakeMessages;

		// Token: 0x04000283 RID: 643
		private RandomNumberGenerator random;

		// Token: 0x04000284 RID: 644
		private RecordProtocol recordProtocol;
	}
}
