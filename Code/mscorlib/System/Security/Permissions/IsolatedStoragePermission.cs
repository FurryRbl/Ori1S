using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents access to generic isolated storage capabilities.</summary>
	// Token: 0x02000604 RID: 1540
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003AAB RID: 15019 RVA: 0x000C9728 File Offset: 0x000C7928
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
		}

		/// <summary>Gets or sets the quota on the overall size of each user's total store.</summary>
		/// <returns>The size, in bytes, of the resource allocated to the user.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000C9748 File Offset: 0x000C7948
		// (set) Token: 0x06003AAD RID: 15021 RVA: 0x000C9750 File Offset: 0x000C7950
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		/// <summary>Gets or sets the type of isolated storage containment allowed.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000C975C File Offset: 0x000C795C
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000C9764 File Offset: 0x000C7964
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				if (!Enum.IsDefined(typeof(IsolatedStorageContainment), value))
				{
					string message = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(message, "IsolatedStorageContainment");
				}
				this.m_allowed = value;
				if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
				{
					this.m_userQuota = long.MaxValue;
					this.m_machineQuota = long.MaxValue;
					this.m_expirationDays = long.MaxValue;
					this.m_permanentData = true;
				}
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000C97F8 File Offset: 0x000C79F8
		public bool IsUnrestricted()
		{
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage == this.m_allowed;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000C9808 File Offset: 0x000C7A08
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Allowed", this.m_allowed.ToString());
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003AB2 RID: 15026 RVA: 0x000C9884 File Offset: 0x000C7A84
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = 0L;
			this.m_permanentData = false;
			this.m_allowed = IsolatedStorageContainment.None;
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
			else
			{
				string text = esd.Attribute("Allowed");
				if (text != null)
				{
					this.UsageAllowed = (IsolatedStorageContainment)((int)Enum.Parse(typeof(IsolatedStorageContainment), text));
				}
				text = esd.Attribute("UserQuota");
				if (text != null)
				{
					Exception ex;
					long.Parse(text, true, out this.m_userQuota, out ex);
				}
			}
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000C9930 File Offset: 0x000C7B30
		internal bool IsEmpty()
		{
			return this.m_userQuota == 0L && this.m_allowed == IsolatedStorageContainment.None;
		}

		// Token: 0x0400198A RID: 6538
		private const int version = 1;

		// Token: 0x0400198B RID: 6539
		internal long m_userQuota;

		// Token: 0x0400198C RID: 6540
		internal long m_machineQuota;

		// Token: 0x0400198D RID: 6541
		internal long m_expirationDays;

		// Token: 0x0400198E RID: 6542
		internal bool m_permanentData;

		// Token: 0x0400198F RID: 6543
		internal IsolatedStorageContainment m_allowed;
	}
}
