using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Provides an exception for <see cref="T:System.Configuration.SettingsProperty" /> objects that are not found.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public class SettingsPropertyNotFoundException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class. </summary>
		// Token: 0x0600113B RID: 4411 RVA: 0x0002E1DC File Offset: 0x0002C3DC
		public SettingsPropertyNotFoundException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on a supplied parameter.</summary>
		/// <param name="message"></param>
		// Token: 0x0600113C RID: 4412 RVA: 0x0002E1E4 File Offset: 0x0002C3E4
		public SettingsPropertyNotFoundException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		// Token: 0x0600113D RID: 4413 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		protected SettingsPropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyNotFoundException" /> class, based on supplied parameters.</summary>
		/// <param name="message"></param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x0600113E RID: 4414 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		public SettingsPropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
