using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x0200068B RID: 1675
	[Serializable]
	internal sealed class SurrogateEncoder : ISerializable, IObjectReference
	{
		// Token: 0x06003FD1 RID: 16337 RVA: 0x000DB038 File Offset: 0x000D9238
		private SurrogateEncoder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.encoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000DB074 File Offset: 0x000D9274
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException("This class cannot be serialized.");
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000DB080 File Offset: 0x000D9280
		public object GetRealObject(StreamingContext context)
		{
			if (this.realObject == null)
			{
				this.realObject = this.encoding.GetEncoder();
			}
			return this.realObject;
		}

		// Token: 0x04001B81 RID: 7041
		private Encoding encoding;

		// Token: 0x04001B82 RID: 7042
		private Encoder realObject;
	}
}
