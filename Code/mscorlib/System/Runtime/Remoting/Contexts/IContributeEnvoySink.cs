﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Contributes an envoy message sink on the client end.</summary>
	// Token: 0x0200047B RID: 1147
	[ComVisible(true)]
	public interface IContributeEnvoySink
	{
		/// <summary>Takes the first sink in the chain of sinks composed so far, and then chains its message sink in front of the chain already formed.</summary>
		/// <returns>The composite sink chain.</returns>
		/// <param name="obj">The server object for which the chain is being created. </param>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		// Token: 0x06002F1D RID: 12061
		IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
