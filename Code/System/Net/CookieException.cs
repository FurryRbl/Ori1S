using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made adding a <see cref="T:System.Net.Cookie" /> to a <see cref="T:System.Net.CookieContainer" />.</summary>
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class.</summary>
		// Token: 0x060019E4 RID: 6628 RVA: 0x00047B34 File Offset: 0x00045D34
		public CookieException()
		{
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00047B3C File Offset: 0x00045D3C
		internal CookieException(string msg) : base(msg)
		{
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00047B48 File Offset: 0x00045D48
		internal CookieException(string msg, Exception e) : base(msg, e)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.CookieException" /> class with specific values of <paramref name="serializationInfo" /> and <paramref name="streamingContext" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00047B54 File Offset: 0x00045D54
		protected CookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x060019E8 RID: 6632 RVA: 0x00047B60 File Offset: 0x00045D60
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.CookieException" />.</summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="streamingContext">The contextual information about the source or destination. A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060019E9 RID: 6633 RVA: 0x00047B6C File Offset: 0x00045D6C
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
