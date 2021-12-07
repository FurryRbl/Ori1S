using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Contributes an object-specific interception sink on the server end of a remoting call.</summary>
	// Token: 0x0200047C RID: 1148
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		/// <summary>Chains the message sink of the provided server object in front of the given sink chain.</summary>
		/// <returns>The composite sink chain.</returns>
		/// <param name="obj">The server object which provides the message sink that is to be chained in front of the given chain. </param>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		// Token: 0x06002F1E RID: 12062
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
