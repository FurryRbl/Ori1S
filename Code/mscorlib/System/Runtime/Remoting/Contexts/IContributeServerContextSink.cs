﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Contributes an interception sink at the context boundary on the server end of a remoting call.</summary>
	// Token: 0x0200047D RID: 1149
	[ComVisible(true)]
	public interface IContributeServerContextSink
	{
		/// <summary>Takes the first sink in the chain of sinks composed so far, and then chains its message sink in front of the chain already formed.</summary>
		/// <returns>The composite sink chain.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		// Token: 0x06002F1F RID: 12063
		IMessageSink GetServerContextSink(IMessageSink nextSink);
	}
}
