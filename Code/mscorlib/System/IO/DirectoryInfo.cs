using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.IO
{
	/// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000233 RID: 563
	[ComVisible(true)]
	[Serializable]
	public sealed class DirectoryInfo : FileSystemInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.DirectoryInfo" /> class on the specified path.</summary>
		/// <param name="path">A string specifying the path on which to create the DirectoryInfo. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		// Token: 0x06001D3D RID: 7485 RVA: 0x0006C260 File Offset: 0x0006A460
		public DirectoryInfo(string path) : this(path, false)
		{
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0006C26C File Offset: 0x0006A46C
		internal DirectoryInfo(string path, bool simpleOriginalPath)
		{
			base.CheckPath(path);
			this.FullPath = Path.GetFullPath(path);
			if (simpleOriginalPath)
			{
				this.OriginalPath = Path.GetFileName(path);
			}
			else
			{
				this.OriginalPath = path;
			}
			this.Initialize();
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0006C2B8 File Offset: 0x0006A4B8
		private DirectoryInfo(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Initialize();
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0006C2C8 File Offset: 0x0006A4C8
		private void Initialize()
		{
			int num = this.FullPath.Length - 1;
			if (num > 1 && this.FullPath[num] == Path.DirectorySeparatorChar)
			{
				num--;
			}
			int num2 = this.FullPath.LastIndexOf(Path.DirectorySeparatorChar, num);
			if (num2 == -1 || (num2 == 0 && num == 0))
			{
				this.current = this.FullPath;
				this.parent = null;
			}
			else
			{
				this.current = this.FullPath.Substring(num2 + 1, num - num2);
				if (num2 == 0 && !Environment.IsRunningOnWindows)
				{
					this.parent = Path.DirectorySeparatorStr;
				}
				else
				{
					this.parent = this.FullPath.Substring(0, num2);
				}
				if (Environment.IsRunningOnWindows && this.parent.Length == 2 && this.parent[1] == ':' && char.IsLetter(this.parent[0]))
				{
					this.parent += Path.DirectorySeparatorChar;
				}
			}
		}

		/// <summary>Gets a value indicating whether the directory exists.</summary>
		/// <returns>true if the directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0006C3EC File Offset: 0x0006A5EC
		public override bool Exists
		{
			get
			{
				base.Refresh(false);
				return this.stat.Attributes != MonoIO.InvalidFileAttributes && (this.stat.Attributes & FileAttributes.Directory) != (FileAttributes)0;
			}
		}

		/// <summary>Gets the name of this <see cref="T:System.IO.DirectoryInfo" /> instance.</summary>
		/// <returns>The directory name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0006C430 File Offset: 0x0006A630
		public override string Name
		{
			get
			{
				return this.current;
			}
		}

		/// <summary>Gets the parent directory of a specified subdirectory.</summary>
		/// <returns>The parent directory, or null if the path is null or if the file path denotes a root (such as "\", "C:", or * "\\server\share").</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0006C438 File Offset: 0x0006A638
		public DirectoryInfo Parent
		{
			get
			{
				if (this.parent == null || this.parent.Length == 0)
				{
					return null;
				}
				return new DirectoryInfo(this.parent);
			}
		}

		/// <summary>Gets the root portion of a path.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object representing the root of a path.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0006C470 File Offset: 0x0006A670
		public DirectoryInfo Root
		{
			get
			{
				string pathRoot = Path.GetPathRoot(this.FullPath);
				if (pathRoot == null)
				{
					return null;
				}
				return new DirectoryInfo(pathRoot);
			}
		}

		/// <summary>Creates a directory.</summary>
		/// <exception cref="T:System.IO.IOException">The directory cannot be created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D45 RID: 7493 RVA: 0x0006C498 File Offset: 0x0006A698
		public void Create()
		{
			Directory.CreateDirectory(this.FullPath);
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid DirectoryInfo characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.-or- A file or directory already has the name specified by <paramref name="path" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.-or-The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D46 RID: 7494 RVA: 0x0006C4A8 File Offset: 0x0006A6A8
		public DirectoryInfo CreateSubdirectory(string path)
		{
			base.CheckPath(path);
			path = Path.Combine(this.FullPath, path);
			Directory.CreateDirectory(path);
			return new DirectoryInfo(path);
		}

		/// <summary>Returns a file list from the current directory.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D47 RID: 7495 RVA: 0x0006C4D8 File Offset: 0x0006A6D8
		public FileInfo[] GetFiles()
		{
			return this.GetFiles("*");
		}

		/// <summary>Returns a file list from the current directory matching the given <paramref name="searchPattern" />.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <param name="searchPattern">The search string, such as "*.txt". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains invalid characters. To determine the invalid characters, use the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D48 RID: 7496 RVA: 0x0006C4E8 File Offset: 0x0006A6E8
		public FileInfo[] GetFiles(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			string[] files = Directory.GetFiles(this.FullPath, searchPattern);
			FileInfo[] array = new FileInfo[files.Length];
			int num = 0;
			foreach (string fileName in files)
			{
				array[num++] = new FileInfo(fileName);
			}
			return array;
		}

		/// <summary>Returns the subdirectories of the current directory.</summary>
		/// <returns>An array of <see cref="T:System.IO.DirectoryInfo" /> objects.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D49 RID: 7497 RVA: 0x0006C550 File Offset: 0x0006A750
		public DirectoryInfo[] GetDirectories()
		{
			return this.GetDirectories("*");
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria.</summary>
		/// <returns>An array of type DirectoryInfo matching <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains invalid characters. To determine the invalid characters, use the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4A RID: 7498 RVA: 0x0006C560 File Offset: 0x0006A760
		public DirectoryInfo[] GetDirectories(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			string[] directories = Directory.GetDirectories(this.FullPath, searchPattern);
			DirectoryInfo[] array = new DirectoryInfo[directories.Length];
			int num = 0;
			foreach (string path in directories)
			{
				array[num++] = new DirectoryInfo(path);
			}
			return array;
		}

		/// <summary>Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries representing all the files and subdirectories in a directory.</summary>
		/// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> entries.</returns>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4B RID: 7499 RVA: 0x0006C5C8 File Offset: 0x0006A7C8
		public FileSystemInfo[] GetFileSystemInfos()
		{
			return this.GetFileSystemInfos("*");
		}

		/// <summary>Retrieves an array of strongly typed <see cref="T:System.IO.FileSystemInfo" /> objects representing the files and subdirectories matching the specified search criteria.</summary>
		/// <returns>An array of strongly typed FileSystemInfo objects matching the search criteria.</returns>
		/// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System". </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains invalid characters. To determine the invalid characters, use the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4C RID: 7500 RVA: 0x0006C5D8 File Offset: 0x0006A7D8
		public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (!Directory.Exists(this.FullPath))
			{
				throw new IOException("Invalid directory");
			}
			string[] directories = Directory.GetDirectories(this.FullPath, searchPattern);
			string[] files = Directory.GetFiles(this.FullPath, searchPattern);
			FileSystemInfo[] array = new FileSystemInfo[directories.Length + files.Length];
			int num = 0;
			foreach (string path in directories)
			{
				array[num++] = new DirectoryInfo(path);
			}
			foreach (string fileName in files)
			{
				array[num++] = new FileInfo(fileName);
			}
			return array;
		}

		/// <summary>Deletes this <see cref="T:System.IO.DirectoryInfo" /> if it is empty.</summary>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory described by this <see cref="T:System.IO.DirectoryInfo" /> object does not exist or could not be found.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is not empty. -or-The directory is the application's current working directory.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4D RID: 7501 RVA: 0x0006C6A0 File Offset: 0x0006A8A0
		public override void Delete()
		{
			this.Delete(false);
		}

		/// <summary>Deletes this instance of a <see cref="T:System.IO.DirectoryInfo" />, specifying whether to delete subdirectories and files.</summary>
		/// <param name="recursive">true to delete this directory, its subdirectories, and all files; otherwise, false. </param>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
		/// <exception cref="T:System.IO.IOException">The directory is read-only.-or- The directory contains one or more files or subdirectories and <paramref name="recursive" /> is false.-or-The directory is the application's current working directory. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4E RID: 7502 RVA: 0x0006C6AC File Offset: 0x0006A8AC
		public void Delete(bool recursive)
		{
			Directory.Delete(this.FullPath, recursive);
		}

		/// <summary>Moves a <see cref="T:System.IO.DirectoryInfo" /> instance and its contents to a new path.</summary>
		/// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destDirName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="destDirName" /> is an empty string (''"). </exception>
		/// <exception cref="T:System.IO.IOException">An attempt was made to move a directory to a different volume. -or-<paramref name="destDirName" /> already exists.-or-You are not authorized to access this path.-or- The directory being moved and the destination directory have the same name.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The destination directory cannot be found.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D4F RID: 7503 RVA: 0x0006C6BC File Offset: 0x0006A8BC
		public void MoveTo(string destDirName)
		{
			if (destDirName == null)
			{
				throw new ArgumentNullException("destDirName");
			}
			if (destDirName.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destDirName");
			}
			Directory.Move(this.FullPath, Path.GetFullPath(destDirName));
		}

		/// <summary>Returns the original path that was passed by the user.</summary>
		/// <returns>Returns the original path that was passed by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001D50 RID: 7504 RVA: 0x0006C6FC File Offset: 0x0006A8FC
		public override string ToString()
		{
			return this.OriginalPath;
		}

		/// <summary>Returns an array of directories in the current <see cref="T:System.IO.DirectoryInfo" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of type DirectoryInfo matching <paramref name="searchPattern" />.</returns>
		/// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
		/// <param name="searchOption">One of the values of the <see cref="T:System.IO.SearchOption" /> enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains invalid characters. To determine the invalid characters, use the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path encapsulated in the DirectoryInfo object is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		// Token: 0x06001D51 RID: 7505 RVA: 0x0006C704 File Offset: 0x0006A904
		public DirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
		{
			if (searchOption == SearchOption.TopDirectoryOnly)
			{
				return this.GetDirectories(searchPattern);
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				string text = Locale.GetText("Invalid enum value '{0}' for '{1}'.", new object[]
				{
					searchOption,
					"SearchOption"
				});
				throw new ArgumentOutOfRangeException("searchOption", text);
			}
			Queue queue = new Queue(this.GetDirectories(searchPattern));
			Queue queue2 = new Queue();
			while (queue.Count > 0)
			{
				DirectoryInfo directoryInfo = (DirectoryInfo)queue.Dequeue();
				DirectoryInfo[] directories = directoryInfo.GetDirectories(searchPattern);
				foreach (DirectoryInfo obj in directories)
				{
					queue.Enqueue(obj);
				}
				queue2.Enqueue(directoryInfo);
			}
			DirectoryInfo[] array2 = new DirectoryInfo[queue2.Count];
			queue2.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0006C7E4 File Offset: 0x0006A9E4
		internal int GetFilesSubdirs(ArrayList l, string pattern)
		{
			FileInfo[] array = null;
			try
			{
				array = this.GetFiles(pattern);
			}
			catch (UnauthorizedAccessException)
			{
				return 0;
			}
			int num = array.Length;
			l.Add(array);
			foreach (DirectoryInfo directoryInfo in this.GetDirectories())
			{
				num += directoryInfo.GetFilesSubdirs(l, pattern);
			}
			return num;
		}

		/// <summary>Returns a file list from the current directory matching the given <paramref name="searchPattern" /> and using a value to determine whether to search subdirectories.</summary>
		/// <returns>An array of type <see cref="T:System.IO.FileInfo" />.</returns>
		/// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
		/// <param name="searchOption">One of the values of the <see cref="T:System.IO.SearchOption" /> enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="searchPattern " />contains invalid characters. To determine invalid characters, use the  <see cref="M:System.IO.Path.GetInvalidPathChars" /> method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="searchPattern" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="searchOption" /> is not a valid <see cref="T:System.IO.SearchOption" /> value.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001D53 RID: 7507 RVA: 0x0006C86C File Offset: 0x0006AA6C
		public FileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
		{
			if (searchOption == SearchOption.TopDirectoryOnly)
			{
				return this.GetFiles(searchPattern);
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				string text = Locale.GetText("Invalid enum value '{0}' for '{1}'.", new object[]
				{
					searchOption,
					"SearchOption"
				});
				throw new ArgumentOutOfRangeException("searchOption", text);
			}
			ArrayList arrayList = new ArrayList();
			int filesSubdirs = this.GetFilesSubdirs(arrayList, searchPattern);
			int num = 0;
			FileInfo[] array = new FileInfo[filesSubdirs];
			foreach (object obj in arrayList)
			{
				FileInfo[] array2 = (FileInfo[])obj;
				array2.CopyTo(array, num);
				num += array2.Length;
			}
			return array;
		}

		/// <summary>Creates a directory using a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object.</summary>
		/// <param name="directorySecurity">The access control to apply to the directory.</param>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.NotSupportedException">Creating a directory with only the colon (:) character was attempted. </exception>
		/// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is read-only or is not empty. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D54 RID: 7508 RVA: 0x0006C950 File Offset: 0x0006AB50
		[MonoLimitation("DirectorySecurity isn't implemented")]
		public void Create(DirectorySecurity directorySecurity)
		{
			if (directorySecurity != null)
			{
				throw new UnauthorizedAccessException();
			}
			this.Create();
		}

		/// <summary>Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the <see cref="T:System.IO.DirectoryInfo" /> class.</summary>
		/// <returns>The last directory specified in <paramref name="path" />.</returns>
		/// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
		/// <param name="directorySecurity">The security to apply.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> does not specify a valid file path or contains invalid DirectoryInfo characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.IOException">The subdirectory cannot be created.-or- A file or directory already has the name specified by <paramref name="path" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. The specified path, file name, or both are too long.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have code access permission to create the directory.-or-The caller does not have code access permission to read the directory described by the returned <see cref="T:System.IO.DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> contains a colon (:) that is not part of a drive label ("C:\").</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D55 RID: 7509 RVA: 0x0006C964 File Offset: 0x0006AB64
		[MonoLimitation("DirectorySecurity isn't implemented")]
		public DirectoryInfo CreateSubdirectory(string path, DirectorySecurity directorySecurity)
		{
			if (directorySecurity != null)
			{
				throw new UnauthorizedAccessException();
			}
			return this.CreateSubdirectory(path);
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the directory.</returns>
		/// <exception cref="T:System.SystemException">The directory could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the directory.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the directory.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The directory is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D56 RID: 7510 RVA: 0x0006C97C File Offset: 0x0006AB7C
		[MonoNotSupported("DirectorySecurity isn't implemented")]
		public DirectorySecurity GetAccessControl()
		{
			throw new UnauthorizedAccessException();
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that encapsulates the access control rules for the file described by the <paramref name="path" /> parameter.ExceptionsException typeCondition<see cref="T:System.SystemException" />The directory could not be found or modified.<see cref="T:System.UnauthorizedAccessException" />The current process does not have access to open the directory.<see cref="T:System.IO.IOException" />An I/O error occurred while opening the directory.<see cref="T:System.PlatformNotSupportedException" />The current operating system is not Microsoft Windows 2000 or later.<see cref="T:System.UnauthorizedAccessException" />The directory is read-only.-or- This operation is not supported on the current platform.-or- The caller does not have the required permission.</returns>
		/// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections" /> values that specifies the type of access control list (ACL) information to receive.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D57 RID: 7511 RVA: 0x0006C984 File Offset: 0x0006AB84
		[MonoNotSupported("DirectorySecurity isn't implemented")]
		public DirectorySecurity GetAccessControl(AccessControlSections includeSections)
		{
			throw new UnauthorizedAccessException();
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object to the directory described by the current <see cref="T:System.IO.DirectoryInfo" /> object.</summary>
		/// <param name="directorySecurity">A <see cref="T:System.Security.AccessControl.DirectorySecurity" /> object that describes an ACL entry to apply to the directory described by the <paramref name="path" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="directorySecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows 2000 or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0006C98C File Offset: 0x0006AB8C
		[MonoLimitation("DirectorySecurity isn't implemented")]
		public void SetAccessControl(DirectorySecurity directorySecurity)
		{
			if (directorySecurity != null)
			{
				throw new ArgumentNullException("directorySecurity");
			}
			throw new UnauthorizedAccessException();
		}

		// Token: 0x04000AEF RID: 2799
		private string current;

		// Token: 0x04000AF0 RID: 2800
		private string parent;
	}
}
