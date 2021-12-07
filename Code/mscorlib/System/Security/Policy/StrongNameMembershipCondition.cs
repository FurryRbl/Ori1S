using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its strong name. This class cannot be inherited.</summary>
	// Token: 0x02000654 RID: 1620
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /> class with the strong name public key blob, name, and version number that determine membership.</summary>
		/// <param name="blob">The strong name public key blob of the software publisher. </param>
		/// <param name="name">The simple name section of the strong name. </param>
		/// <param name="version">The version number of the strong name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is null.-or-The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06003DAE RID: 15790 RVA: 0x000D4894 File Offset: 0x000D2A94
		public StrongNameMembershipCondition(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			this.blob = blob;
			this.name = name;
			if (version != null)
			{
				this.assemblyVersion = (Version)version.Clone();
			}
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000D48EC File Offset: 0x000D2AEC
		internal StrongNameMembershipCondition(SecurityElement e)
		{
			this.FromXml(e);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000D4904 File Offset: 0x000D2B04
		internal StrongNameMembershipCondition()
		{
		}

		/// <summary>Gets or sets the simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The simple name of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentException">The value is null.-or-The value is an empty string ("").</exception>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000D4914 File Offset: 0x000D2B14
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x000D491C File Offset: 0x000D2B1C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x000D4928 File Offset: 0x000D2B28
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x000D4930 File Offset: 0x000D2B30
		public Version Version
		{
			get
			{
				return this.assemblyVersion;
			}
			set
			{
				this.assemblyVersion = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the <see cref="T:System.Security.Policy.StrongName" /> for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set the <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> to null. </exception>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x000D493C File Offset: 0x000D2B3C
		// (set) Token: 0x06003DB6 RID: 15798 RVA: 0x000D4944 File Offset: 0x000D2B44
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.blob;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PublicKey");
				}
				this.blob = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> against which to make the test. </param>
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000D4960 File Offset: 0x000D2B60
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				StrongName strongName = obj as StrongName;
				if (strongName != null)
				{
					return strongName.PublicKey.Equals(this.blob) && (this.name == null || !(this.name != strongName.Name)) && (!(this.assemblyVersion != null) || this.assemblyVersion.Equals(strongName.Version));
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" /></returns>
		// Token: 0x06003DB8 RID: 15800 RVA: 0x000D4A08 File Offset: 0x000D2C08
		public IMembershipCondition Copy()
		{
			return new StrongNameMembershipCondition(this.blob, this.name, this.assemblyVersion);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.Policy.StrongName" /> from the specified object is equivalent to the <see cref="T:System.Security.Policy.StrongName" /> contained in the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property of the current object or the specified object is null. </exception>
		// Token: 0x06003DB9 RID: 15801 RVA: 0x000D4A24 File Offset: 0x000D2C24
		public override bool Equals(object o)
		{
			StrongNameMembershipCondition strongNameMembershipCondition = o as StrongNameMembershipCondition;
			if (strongNameMembershipCondition == null)
			{
				return false;
			}
			if (!strongNameMembershipCondition.PublicKey.Equals(this.PublicKey))
			{
				return false;
			}
			if (this.name != strongNameMembershipCondition.Name)
			{
				return false;
			}
			if (this.assemblyVersion != null)
			{
				return this.assemblyVersion.Equals(strongNameMembershipCondition.Version);
			}
			return strongNameMembershipCondition.Version == null;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>The hash code for the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.StrongNameMembershipCondition.PublicKey" /> property is null. </exception>
		// Token: 0x06003DBA RID: 15802 RVA: 0x000D4AA0 File Offset: 0x000D2CA0
		public override int GetHashCode()
		{
			return this.blob.GetHashCode();
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		// Token: 0x06003DBB RID: 15803 RVA: 0x000D4AB0 File Offset: 0x000D2CB0
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003DBC RID: 15804 RVA: 0x000D4ABC File Offset: 0x000D2CBC
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this.blob = StrongNamePublicKeyBlob.FromString(e.Attribute("PublicKeyBlob"));
			this.name = e.Attribute("Name");
			string text = e.Attribute("AssemblyVersion");
			if (text == null)
			{
				this.assemblyVersion = null;
			}
			else
			{
				this.assemblyVersion = new Version(text);
			}
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongNameMembershipCondition" />.</returns>
		// Token: 0x06003DBD RID: 15805 RVA: 0x000D4B34 File Offset: 0x000D2D34
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("StrongName - ");
			stringBuilder.Append(this.blob);
			if (this.name != null)
			{
				stringBuilder.AppendFormat(" name = {0}", this.name);
			}
			if (this.assemblyVersion != null)
			{
				stringBuilder.AppendFormat(" version = {0}", this.assemblyVersion);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06003DBE RID: 15806 RVA: 0x000D4BA0 File Offset: 0x000D2DA0
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> context, which is used to resolve <see cref="T:System.Security.NamedPermissionSet" /> references. </param>
		// Token: 0x06003DBF RID: 15807 RVA: 0x000D4BAC File Offset: 0x000D2DAC
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(StrongNameMembershipCondition), this.version);
			if (this.blob != null)
			{
				securityElement.AddAttribute("PublicKeyBlob", this.blob.ToString());
			}
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.assemblyVersion != null)
			{
				string text = this.assemblyVersion.ToString();
				if (text != "0.0")
				{
					securityElement.AddAttribute("AssemblyVersion", text);
				}
			}
			return securityElement;
		}

		// Token: 0x04001A9A RID: 6810
		private readonly int version = 1;

		// Token: 0x04001A9B RID: 6811
		private StrongNamePublicKeyBlob blob;

		// Token: 0x04001A9C RID: 6812
		private string name;

		// Token: 0x04001A9D RID: 6813
		private Version assemblyVersion;
	}
}
