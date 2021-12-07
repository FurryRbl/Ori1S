using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Marks a sink as a client formatter sink that serializes messages into a stream.</summary>
	// Token: 0x02000458 RID: 1112
	[ComVisible(true)]
	public interface IClientFormatterSink : IChannelSinkBase, IClientChannelSink, IMessageSink
	{
	}
}
