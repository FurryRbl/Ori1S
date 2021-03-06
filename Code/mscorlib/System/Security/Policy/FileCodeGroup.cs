using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Grants permission to manipulate files located in the code assemblies to code assemblies that match the membership condition. This class cannot be inherited.</summary>
	// Token: 0x0200063C RID: 1596
	[ComVisible(true)]
	[Serializable]
	public sealed class FileCodeGroup : CodeGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.FileCodeGroup" /> class.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. This value is used to construct the <see cref="T:System.Security.Permissions.FileIOPermission" /> that is granted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="access" /> parameter is not valid. </exception>
		// Token: 0x06003CCC RID: 15564 RVA: 0x000D10E4 File Offset: 0x000CF2E4
		public FileCodeGroup(IMembershipCondition membershipCondition, FileIOPermissionAccess access) : base(membershipCondition, null)
		{
			this.m_access = access;
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000D10F8 File Offset: 0x000CF2F8
		internal FileCodeGroup(SecurityElement e, PolicyLevel level) : base(e, level)
		{
		}

		/// <summary>Makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06003CCE RID: 15566 RVA: 0x000D1104 File Offset: 0x000CF304
		public override CodeGroup Copy()
		{
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			fileCodeGroup.Name = base.Name;
			fileCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				fileCodeGroup.AddChild(codeGroup.Copy());
			}
			return fileCodeGroup;
		}

		/// <summary>Gets the merge logic.</summary>
		/// <returns>The string "Union".</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000D11A4 File Offset: 0x000CF3A4
		public override string MergeLogic
		{
			get
			{
				return "Union";
			}
		}

		/// <summary>Resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement consisting of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The current policy is null.-or- More than one code group (including the parent code group and all child code groups) is marked <see cref="F:System.Security.Policy.PolicyStatementAttribute.Exclusive" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003CD0 RID: 15568 RVA: 0x000D11AC File Offset: 0x000CF3AC
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			PermissionSet permissionSet = null;
			if (base.PolicyStatement == null)
			{
				permissionSet = new PermissionSet(PermissionState.None);
			}
			else
			{
				permissionSet = base.PolicyStatement.PermissionSet.Copy();
			}
			if (base.Children.Count > 0)
			{
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup = (CodeGroup)obj;
					PolicyStatement policyStatement = codeGroup.Resolve(evidence);
					if (policyStatement != null)
					{
						permissionSet = permissionSet.Union(policyStatement.PermissionSet);
					}
				}
			}
			PolicyStatement policyStatement2;
			if (base.PolicyStatement != null)
			{
				policyStatement2 = base.PolicyStatement.Copy();
			}
			else
			{
				policyStatement2 = PolicyStatement.Empty();
			}
			policyStatement2.PermissionSet = permissionSet;
			return policyStatement2;
		}

		/// <summary>Resolves matching code groups.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="evidence" /> parameter is null. </exception>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x000D12C4 File Offset: 0x000CF4C4
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (!base.MembershipCondition.Check(evidence))
			{
				return null;
			}
			FileCodeGroup fileCodeGroup = new FileCodeGroup(base.MembershipCondition, this.m_access);
			foreach (object obj in base.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				CodeGroup codeGroup2 = codeGroup.ResolveMatchingCodeGroups(evidence);
				if (codeGroup2 != null)
				{
					fileCodeGroup.AddChild(codeGroup2);
				}
			}
			return fileCodeGroup;
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000D137C File Offset: 0x000CF57C
		public override string AttributeString
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The concatenatation of the string "Same directory FileIO - " and the access type.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000D1380 File Offset: 0x000CF580
		public override string PermissionSetName
		{
			get
			{
				return "Same directory FileIO - " + this.m_access.ToString();
			}
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The code group to compare with the current code group. </param>
		// Token: 0x06003CD4 RID: 15572 RVA: 0x000D139C File Offset: 0x000CF59C
		public override bool Equals(object o)
		{
			return o is FileCodeGroup && this.m_access == ((FileCodeGroup)o).m_access && base.Equals((CodeGroup)o, false);
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000D13DC File Offset: 0x000CF5DC
		public override int GetHashCode()
		{
			return this.m_access.GetHashCode();
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x000D13F0 File Offset: 0x000CF5F0
		protected override void ParseXml(SecurityElement e, PolicyLevel level)
		{
			string text = e.Attribute("Access");
			if (text != null)
			{
				this.m_access = (FileIOPermissionAccess)((int)Enum.Parse(typeof(FileIOPermissionAccess), text, true));
			}
			else
			{
				this.m_access = FileIOPermissionAccess.NoAccess;
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x000D1438 File Offset: 0x000CF638
		protected override void CreateXml(SecurityElement element, PolicyLevel level)
		{
			element.AddAttribute("Access", this.m_access.ToString());
		}

		// Token: 0x04001A70 RID: 6768
		private FileIOPermissionAccess m_access;
	}
}
