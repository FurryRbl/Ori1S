using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown by the marshaler when it encounters an argument of a variant type that can not be marshaled to managed code.</summary>
	// Token: 0x020003A8 RID: 936
	[ComVisible(true)]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class with default values.</summary>
		// Token: 0x06002A9A RID: 10906 RVA: 0x000929E4 File Offset: 0x00090BE4
		public InvalidOleVariantTypeException() : base(Locale.GetText("Found native variant type cannot be marshalled to managed code"))
		{
			base.HResult = -2146233039;
		}

		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class with a specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception. </param>
		// Token: 0x06002A9B RID: 10907 RVA: 0x00092A04 File Offset: 0x00090C04
		public InvalidOleVariantTypeException(string message) : base(message)
		{
			base.HResult = -2146233039;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002A9C RID: 10908 RVA: 0x00092A18 File Offset: 0x00090C18
		public InvalidOleVariantTypeException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233039;
		}

		/// <summary>Initializes a new instance of the InvalidOleVariantTypeException class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06002A9D RID: 10909 RVA: 0x00092A30 File Offset: 0x00090C30
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04001153 RID: 4435
		private const int ErrorCode = -2146233039;
	}
}
