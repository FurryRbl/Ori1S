using System;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when a <see cref="Overload:System.Net.NetworkInformation.Ping.Send" /> or <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> method calls a method that throws an exception.</summary>
	// Token: 0x020003B6 RID: 950
	[Serializable]
	public class PingException : InvalidOperationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class using the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06002115 RID: 8469 RVA: 0x00061F7C File Offset: 0x0006017C
		public PingException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" />  class using the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		/// <param name="innerException">The exception that causes the current exception.</param>
		// Token: 0x06002116 RID: 8470 RVA: 0x00061F88 File Offset: 0x00060188
		public PingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingException" /> class with serialized data. </summary>
		/// <param name="serializationInfo">The object that holds the serialized object data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the contextual information about the source or destination for this serialization.</param>
		// Token: 0x06002117 RID: 8471 RVA: 0x00061F94 File Offset: 0x00060194
		protected PingException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
