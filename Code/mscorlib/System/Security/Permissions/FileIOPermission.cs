using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files and folders. This class cannot be inherited.</summary>
	// Token: 0x020005F7 RID: 1527
	[ComVisible(true)]
	[Serializable]
	public sealed class FileIOPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003A36 RID: 14902 RVA: 0x000C7E9C File Offset: 0x000C609C
		public FileIOPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.m_Unrestricted = true;
				this.m_AllFilesAccess = FileIOPermissionAccess.AllAccess;
				this.m_AllLocalFilesAccess = FileIOPermissionAccess.AllAccess;
			}
			this.CreateLists();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="path">The absolute path of the file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter does not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003A37 RID: 14903 RVA: 0x000C7EDC File Offset: 0x000C60DC
		public FileIOPermission(FileIOPermissionAccess access, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CreateLists();
			this.AddPathList(access, path);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not a valid string. </exception>
		// Token: 0x06003A38 RID: 14904 RVA: 0x000C7F10 File Offset: 0x000C6110
		public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			this.CreateLists();
			this.AddPathList(access, pathList);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated file or directory and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="path">The absolute path of the file or directory.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter does not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003A39 RID: 14905 RVA: 0x000C7F44 File Offset: 0x000C6144
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermission" /> class with the specified access to the designated files and directories and the specified access rights to file control information.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> enumeration values. </param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  enumeration values.</param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not a valid string. </exception>
		// Token: 0x06003A3A RID: 14906 RVA: 0x000C7F54 File Offset: 0x000C6154
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x000C7F7C File Offset: 0x000C617C
		int IBuiltInPermission.GetTokenIndex()
		{
			return 2;
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x000C7F80 File Offset: 0x000C6180
		internal void CreateLists()
		{
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.appendList = new ArrayList();
			this.pathList = new ArrayList();
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>The set of file I/O flags for all files.</returns>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000C7FBC File Offset: 0x000C61BC
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x000C7FC4 File Offset: 0x000C61C4
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.m_AllFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllFilesAccess = value;
				}
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>The set of file I/O flags for all local files.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000C7FD8 File Offset: 0x000C61D8
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000C7FE0 File Offset: 0x000C61E0
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.m_AllLocalFilesAccess;
			}
			set
			{
				if (!this.m_Unrestricted)
				{
					this.m_AllLocalFilesAccess = value;
				}
			}
		}

		/// <summary>Adds access for the specified file or directory to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="path">The absolute path of a file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter did not specify the absolute path to the file or directory. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		// Token: 0x06003A42 RID: 14914 RVA: 0x000C7FF4 File Offset: 0x000C61F4
		public void AddPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.AddPathInternal(access, path);
		}

		/// <summary>Adds access for the specified files and directories to the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> array is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="pathList" /> parameter is null. </exception>
		// Token: 0x06003A43 RID: 14915 RVA: 0x000C8018 File Offset: 0x000C6218
		public void AddPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			foreach (string path in pathList)
			{
				this.AddPathInternal(access, path);
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x000C8060 File Offset: 0x000C6260
		internal void AddPathInternal(FileIOPermissionAccess access, string path)
		{
			path = Path.InsecureGetFullPath(path);
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Add(path);
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Add(path);
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Add(path);
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003A45 RID: 14917 RVA: 0x000C80D0 File Offset: 0x000C62D0
		public override IPermission Copy()
		{
			if (this.m_Unrestricted)
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			return new FileIOPermission(PermissionState.None)
			{
				readList = (ArrayList)this.readList.Clone(),
				writeList = (ArrayList)this.writeList.Clone(),
				appendList = (ArrayList)this.appendList.Clone(),
				pathList = (ArrayList)this.pathList.Clone(),
				m_AllFilesAccess = this.m_AllFilesAccess,
				m_AllLocalFilesAccess = this.m_AllLocalFilesAccess
			};
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not compatible. </exception>
		// Token: 0x06003A46 RID: 14918 RVA: 0x000C8168 File Offset: 0x000C6368
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.m_Unrestricted = true;
			}
			else
			{
				this.m_Unrestricted = false;
				string text = esd.Attribute("Read");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Read, array);
				}
				text = esd.Attribute("Write");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Write, array);
				}
				text = esd.Attribute("Append");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.Append, array);
				}
				text = esd.Attribute("PathDiscovery");
				if (text != null)
				{
					string[] array = text.Split(new char[]
					{
						';'
					});
					this.AddPathList(FileIOPermissionAccess.PathDiscovery, array);
				}
			}
		}

		/// <summary>Gets all files and directories with the specified <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.</summary>
		/// <returns>An array containing the paths of the files and directories to which access specified by the <paramref name="access" /> parameter is granted.</returns>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents a single type of file access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- <paramref name="access" /> is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, which represents more than one type of file access, or <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, which does not represent any type of file access. </exception>
		// Token: 0x06003A47 RID: 14919 RVA: 0x000C8254 File Offset: 0x000C6454
		public string[] GetPathList(FileIOPermissionAccess access)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			ArrayList arrayList = new ArrayList();
			switch (access)
			{
			case FileIOPermissionAccess.NoAccess:
				goto IL_9D;
			case FileIOPermissionAccess.Read:
				arrayList.AddRange(this.readList);
				goto IL_9D;
			case FileIOPermissionAccess.Write:
				arrayList.AddRange(this.writeList);
				goto IL_9D;
			case FileIOPermissionAccess.Append:
				arrayList.AddRange(this.appendList);
				goto IL_9D;
			case FileIOPermissionAccess.PathDiscovery:
				arrayList.AddRange(this.pathList);
				goto IL_9D;
			}
			FileIOPermission.ThrowInvalidFlag(access, false);
			IL_9D:
			return (arrayList.Count <= 0) ? null : ((string[])arrayList.ToArray(typeof(string)));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A48 RID: 14920 RVA: 0x000C8328 File Offset: 0x000C6528
		public override IPermission Intersect(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.Copy();
			}
			if (fileIOPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			FileIOPermission fileIOPermission2 = new FileIOPermission(PermissionState.None);
			fileIOPermission2.AllFiles = (this.m_AllFilesAccess & fileIOPermission.AllFiles);
			fileIOPermission2.AllLocalFiles = (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles);
			FileIOPermission.IntersectKeys(this.readList, fileIOPermission.readList, fileIOPermission2.readList);
			FileIOPermission.IntersectKeys(this.writeList, fileIOPermission.writeList, fileIOPermission2.writeList);
			FileIOPermission.IntersectKeys(this.appendList, fileIOPermission.appendList, fileIOPermission2.appendList);
			FileIOPermission.IntersectKeys(this.pathList, fileIOPermission.pathList, fileIOPermission2.pathList);
			return (!fileIOPermission2.IsEmpty()) ? fileIOPermission2 : null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A49 RID: 14921 RVA: 0x000C8404 File Offset: 0x000C6604
		public override bool IsSubsetOf(IPermission target)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(target);
			if (fileIOPermission == null)
			{
				return false;
			}
			if (fileIOPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return fileIOPermission.IsUnrestricted();
			}
			return fileIOPermission.IsUnrestricted() || ((this.m_AllFilesAccess & fileIOPermission.AllFiles) == this.m_AllFilesAccess && (this.m_AllLocalFilesAccess & fileIOPermission.AllLocalFiles) == this.m_AllLocalFilesAccess && FileIOPermission.KeyIsSubsetOf(this.appendList, fileIOPermission.appendList) && FileIOPermission.KeyIsSubsetOf(this.readList, fileIOPermission.readList) && FileIOPermission.KeyIsSubsetOf(this.writeList, fileIOPermission.writeList) && FileIOPermission.KeyIsSubsetOf(this.pathList, fileIOPermission.pathList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003A4A RID: 14922 RVA: 0x000C84E8 File Offset: 0x000C66E8
		public bool IsUnrestricted()
		{
			return this.m_Unrestricted;
		}

		/// <summary>Sets the specified access to the specified file or directory, replacing the existing state of the permission.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="path">The absolute path of the file or directory. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- The <paramref name="path" /> parameter is not a valid string.-or- The <paramref name="path" /> parameter did not specify the absolute path to the file or directory. </exception>
		// Token: 0x06003A4B RID: 14923 RVA: 0x000C84F0 File Offset: 0x000C66F0
		public void SetPathList(FileIOPermissionAccess access, string path)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(path);
			this.Clear(access);
			this.AddPathInternal(access, path);
		}

		/// <summary>Sets the specified access to the specified files and directories, replacing the current state for the specified access with the new set of paths.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values. </param>
		/// <param name="pathList">An array containing the absolute paths of the files and directories. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.-or- An entry in the <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003A4C RID: 14924 RVA: 0x000C8524 File Offset: 0x000C6724
		public void SetPathList(FileIOPermissionAccess access, string[] pathList)
		{
			if ((FileIOPermissionAccess.AllAccess & access) != access)
			{
				FileIOPermission.ThrowInvalidFlag(access, true);
			}
			FileIOPermission.ThrowIfInvalidPath(pathList);
			this.Clear(access);
			foreach (string path in pathList)
			{
				this.AddPathInternal(access, path);
			}
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003A4D RID: 14925 RVA: 0x000C8574 File Offset: 0x000C6774
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string[] array = this.GetPathList(FileIOPermissionAccess.Append);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Append", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Read);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Read", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.Write);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("Write", string.Join(";", array));
				}
				array = this.GetPathList(FileIOPermissionAccess.PathDiscovery);
				if (array != null && array.Length > 0)
				{
					securityElement.AddAttribute("PathDiscovery", string.Join(";", array));
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003A4E RID: 14926 RVA: 0x000C8660 File Offset: 0x000C6860
		public override IPermission Union(IPermission other)
		{
			FileIOPermission fileIOPermission = FileIOPermission.Cast(other);
			if (fileIOPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileIOPermission.IsUnrestricted())
			{
				return new FileIOPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && fileIOPermission.IsEmpty())
			{
				return null;
			}
			FileIOPermission fileIOPermission2 = (FileIOPermission)this.Copy();
			fileIOPermission2.AllFiles |= fileIOPermission.AllFiles;
			fileIOPermission2.AllLocalFiles |= fileIOPermission.AllLocalFiles;
			string[] array = fileIOPermission.GetPathList(FileIOPermissionAccess.Read);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.readList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Write);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.writeList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.Append);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.appendList, array);
			}
			array = fileIOPermission.GetPathList(FileIOPermissionAccess.PathDiscovery);
			if (array != null)
			{
				FileIOPermission.UnionKeys(fileIOPermission2.pathList, array);
			}
			return fileIOPermission2;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> object is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Permissions.FileIOPermission" /> is equal to the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Permissions.FileIOPermission" /> object to compare with the current <see cref="T:System.Security.Permissions.FileIOPermission" />. </param>
		// Token: 0x06003A4F RID: 14927 RVA: 0x000C8754 File Offset: 0x000C6954
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return false;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Permissions.FileIOPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.FileIOPermission" /> object.</returns>
		// Token: 0x06003A50 RID: 14928 RVA: 0x000C8758 File Offset: 0x000C6958
		[MonoTODO("(2.0)")]
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000C8760 File Offset: 0x000C6960
		private bool IsEmpty()
		{
			return !this.m_Unrestricted && this.appendList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0 && this.pathList.Count == 0;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000C87BC File Offset: 0x000C69BC
		private static FileIOPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileIOPermission fileIOPermission = target as FileIOPermission;
			if (fileIOPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileIOPermission));
			}
			return fileIOPermission;
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000C87F0 File Offset: 0x000C69F0
		internal static void ThrowInvalidFlag(FileIOPermissionAccess access, bool context)
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
			throw new ArgumentException(string.Format(text, access), "access");
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000C8838 File Offset: 0x000C6A38
		internal static void ThrowIfInvalidPath(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName != null && directoryName.LastIndexOfAny(FileIOPermission.BadPathNameCharacters) >= 0)
			{
				string message = string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), path);
				throw new ArgumentException(message, "path");
			}
			string fileName = Path.GetFileName(path);
			if (fileName != null && fileName.LastIndexOfAny(FileIOPermission.BadFileNameCharacters) >= 0)
			{
				string message2 = string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), path);
				throw new ArgumentException(message2, "path");
			}
			if (!Path.IsPathRooted(path))
			{
				string text = Locale.GetText("Absolute path information is required.");
				throw new ArgumentException(text, "path");
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000C88E0 File Offset: 0x000C6AE0
		internal static void ThrowIfInvalidPath(string[] paths)
		{
			foreach (string path in paths)
			{
				FileIOPermission.ThrowIfInvalidPath(path);
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000C8910 File Offset: 0x000C6B10
		internal void Clear(FileIOPermissionAccess access)
		{
			if ((access & FileIOPermissionAccess.Read) == FileIOPermissionAccess.Read)
			{
				this.readList.Clear();
			}
			if ((access & FileIOPermissionAccess.Write) == FileIOPermissionAccess.Write)
			{
				this.writeList.Clear();
			}
			if ((access & FileIOPermissionAccess.Append) == FileIOPermissionAccess.Append)
			{
				this.appendList.Clear();
			}
			if ((access & FileIOPermissionAccess.PathDiscovery) == FileIOPermissionAccess.PathDiscovery)
			{
				this.pathList.Clear();
			}
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x000C8970 File Offset: 0x000C6B70
		internal static bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string path = (string)obj;
				foreach (object obj2 in target)
				{
					string subset = (string)obj2;
					if (Path.IsPathSubsetOf(subset, path))
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

		// Token: 0x06003A58 RID: 14936 RVA: 0x000C8A58 File Offset: 0x000C6C58
		internal static void UnionKeys(IList list, string[] paths)
		{
			foreach (string text in paths)
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					int j;
					for (j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (Path.IsPathSubsetOf(text, text2))
						{
							list[j] = text;
							break;
						}
						if (Path.IsPathSubsetOf(text2, text))
						{
							break;
						}
					}
					if (j == count)
					{
						list.Add(text);
					}
				}
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000C8AFC File Offset: 0x000C6CFC
		internal static void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (Path.IsPathSubsetOf(text, text2))
						{
							result.Add(text2);
						}
					}
					else if (Path.IsPathSubsetOf(text2, text))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x04001941 RID: 6465
		private const int version = 1;

		// Token: 0x04001942 RID: 6466
		private static char[] BadPathNameCharacters = Path.GetInvalidPathChars();

		// Token: 0x04001943 RID: 6467
		private static char[] BadFileNameCharacters = Path.GetInvalidFileNameChars();

		// Token: 0x04001944 RID: 6468
		private bool m_Unrestricted;

		// Token: 0x04001945 RID: 6469
		private FileIOPermissionAccess m_AllFilesAccess;

		// Token: 0x04001946 RID: 6470
		private FileIOPermissionAccess m_AllLocalFilesAccess;

		// Token: 0x04001947 RID: 6471
		private ArrayList readList;

		// Token: 0x04001948 RID: 6472
		private ArrayList writeList;

		// Token: 0x04001949 RID: 6473
		private ArrayList appendList;

		// Token: 0x0400194A RID: 6474
		private ArrayList pathList;
	}
}
