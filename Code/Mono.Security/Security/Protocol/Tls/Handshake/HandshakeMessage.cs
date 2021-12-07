using System;

namespace Mono.Security.Protocol.Tls.Handshake
{
	// Token: 0x020000A5 RID: 165
	internal abstract class HandshakeMessage : TlsStream
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x00023488 File Offset: 0x00021688
		public HandshakeMessage(Context context, HandshakeType handshakeType) : this(context, handshakeType, ContentType.Handshake)
		{
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00023494 File Offset: 0x00021694
		public HandshakeMessage(Context context, HandshakeType handshakeType, ContentType contentType)
		{
			this.context = context;
			this.handshakeType = handshakeType;
			this.contentType = contentType;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000234B4 File Offset: 0x000216B4
		public HandshakeMessage(Context context, HandshakeType handshakeType, byte[] data) : base(data)
		{
			this.context = context;
			this.handshakeType = handshakeType;
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000234CC File Offset: 0x000216CC
		public Context Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000234D4 File Offset: 0x000216D4
		public HandshakeType HandshakeType
		{
			get
			{
				return this.handshakeType;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000234DC File Offset: 0x000216DC
		public ContentType ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x06000658 RID: 1624
		protected abstract void ProcessAsTls1();

		// Token: 0x06000659 RID: 1625
		protected abstract void ProcessAsSsl3();

		// Token: 0x0600065A RID: 1626 RVA: 0x000234E4 File Offset: 0x000216E4
		public void Process()
		{
			SecurityProtocolType securityProtocol = this.Context.SecurityProtocol;
			if (securityProtocol != SecurityProtocolType.Default)
			{
				if (securityProtocol != SecurityProtocolType.Ssl2)
				{
					if (securityProtocol == SecurityProtocolType.Ssl3)
					{
						this.ProcessAsSsl3();
						return;
					}
					if (securityProtocol == SecurityProtocolType.Tls)
					{
						goto IL_37;
					}
				}
				throw new NotSupportedException("Unsupported security protocol type");
			}
			IL_37:
			this.ProcessAsTls1();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0002354C File Offset: 0x0002174C
		public virtual void Update()
		{
			if (this.CanWrite)
			{
				if (this.cache == null)
				{
					this.cache = this.EncodeMessage();
				}
				this.context.HandshakeMessages.Write(this.cache);
				base.Reset();
				this.cache = null;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000235A0 File Offset: 0x000217A0
		public virtual byte[] EncodeMessage()
		{
			this.cache = null;
			if (this.CanWrite)
			{
				byte[] array = base.ToArray();
				int num = array.Length;
				this.cache = new byte[4 + num];
				this.cache[0] = (byte)this.HandshakeType;
				this.cache[1] = (byte)(num >> 16);
				this.cache[2] = (byte)(num >> 8);
				this.cache[3] = (byte)num;
				Buffer.BlockCopy(array, 0, this.cache, 4, num);
			}
			return this.cache;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00023620 File Offset: 0x00021820
		public static bool Compare(byte[] buffer1, byte[] buffer2)
		{
			if (buffer1 == null || buffer2 == null)
			{
				return false;
			}
			if (buffer1.Length != buffer2.Length)
			{
				return false;
			}
			for (int i = 0; i < buffer1.Length; i++)
			{
				if (buffer1[i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400030B RID: 779
		private Context context;

		// Token: 0x0400030C RID: 780
		private HandshakeType handshakeType;

		// Token: 0x0400030D RID: 781
		private ContentType contentType;

		// Token: 0x0400030E RID: 782
		private byte[] cache;
	}
}
