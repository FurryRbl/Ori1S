using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x0200028A RID: 650
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x060016CD RID: 5837 RVA: 0x0003EA20 File Offset: 0x0003CC20
		public InvalidDataException() : base(Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060016CE RID: 5838 RVA: 0x0003EA40 File Offset: 0x0003CC40
		public InvalidDataException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x060016CF RID: 5839 RVA: 0x0003EA54 File Offset: 0x0003CC54
		public InvalidDataException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0003EA6C File Offset: 0x0003CC6C
		private InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400076E RID: 1902
		private const int Result = -2146233085;
	}
}
