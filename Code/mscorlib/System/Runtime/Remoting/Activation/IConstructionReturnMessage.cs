using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Identifies a <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> that is returned after attempting to activate a remote object.</summary>
	// Token: 0x02000440 RID: 1088
	[ComVisible(true)]
	public interface IConstructionReturnMessage : IMessage, IMethodMessage, IMethodReturnMessage
	{
	}
}
