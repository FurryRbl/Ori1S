using System;
using System.Collections.Specialized;

namespace System.Configuration.Provider
{
	/// <summary>Provides a base implementation for the extensible provider model.</summary>
	// Token: 0x02000011 RID: 17
	public abstract class ProviderBase
	{
		/// <summary>Initializes the provider.</summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
		/// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> on a provider after the provider has already been initialized.</exception>
		// Token: 0x0600008E RID: 142 RVA: 0x000023EC File Offset: 0x000005EC
		public virtual void Initialize(string name, NameValueCollection config)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Provider name cannot be null or empty.", "name");
			}
			if (this.alreadyInitialized)
			{
				throw new InvalidOperationException("This provider instance has already been initialized.");
			}
			this.alreadyInitialized = true;
			this._name = name;
			if (config != null)
			{
				this._description = config["description"];
				config.Remove("description");
			}
			if (string.IsNullOrEmpty(this._description))
			{
				this._description = this._name;
			}
		}

		/// <summary>Gets the friendly name used to refer to the provider during configuration.</summary>
		/// <returns>The friendly name used to refer to the provider during configuration.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002488 File Offset: 0x00000688
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).</summary>
		/// <returns>A brief, friendly description suitable for display in administrative tools or other UIs.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002490 File Offset: 0x00000690
		public virtual string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000020 RID: 32
		private bool alreadyInitialized;

		// Token: 0x04000021 RID: 33
		private string _description;

		// Token: 0x04000022 RID: 34
		private string _name;
	}
}
