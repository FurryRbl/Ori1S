using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B3 RID: 1203
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x060030DD RID: 12509 RVA: 0x000A09E0 File Offset: 0x0009EBE0
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(si, sc);
			}
			else
			{
				RemotingServices.GetObjectData(obj, si, sc);
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000A0A28 File Offset: 0x0009EC28
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException();
		}
	}
}
