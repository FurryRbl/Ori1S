using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream and cannot be retried.</summary>
	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with no message. </summary>
		// Token: 0x06002688 RID: 9864 RVA: 0x00077588 File Offset: 0x00075788
		public InvalidCredentialException() : base(Locale.GetText("Invalid credentials exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06002689 RID: 9865 RVA: 0x0007759C File Offset: 0x0007579C
		public InvalidCredentialException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x0600268A RID: 9866 RVA: 0x000775A8 File Offset: 0x000757A8
		public InvalidCredentialException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.InvalidCredentialException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance. </param>
		// Token: 0x0600268B RID: 9867 RVA: 0x000775B4 File Offset: 0x000757B4
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
