using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x02000400 RID: 1024
	[Serializable]
	public class SocketException : System.ComponentModel.Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06002426 RID: 9254 RVA: 0x0006CB90 File Offset: 0x0006AD90
		public SocketException() : base(SocketException.WSAGetLastError_internal())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred. </param>
		// Token: 0x06002427 RID: 9255 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		public SocketException(int error) : base(error)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		// Token: 0x06002428 RID: 9256 RVA: 0x0006CBAC File Offset: 0x0006ADAC
		protected SocketException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		internal SocketException(int error, string message) : base(error, message)
		{
		}

		// Token: 0x0600242A RID: 9258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_internal();

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0006CBC4 File Offset: 0x0006ADC4
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0006CBCC File Offset: 0x0006ADCC
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message. </returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600242D RID: 9261 RVA: 0x0006CBD4 File Offset: 0x0006ADD4
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}
	}
}
