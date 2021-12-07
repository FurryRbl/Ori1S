using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an array with the wrong number of dimensions is passed to a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016D RID: 365
	[ComVisible(true)]
	[Serializable]
	public class RankException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class.</summary>
		// Token: 0x06001361 RID: 4961 RVA: 0x0004D900 File Offset: 0x0004BB00
		public RankException() : base(Locale.GetText("Two arrays must have the same number of dimensions."))
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06001362 RID: 4962 RVA: 0x0004D920 File Offset: 0x0004BB20
		public RankException(string message) : base(message)
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001363 RID: 4963 RVA: 0x0004D934 File Offset: 0x0004BB34
		public RankException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001364 RID: 4964 RVA: 0x0004D94C File Offset: 0x0004BB4C
		protected RankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040005A4 RID: 1444
		private const int Result = -2146233065;
	}
}
