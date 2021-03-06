using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Controls access to system and user environment variables. This class cannot be inherited.</summary>
	// Token: 0x020005F1 RID: 1521
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003A0A RID: 14858 RVA: 0x000C719C File Offset: 0x000C539C
		public EnvironmentPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.EnvironmentPermission" /> class with the specified access to the specified environment variables.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated) to which access is granted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003A0B RID: 14859 RVA: 0x000C71C8 File Offset: 0x000C53C8
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.SetPathList(flag, pathList);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000C71FC File Offset: 0x000C53FC
		int IBuiltInPermission.GetTokenIndex()
		{
			return 0;
		}

		/// <summary>Adds access for the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003A0D RID: 14861 RVA: 0x000C7200 File Offset: 0x000C5400
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
			{
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					if (!this.readList.Contains(text))
					{
						this.readList.Add(text);
					}
				}
				break;
			}
			case EnvironmentPermissionAccess.Write:
			{
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string text2 in array)
				{
					if (!this.writeList.Contains(text2))
					{
						this.writeList.Add(text2);
					}
				}
				break;
			}
			case EnvironmentPermissionAccess.AllAccess:
			{
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string text3 in array)
				{
					if (!this.readList.Contains(text3))
					{
						this.readList.Add(text3);
					}
					if (!this.writeList.Contains(text3))
					{
						this.writeList.Add(text3);
					}
				}
				break;
			}
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003A0E RID: 14862 RVA: 0x000C7370 File Offset: 0x000C5570
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(this._state);
			string pathList = this.GetPathList(EnvironmentPermissionAccess.Read);
			if (pathList != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, pathList);
			}
			pathList = this.GetPathList(EnvironmentPermissionAccess.Write);
			if (pathList != null)
			{
				environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, pathList);
			}
			return environmentPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003A0F RID: 14863 RVA: 0x000C73B8 File Offset: 0x000C55B8
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Read");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Read, text);
			}
			string text2 = esd.Attribute("Write");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(EnvironmentPermissionAccess.Write, text2);
			}
		}

		/// <summary>Gets all environment variables with the specified <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.</summary>
		/// <returns>A list of environment variables (semicolon-separated) for the selected flag.</returns>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values that represents a single type of environment variable access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.-or- <paramref name="flag" /> is <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.AllAccess" />, which represents more than one type of environment variable access, or <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.NoAccess" />, which does not represent any type of environment variable access. </exception>
		// Token: 0x06003A10 RID: 14864 RVA: 0x000C7434 File Offset: 0x000C5634
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
			case EnvironmentPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(flag, true);
				break;
			case EnvironmentPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case EnvironmentPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
			return null;
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A11 RID: 14865 RVA: 0x000C7494 File Offset: 0x000C5694
		public override IPermission Intersect(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.Copy();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			int num = 0;
			EnvironmentPermission environmentPermission2 = new EnvironmentPermission(PermissionState.None);
			string pathList = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (pathList != null)
			{
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string text in array)
				{
					if (this.readList.Contains(text))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, text);
						num++;
					}
				}
			}
			string pathList2 = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (pathList2 != null)
			{
				string[] array3 = pathList2.Split(new char[]
				{
					';'
				});
				foreach (string text2 in array3)
				{
					if (this.writeList.Contains(text2))
					{
						environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, text2);
						num++;
					}
				}
			}
			return (num <= 0) ? null : environmentPermission2;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A12 RID: 14866 RVA: 0x000C75B8 File Offset: 0x000C57B8
		public override bool IsSubsetOf(IPermission target)
		{
			EnvironmentPermission environmentPermission = this.Cast(target);
			if (environmentPermission == null)
			{
				return false;
			}
			if (this.IsUnrestricted())
			{
				return environmentPermission.IsUnrestricted();
			}
			if (environmentPermission.IsUnrestricted())
			{
				return true;
			}
			foreach (object obj in this.readList)
			{
				string item = (string)obj;
				if (!environmentPermission.readList.Contains(item))
				{
					return false;
				}
			}
			foreach (object obj2 in this.writeList)
			{
				string item2 = (string)obj2;
				if (!environmentPermission.writeList.Contains(item2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003A13 RID: 14867 RVA: 0x000C76E8 File Offset: 0x000C58E8
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		/// <summary>Sets the specified access to the specified environment variables to the existing state of the permission.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of environment variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />. </exception>
		// Token: 0x06003A14 RID: 14868 RVA: 0x000C76F4 File Offset: 0x000C58F4
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (flag)
			{
			case EnvironmentPermissionAccess.NoAccess:
				break;
			case EnvironmentPermissionAccess.Read:
			{
				this.readList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value in array)
				{
					this.readList.Add(value);
				}
				break;
			}
			case EnvironmentPermissionAccess.Write:
			{
				this.writeList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value2 in array)
				{
					this.writeList.Add(value2);
				}
				break;
			}
			case EnvironmentPermissionAccess.AllAccess:
			{
				this.readList.Clear();
				this.writeList.Clear();
				string[] array = pathList.Split(new char[]
				{
					';'
				});
				foreach (string value3 in array)
				{
					this.readList.Add(value3);
					this.writeList.Add(value3);
				}
				break;
			}
			default:
				this.ThrowInvalidFlag(flag, false);
				break;
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003A15 RID: 14869 RVA: 0x000C784C File Offset: 0x000C5A4C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string pathList = this.GetPathList(EnvironmentPermissionAccess.Read);
				if (pathList != null)
				{
					securityElement.AddAttribute("Read", pathList);
				}
				pathList = this.GetPathList(EnvironmentPermissionAccess.Write);
				if (pathList != null)
				{
					securityElement.AddAttribute("Write", pathList);
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A16 RID: 14870 RVA: 0x000C78B8 File Offset: 0x000C5AB8
		public override IPermission Union(IPermission other)
		{
			EnvironmentPermission environmentPermission = this.Cast(other);
			if (environmentPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && environmentPermission.IsEmpty())
			{
				return null;
			}
			EnvironmentPermission environmentPermission2 = (EnvironmentPermission)this.Copy();
			string pathList = environmentPermission.GetPathList(EnvironmentPermissionAccess.Read);
			if (pathList != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Read, pathList);
			}
			pathList = environmentPermission.GetPathList(EnvironmentPermissionAccess.Write);
			if (pathList != null)
			{
				environmentPermission2.AddPathList(EnvironmentPermissionAccess.Write, pathList);
			}
			return environmentPermission2;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000C7948 File Offset: 0x000C5B48
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000C7984 File Offset: 0x000C5B84
		private EnvironmentPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			EnvironmentPermission environmentPermission = target as EnvironmentPermission;
			if (environmentPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(EnvironmentPermission));
			}
			return environmentPermission;
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000C79B8 File Offset: 0x000C5BB8
		internal void ThrowInvalidFlag(EnvironmentPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000C7A00 File Offset: 0x000C5C00
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string value = (string)obj;
				stringBuilder.Append(value);
				stringBuilder.Append(";");
			}
			string text = stringBuilder.ToString();
			int length = text.Length;
			if (length > 0)
			{
				return text.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x0400192D RID: 6445
		private const int version = 1;

		// Token: 0x0400192E RID: 6446
		private PermissionState _state;

		// Token: 0x0400192F RID: 6447
		private ArrayList readList;

		// Token: 0x04001930 RID: 6448
		private ArrayList writeList;
	}
}
