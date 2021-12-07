using System;
using System.Collections;

namespace System.Security.Permissions
{
	/// <summary>Allows control of code access security permissions.</summary>
	// Token: 0x0200045C RID: 1116
	[Serializable]
	public abstract class ResourcePermissionBase : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class.</summary>
		// Token: 0x06002807 RID: 10247 RVA: 0x0007EBC8 File Offset: 0x0007CDC8
		protected ResourcePermissionBase()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBase" /> class with the specified level of access to resources at creation.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06002808 RID: 10248 RVA: 0x0007EBDC File Offset: 0x0007CDDC
		protected ResourcePermissionBase(PermissionState state) : this()
		{
			PermissionHelper.CheckPermissionState(state, true);
			this._unrestricted = (state == PermissionState.Unrestricted);
		}

		/// <summary>Gets or sets an enumeration value that describes the types of access that you are giving the resource.</summary>
		/// <returns>An enumeration value that is derived from <see cref="T:System.Type" /> and describes the types of access that you are giving the resource.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The property value is not an enumeration value. </exception>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x0007EC10 File Offset: 0x0007CE10
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x0007EC18 File Offset: 0x0007CE18
		protected Type PermissionAccessType
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PermissionAccessType");
				}
				if (!value.IsEnum)
				{
					throw new ArgumentException("!Enum", "PermissionAccessType");
				}
				this._type = value;
			}
		}

		/// <summary>Gets or sets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are trying to protect.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the array is 0. </exception>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x0007EC50 File Offset: 0x0007CE50
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x0007EC58 File Offset: 0x0007CE58
		protected string[] TagNames
		{
			get
			{
				return this._tags;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("TagNames");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("Length==0", "TagNames");
				}
				this._tags = value;
			}
		}

		/// <summary>Adds a permission entry to the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.-or- The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is already included in the permission. </exception>
		// Token: 0x0600280E RID: 10254 RVA: 0x0007EC98 File Offset: 0x0007CE98
		protected void AddPermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			if (this.Exists(entry))
			{
				string text = Locale.GetText("Entry already exists.");
				throw new InvalidOperationException(text);
			}
			this._list.Add(entry);
		}

		/// <summary>Clears the permission of the added permission entries.</summary>
		// Token: 0x0600280F RID: 10255 RVA: 0x0007ECD8 File Offset: 0x0007CED8
		protected void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002810 RID: 10256 RVA: 0x0007ECE8 File Offset: 0x0007CEE8
		public override IPermission Copy()
		{
			ResourcePermissionBase resourcePermissionBase = ResourcePermissionBase.CreateFromType(base.GetType(), this._unrestricted);
			if (this._tags != null)
			{
				resourcePermissionBase._tags = (string[])this._tags.Clone();
			}
			resourcePermissionBase._type = this._type;
			resourcePermissionBase._list.AddRange(this._list);
			return resourcePermissionBase;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> parameter is not a valid permission element.-or- The version number of the <paramref name="securityElement" /> parameter is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is null.</exception>
		// Token: 0x06002811 RID: 10257 RVA: 0x0007ED48 File Offset: 0x0007CF48
		[MonoTODO("incomplete - need more test")]
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			this.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this._list.Clear();
			this._unrestricted = PermissionHelper.IsUnrestricted(securityElement);
			if (securityElement.Children == null || securityElement.Children.Count < 1)
			{
				return;
			}
			string[] array = new string[1];
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				array[0] = securityElement2.Attribute("name");
				int permissionAccess = (int)Enum.Parse(this.PermissionAccessType, securityElement2.Attribute("access"));
				ResourcePermissionBaseEntry entry = new ResourcePermissionBaseEntry(permissionAccess, array);
				this.AddPermissionAccess(entry);
			}
		}

		/// <summary>Returns an array of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects added to this permission.</summary>
		/// <returns>An array of <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> objects that were added to this permission.</returns>
		// Token: 0x06002812 RID: 10258 RVA: 0x0007EE4C File Offset: 0x0007D04C
		protected ResourcePermissionBaseEntry[] GetPermissionEntries()
		{
			ResourcePermissionBaseEntry[] array = new ResourcePermissionBaseEntry[this._list.Count];
			this._list.CopyTo(array, 0);
			return array;
		}

		/// <summary>Creates and returns a permission object that is the intersection of the current permission object and a target permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current object and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="target">A permission object of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The target permission object is not of the same type as the current permission object. </exception>
		// Token: 0x06002813 RID: 10259 RVA: 0x0007EE78 File Offset: 0x0007D078
		public override IPermission Intersect(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return null;
			}
			bool flag = this.IsUnrestricted();
			bool flag2 = resourcePermissionBase.IsUnrestricted();
			if (this.IsEmpty() && !flag2)
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty() && !flag)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag && flag2);
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
				if (flag2 || resourcePermissionBase.Exists(entry))
				{
					resourcePermissionBase2.AddPermissionAccess(entry);
				}
			}
			foreach (object obj2 in resourcePermissionBase._list)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj2;
				if ((flag || this.Exists(entry2)) && !resourcePermissionBase2.Exists(entry2))
				{
					resourcePermissionBase2.AddPermissionAccess(entry2);
				}
			}
			return resourcePermissionBase2;
		}

		/// <summary>Determines whether the current permission object is a subset of the specified permission.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object; otherwise, false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. </param>
		// Token: 0x06002814 RID: 10260 RVA: 0x0007EFE8 File Offset: 0x0007D1E8
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return true;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				return false;
			}
			if (resourcePermissionBase.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return resourcePermissionBase.IsUnrestricted();
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
				if (!resourcePermissionBase.Exists(entry))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value indicating whether the permission is unrestricted.</summary>
		/// <returns>true if permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06002815 RID: 10261 RVA: 0x0007F0A0 File Offset: 0x0007D2A0
		public bool IsUnrestricted()
		{
			return this._unrestricted;
		}

		/// <summary>Removes a permission entry from the permission.</summary>
		/// <param name="entry">The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is not equal to the number of elements in the <see cref="P:System.Security.Permissions.ResourcePermissionBase.TagNames" /> property.-or- The <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> is not in the permission. </exception>
		// Token: 0x06002816 RID: 10262 RVA: 0x0007F0A8 File Offset: 0x0007D2A8
		protected void RemovePermissionAccess(ResourcePermissionBaseEntry entry)
		{
			this.CheckEntry(entry);
			for (int i = 0; i < this._list.Count; i++)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)this._list[i];
				if (this.Equals(entry, entry2))
				{
					this._list.RemoveAt(i);
					return;
				}
			}
			string text = Locale.GetText("Entry doesn't exists.");
			throw new InvalidOperationException(text);
		}

		/// <summary>Creates and returns an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002817 RID: 10263 RVA: 0x0007F118 File Offset: 0x0007D318
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(base.GetType(), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry resourcePermissionBaseEntry = (ResourcePermissionBaseEntry)obj;
					SecurityElement securityElement2 = securityElement;
					string text = null;
					if (this.PermissionAccessType != null)
					{
						text = Enum.Format(this.PermissionAccessType, resourcePermissionBaseEntry.PermissionAccess, "g");
					}
					for (int i = 0; i < this._tags.Length; i++)
					{
						SecurityElement securityElement3 = new SecurityElement(this._tags[i]);
						securityElement3.AddAttribute("name", resourcePermissionBaseEntry.PermissionAccessPath[i]);
						if (text != null)
						{
							securityElement3.AddAttribute("access", text);
						}
						securityElement2.AddChild(securityElement3);
					}
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission object that combines the current permission object and the target permission object.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> permission object is not of the same type as the current permission object. </exception>
		// Token: 0x06002818 RID: 10264 RVA: 0x0007F240 File Offset: 0x0007D440
		public override IPermission Union(IPermission target)
		{
			ResourcePermissionBase resourcePermissionBase = this.Cast(target);
			if (resourcePermissionBase == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && resourcePermissionBase.IsEmpty())
			{
				return null;
			}
			if (resourcePermissionBase.IsEmpty())
			{
				return this.Copy();
			}
			if (this.IsEmpty())
			{
				return resourcePermissionBase.Copy();
			}
			bool flag = this.IsUnrestricted() || resourcePermissionBase.IsUnrestricted();
			ResourcePermissionBase resourcePermissionBase2 = ResourcePermissionBase.CreateFromType(base.GetType(), flag);
			if (!flag)
			{
				foreach (object obj in this._list)
				{
					ResourcePermissionBaseEntry entry = (ResourcePermissionBaseEntry)obj;
					resourcePermissionBase2.AddPermissionAccess(entry);
				}
				foreach (object obj2 in resourcePermissionBase._list)
				{
					ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj2;
					if (!resourcePermissionBase2.Exists(entry2))
					{
						resourcePermissionBase2.AddPermissionAccess(entry2);
					}
				}
			}
			return resourcePermissionBase2;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0007F3A4 File Offset: 0x0007D5A4
		private bool IsEmpty()
		{
			return !this._unrestricted && this._list.Count == 0;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
		private ResourcePermissionBase Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ResourcePermissionBase resourcePermissionBase = target as ResourcePermissionBase;
			if (resourcePermissionBase == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(ResourcePermissionBase));
			}
			return resourcePermissionBase;
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x0007F3F8 File Offset: 0x0007D5F8
		internal void CheckEntry(ResourcePermissionBaseEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (entry.PermissionAccessPath == null || entry.PermissionAccessPath.Length != this._tags.Length)
			{
				string text = Locale.GetText("Entry doesn't match TagNames");
				throw new InvalidOperationException(text);
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x0007F448 File Offset: 0x0007D648
		internal bool Equals(ResourcePermissionBaseEntry entry1, ResourcePermissionBaseEntry entry2)
		{
			if (entry1.PermissionAccess != entry2.PermissionAccess)
			{
				return false;
			}
			if (entry1.PermissionAccessPath.Length != entry2.PermissionAccessPath.Length)
			{
				return false;
			}
			for (int i = 0; i < entry1.PermissionAccessPath.Length; i++)
			{
				if (entry1.PermissionAccessPath[i] != entry2.PermissionAccessPath[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x0007F4B8 File Offset: 0x0007D6B8
		internal bool Exists(ResourcePermissionBaseEntry entry)
		{
			if (this._list.Count == 0)
			{
				return false;
			}
			foreach (object obj in this._list)
			{
				ResourcePermissionBaseEntry entry2 = (ResourcePermissionBaseEntry)obj;
				if (this.Equals(entry2, entry))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x0007F54C File Offset: 0x0007D74C
		internal int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
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

		// Token: 0x0600281F RID: 10271 RVA: 0x0007F63C File Offset: 0x0007D83C
		internal static void ValidateMachineName(string name)
		{
			if (name == null || name.Length == 0 || name.IndexOfAny(ResourcePermissionBase.invalidChars) != -1)
			{
				string text = Locale.GetText("Invalid machine name '{0}'.");
				if (name == null)
				{
					name = "(null)";
				}
				text = string.Format(text, name);
				throw new ArgumentException(text, "MachineName");
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x0007F698 File Offset: 0x0007D898
		internal static ResourcePermissionBase CreateFromType(Type type, bool unrestricted)
		{
			return (ResourcePermissionBase)Activator.CreateInstance(type, new object[]
			{
				(!unrestricted) ? PermissionState.None : PermissionState.Unrestricted
			});
		}

		// Token: 0x040018B9 RID: 6329
		private const int version = 1;

		/// <summary>Specifies the character to be used to represent the any wildcard character.</summary>
		// Token: 0x040018BA RID: 6330
		public const string Any = "*";

		/// <summary>Specifies the character to be used to represent a local reference.</summary>
		// Token: 0x040018BB RID: 6331
		public const string Local = ".";

		// Token: 0x040018BC RID: 6332
		private ArrayList _list;

		// Token: 0x040018BD RID: 6333
		private bool _unrestricted;

		// Token: 0x040018BE RID: 6334
		private Type _type;

		// Token: 0x040018BF RID: 6335
		private string[] _tags;

		// Token: 0x040018C0 RID: 6336
		private static char[] invalidChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' ',
			'\\',
			'Š'
		};
	}
}
