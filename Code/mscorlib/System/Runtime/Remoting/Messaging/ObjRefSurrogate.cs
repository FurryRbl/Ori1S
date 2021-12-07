using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B4 RID: 1204
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x060030E0 RID: 12512 RVA: 0x000A0A38 File Offset: 0x0009EC38
		public virtual void GetObjectData(object obj, SerializationInfo si, StreamingContext sc)
		{
			if (obj == null || si == null)
			{
				throw new ArgumentNullException();
			}
			((ObjRef)obj).GetObjectData(si, sc);
			si.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000A0A68 File Offset: 0x0009EC68
		public virtual object SetObjectData(object obj, SerializationInfo si, StreamingContext sc, ISurrogateSelector selector)
		{
			throw new NotSupportedException("Do not use RemotingSurrogateSelector when deserializating");
		}
	}
}
