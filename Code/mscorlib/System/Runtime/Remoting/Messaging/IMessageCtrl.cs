using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a way to control asynchronous messages after they have dispatched using the <see cref="M:System.Runtime.Remoting.Messaging.IMessageSink.AsyncProcessMessage(System.Runtime.Remoting.Messaging.IMessage,System.Runtime.Remoting.Messaging.IMessageSink)" />.</summary>
	// Token: 0x0200049B RID: 1179
	[ComVisible(true)]
	public interface IMessageCtrl
	{
		/// <summary>Cancels an asynchronous call.</summary>
		/// <param name="msToCancel">The number of milliseconds after which to cancel the message. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06002FE6 RID: 12262
		void Cancel(int msToCancel);
	}
}
