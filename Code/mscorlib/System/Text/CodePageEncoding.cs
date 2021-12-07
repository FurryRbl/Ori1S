using System;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x0200066F RID: 1647
	[Serializable]
	internal sealed class CodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06003E99 RID: 16025 RVA: 0x000D70C4 File Offset: 0x000D52C4
		private CodePageEncoding(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06003E9A RID: 16026 RVA: 0x000D7198 File Offset: 0x000D5398
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException("This class cannot be serialized.");
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000D71A4 File Offset: 0x000D53A4
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

		// Token: 0x04001B2B RID: 6955
		private int codePage;

		// Token: 0x04001B2C RID: 6956
		private bool isReadOnly;

		// Token: 0x04001B2D RID: 6957
		private EncoderFallback encoderFallback;

		// Token: 0x04001B2E RID: 6958
		private DecoderFallback decoderFallback;

		// Token: 0x04001B2F RID: 6959
		private Encoding realObject;

		// Token: 0x02000670 RID: 1648
		[Serializable]
		private sealed class Decoder : ISerializable, IObjectReference
		{
			// Token: 0x06003E9C RID: 16028 RVA: 0x000D7204 File Offset: 0x000D5404
			private Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x06003E9D RID: 16029 RVA: 0x000D7240 File Offset: 0x000D5440
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException("This class cannot be serialized.");
			}

			// Token: 0x06003E9E RID: 16030 RVA: 0x000D724C File Offset: 0x000D544C
			public object GetRealObject(StreamingContext context)
			{
				if (this.realObject == null)
				{
					this.realObject = this.encoding.GetDecoder();
				}
				return this.realObject;
			}

			// Token: 0x04001B30 RID: 6960
			private Encoding encoding;

			// Token: 0x04001B31 RID: 6961
			private System.Text.Decoder realObject;
		}
	}
}
