using System;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error is made while using a network protocol.</summary>
	// Token: 0x020003DA RID: 986
	[Serializable]
	public class ProtocolViolationException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class.</summary>
		// Token: 0x060021AD RID: 8621 RVA: 0x00062988 File Offset: 0x00060B88
		public ProtocolViolationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class with the specified message.</summary>
		/// <param name="message">The error message string. </param>
		// Token: 0x060021AE RID: 8622 RVA: 0x00062990 File Offset: 0x00060B90
		public ProtocolViolationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.ProtocolViolationException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information that is required to deserialize the <see cref="T:System.Net.ProtocolViolationException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.ProtocolViolationException" />. </param>
		// Token: 0x060021AF RID: 8623 RVA: 0x0006299C File Offset: 0x00060B9C
		protected ProtocolViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.ProtocolViolationException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060021B0 RID: 8624 RVA: 0x000629A8 File Offset: 0x00060BA8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060021B1 RID: 8625 RVA: 0x000629B4 File Offset: 0x00060BB4
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
