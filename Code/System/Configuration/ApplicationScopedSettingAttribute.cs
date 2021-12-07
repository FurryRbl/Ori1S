using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings property has a common value for all users of an application. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C6 RID: 454
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ApplicationScopedSettingAttribute : SettingAttribute
	{
	}
}
