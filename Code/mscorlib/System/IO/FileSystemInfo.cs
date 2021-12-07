using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.IO
{
	/// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000245 RID: 581
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
		// Token: 0x06001E2B RID: 7723 RVA: 0x0006FC90 File Offset: 0x0006DE90
		protected FileSystemInfo()
		{
			this.valid = false;
			this.FullPath = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> is null.</exception>
		// Token: 0x06001E2C RID: 7724 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = info.GetString("FullPath");
			this.OriginalPath = info.GetString("OriginalPath");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06001E2D RID: 7725 RVA: 0x0006FCE4 File Offset: 0x0006DEE4
		[ComVisible(false)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
		}

		/// <summary>Gets a value indicating whether the file or directory exists.</summary>
		/// <returns>true if the file or directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001E2E RID: 7726
		public abstract bool Exists { get; }

		/// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the Name property gets the name of the directory.</summary>
		/// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001E2F RID: 7727
		public abstract string Name { get; }

		/// <summary>Gets the full path of the directory or file.</summary>
		/// <returns>A string containing the full path.</returns>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0006FD28 File Offset: 0x0006DF28
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		/// <summary>Gets the string representing the extension part of the file.</summary>
		/// <returns>A string containing the <see cref="T:System.IO.FileSystemInfo" /> extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0006FD30 File Offset: 0x0006DF30
		public string Extension
		{
			get
			{
				return Path.GetExtension(this.Name);
			}
		}

		/// <summary>Gets or sets the current directory or file.</summary>
		/// <returns>
		///   <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute. </exception>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0006FD40 File Offset: 0x0006DF40
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0006FD54 File Offset: 0x0006DF54
		public FileAttributes Attributes
		{
			get
			{
				this.Refresh(false);
				return this.stat.Attributes;
			}
			set
			{
				MonoIOError error;
				if (!MonoIO.SetFileAttributes(this.FullName, value, out error))
				{
					throw MonoIO.GetException(this.FullName, error);
				}
				this.Refresh(true);
			}
		}

		/// <summary>Gets or sets the creation time of the current directory or file.</summary>
		/// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x0006FD88 File Offset: 0x0006DF88
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
		public DateTime CreationTime
		{
			get
			{
				this.Refresh(false);
				return DateTime.FromFileTime(this.stat.CreationTime);
			}
			set
			{
				long creation_time = value.ToFileTime();
				MonoIOError error;
				if (!MonoIO.SetFileTime(this.FullName, creation_time, -1L, -1L, out error))
				{
					throw MonoIO.GetException(this.FullName, error);
				}
				this.Refresh(true);
			}
		}

		/// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current directory or file.</summary>
		/// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0006FDE4 File Offset: 0x0006DFE4
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x0006FE00 File Offset: 0x0006E000
		[ComVisible(false)]
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.CreationTime.ToUniversalTime();
			}
			set
			{
				this.CreationTime = value.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
		/// <returns>The time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0006FE10 File Offset: 0x0006E010
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x0006FE2C File Offset: 0x0006E02C
		public DateTime LastAccessTime
		{
			get
			{
				this.Refresh(false);
				return DateTime.FromFileTime(this.stat.LastAccessTime);
			}
			set
			{
				long last_access_time = value.ToFileTime();
				MonoIOError error;
				if (!MonoIO.SetFileTime(this.FullName, -1L, last_access_time, -1L, out error))
				{
					throw MonoIO.GetException(this.FullName, error);
				}
				this.Refresh(true);
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
		/// <returns>The UTC time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0006FE6C File Offset: 0x0006E06C
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x0006FE90 File Offset: 0x0006E090
		[ComVisible(false)]
		public DateTime LastAccessTimeUtc
		{
			get
			{
				this.Refresh(false);
				return this.LastAccessTime.ToUniversalTime();
			}
			set
			{
				this.LastAccessTime = value.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
		/// <returns>The time the current file was last written.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0006FEBC File Offset: 0x0006E0BC
		public DateTime LastWriteTime
		{
			get
			{
				this.Refresh(false);
				return DateTime.FromFileTime(this.stat.LastWriteTime);
			}
			set
			{
				long last_write_time = value.ToFileTime();
				MonoIOError error;
				if (!MonoIO.SetFileTime(this.FullName, -1L, -1L, last_write_time, out error))
				{
					throw MonoIO.GetException(this.FullName, error);
				}
				this.Refresh(true);
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
		/// <returns>The UTC time when the current file was last written to.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0006FEFC File Offset: 0x0006E0FC
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x0006FF20 File Offset: 0x0006E120
		[ComVisible(false)]
		public DateTime LastWriteTimeUtc
		{
			get
			{
				this.Refresh(false);
				return this.LastWriteTime.ToUniversalTime();
			}
			set
			{
				this.LastWriteTime = value.ToLocalTime();
			}
		}

		/// <summary>Deletes a file or directory.</summary>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E40 RID: 7744
		public abstract void Delete();

		/// <summary>Refreshes the state of the object.</summary>
		/// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E41 RID: 7745 RVA: 0x0006FF30 File Offset: 0x0006E130
		public void Refresh()
		{
			this.Refresh(true);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0006FF3C File Offset: 0x0006E13C
		internal void Refresh(bool force)
		{
			if (this.valid && !force)
			{
				return;
			}
			MonoIOError monoIOError;
			MonoIO.GetFileStat(this.FullName, out this.stat, out monoIOError);
			this.valid = true;
			this.InternalRefresh();
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0006FF7C File Offset: 0x0006E17C
		internal virtual void InternalRefresh()
		{
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0006FF80 File Offset: 0x0006E180
		internal void CheckPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
		}

		/// <summary>Represents the fully qualified path of the directory or file.</summary>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		// Token: 0x04000B50 RID: 2896
		protected string FullPath;

		/// <summary>The path originally specified by the user, whether relative or absolute.</summary>
		// Token: 0x04000B51 RID: 2897
		protected string OriginalPath;

		// Token: 0x04000B52 RID: 2898
		internal MonoIOStat stat;

		// Token: 0x04000B53 RID: 2899
		internal bool valid;
	}
}
