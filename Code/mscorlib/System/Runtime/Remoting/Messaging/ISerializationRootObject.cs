using System;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004A2 RID: 1186
	internal interface ISerializationRootObject
	{
		// Token: 0x06003002 RID: 12290
		void RootSetObjectData(SerializationInfo info, StreamingContext context);
	}
}
