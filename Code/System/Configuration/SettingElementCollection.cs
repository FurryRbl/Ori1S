using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.SettingElement" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001ED RID: 493
	public sealed class SettingElementCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.SettingElement" /> object to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.SettingElement" /> object to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060010E8 RID: 4328 RVA: 0x0002D774 File Offset: 0x0002B974
		public void Add(SettingElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingElement" /> objects from the collection.</summary>
		// Token: 0x060010E9 RID: 4329 RVA: 0x0002D780 File Offset: 0x0002B980
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingElement" /> object from the collection. </summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElement" /> object.</returns>
		/// <param name="elementKey">A string value representing the <see cref="T:System.Configuration.SettingElement" /> object in the collection.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010EA RID: 4330 RVA: 0x0002D788 File Offset: 0x0002B988
		public SettingElement Get(string elementKey)
		{
			foreach (object obj in this)
			{
				SettingElement settingElement = (SettingElement)obj;
				if (settingElement.Name == elementKey)
				{
					return settingElement;
				}
			}
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingElement" /> object from the collection.</summary>
		/// <param name="element">A <see cref="T:System.Configuration.SettingElement" /> object.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060010EB RID: 4331 RVA: 0x0002D808 File Offset: 0x0002BA08
		public void Remove(SettingElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			base.BaseRemove(element.Name);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0002D828 File Offset: 0x0002BA28
		protected override ConfigurationElement CreateNewElement()
		{
			return new SettingElement();
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0002D830 File Offset: 0x0002BA30
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SettingElement)element).Name;
		}

		/// <summary>Gets the type of the configuration collection.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object of the collection.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0002D840 File Offset: 0x0002BA40
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0002D844 File Offset: 0x0002BA44
		protected override string ElementName
		{
			get
			{
				return "setting";
			}
		}
	}
}
