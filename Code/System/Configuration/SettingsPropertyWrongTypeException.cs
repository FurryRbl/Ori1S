using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception that is thrown when an invalid type is used with a <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
	// Token: 0x020001F9 RID: 505
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class.</summary>
		// Token: 0x0600115B RID: 4443 RVA: 0x0002E84C File Offset: 0x0002CA4C
		public SettingsPropertyWrongTypeException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameter.</summary>
		/// <param name="message">A string containing an exception message.</param>
		// Token: 0x0600115C RID: 4444 RVA: 0x0002E854 File Offset: 0x0002CA54
		public SettingsPropertyWrongTypeException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination of the serialized stream.</param>
		// Token: 0x0600115D RID: 4445 RVA: 0x0002E860 File Offset: 0x0002CA60
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyWrongTypeException" /> class based on the supplied parameters.</summary>
		/// <param name="message">A string containing an exception message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600115E RID: 4446 RVA: 0x0002E86C File Offset: 0x0002CA6C
		public SettingsPropertyWrongTypeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
