using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception thrown if the main assembly does not contain the resources for the neutral culture, and they are required because of a missing appropriate satellite assembly.</summary>
	// Token: 0x02000304 RID: 772
	[ComVisible(true)]
	[Serializable]
	public class MissingManifestResourceException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with default properties.</summary>
		// Token: 0x060027DC RID: 10204 RVA: 0x0008DEAC File Offset: 0x0008C0AC
		public MissingManifestResourceException() : base(Locale.GetText("The assembly does not contain the resources for the required culture."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x060027DD RID: 10205 RVA: 0x0008DEC0 File Offset: 0x0008C0C0
		public MissingManifestResourceException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		// Token: 0x060027DE RID: 10206 RVA: 0x0008DECC File Offset: 0x0008C0CC
		protected MissingManifestResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingManifestResourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060027DF RID: 10207 RVA: 0x0008DED8 File Offset: 0x0008C0D8
		public MissingManifestResourceException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
