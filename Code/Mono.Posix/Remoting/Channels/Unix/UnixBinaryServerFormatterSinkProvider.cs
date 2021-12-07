using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x0200007D RID: 125
	internal class UnixBinaryServerFormatterSinkProvider : IServerChannelSinkProvider, IServerFormatterSinkProvider
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0000E110 File Offset: 0x0000C310
		public UnixBinaryServerFormatterSinkProvider()
		{
			this._binaryCore = UnixBinaryCore.DefaultInstance;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000E124 File Offset: 0x0000C324
		public UnixBinaryServerFormatterSinkProvider(IDictionary properties, ICollection providerData)
		{
			this._binaryCore = new UnixBinaryCore(this, properties, UnixBinaryServerFormatterSinkProvider.AllowedProperties);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000E160 File Offset: 0x0000C360
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000E168 File Offset: 0x0000C368
		public IServerChannelSinkProvider Next
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

		// Token: 0x0600059D RID: 1437 RVA: 0x0000E174 File Offset: 0x0000C374
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			IServerChannelSink nextSink = null;
			if (this.next != null)
			{
				nextSink = this.next.CreateSink(channel);
			}
			return new UnixBinaryServerFormatterSink(nextSink, channel)
			{
				BinaryCore = this._binaryCore
			};
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x0400042C RID: 1068
		private IServerChannelSinkProvider next;

		// Token: 0x0400042D RID: 1069
		private UnixBinaryCore _binaryCore;

		// Token: 0x0400042E RID: 1070
		internal static string[] AllowedProperties = new string[]
		{
			"includeVersions",
			"strictBinding"
		};
	}
}
