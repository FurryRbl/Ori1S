using System;

namespace System.Configuration
{
	/// <summary>Manages the path context for the current application. This class cannot be inherited.</summary>
	// Token: 0x02000047 RID: 71
	public sealed class ExeContext
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00008438 File Offset: 0x00006638
		internal ExeContext(string path, ConfigurationUserLevel level)
		{
			this.path = path;
			this.level = level;
		}

		/// <summary>Gets the current path for the application.</summary>
		/// <returns>A string value containing the current path.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00008450 File Offset: 0x00006650
		public string ExePath
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets an object representing the path level of the current application.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationUserLevel" /> object representing the path level of the current application.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008458 File Offset: 0x00006658
		public ConfigurationUserLevel UserLevel
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x040000D7 RID: 215
		private string path;

		// Token: 0x040000D8 RID: 216
		private ConfigurationUserLevel level;
	}
}
