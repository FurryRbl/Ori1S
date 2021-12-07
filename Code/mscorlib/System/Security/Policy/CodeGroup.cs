using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Represents the abstract base class from which all implementations of code groups must derive.</summary>
	// Token: 0x02000636 RID: 1590
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Policy.CodeGroup" />.</summary>
		/// <param name="membershipCondition">A membership condition that tests evidence to determine whether this code group applies policy. </param>
		/// <param name="policy">The policy statement for the code group in the form of a permission set and attributes to grant code that matches the membership condition. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="membershipCondition" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="membershipCondition" /> parameter is not valid.-or- The type of the <paramref name="policy" /> parameter is not valid. </exception>
		// Token: 0x06003C7F RID: 15487 RVA: 0x000CFB60 File Offset: 0x000CDD60
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy != null)
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x000CFBB0 File Offset: 0x000CDDB0
		internal CodeGroup(SecurityElement e, PolicyLevel level)
		{
			this.FromXml(e, level);
		}

		/// <summary>When overridden in a derived class, makes a deep copy of the current code group.</summary>
		/// <returns>An equivalent copy of the current code group, including its membership conditions and child code groups.</returns>
		// Token: 0x06003C81 RID: 15489
		public abstract CodeGroup Copy();

		/// <summary>When overridden in a derived class, gets the merge logic for the code group.</summary>
		/// <returns>A description of the merge logic for the code group.</returns>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003C82 RID: 15490
		public abstract string MergeLogic { get; }

		/// <summary>When overridden in a derived class, resolves policy for the code group and its descendants for a set of evidence.</summary>
		/// <returns>A policy statement that consists of the permissions granted by the code group with optional attributes, or null if the code group does not apply (the membership condition does not match the specified evidence).</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		// Token: 0x06003C83 RID: 15491
		public abstract PolicyStatement Resolve(Evidence evidence);

		/// <summary>When overridden in a derived class, resolves matching code groups.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.CodeGroup" /> that is the root of the tree of matching code groups.</returns>
		/// <param name="evidence">The evidence for the assembly. </param>
		// Token: 0x06003C84 RID: 15492
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		/// <summary>Gets or sets the policy statement associated with the code group.</summary>
		/// <returns>The policy statement for the code group.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000CFBCC File Offset: 0x000CDDCC
		// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000CFBD4 File Offset: 0x000CDDD4
		public PolicyStatement PolicyStatement
		{
			get
			{
				return this.m_policy;
			}
			set
			{
				this.m_policy = value;
			}
		}

		/// <summary>Gets or sets the description of the code group.</summary>
		/// <returns>The description of the code group.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000CFBE0 File Offset: 0x000CDDE0
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000CFBE8 File Offset: 0x000CDDE8
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		/// <summary>Gets or sets the code group's membership condition.</summary>
		/// <returns>The membership condition that determines to which evidence the code group is applicable.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this parameter to null. </exception>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000CFBF4 File Offset: 0x000CDDF4
		// (set) Token: 0x06003C8A RID: 15498 RVA: 0x000CFBFC File Offset: 0x000CDDFC
		public IMembershipCondition MembershipCondition
		{
			get
			{
				return this.m_membershipCondition;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("value");
				}
				this.m_membershipCondition = value;
			}
		}

		/// <summary>Gets or sets the name of the code group.</summary>
		/// <returns>The name of the code group.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000CFC18 File Offset: 0x000CDE18
		// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000CFC20 File Offset: 0x000CDE20
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		/// <summary>Gets or sets an ordered list of the child code groups of a code group.</summary>
		/// <returns>A list of child code groups.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set this property to null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property with a list of children that are not <see cref="T:System.Security.Policy.CodeGroup" /> objects.</exception>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000CFC2C File Offset: 0x000CDE2C
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000CFC34 File Offset: 0x000CDE34
		public IList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_children = new ArrayList(value);
			}
		}

		/// <summary>Gets a string representation of the attributes of the policy statement for the code group.</summary>
		/// <returns>A string representation of the attributes of the policy statement for the code group.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000CFC54 File Offset: 0x000CDE54
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		/// <summary>Gets the name of the named permission set for the code group.</summary>
		/// <returns>The name of a named permission set of the policy level.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000CFC70 File Offset: 0x000CDE70
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null)
				{
					return null;
				}
				if (this.m_policy.PermissionSet is NamedPermissionSet)
				{
					return ((NamedPermissionSet)this.m_policy.PermissionSet).Name;
				}
				return null;
			}
		}

		/// <summary>Adds a child code group to the current code group.</summary>
		/// <param name="group">The code group to be added as a child. This new child code group is added to the end of the list. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="group" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not a valid code group. </exception>
		// Token: 0x06003C91 RID: 15505 RVA: 0x000CFCB8 File Offset: 0x000CDEB8
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.m_children.Add(group.Copy());
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="o">The code group to compare with the current code group. </param>
		// Token: 0x06003C92 RID: 15506 RVA: 0x000CFCE0 File Offset: 0x000CDEE0
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			return codeGroup != null && this.Equals(codeGroup, false);
		}

		/// <summary>Determines whether the specified code group is equivalent to the current code group, checking the child code groups as well, if specified.</summary>
		/// <returns>true if the specified code group is equivalent to the current code group; otherwise, false.</returns>
		/// <param name="cg">The code group to compare with the current code group. </param>
		/// <param name="compareChildren">true to compare child code groups, as well; otherwise, false. </param>
		// Token: 0x06003C93 RID: 15507 RVA: 0x000CFD04 File Offset: 0x000CDF04
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (cg.Name != this.Name)
			{
				return false;
			}
			if (cg.Description != this.Description)
			{
				return false;
			}
			if (!cg.MembershipCondition.Equals(this.m_membershipCondition))
			{
				return false;
			}
			if (compareChildren)
			{
				int count = cg.Children.Count;
				if (this.Children.Count != count)
				{
					return false;
				}
				for (int i = 0; i < count; i++)
				{
					if (!((CodeGroup)this.Children[i]).Equals((CodeGroup)cg.Children[i], false))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Removes the specified child code group.</summary>
		/// <param name="group">The code group to be removed as a child. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="group" /> parameter is not an immediate child code group of the current code group. </exception>
		// Token: 0x06003C94 RID: 15508 RVA: 0x000CFDC0 File Offset: 0x000CDFC0
		public void RemoveChild(CodeGroup group)
		{
			if (group != null)
			{
				this.m_children.Remove(group);
			}
		}

		/// <summary>Gets the hash code of the current code group.</summary>
		/// <returns>The hash code of the current code group.</returns>
		// Token: 0x06003C95 RID: 15509 RVA: 0x000CFDD4 File Offset: 0x000CDFD4
		public override int GetHashCode()
		{
			int num = this.m_membershipCondition.GetHashCode();
			if (this.m_policy != null)
			{
				num += this.m_policy.GetHashCode();
			}
			return num;
		}

		/// <summary>Reconstructs a security object with a given state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		// Token: 0x06003C96 RID: 15510 RVA: 0x000CFE08 File Offset: 0x000CE008
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a given state and policy level from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		// Token: 0x06003C97 RID: 15511 RVA: 0x000CFE14 File Offset: 0x000CE014
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			string text = e.Attribute("PermissionSetName");
			PermissionSet permissionSet;
			if (text != null && level != null)
			{
				permissionSet = level.GetNamedPermissionSet(text);
			}
			else
			{
				SecurityElement securityElement = e.SearchForChildByTag("PermissionSet");
				if (securityElement != null)
				{
					Type type = Type.GetType(securityElement.Attribute("class"));
					permissionSet = (PermissionSet)Activator.CreateInstance(type, true);
					permissionSet.FromXml(securityElement);
				}
				else
				{
					permissionSet = new PermissionSet(new PermissionSet(PermissionState.None));
				}
			}
			this.m_policy = new PolicyStatement(permissionSet);
			this.m_children.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2.Tag == "CodeGroup")
					{
						this.AddChild(CodeGroup.CreateFromXml(securityElement2, level));
					}
				}
			}
			this.m_membershipCondition = null;
			SecurityElement securityElement3 = e.SearchForChildByTag("IMembershipCondition");
			if (securityElement3 != null)
			{
				string text2 = securityElement3.Attribute("class");
				Type type2 = Type.GetType(text2);
				if (type2 == null)
				{
					type2 = Type.GetType("System.Security.Policy." + text2);
				}
				this.m_membershipCondition = (IMembershipCondition)Activator.CreateInstance(type2, true);
				this.m_membershipCondition.FromXml(securityElement3, level);
			}
			this.m_name = e.Attribute("Name");
			this.m_description = e.Attribute("Description");
			this.ParseXml(e, level);
		}

		/// <summary>When overridden in a derived class, reconstructs properties and internal state specific to a derived code group from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06003C98 RID: 15512 RVA: 0x000CFFF4 File Offset: 0x000CE1F4
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003C99 RID: 15513 RVA: 0x000CFFF8 File Offset: 0x000CE1F8
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object, its current state, and the policy level within which the code exists.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level within which the code group exists. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003C9A RID: 15514 RVA: 0x000D0004 File Offset: 0x000CE204
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.Name != null)
			{
				securityElement.AddAttribute("Name", this.Name);
			}
			if (this.Description != null)
			{
				securityElement.AddAttribute("Description", this.Description);
			}
			if (this.MembershipCondition != null)
			{
				securityElement.AddChild(this.MembershipCondition.ToXml());
			}
			if (this.PolicyStatement != null && this.PolicyStatement.PermissionSet != null)
			{
				securityElement.AddChild(this.PolicyStatement.PermissionSet.ToXml());
			}
			foreach (object obj in this.Children)
			{
				CodeGroup codeGroup = (CodeGroup)obj;
				securityElement.AddChild(codeGroup.ToXml());
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		/// <summary>When overridden in a derived class, serializes properties and internal state specific to a derived code group and adds the serialization to the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="element">The XML encoding to which to add the serialization. </param>
		/// <param name="level">The policy level within which the code group exists. </param>
		// Token: 0x06003C9B RID: 15515 RVA: 0x000D0138 File Offset: 0x000CE338
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000D013C File Offset: 0x000CE33C
		internal static CodeGroup CreateFromXml(SecurityElement se, PolicyLevel level)
		{
			string text = se.Attribute("class");
			string text2 = text;
			int num = text2.IndexOf(",");
			if (num > 0)
			{
				text2 = text2.Substring(0, num);
			}
			num = text2.LastIndexOf(".");
			if (num > 0)
			{
				text2 = text2.Substring(num + 1);
			}
			string text3 = text2;
			switch (text3)
			{
			case "FileCodeGroup":
				return new FileCodeGroup(se, level);
			case "FirstMatchCodeGroup":
				return new FirstMatchCodeGroup(se, level);
			case "NetCodeGroup":
				return new NetCodeGroup(se, level);
			case "UnionCodeGroup":
				return new UnionCodeGroup(se, level);
			}
			Type type = Type.GetType(text);
			CodeGroup codeGroup = (CodeGroup)Activator.CreateInstance(type, true);
			codeGroup.FromXml(se, level);
			return codeGroup;
		}

		// Token: 0x04001A3E RID: 6718
		private PolicyStatement m_policy;

		// Token: 0x04001A3F RID: 6719
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001A40 RID: 6720
		private string m_description;

		// Token: 0x04001A41 RID: 6721
		private string m_name;

		// Token: 0x04001A42 RID: 6722
		private ArrayList m_children = new ArrayList();
	}
}
