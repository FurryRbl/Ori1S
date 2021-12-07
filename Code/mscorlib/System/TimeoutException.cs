using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the time allotted for a process or operation has expired.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000186 RID: 390
	[ComVisible(true)]
	[Serializable]
	public class TimeoutException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class.</summary>
		// Token: 0x06001450 RID: 5200 RVA: 0x00051EC0 File Offset: 0x000500C0
		public TimeoutException() : base(Locale.GetText("The operation has timed-out."))
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06001451 RID: 5201 RVA: 0x00051EE0 File Offset: 0x000500E0
		public TimeoutException(string message) : base(message)
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">The message that describes the error. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001452 RID: 5202 RVA: 0x00051EF4 File Offset: 0x000500F4
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233083;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination. The <paramref name="context" /> parameter is reserved for future use, and can be specified as null.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null, or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06001453 RID: 5203 RVA: 0x00051F0C File Offset: 0x0005010C
		protected TimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040007E7 RID: 2023
		private const int Result = -2146233083;
	}
}
