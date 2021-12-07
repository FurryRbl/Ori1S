using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates the status of the server message processing.</summary>
	// Token: 0x02000465 RID: 1125
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		/// <summary>The server synchronously processed the message.</summary>
		// Token: 0x040013DE RID: 5086
		Complete,
		/// <summary>The message was dispatched and no response can be sent.</summary>
		// Token: 0x040013DF RID: 5087
		OneWay,
		/// <summary>The call was dispatched asynchronously, which indicates that the sink must store response data on the stack for later processing.</summary>
		// Token: 0x040013E0 RID: 5088
		Async
	}
}
