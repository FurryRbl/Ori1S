using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown in <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> when the filter criteria is not valid for the type of filter you are using.</summary>
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the default properties.</summary>
		// Token: 0x06002195 RID: 8597 RVA: 0x0007A59C File Offset: 0x0007879C
		public InvalidFilterCriteriaException() : base(Locale.GetText("Filter Criteria is not valid."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the given HRESULT and message string.</summary>
		/// <param name="message">The message text for the exception. </param>
		// Token: 0x06002196 RID: 8598 RVA: 0x0007A5B0 File Offset: 0x000787B0
		public InvalidFilterCriteriaException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002197 RID: 8599 RVA: 0x0007A5BC File Offset: 0x000787BC
		public InvalidFilterCriteriaException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.InvalidFilterCriteriaException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">A SerializationInfo object that contains the information required to serialize this instance. </param>
		/// <param name="context">A StreamingContext object that contains the source and destination of the serialized stream associated with this instance. </param>
		// Token: 0x06002198 RID: 8600 RVA: 0x0007A5C8 File Offset: 0x000787C8
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
