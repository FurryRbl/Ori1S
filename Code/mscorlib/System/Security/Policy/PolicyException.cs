using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	/// <summary>The exception that is thrown when policy forbids code to run.</summary>
	// Token: 0x0200064B RID: 1611
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException, _Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with default properties.</summary>
		// Token: 0x06003D3D RID: 15677 RVA: 0x000D27AC File Offset: 0x000D09AC
		public PolicyException() : base(Locale.GetText("Cannot run because of policy."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003D3E RID: 15678 RVA: 0x000D27C0 File Offset: 0x000D09C0
		public PolicyException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06003D3F RID: 15679 RVA: 0x000D27CC File Offset: 0x000D09CC
		protected PolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="exception">The exception that is the cause of the current exception. If the <paramref name="exception" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003D40 RID: 15680 RVA: 0x000D27D8 File Offset: 0x000D09D8
		public PolicyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
