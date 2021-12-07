using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when there is a syntax error in XML parsing. This class cannot be inherited.</summary>
	// Token: 0x02000553 RID: 1363
	[ComVisible(true)]
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with default properties.</summary>
		// Token: 0x06003591 RID: 13713 RVA: 0x000B12AC File Offset: 0x000AF4AC
		public XmlSyntaxException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		// Token: 0x06003592 RID: 13714 RVA: 0x000B12B4 File Offset: 0x000AF4B4
		public XmlSyntaxException(int lineNumber) : base(string.Format(Locale.GetText("Invalid syntax on line {0}."), lineNumber))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected. </param>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003593 RID: 13715 RVA: 0x000B12D4 File Offset: 0x000AF4D4
		public XmlSyntaxException(int lineNumber, string message) : base(string.Format(Locale.GetText("Invalid syntax on line {0} - {1}."), lineNumber, message))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003594 RID: 13716 RVA: 0x000B12F4 File Offset: 0x000AF4F4
		public XmlSyntaxException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003595 RID: 13717 RVA: 0x000B1300 File Offset: 0x000AF500
		public XmlSyntaxException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000B130C File Offset: 0x000AF50C
		internal XmlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
