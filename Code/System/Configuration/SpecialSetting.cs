using System;

namespace System.Configuration
{
	/// <summary>Specifies the special setting category of a application settings property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000206 RID: 518
	public enum SpecialSetting
	{
		/// <summary>The configuration property represents a connection string, typically for a data store or network resource. </summary>
		// Token: 0x04000503 RID: 1283
		ConnectionString,
		/// <summary>The configuration property represents a Uniform Resource Locator (URL) to a Web service.</summary>
		// Token: 0x04000504 RID: 1284
		WebServiceUrl
	}
}
