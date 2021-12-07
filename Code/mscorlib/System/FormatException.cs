using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the format of an argument does not meet the parameter specifications of the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000139 RID: 313
	[ComVisible(true)]
	[Serializable]
	public class FormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class.</summary>
		// Token: 0x06001134 RID: 4404 RVA: 0x000461AC File Offset: 0x000443AC
		public FormatException() : base(Locale.GetText("Invalid format."))
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001135 RID: 4405 RVA: 0x000461CC File Offset: 0x000443CC
		public FormatException(string message) : base(message)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001136 RID: 4406 RVA: 0x000461E0 File Offset: 0x000443E0
		public FormatException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001137 RID: 4407 RVA: 0x000461F8 File Offset: 0x000443F8
		protected FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040004FF RID: 1279
		private const int Result = -2146233033;
	}
}
