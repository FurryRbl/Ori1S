using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000078 RID: 120
	internal class UnixBinaryClientFormatterSinkProvider : IClientChannelSinkProvider, IClientFormatterSinkProvider
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public UnixBinaryClientFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public UnixBinaryClientFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryClientFormatterSinkProvider.allowedProperties);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		public IClientChannelSinkProvider Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			IClientChannelSink nextSink = null;
			if (this.next != null)
			{
				nextSink = this.next.CreateSink(channel, url, remoteChannelData);
			}
			return new UnixBinaryClientFormatterSink(nextSink)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x0400041D RID: 1053
		private IClientChannelSinkProvider next;

		// Token: 0x0400041E RID: 1054
		private UnixBinaryCore _binaryCore;

		// Token: 0x0400041F RID: 1055
		private static string[] allowedProperties = new string[]
		{
			"includeVersions",
			"strictBinding"
		};
	}
}
