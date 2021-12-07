using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the statement of a <see cref="T:System.Security.Policy.CodeGroup" /> describing the permissions and other information that apply to code with a particular set of evidence. This class cannot be inherited.</summary>
	// Token: 0x0200064D RID: 1613
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		// Token: 0x06003D62 RID: 15714 RVA: 0x000D3BA0 File Offset: 0x000D1DA0
		public PolicyStatement(PermissionSet permSet) : this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyStatement" /> class with the specified <see cref="T:System.Security.PermissionSet" /> and attributes.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> with which to initialize the new instance. </param>
		/// <param name="attributes">A bitwise combination of the <see cref="T:System.Security.Policy.PolicyStatementAttribute" /> values. </param>
		// Token: 0x06003D63 RID: 15715 RVA: 0x000D3BAC File Offset: 0x000D1DAC
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet != null)
			{
				this.perms = permSet.Copy();
				this.perms.SetReadOnly(true);
			}
			this.attrs = attributes;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.PermissionSet" /> of the policy statement.</summary>
		/// <returns>The <see cref="T:System.Security.PermissionSet" /> of the policy statement.</returns>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000D3BE4 File Offset: 0x000D1DE4
		// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000D3C10 File Offset: 0x000D1E10
		public PermissionSet PermissionSet
		{
			get
			{
				if (this.perms == null)
				{
					this.perms = new PermissionSet(PermissionState.None);
					this.perms.SetReadOnly(true);
				}
				return this.perms;
			}
			set
			{
				this.perms = value;
			}
		}

		/// <summary>Gets or sets the attributes of the policy statement.</summary>
		/// <returns>The attributes of the policy statement.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000D3C1C File Offset: 0x000D1E1C
		// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000D3C24 File Offset: 0x000D1E24
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.attrs;
			}
			set
			{
				switch (value)
				{
				case PolicyStatementAttribute.Nothing:
				case PolicyStatementAttribute.Exclusive:
				case PolicyStatementAttribute.LevelFinal:
				case PolicyStatementAttribute.All:
					this.attrs = value;
					return;
				default:
				{
					string text = Locale.GetText("Invalid value for {0}.");
					throw new ArgumentException(string.Format(text, "PolicyStatementAttribute"));
				}
				}
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement.</summary>
		/// <returns>A text string representing the attributes of the policy statement.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000D3C78 File Offset: 0x000D1E78
		public string AttributeString
		{
			get
			{
				switch (this.attrs)
				{
				case PolicyStatementAttribute.Exclusive:
					return "Exclusive";
				case PolicyStatementAttribute.LevelFinal:
					return "LevelFinal";
				case PolicyStatementAttribute.All:
					return "Exclusive LevelFinal";
				default:
					return string.Empty;
				}
			}
		}

		/// <summary>Creates an equivalent copy of the current policy statement.</summary>
		/// <returns>A new copy of the <see cref="T:System.Security.Policy.PolicyStatement" /> with <see cref="P:System.Security.Policy.PolicyStatement.PermissionSet" /> and <see cref="P:System.Security.Policy.PolicyStatement.Attributes" /> identical to those of the current <see cref="T:System.Security.Policy.PolicyStatement" />.</returns>
		// Token: 0x06003D69 RID: 15721 RVA: 0x000D3CBC File Offset: 0x000D1EBC
		public PolicyStatement Copy()
		{
			return new PolicyStatement(this.perms, this.attrs);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003D6A RID: 15722 RVA: 0x000D3CD0 File Offset: 0x000D1ED0
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid <see cref="T:System.Security.Policy.PolicyStatement" /> encoding. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003D6B RID: 15723 RVA: 0x000D3CDC File Offset: 0x000D1EDC
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PolicyStatement")
			{
				throw new ArgumentException(Locale.GetText("Invalid tag."));
			}
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.attrs = (PolicyStatementAttribute)((int)Enum.Parse(typeof(PolicyStatementAttribute), text));
			}
			SecurityElement et2 = et.SearchForChildByTag("PermissionSet");
			this.PermissionSet.FromXml(et2);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003D6C RID: 15724 RVA: 0x000D3D64 File Offset: 0x000D1F64
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context for lookup of <see cref="T:System.Security.NamedPermissionSet" /> values. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003D6D RID: 15725 RVA: 0x000D3D70 File Offset: 0x000D1F70
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.attrs != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", this.attrs.ToString());
			}
			securityElement.AddChild(this.PermissionSet.ToXml());
			return securityElement;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.PolicyStatement" /> object is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Policy.PolicyStatement" /> is equal to the current <see cref="T:System.Security.Policy.PolicyStatement" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Policy.PolicyStatement" /> object to compare with the current <see cref="T:System.Security.Policy.PolicyStatement" />. </param>
		// Token: 0x06003D6E RID: 15726 RVA: 0x000D3DD0 File Offset: 0x000D1FD0
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.PermissionSet.Equals(obj) && this.attrs == policyStatement.attrs;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.PolicyStatement" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.PolicyStatement" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003D6F RID: 15727 RVA: 0x000D3E18 File Offset: 0x000D2018
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.PermissionSet.GetHashCode() ^ (int)this.attrs;
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000D3E2C File Offset: 0x000D202C
		internal static PolicyStatement Empty()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001A8A RID: 6794
		private PermissionSet perms;

		// Token: 0x04001A8B RID: 6795
		private PolicyStatementAttribute attrs;
	}
}
