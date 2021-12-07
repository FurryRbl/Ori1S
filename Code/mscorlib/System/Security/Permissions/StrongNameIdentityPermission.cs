using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for strong names. This class cannot be inherited.</summary>
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003BBE RID: 15294 RVA: 0x000CD238 File Offset: 0x000CB438
		public StrongNameIdentityPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this._list = new ArrayList();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> class for the specified strong name identity.</summary>
		/// <param name="blob">The public key defining the strong name identity namespace. </param>
		/// <param name="name">The simple name part of the strong name identity. This corresponds to the name of the assembly. </param>
		/// <param name="version">The version number of the identity. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06003BBF RID: 15295 RVA: 0x000CD27C File Offset: 0x000CB47C
		public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name != null && name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			this._state = PermissionState.None;
			this._list = new ArrayList();
			this._list.Add(new StrongNameIdentityPermission.SNIP(blob, name, version));
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000CD2E8 File Offset: 0x000CB4E8
		internal StrongNameIdentityPermission(StrongNameIdentityPermission snip)
		{
			this._state = snip._state;
			this._list = new ArrayList(snip._list.Count);
			foreach (object obj in snip._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				this._list.Add(new StrongNameIdentityPermission.SNIP(snip2.PublicKey, snip2.Name, snip2.AssemblyVersion));
			}
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000CD3B4 File Offset: 0x000CB5B4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 12;
		}

		/// <summary>Gets or sets the simple name portion of the strong name identity.</summary>
		/// <returns>The simple name of the identity.</returns>
		/// <exception cref="T:System.ArgumentException">The value is an empty string ("").</exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000CD3B8 File Offset: 0x000CB5B8
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x000CD3F8 File Offset: 0x000CB5F8
		public string Name
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).Name;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					throw new ArgumentException("name");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.Name = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the public key blob that defines the strong name identity namespace.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> that contains the public key of the identity, or null if there is no key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is set to null. </exception>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000CD464 File Offset: 0x000CB664
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x000CD4A4 File Offset: 0x000CB6A4
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).PublicKey;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.PublicKey = value;
				this._list[0] = snip;
			}
		}

		/// <summary>Gets or sets the version number of the identity.</summary>
		/// <returns>The version of the identity.</returns>
		/// <exception cref="T:System.NotSupportedException">The property value cannot be retrieved because it contains an ambiguous identity. </exception>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000CD508 File Offset: 0x000CB708
		// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x000CD548 File Offset: 0x000CB748
		public Version Version
		{
			get
			{
				if (this._list.Count > 1)
				{
					throw new NotSupportedException();
				}
				return ((StrongNameIdentityPermission.SNIP)this._list[0]).AssemblyVersion;
			}
			set
			{
				if (this._list.Count > 1)
				{
					this.ResetToDefault();
				}
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)this._list[0];
				snip.AssemblyVersion = value;
				this._list[0] = snip;
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000CD598 File Offset: 0x000CB798
		internal void ResetToDefault()
		{
			this._list.Clear();
			this._list.Add(StrongNameIdentityPermission.SNIP.CreateDefault());
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003BCA RID: 15306 RVA: 0x000CD5BC File Offset: 0x000CB7BC
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			return new StrongNameIdentityPermission(this);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid permission element.-or- The <paramref name="e" /> parameter's version number is not valid. </exception>
		// Token: 0x06003BCB RID: 15307 RVA: 0x000CD5D8 File Offset: 0x000CB7D8
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._list.Clear();
			if (e.Children != null && e.Children.Count > 0)
			{
				foreach (object obj in e.Children)
				{
					SecurityElement se = (SecurityElement)obj;
					this._list.Add(this.FromSecurityElement(se));
				}
			}
			else
			{
				this._list.Add(this.FromSecurityElement(e));
			}
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x000CD6AC File Offset: 0x000CB8AC
		private StrongNameIdentityPermission.SNIP FromSecurityElement(SecurityElement se)
		{
			string name = se.Attribute("Name");
			StrongNamePublicKeyBlob pk = StrongNamePublicKeyBlob.FromString(se.Attribute("PublicKeyBlob"));
			string text = se.Attribute("AssemblyVersion");
			Version version = (text != null) ? new Version(text) : null;
			return new StrongNameIdentityPermission.SNIP(pk, name, version);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission, or null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BCD RID: 15309 RVA: 0x000CD700 File Offset: 0x000CB900
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				throw new ArgumentException(Locale.GetText("Wrong permission type."));
			}
			if (this.IsEmpty() || strongNameIdentityPermission.IsEmpty())
			{
				return null;
			}
			if (!this.Match(strongNameIdentityPermission.Name))
			{
				return null;
			}
			string name = (this.Name.Length >= strongNameIdentityPermission.Name.Length) ? strongNameIdentityPermission.Name : this.Name;
			if (!this.Version.Equals(strongNameIdentityPermission.Version))
			{
				return null;
			}
			if (!this.PublicKey.Equals(strongNameIdentityPermission.PublicKey))
			{
				return null;
			}
			return new StrongNameIdentityPermission(this.PublicKey, name, this.Version);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003BCE RID: 15310 RVA: 0x000CD7CC File Offset: 0x000CB9CC
		public override bool IsSubsetOf(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsEmpty())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return strongNameIdentityPermission.IsUnrestricted();
			}
			if (strongNameIdentityPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				foreach (object obj2 in strongNameIdentityPermission._list)
				{
					StrongNameIdentityPermission.SNIP target2 = (StrongNameIdentityPermission.SNIP)obj2;
					if (!snip.IsSubsetOf(target2))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003BCF RID: 15311 RVA: 0x000CD8F0 File Offset: 0x000CBAF0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._list.Count > 1)
			{
				foreach (object obj in this._list)
				{
					StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
					SecurityElement securityElement2 = new SecurityElement("StrongName");
					this.ToSecurityElement(securityElement2, snip);
					securityElement.AddChild(securityElement2);
				}
			}
			else if (this._list.Count == 1)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)this._list[0];
				if (!this.IsEmpty(snip2))
				{
					this.ToSecurityElement(securityElement, snip2);
				}
			}
			return securityElement;
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x000CD9D0 File Offset: 0x000CBBD0
		private void ToSecurityElement(SecurityElement se, StrongNameIdentityPermission.SNIP snip)
		{
			if (snip.PublicKey != null)
			{
				se.AddAttribute("PublicKeyBlob", snip.PublicKey.ToString());
			}
			if (snip.Name != null)
			{
				se.AddAttribute("Name", snip.Name);
			}
			if (snip.AssemblyVersion != null)
			{
				se.AddAttribute("AssemblyVersion", snip.AssemblyVersion.ToString());
			}
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. -or-The two permissions are not equal and one is a subset of the other.</exception>
		// Token: 0x06003BD1 RID: 15313 RVA: 0x000CDA48 File Offset: 0x000CBC48
		public override IPermission Union(IPermission target)
		{
			StrongNameIdentityPermission strongNameIdentityPermission = this.Cast(target);
			if (strongNameIdentityPermission == null || strongNameIdentityPermission.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return strongNameIdentityPermission.Copy();
			}
			StrongNameIdentityPermission strongNameIdentityPermission2 = (StrongNameIdentityPermission)this.Copy();
			foreach (object obj in strongNameIdentityPermission._list)
			{
				StrongNameIdentityPermission.SNIP snip = (StrongNameIdentityPermission.SNIP)obj;
				if (!this.IsEmpty(snip) && !this.Contains(snip))
				{
					strongNameIdentityPermission2._list.Add(snip);
				}
			}
			return strongNameIdentityPermission2;
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		private bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000CDB28 File Offset: 0x000CBD28
		private bool Contains(StrongNameIdentityPermission.SNIP snip)
		{
			foreach (object obj in this._list)
			{
				StrongNameIdentityPermission.SNIP snip2 = (StrongNameIdentityPermission.SNIP)obj;
				bool flag = (snip2.PublicKey == null && snip.PublicKey == null) || (snip2.PublicKey != null && snip2.PublicKey.Equals(snip.PublicKey));
				bool flag2 = snip2.IsNameSubsetOf(snip.Name);
				bool flag3 = (snip2.AssemblyVersion == null && snip.AssemblyVersion == null) || (snip2.AssemblyVersion != null && snip2.AssemblyVersion.Equals(snip.AssemblyVersion));
				if (flag && flag2 && flag3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000CDC50 File Offset: 0x000CBE50
		private bool IsEmpty(StrongNameIdentityPermission.SNIP snip)
		{
			return this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000CDCAC File Offset: 0x000CBEAC
		private bool IsEmpty()
		{
			return !this.IsUnrestricted() && this._list.Count <= 1 && this.PublicKey == null && (this.Name == null || this.Name.Length <= 0) && (this.Version == null || StrongNameIdentityPermission.defaultVersion.Equals(this.Version));
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000CDD28 File Offset: 0x000CBF28
		private StrongNameIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			StrongNameIdentityPermission strongNameIdentityPermission = target as StrongNameIdentityPermission;
			if (strongNameIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(StrongNameIdentityPermission));
			}
			return strongNameIdentityPermission;
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x000CDD5C File Offset: 0x000CBF5C
		private bool Match(string target)
		{
			if (this.Name == null || target == null)
			{
				return false;
			}
			int num = this.Name.LastIndexOf('*');
			int num2 = target.LastIndexOf('*');
			int length;
			if (num == -1 && num2 == -1)
			{
				length = Math.Max(this.Name.Length, target.Length);
			}
			else if (num == -1)
			{
				length = num2;
			}
			else if (num2 == -1)
			{
				length = num;
			}
			else
			{
				length = Math.Min(num, num2);
			}
			return string.Compare(this.Name, 0, target, 0, length, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x04001A03 RID: 6659
		private const int version = 1;

		// Token: 0x04001A04 RID: 6660
		private static Version defaultVersion = new Version(0, 0);

		// Token: 0x04001A05 RID: 6661
		private PermissionState _state;

		// Token: 0x04001A06 RID: 6662
		private ArrayList _list;

		// Token: 0x02000621 RID: 1569
		private struct SNIP
		{
			// Token: 0x06003BD8 RID: 15320 RVA: 0x000CDE00 File Offset: 0x000CC000
			internal SNIP(StrongNamePublicKeyBlob pk, string name, Version version)
			{
				this.PublicKey = pk;
				this.Name = name;
				this.AssemblyVersion = version;
			}

			// Token: 0x06003BD9 RID: 15321 RVA: 0x000CDE18 File Offset: 0x000CC018
			internal static StrongNameIdentityPermission.SNIP CreateDefault()
			{
				return new StrongNameIdentityPermission.SNIP(null, string.Empty, (Version)StrongNameIdentityPermission.defaultVersion.Clone());
			}

			// Token: 0x06003BDA RID: 15322 RVA: 0x000CDE34 File Offset: 0x000CC034
			internal bool IsNameSubsetOf(string target)
			{
				if (this.Name == null)
				{
					return target == null;
				}
				if (target == null)
				{
					return true;
				}
				int num = this.Name.LastIndexOf('*');
				if (num == 0)
				{
					return true;
				}
				if (num == -1)
				{
					num = this.Name.Length;
				}
				return string.Compare(this.Name, 0, target, 0, num, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x06003BDB RID: 15323 RVA: 0x000CDE9C File Offset: 0x000CC09C
			internal bool IsSubsetOf(StrongNameIdentityPermission.SNIP target)
			{
				return (this.PublicKey != null && this.PublicKey.Equals(target.PublicKey)) || (this.IsNameSubsetOf(target.Name) && (!(this.AssemblyVersion != null) || this.AssemblyVersion.Equals(target.AssemblyVersion)) && this.PublicKey == null && target.PublicKey == null);
			}

			// Token: 0x04001A07 RID: 6663
			public StrongNamePublicKeyBlob PublicKey;

			// Token: 0x04001A08 RID: 6664
			public string Name;

			// Token: 0x04001A09 RID: 6665
			public Version AssemblyVersion;
		}
	}
}
