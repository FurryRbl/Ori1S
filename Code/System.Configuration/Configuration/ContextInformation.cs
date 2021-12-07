using System;

namespace System.Configuration
{
	/// <summary>Encapsulates the context information that is associated with a <see cref="T:System.Configuration.ConfigurationElement" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	public sealed class ContextInformation
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00008040 File Offset: 0x00006240
		internal ContextInformation(Configuration config, object ctx)
		{
			this.ctx = ctx;
			this.config = config;
		}

		/// <summary>Provides an object containing configuration-section information based on the specified section name.</summary>
		/// <returns>An object containing the specified section within the configuration.</returns>
		/// <param name="sectionName">The name of the configuration section.</param>
		// Token: 0x06000277 RID: 631 RVA: 0x00008058 File Offset: 0x00006258
		public object GetSection(string sectionName)
		{
			return this.config.GetSection(sectionName);
		}

		/// <summary>Gets the context of the environment where the configuration property is being evaluated.</summary>
		/// <returns>An object specifying the environment where the configuration property is being evaluated.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008068 File Offset: 0x00006268
		public object HostingContext
		{
			get
			{
				return this.ctx;
			}
		}

		/// <summary>Gets a value specifying whether the configuration property is being evaluated at the machine configuration level.</summary>
		/// <returns>true if the configuration property is being evaluated at the machine configuration level; otherwise, false.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008070 File Offset: 0x00006270
		[MonoInternalNote("should this use HostingContext instead?")]
		public bool IsMachineLevel
		{
			get
			{
				return this.config.ConfigPath == "machine";
			}
		}

		// Token: 0x040000CC RID: 204
		private object ctx;

		// Token: 0x040000CD RID: 205
		private Configuration config;
	}
}
