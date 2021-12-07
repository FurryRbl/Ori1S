using System;

namespace System.Xml
{
	/// <summary>Specifies how to treat the time value when converting between string and <see cref="T:System.DateTime" />.</summary>
	// Token: 0x020000F9 RID: 249
	public enum XmlDateTimeSerializationMode
	{
		/// <summary>Treat as local time. If the <see cref="T:System.DateTime" /> object represents a Coordinated Universal Time (UTC), it is converted to the local time.</summary>
		// Token: 0x040004FF RID: 1279
		Local,
		/// <summary>Treat as a UTC. If the <see cref="T:System.DateTime" /> object represents a local time, it is converted to a UTC.</summary>
		// Token: 0x04000500 RID: 1280
		Utc,
		/// <summary>Treat as a local time if a <see cref="T:System.DateTime" /> is being converted to a string.</summary>
		// Token: 0x04000501 RID: 1281
		Unspecified,
		/// <summary>Time zone information should be preserved when converting.</summary>
		// Token: 0x04000502 RID: 1282
		RoundtripKind
	}
}
