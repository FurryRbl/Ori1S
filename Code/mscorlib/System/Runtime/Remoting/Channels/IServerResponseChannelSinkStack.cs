using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides the stack functionality for a stack of server response channel sinks.</summary>
	// Token: 0x0200045C RID: 1116
	[ComVisible(true)]
	public interface IServerResponseChannelSinkStack
	{
		/// <summary>Requests asynchronous processing of a method call on the sinks in the current sink stack.</summary>
		/// <param name="msg">The response message.</param>
		/// <param name="headers">The headers retrieved from the server response stream.</param>
		/// <param name="stream">The stream coming back from the transport sink.</param>
		// Token: 0x06002E89 RID: 11913
		void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

		/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</summary>
		/// <returns>The <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</returns>
		/// <param name="msg">The message to be serialized onto the requested stream. </param>
		/// <param name="headers">The headers retrieved from the server response stream. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x06002E8A RID: 11914
		Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
	}
}
