using System;
using System.Runtime.Serialization;

namespace System.Text
{
	/// <summary>Represents a code page encoding.</summary>
	// Token: 0x02000686 RID: 1670
	[Serializable]
	internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06003F81 RID: 16257 RVA: 0x000D9DCC File Offset: 0x000D7FCC
		private MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.codePage = (int)info.GetValue("m_codePage", typeof(int));
			try
			{
				this.isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.isReadOnly = true;
			}
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x000D9EA0 File Offset: 0x000D80A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException("This class cannot be serialized.");
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x000D9EAC File Offset: 0x000D80AC
		public object GetRealObject(StreamingContext context)
		{
			if (this.realObject == null)
			{
				Encoding encoding = Encoding.GetEncoding(this.codePage);
				if (!this.isReadOnly)
				{
					encoding = (Encoding)encoding.Clone();
					encoding.EncoderFallback = this.encoderFallback;
					encoding.DecoderFallback = this.decoderFallback;
				}
				this.realObject = encoding;
			}
			return this.realObject;
		}

		// Token: 0x04001B6E RID: 7022
		private int codePage;

		// Token: 0x04001B6F RID: 7023
		private bool isReadOnly;

		// Token: 0x04001B70 RID: 7024
		private EncoderFallback encoderFallback;

		// Token: 0x04001B71 RID: 7025
		private DecoderFallback decoderFallback;

		// Token: 0x04001B72 RID: 7026
		private Encoding realObject;

		// Token: 0x02000687 RID: 1671
		[Serializable]
		private sealed class MLangEncoder : ISerializable, IObjectReference
		{
			// Token: 0x06003F84 RID: 16260 RVA: 0x000D9F0C File Offset: 0x000D810C
			private MLangEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.encoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06003F85 RID: 16261 RVA: 0x000D9F48 File Offset: 0x000D8148
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException("This class cannot be serialized.");
			}

			// Token: 0x06003F86 RID: 16262 RVA: 0x000D9F54 File Offset: 0x000D8154
			public object GetRealObject(StreamingContext context)
			{
				if (this.realObject == null)
				{
					this.realObject = this.encoding.GetEncoder();
				}
				return this.realObject;
			}

			// Token: 0x04001B73 RID: 7027
			private Encoding encoding;

			// Token: 0x04001B74 RID: 7028
			private Encoder realObject;
		}

		// Token: 0x02000688 RID: 1672
		[Serializable]
		private sealed class MLangDecoder : ISerializable, IObjectReference
		{
			// Token: 0x06003F87 RID: 16263 RVA: 0x000D9F84 File Offset: 0x000D8184
			private MLangDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.encoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06003F88 RID: 16264 RVA: 0x000D9FC0 File Offset: 0x000D81C0
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException("This class cannot be serialized.");
			}

			// Token: 0x06003F89 RID: 16265 RVA: 0x000D9FCC File Offset: 0x000D81CC
			public object GetRealObject(StreamingContext context)
			{
				if (this.realObject == null)
				{
					this.realObject = this.encoding.GetDecoder();
				}
				return this.realObject;
			}

			// Token: 0x04001B75 RID: 7029
			private Encoding encoding;

			// Token: 0x04001B76 RID: 7030
			private Decoder realObject;
		}
	}
}
