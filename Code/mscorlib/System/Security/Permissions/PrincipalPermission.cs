using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Permissions
{
	/// <summary>Allows checks against the active principal (see <see cref="T:System.Security.Principal.IPrincipal" />) using the language constructs defined for both declarative and imperative security actions. This class cannot be inherited.</summary>
	// Token: 0x0200060F RID: 1551
	[ComVisible(true)]
	[Serializable]
	public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003B0A RID: 15114 RVA: 0x000CA460 File Offset: 0x000C8660
		public PrincipalPermission(PermissionState state)
		{
			this.principals = new ArrayList();
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				PrincipalPermission.PrincipalInfo value = new PrincipalPermission.PrincipalInfo(null, null, true);
				this.principals.Add(value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class for the specified <paramref name="name" /> and <paramref name="role" />.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user. </param>
		/// <param name="role">The role of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user (for example, Administrator). </param>
		// Token: 0x06003B0B RID: 15115 RVA: 0x000CA4A4 File Offset: 0x000C86A4
		public PrincipalPermission(string name, string role) : this(name, role, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PrincipalPermission" /> class for the specified <paramref name="name" />, <paramref name="role" />, and authentication status.</summary>
		/// <param name="name">The name of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user. </param>
		/// <param name="role">The role of the <see cref="T:System.Security.Principal.IPrincipal" /> object's user (for example, Administrator). </param>
		/// <param name="isAuthenticated">true to signify that the user is authenticated; otherwise, false. </param>
		// Token: 0x06003B0C RID: 15116 RVA: 0x000CA4B0 File Offset: 0x000C86B0
		public PrincipalPermission(string name, string role, bool isAuthenticated)
		{
			this.principals = new ArrayList();
			PrincipalPermission.PrincipalInfo value = new PrincipalPermission.PrincipalInfo(name, role, isAuthenticated);
			this.principals.Add(value);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000CA4E4 File Offset: 0x000C86E4
		internal PrincipalPermission(ArrayList principals)
		{
			this.principals = (ArrayList)principals.Clone();
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000CA500 File Offset: 0x000C8700
		int IBuiltInPermission.GetTokenIndex()
		{
			return 8;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003B0F RID: 15119 RVA: 0x000CA504 File Offset: 0x000C8704
		public IPermission Copy()
		{
			return new PrincipalPermission(this.principals);
		}

		/// <summary>Determines at run time whether the current principal matches the principal specified by the current permission.</summary>
		/// <exception cref="T:System.Security.SecurityException">The current principal does not pass the security check for the principal specified by the current permission.-or- The current <see cref="T:System.Security.Principal.IPrincipal" /> is null. </exception>
		// Token: 0x06003B10 RID: 15120 RVA: 0x000CA514 File Offset: 0x000C8714
		public void Demand()
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null)
			{
				throw new SecurityException("no Principal");
			}
			if (this.principals.Count > 0)
			{
				bool flag = false;
				foreach (object obj in this.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
					if ((principalInfo.Name == null || principalInfo.Name == currentPrincipal.Identity.Name) && (principalInfo.Role == null || currentPrincipal.IsInRole(principalInfo.Role)) && ((principalInfo.IsAuthenticated && currentPrincipal.Identity.IsAuthenticated) || !principalInfo.IsAuthenticated))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new SecurityException("Demand for principal refused.");
				}
			}
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter is not a valid permission element.-or- The <paramref name="elem" /> parameter's version number is not valid. </exception>
		// Token: 0x06003B11 RID: 15121 RVA: 0x000CA62C File Offset: 0x000C882C
		public void FromXml(SecurityElement elem)
		{
			this.CheckSecurityElement(elem, "elem", 1, 1);
			this.principals.Clear();
			if (elem.Children != null)
			{
				foreach (object obj in elem.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (securityElement.Tag != "Identity")
					{
						throw new ArgumentException("not IPermission/Identity");
					}
					string name = securityElement.Attribute("ID");
					string role = securityElement.Attribute("Role");
					string text = securityElement.Attribute("Authenticated");
					bool isAuthenticated = false;
					if (text != null)
					{
						try
						{
							isAuthenticated = bool.Parse(text);
						}
						catch
						{
						}
					}
					PrincipalPermission.PrincipalInfo value = new PrincipalPermission.PrincipalInfo(name, role, isAuthenticated);
					this.principals.Add(value);
				}
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission will be null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission. </exception>
		// Token: 0x06003B12 RID: 15122 RVA: 0x000CA750 File Offset: 0x000C8950
		public IPermission Intersect(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.Copy();
			}
			if (principalPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(PermissionState.None);
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if (principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						string text = null;
						if (principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null)
						{
							text = principalInfo.Name;
						}
						else if (principalInfo.Name == null)
						{
							text = principalInfo2.Name;
						}
						string text2 = null;
						if (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null)
						{
							text2 = principalInfo.Role;
						}
						else if (principalInfo.Role == null)
						{
							text2 = principalInfo2.Role;
						}
						if (text != null || text2 != null)
						{
							PrincipalPermission.PrincipalInfo value = new PrincipalPermission.PrincipalInfo(text, text2, principalInfo.IsAuthenticated);
							principalPermission2.principals.Add(value);
						}
					}
				}
			}
			return (principalPermission2.principals.Count <= 0) ? null : principalPermission2;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission. </exception>
		// Token: 0x06003B13 RID: 15123 RVA: 0x000CA93C File Offset: 0x000C8B3C
		public bool IsSubsetOf(IPermission target)
		{
			PrincipalPermission principalPermission = this.Cast(target);
			if (principalPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return principalPermission.IsUnrestricted();
			}
			if (principalPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				bool flag = false;
				foreach (object obj2 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj2;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003B14 RID: 15124 RVA: 0x000CAAA8 File Offset: 0x000C8CA8
		public bool IsUnrestricted()
		{
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				if (principalInfo.Name == null && principalInfo.Role == null && principalInfo.IsAuthenticated)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates and returns a string representing the current permission.</summary>
		/// <returns>A representation of the current permission.</returns>
		// Token: 0x06003B15 RID: 15125 RVA: 0x000CAB3C File Offset: 0x000C8D3C
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003B16 RID: 15126 RVA: 0x000CAB4C File Offset: 0x000C8D4C
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Permission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", 1.ToString());
			foreach (object obj in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj;
				SecurityElement securityElement2 = new SecurityElement("Identity");
				if (principalInfo.Name != null)
				{
					securityElement2.AddAttribute("ID", principalInfo.Name);
				}
				if (principalInfo.Role != null)
				{
					securityElement2.AddAttribute("Role", principalInfo.Role);
				}
				if (principalInfo.IsAuthenticated)
				{
					securityElement2.AddAttribute("Authenticated", "true");
				}
				securityElement.AddChild(securityElement2);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is an object that is not of the same type as the current permission. </exception>
		// Token: 0x06003B17 RID: 15127 RVA: 0x000CAC78 File Offset: 0x000C8E78
		public IPermission Union(IPermission other)
		{
			PrincipalPermission principalPermission = this.Cast(other);
			if (principalPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
			{
				return new PrincipalPermission(PermissionState.Unrestricted);
			}
			PrincipalPermission principalPermission2 = new PrincipalPermission(this.principals);
			foreach (object obj in principalPermission.principals)
			{
				PrincipalPermission.PrincipalInfo value = (PrincipalPermission.PrincipalInfo)obj;
				principalPermission2.principals.Add(value);
			}
			return principalPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.PrincipalPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.PrincipalPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.PrincipalPermission" /> is equal to the current <see cref="T:System.Security.Permissions.PrincipalPermission" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.PrincipalPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.PrincipalPermission" />. </param>
		// Token: 0x06003B18 RID: 15128 RVA: 0x000CAD30 File Offset: 0x000C8F30
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PrincipalPermission principalPermission = obj as PrincipalPermission;
			if (principalPermission == null)
			{
				return false;
			}
			if (this.principals.Count != principalPermission.principals.Count)
			{
				return false;
			}
			foreach (object obj2 in this.principals)
			{
				PrincipalPermission.PrincipalInfo principalInfo = (PrincipalPermission.PrincipalInfo)obj2;
				bool flag = false;
				foreach (object obj3 in principalPermission.principals)
				{
					PrincipalPermission.PrincipalInfo principalInfo2 = (PrincipalPermission.PrincipalInfo)obj3;
					if ((principalInfo.Name == principalInfo2.Name || principalInfo2.Name == null) && (principalInfo.Role == principalInfo2.Role || principalInfo2.Role == null) && principalInfo.IsAuthenticated == principalInfo2.IsAuthenticated)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.PrincipalPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.PrincipalPermission" /> object.</returns>
		// Token: 0x06003B19 RID: 15129 RVA: 0x000CAEA4 File Offset: 0x000C90A4
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000CAEAC File Offset: 0x000C90AC
		private PrincipalPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PrincipalPermission principalPermission = target as PrincipalPermission;
			if (principalPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PrincipalPermission));
			}
			return principalPermission;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000CAEE0 File Offset: 0x000C90E0
		private bool IsEmpty()
		{
			return this.principals.Count == 0;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000CAEF0 File Offset: 0x000C90F0
		internal int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "Permission")
			{
				string message = string.Format(Locale.GetText("Invalid tag {0}"), se.Tag);
				throw new ArgumentException(message, parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception innerException)
				{
					string text2 = Locale.GetText("Couldn't parse version from '{0}'.");
					text2 = string.Format(text2, text);
					throw new ArgumentException(text2, parameterName, innerException);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text3 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text3 = string.Format(text3, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text3, parameterName);
			}
			return num;
		}

		// Token: 0x040019B7 RID: 6583
		private const int version = 1;

		// Token: 0x040019B8 RID: 6584
		private ArrayList principals;

		// Token: 0x02000610 RID: 1552
		internal class PrincipalInfo
		{
			// Token: 0x06003B1D RID: 15133 RVA: 0x000CAFE0 File Offset: 0x000C91E0
			public PrincipalInfo(string name, string role, bool isAuthenticated)
			{
				this._name = name;
				this._role = role;
				this._isAuthenticated = isAuthenticated;
			}

			// Token: 0x17000B24 RID: 2852
			// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000CB000 File Offset: 0x000C9200
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x17000B25 RID: 2853
			// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000CB008 File Offset: 0x000C9208
			public string Role
			{
				get
				{
					return this._role;
				}
			}

			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06003B20 RID: 15136 RVA: 0x000CB010 File Offset: 0x000C9210
			public bool IsAuthenticated
			{
				get
				{
					return this._isAuthenticated;
				}
			}

			// Token: 0x040019B9 RID: 6585
			private string _name;

			// Token: 0x040019BA RID: 6586
			private string _role;

			// Token: 0x040019BB RID: 6587
			private bool _isAuthenticated;
		}
	}
}
