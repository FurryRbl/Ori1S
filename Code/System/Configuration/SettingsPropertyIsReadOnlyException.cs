using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for read-only <see cref="T:System.Configuration.SettingsProperty" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F5 RID: 501
	[Serializable]
	public class SettingsPropertyIsReadOnlyException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class.</summary>
		// Token: 0x06001137 RID: 4407 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
		public SettingsPropertyIsReadOnlyException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on a supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x06001138 RID: 4408 RVA: 0x0002E1B8 File Offset: 0x0002C3B8
		public SettingsPropertyIsReadOnlyException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x06001139 RID: 4409 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
		protected SettingsPropertyIsReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyIsReadOnlyException" /> class based on supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600113A RID: 4410 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		public SettingsPropertyIsReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
