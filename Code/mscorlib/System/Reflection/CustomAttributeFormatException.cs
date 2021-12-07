using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the binary format of a custom attribute is invalid.</summary>
	// Token: 0x02000288 RID: 648
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the default properties.</summary>
		// Token: 0x0600213A RID: 8506 RVA: 0x00079C40 File Offset: 0x00077E40
		public CustomAttributeFormatException() : base(Locale.GetText("The Binary format of the custom attribute is invalid."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason this exception was thrown. </param>
		// Token: 0x0600213B RID: 8507 RVA: 0x00079C54 File Offset: 0x00077E54
		public CustomAttributeFormatException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600213C RID: 8508 RVA: 0x00079C60 File Offset: 0x00077E60
		public CustomAttributeFormatException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeFormatException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the custom attribute. </param>
		/// <param name="context">The source and destination for the custom attribute. </param>
		// Token: 0x0600213D RID: 8509 RVA: 0x00079C6C File Offset: 0x00077E6C
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
