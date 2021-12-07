﻿using System;

namespace System.Web
{
	/// <summary>Specifies the trust level that is granted to an ASP.NET Web application.</summary>
	// Token: 0x020004BE RID: 1214
	[Serializable]
	public enum AspNetHostingPermissionLevel
	{
		/// <summary>Indicates that no permission is granted. All demands for <see cref="T:System.Web.AspNetHostingPermission" /> will fail.</summary>
		// Token: 0x04001B8A RID: 7050
		None = 100,
		/// <summary>Indicates that features protected with a demand for the <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" /> level will succeed. This level allows code to execute but not to interact with resources present on the system. This level is granted by configuring at least the <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" /> trust level using the trust section in a configuration file.</summary>
		// Token: 0x04001B8B RID: 7051
		Minimal = 200,
		/// <summary>Indicates that features protected with a demand for any level less than or equal to the <see cref="F:System.Web.AspNetHostingPermissionLevel.Low" /> level will succeed. This level is intended to allow read-only access to limited resources in a constrained environment. This level is granted by specifying the <see cref="F:System.Web.AspNetHostingPermissionLevel.Low" /> trust level in the trust section in a configuration file.</summary>
		// Token: 0x04001B8C RID: 7052
		Low = 300,
		/// <summary>Indicates that features protected with a demand for any level less than or equal to the <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> level will succeed. This level is granted by configuring at least the <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> trust level in the trust section in a configuration file.</summary>
		// Token: 0x04001B8D RID: 7053
		Medium = 400,
		/// <summary>Indicates that features protected with a demand for any level less than or equal to the High trust level will succeed. This level is intended for highly trusted managed-code applications that need to use most of the managed permissions that support semi-trusted access. It does not grant some of the highest permissions (for example, the ability to call into native code), but it does provide a way to run trusted applications with least privilege or to provide some level of constraints for highly trusted applications. This level is granted by configuring at least the <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> trust level in the trust section in a configuration file.</summary>
		// Token: 0x04001B8E RID: 7054
		High = 500,
		/// <summary>Indicates that all demands for permission to use all features of an application will be granted. This is equivalent to granting Full trust level in the trust section in a configuration file.</summary>
		// Token: 0x04001B8F RID: 7055
		Unrestricted = 600
	}
}
