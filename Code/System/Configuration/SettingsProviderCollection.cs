﻿using System;
using System.Configuration.Provider;

namespace System.Configuration
{
	/// <summary>Represents a collection of application settings providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001FB RID: 507
	public class SettingsProviderCollection : ProviderCollection
	{
		/// <summary>Adds a new settings provider to the collection.</summary>
		/// <param name="provider">A <see cref="T:System.Configuration.Provider.ProviderBase" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="provider" /> parameter is not of type <see cref="T:System.Configuration.SettingsProvider" />. -or-The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property of the provider parameter is null or an empty string.-or-A settings provider with the same <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> already exists in the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="provider" /> parameter is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001163 RID: 4451 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
		public override void Add(ProviderBase provider)
		{
			if (!(provider is SettingsProvider))
			{
				throw new ArgumentException("SettingsProvider is expected");
			}
			if (string.IsNullOrEmpty(provider.Name))
			{
				throw new ArgumentException("Provider name cannot be null or empty");
			}
			base.Add(provider);
		}

		/// <summary>Gets the settings provider in the collection that matches the specified name.</summary>
		/// <returns>If found, the <see cref="T:System.Configuration.SettingsProvider" /> whose name matches that specified by the name parameter; otherwise, null.</returns>
		/// <param name="name">A <see cref="T:System.String" /> containing the friendly name of the settings provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only when setting this value.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003F4 RID: 1012
		public SettingsProvider this[string name]
		{
			get
			{
				return (SettingsProvider)base[name];
			}
		}
	}
}
