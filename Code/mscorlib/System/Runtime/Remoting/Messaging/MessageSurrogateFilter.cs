using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Determines whether the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> class should ignore a particular <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> property while creating an <see cref="T:System.Runtime.Remoting.ObjRef" /> for a <see cref="T:System.MarshalByRefObject" /> class.</summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	// Token: 0x020006F7 RID: 1783
	// (Invoke) Token: 0x060043D0 RID: 17360
	[ComVisible(true)]
	public delegate bool MessageSurrogateFilter(string key, object value);
}
