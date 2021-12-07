using System;

namespace System.Configuration
{
	/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000204 RID: 516
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
		/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
		// Token: 0x0600117E RID: 4478 RVA: 0x0002EA18 File Offset: 0x0002CC18
		public SettingsGroupNameAttribute(string groupName)
		{
			this.group_name = groupName;
		}

		/// <summary>Gets the name of the application settings property group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x0002EA28 File Offset: 0x0002CC28
		public string GroupName
		{
			get
			{
				return this.group_name;
			}
		}

		// Token: 0x04000501 RID: 1281
		private string group_name;
	}
}
