﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to dereference a null object reference.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000162 RID: 354
	[ComVisible(true)]
	[Serializable]
	public class NullReferenceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "The value 'null' was found where an instance of an object was required." This message takes into account the current system culture.</summary>
		// Token: 0x060012D4 RID: 4820 RVA: 0x00049810 File Offset: 0x00047A10
		public NullReferenceException() : base(Locale.GetText("A null value was found where an object instance was required."))
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture. </param>
		// Token: 0x060012D5 RID: 4821 RVA: 0x00049830 File Offset: 0x00047A30
		public NullReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x00049844 File Offset: 0x00047A44
		public NullReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.NullReferenceException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x060012D7 RID: 4823 RVA: 0x0004985C File Offset: 0x00047A5C
		protected NullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400054D RID: 1357
		private const int Result = -2147467261;
	}
}
