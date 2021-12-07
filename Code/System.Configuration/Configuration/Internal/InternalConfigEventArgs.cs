using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines a class that allows the .NET Framework infrastructure to specify event arguments for configuration events.</summary>
	// Token: 0x02000010 RID: 16
	public sealed class InternalConfigEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> class.</summary>
		/// <param name="configPath">A configuration path.</param>
		// Token: 0x0600008A RID: 138 RVA: 0x000023C0 File Offset: 0x000005C0
		public InternalConfigEventArgs(string configPath)
		{
			this.configPath = configPath;
		}

		/// <summary>Gets or sets the configuration path related to the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> object.</summary>
		/// <returns>A string value specifying the configuration path.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000023D0 File Offset: 0x000005D0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000023D8 File Offset: 0x000005D8
		public string ConfigPath
		{
			get
			{
				return this.configPath;
			}
			set
			{
				this.configPath = value;
			}
		}

		// Token: 0x0400001F RID: 31
		private string configPath;
	}
}
