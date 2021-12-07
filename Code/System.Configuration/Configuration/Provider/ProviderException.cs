using System;
using System.Runtime.Serialization;

namespace System.Configuration.Provider
{
	/// <summary>The exception that is thrown when a configuration provider error has occurred. This exception class is also used by providers to throw exceptions when internal errors occur within the provider that do not map to other pre-existing exception classes.</summary>
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class ProviderException : Exception
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		// Token: 0x06000091 RID: 145 RVA: 0x00002498 File Offset: 0x00000698
		public ProviderException()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="info">The object that holds the information to deserialize.</param>
		/// <param name="context">Contextual information about the source or destination.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x000024A0 File Offset: 0x000006A0
		protected ProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x000024AC File Offset: 0x000006AC
		public ProviderException(string message) : base(message)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.Provider.ProviderException" /> class.</summary>
		/// <param name="message">A message describing why this <see cref="T:System.Configuration.Provider.ProviderException" /> was thrown.</param>
		/// <param name="innerException">The exception that caused this <see cref="T:System.Configuration.Provider.ProviderException" /> to be thrown.</param>
		// Token: 0x06000094 RID: 148 RVA: 0x000024B8 File Offset: 0x000006B8
		public ProviderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
