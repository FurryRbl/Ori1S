using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Wraps remoting data for passing between message sinks, either for requests from client to server or for the subsequent responses.</summary>
	// Token: 0x020004A1 RID: 1185
	[ComVisible(true)]
	public class InternalMessageWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.InternalMessageWrapper" /> class. </summary>
		/// <param name="msg">A message that acts either as an outgoing method call on a remote object, or as the subsequent response.</param>
		// Token: 0x06003001 RID: 12289 RVA: 0x0009DEA4 File Offset: 0x0009C0A4
		public InternalMessageWrapper(IMessage msg)
		{
			this.WrappedMessage = msg;
		}

		/// <summary>Represents the request or response <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> interface that is wrapped by the message wrapper. </summary>
		// Token: 0x0400145D RID: 5213
		protected IMessage WrappedMessage;
	}
}
