using System;
using System.Security;
using System.Security.Permissions;

namespace System.Configuration
{
	/// <summary>Provides a permission structure that allows methods or classes to access configuration files. </summary>
	// Token: 0x0200002E RID: 46
	[Serializable]
	public sealed class ConfigurationPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPermission" /> class. </summary>
		/// <param name="state">The permission level to grant.</param>
		/// <exception cref="T:System.ArgumentException">The value of <paramref name="state" /> is neither <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> nor <see cref="F:System.Security.Permissions.PermissionState.None" />.</exception>
		// Token: 0x060001D0 RID: 464 RVA: 0x0000699C File Offset: 0x00004B9C
		public ConfigurationPermission(PermissionState state)
		{
			this.unrestricted = (state == PermissionState.Unrestricted);
		}

		/// <summary>Returns a new <see cref="T:System.Configuration.ConfigurationPermission" /> object with the same permission level.</summary>
		/// <returns>A new <see cref="T:System.Configuration.ConfigurationPermission" /> with the same permission level.</returns>
		// Token: 0x060001D1 RID: 465 RVA: 0x000069B0 File Offset: 0x00004BB0
		public override IPermission Copy()
		{
			return new ConfigurationPermission((!this.unrestricted) ? PermissionState.None : PermissionState.Unrestricted);
		}

		/// <summary>Reads the value of the permission state from XML.</summary>
		/// <param name="securityElement">The configuration element from which the permission state is read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.SecurityElement.Tag" /> for the given <paramref name="securityElement" /> does not equal "IPermission".</exception>
		/// <exception cref="T:System.ArgumentException">The class attribute of the given <paramref name="securityElement " />equals null.</exception>
		/// <exception cref="T:System.ArgumentException">The class attribute of the given <paramref name="securityElement" /> is not the type name for <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		/// <exception cref="T:System.ArgumentException">The version attribute for the given <paramref name="securityElement" /> does not equal 1.</exception>
		/// <exception cref="T:System.ArgumentException">The unrestricted attribute for the given <paramref name="securityElement" /> is neither true nor false.</exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x000069CC File Offset: 0x00004BCC
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.unrestricted = (string.Compare(text, "true", StringComparison.InvariantCultureIgnoreCase) == 0);
			}
		}

		/// <summary>Returns the logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> object and a given object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>The logical intersection between the <see cref="T:System.Configuration.ConfigurationPermission" /> and a given object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <param name="target">The object containing the permissions to perform the intersection with.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x060001D3 RID: 467 RVA: 0x00006A34 File Offset: 0x00004C34
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((!this.unrestricted || !configurationPermission.IsUnrestricted()) ? PermissionState.None : PermissionState.Unrestricted);
		}

		/// <summary>Returns the logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> object and an object that implements the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>The logical union of the <see cref="T:System.Configuration.ConfigurationPermission" /> and an object that implements <see cref="T:System.Security.IPermission" />.</returns>
		/// <param name="target">The object to perform the union with.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x060001D4 RID: 468 RVA: 0x00006A84 File Offset: 0x00004C84
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return new ConfigurationPermission((!this.unrestricted && !configurationPermission.IsUnrestricted()) ? PermissionState.None : PermissionState.Unrestricted);
		}

		/// <summary>Compares the <see cref="T:System.Configuration.ConfigurationPermission" /> object with an object implementing the <see cref="T:System.Security.IPermission" /> interface.</summary>
		/// <returns>true if the permission state is equal; otherwise, false.</returns>
		/// <param name="target">The object to compare to.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not typed as <see cref="T:System.Configuration.ConfigurationPermission" />.</exception>
		// Token: 0x060001D5 RID: 469 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.unrestricted;
			}
			ConfigurationPermission configurationPermission = target as ConfigurationPermission;
			if (configurationPermission == null)
			{
				throw new ArgumentException("target");
			}
			return !this.unrestricted || configurationPermission.IsUnrestricted();
		}

		/// <summary>Indicates whether the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> object is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</summary>
		/// <returns>true if the permission state for the <see cref="T:System.Configuration.ConfigurationPermission" /> is the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> value of <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, false.</returns>
		// Token: 0x060001D6 RID: 470 RVA: 0x00006B20 File Offset: 0x00004D20
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Returns a <see cref="T:System.Security.SecurityElement" /> object with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> with attribute values based on the current <see cref="T:System.Configuration.ConfigurationPermission" />.</returns>
		// Token: 0x060001D7 RID: 471 RVA: 0x00006B28 File Offset: 0x00004D28
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x04000092 RID: 146
		private bool unrestricted;
	}
}
