using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a file, supporting both synchronous and asynchronous read and write operations.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000243 RID: 579
	[ComVisible(true)]
	public class FileStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x0006E59C File Offset: 0x0006C79C
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access) : this(handle, access, true, 8192, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission and FileStream instance ownership.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0006E5B0 File Offset: 0x0006C7B0
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle) : this(handle, access, ownsHandle, 8192, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, FileStream instance ownership, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x0006E5C4 File Offset: 0x0006C7C4
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize) : this(handle, access, ownsHandle, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, FileStream instance ownership, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="ownsHandle">true if the file handle will be owned by this FileStream instance; otherwise, false. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="isAsync">true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="access" /> is less than FileAccess.Read or greater than FileAccess.ReadWrite or <paramref name="bufferSize" /> is less than or equal to 0. </exception>
		/// <exception cref="T:System.ArgumentException">The handle is invalid. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x0006E5D4 File Offset: 0x0006C7D4
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync) : this(handle, access, ownsHandle, bufferSize, isAsync, false)
		{
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0006E5E4 File Offset: 0x0006C7E4
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		internal FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool noBuffering)
		{
			this.name = "[Unknown]";
			base..ctor();
			this.handle = MonoIO.InvalidHandle;
			if (handle == this.handle)
			{
				throw new ArgumentException("handle", Locale.GetText("Invalid."));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			MonoIOError monoIOError;
			MonoFileType fileType = MonoIO.GetFileType(handle, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.name, monoIOError);
			}
			if (fileType == MonoFileType.Unknown)
			{
				throw new IOException("Invalid handle.");
			}
			if (fileType == MonoFileType.Disk)
			{
				this.canseek = true;
			}
			else
			{
				this.canseek = false;
			}
			this.handle = handle;
			this.access = access;
			this.owner = ownsHandle;
			this.async = isAsync;
			this.anonymous = false;
			this.InitBuffer(bufferSize, noBuffering);
			if (this.canseek)
			{
				this.buf_start = MonoIO.Seek(handle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.name, monoIOError);
				}
			}
			this.append_startpos = 0L;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path and creation mode.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06001DEB RID: 7659 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		public FileStream(string path, FileMode mode) : this(path, mode, (mode != FileMode.Append) ? FileAccess.ReadWrite : FileAccess.Write, FileShare.Read, 8192, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, and read/write permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06001DEC RID: 7660 RVA: 0x0006E724 File Offset: 0x0006C924
		public FileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, (access != FileAccess.Write) ? FileShare.Read : FileShare.None, 8192, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write permission, and sharing permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value. </exception>
		// Token: 0x06001DED RID: 7661 RVA: 0x0006E750 File Offset: 0x0006C950
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 8192, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, and buffer size.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists. -or-The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06001DEE RID: 7662 RVA: 0x0006E770 File Offset: 0x0006C970
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="useAsync">Specifies whether to use asynchronous I/O or synchronous I/O. However, note that the underlying operating system might not support asynchronous I/O, so when specifying true, the handle might be opened synchronously depending on the platform. When opened asynchronously, the <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> and <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> methods perform better on large reads or writes, but they might be much slower for small reads or writes. If the application is designed to take advantage of asynchronous I/O, set the <paramref name="useAsync" /> parameter to true. Using asynchronous I/O correctly can speed up applications by as much as a factor of 10, but using it without redesigning the application for asynchronous I/O can decrease performance by as much as a factor of 10. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists.-or- The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to FileShare.Delete.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06001DEF RID: 7663 RVA: 0x0006E78C File Offset: 0x0006C98C
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : this(path, mode, access, share, bufferSize, useAsync, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate. </param>
		/// <param name="mode">A constant that determines how to open or create the file. </param>
		/// <param name="access">A constant that determines how the file can be accessed by the FileStream object. This gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. <see cref="P:System.IO.FileStream.CanSeek" /> is true if <paramref name="path" /> specifies a disk file. </param>
		/// <param name="share">A constant that determines how the file will be shared by processes. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes. </param>
		/// <param name="options">A value that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists.-or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0006E7AC File Offset: 0x0006C9AC
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options) : this(path, mode, access, share, bufferSize, false, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission. </summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DF1 RID: 7665 RVA: 0x0006E7CC File Offset: 0x0006C9CC
		public FileStream(SafeFileHandle handle, FileAccess access) : this(handle, access, 8192, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed.  </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x0006E7DC File Offset: 0x0006C9DC
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) : this(handle, access, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this FileStream object will encapsulate. </param>
		/// <param name="access">A constant that gets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the FileStream object. </param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="isAsync">true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.-or-The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as a disk error.-or-The stream has been closed.  </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is Write or ReadWrite and the file handle is set for read-only access. </exception>
		// Token: 0x06001DF3 RID: 7667 RVA: 0x0006E7E8 File Offset: 0x0006C9E8
		[MonoLimitation("Need to use SafeFileHandle instead of underlying handle")]
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) : this(handle.DangerousGetHandle(), access, false, bufferSize, isAsync)
		{
			this.safeHandle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0006E810 File Offset: 0x0006CA10
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, additional file options, access control and audit security.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. For <paramref name="bufferSize" /> values between one and eight, the actual buffer size is set to eight bytes.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <param name="fileSecurity">A constant that determines the access control and audit security for the file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters. -or-<paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.-or- <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is FileMode.Truncate or FileMode.Open, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as specifying FileMode.CreateNew and the file specified by <paramref name="path" /> already exists. -or-The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is Write or ReadWrite and the file or directory is set for read-only access. -or-<see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		// Token: 0x06001DF5 RID: 7669 RVA: 0x0006E828 File Offset: 0x0006CA28
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
		{
			this.name = "[Unknown]";
			base..ctor();
			throw new NotImplementedException();
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0006E840 File Offset: 0x0006CA40
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool isAsync, bool anonymous) : this(path, mode, access, share, bufferSize, anonymous, (!isAsync) ? FileOptions.None : FileOptions.Asynchronous)
		{
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0006E870 File Offset: 0x0006CA70
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool anonymous, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			share &= ~FileShare.Inheritable;
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				throw new ArgumentOutOfRangeException("mode", "Enum value was out of legal range.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if (share < FileShare.None || share > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				throw new ArgumentOutOfRangeException("share", "Enum value was out of legal range.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Name has invalid chars");
			}
			if (Directory.Exists(path))
			{
				string text = Locale.GetText("Access to the path '{0}' is denied.");
				throw new UnauthorizedAccessException(string.Format(text, this.GetSecureFileName(path, false)));
			}
			if (mode == FileMode.Append && (access & FileAccess.Read) == FileAccess.Read)
			{
				throw new ArgumentException("Append access can be requested only in write-only mode.");
			}
			if ((access & FileAccess.Write) == (FileAccess)0 && mode != FileMode.Open && mode != FileMode.OpenOrCreate)
			{
				string text2 = Locale.GetText("Combining FileMode: {0} with FileAccess: {1} is invalid.");
				throw new ArgumentException(string.Format(text2, access, mode));
			}
			string directoryName;
			if (Path.DirectorySeparatorChar != '/' && path.IndexOf('/') >= 0)
			{
				directoryName = Path.GetDirectoryName(Path.GetFullPath(path));
			}
			else
			{
				directoryName = Path.GetDirectoryName(path);
			}
			if (directoryName.Length > 0)
			{
				string fullPath = Path.GetFullPath(directoryName);
				if (!Directory.Exists(fullPath))
				{
					string text3 = Locale.GetText("Could not find a part of the path \"{0}\".");
					string arg = (!anonymous) ? Path.GetFullPath(path) : directoryName;
					throw new DirectoryNotFoundException(string.Format(text3, arg));
				}
			}
			if (access == FileAccess.Read && mode != FileMode.Create && mode != FileMode.OpenOrCreate && mode != FileMode.CreateNew && !File.Exists(path))
			{
				string text4 = Locale.GetText("Could not find file \"{0}\".");
				string secureFileName = this.GetSecureFileName(path);
				throw new FileNotFoundException(string.Format(text4, secureFileName), secureFileName);
			}
			if (!anonymous)
			{
				this.name = path;
			}
			MonoIOError error;
			this.handle = MonoIO.Open(path, mode, access, share, options, out error);
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw MonoIO.GetException(this.GetSecureFileName(path), error);
			}
			this.access = access;
			this.owner = true;
			this.anonymous = anonymous;
			if (MonoIO.GetFileType(this.handle, out error) == MonoFileType.Disk)
			{
				this.canseek = true;
				this.async = ((options & FileOptions.Asynchronous) != FileOptions.None);
			}
			else
			{
				this.canseek = false;
				this.async = false;
			}
			if (access == FileAccess.Read && this.canseek && bufferSize == 8192)
			{
				long length = this.Length;
				if ((long)bufferSize > length)
				{
					bufferSize = (int)((length >= 1000L) ? length : 1000L);
				}
			}
			this.InitBuffer(bufferSize, false);
			if (mode == FileMode.Append)
			{
				this.Seek(0L, SeekOrigin.End);
				this.append_startpos = this.Position;
			}
			else
			{
				this.append_startpos = 0L;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>true if the stream supports reading; false if the stream is closed or was opened with write-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0006EBB4 File Offset: 0x0006CDB4
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.Read || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>true if the stream supports writing; false if the stream is closed or was opened with read-only access.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0006EBD0 File Offset: 0x0006CDD0
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.Write || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>true if the stream supports seeking; false if the stream is closed or if the FileStream was constructed from an operating-system handle such as a pipe or output to the console.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0006EBEC File Offset: 0x0006CDEC
		public override bool CanSeek
		{
			get
			{
				return this.canseek;
			}
		}

		/// <summary>Gets a value indicating whether the FileStream was opened asynchronously or synchronously.</summary>
		/// <returns>true if the FileStream was opened asynchronously; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0006EBF4 File Offset: 0x0006CDF4
		public virtual bool IsAsync
		{
			get
			{
				return this.async;
			}
		}

		/// <summary>Gets the name of the FileStream that was passed to the constructor.</summary>
		/// <returns>A string that is the name of the FileStream.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0006EBFC File Offset: 0x0006CDFC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.FileStream.CanSeek" /> for this stream is false. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs, such as the file being closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0006EC04 File Offset: 0x0006CE04
		public override long Length
		{
			get
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				this.FlushBufferIfDirty();
				MonoIOError monoIOError;
				long length = MonoIO.GetLength(this.handle, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return length;
			}
		}

		/// <summary>Gets or sets the current position of this stream.</summary>
		/// <returns>The current position of this stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. - or -The position was set to a very large value beyond the end of the stream in Windows 98 or earlier.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the position to a negative value. </exception>
		/// <exception cref="T:System.IO.EndOfStreamException">Attempted seeking past the end of a stream that does not support this. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0006EC78 File Offset: 0x0006CE78
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x0006ECCC File Offset: 0x0006CECC
		public override long Position
		{
			get
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				return this.buf_start + (long)this.buf_offset;
			}
			set
			{
				if (this.handle == MonoIO.InvalidHandle)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("Attempt to set the position to a negative value");
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>Gets the operating system file handle for the file that the current FileStream object encapsulates.</summary>
		/// <returns>The operating system file handle for the file encapsulated by this FileStream object, or -1 if the FileStream has been closed.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0006ED2C File Offset: 0x0006CF2C
		[Obsolete("Use SafeFileHandle instead")]
		public virtual IntPtr Handle
		{
			[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			get
			{
				return this.handle;
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0006ED34 File Offset: 0x0006CF34
		public virtual SafeFileHandle SafeFileHandle
		{
			[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			get
			{
				SafeFileHandle result;
				if (this.safeHandle != null)
				{
					result = this.safeHandle;
				}
				else
				{
					result = new SafeFileHandle(this.handle, this.owner);
				}
				this.FlushBuffer();
				return result;
			}
		}

		/// <summary>Reads a byte from the file and advances the read position one byte.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0006ED74 File Offset: 0x0006CF74
		public override int ReadByte()
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.buf_size != 0)
			{
				if (this.buf_offset >= this.buf_length)
				{
					this.RefillBuffer();
					if (this.buf_length == 0)
					{
						return -1;
					}
				}
				return (int)this.buf[this.buf_offset++];
			}
			if (this.ReadData(this.handle, this.buf, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.buf[0];
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte to write to the stream. </param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E03 RID: 7683 RVA: 0x0006EE24 File Offset: 0x0006D024
		public override void WriteByte(byte value)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.buf_offset == this.buf_size)
			{
				this.FlushBuffer();
			}
			if (this.buf_size == 0)
			{
				this.buf[0] = value;
				this.buf_dirty = true;
				this.buf_length = 1;
				this.FlushBuffer();
				return;
			}
			this.buf[this.buf_offset++] = value;
			if (this.buf_offset > this.buf_length)
			{
				this.buf_length = this.buf_offset;
			}
			this.buf_dirty = true;
		}

		/// <summary>Reads a block of bytes from the stream and writes the data in a given buffer.</summary>
		/// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached.</returns>
		/// <param name="array">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - <paramref name="1)" /> replaced by the bytes read from the current source. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed. </param>
		/// <param name="count">The maximum number of bytes to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E04 RID: 7684 RVA: 0x0006EEE4 File Offset: 0x0006D0E4
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			int num = array.Length;
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginRead(array, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0006EFB8 File Offset: 0x0006D1B8
		private int ReadInternal(byte[] dest, int offset, int count)
		{
			int num = 0;
			int num2 = this.ReadSegment(dest, offset, count);
			num += num2;
			count -= num2;
			if (count == 0)
			{
				return num;
			}
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				num2 = this.ReadData(this.handle, dest, offset + num, count);
				this.buf_start += (long)num2;
			}
			else
			{
				this.RefillBuffer();
				num2 = this.ReadSegment(dest, offset + num, count);
			}
			return num + num2;
		}

		/// <summary>Begins an asynchronous read.</summary>
		/// <returns>An object that references the asynchronous read.</returns>
		/// <param name="array">The buffer to read data into. </param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading. </param>
		/// <param name="numBytes">The maximum number of bytes to read. </param>
		/// <param name="userCallback">The method to be called when the asynchronous read operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">The array length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An asynchronous read was attempted past the end of the file. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E06 RID: 7686 RVA: 0x0006F034 File Offset: 0x0006D234
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("Buffer too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
			}
			FileStream.ReadDelegate readDelegate = new FileStream.ReadDelegate(this.ReadInternal);
			return readDelegate.BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Waits for the pending asynchronous read to complete.</summary>
		/// <returns>The number of bytes read from the stream, between 0 and the number of bytes you requested. Streams only return 0 at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E07 RID: 7687 RVA: 0x0006F100 File Offset: 0x0006D300
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				return base.EndRead(asyncResult);
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Writes a block of bytes to this stream using data from a buffer.</summary>
		/// <param name="array">The buffer containing data to write to the stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> at which to begin copying bytes to the current stream. </param>
		/// <param name="count">The number of bytes to be written to the current stream. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. - or -Another thread may have caused an unexpected change in the position of the operating system's file handle. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.NotSupportedException">The current stream instance does not support writing. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E08 RID: 7688 RVA: 0x0006F178 File Offset: 0x0006D378
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginWrite(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			this.WriteInternal(array, offset, count);
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0006F238 File Offset: 0x0006D438
		private void WriteInternal(byte[] src, int offset, int count)
		{
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				int i = count;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num = MonoIO.Write(this.handle, src, offset, i, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num;
					offset += num;
				}
				this.buf_start += (long)count;
			}
			else
			{
				int num2 = 0;
				while (count > 0)
				{
					int num3 = this.WriteSegment(src, offset + num2, count);
					num2 += num3;
					count -= num3;
					if (count == 0)
					{
						break;
					}
					this.FlushBuffer();
				}
			}
		}

		/// <summary>Begins an asynchronous write.</summary>
		/// <returns>An object that references the asynchronous write.</returns>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="numBytes">The maximum number of bytes to write. </param>
		/// <param name="userCallback">The method to be called when the asynchronous write operation is completed. </param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous write request from other requests. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> length minus <paramref name="offset" /> is less than <paramref name="numBytes" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0006F2E4 File Offset: 0x0006D4E4
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("array too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
			}
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(userCallback, stateObject);
			fileStreamAsyncResult.BytesRead = -1;
			fileStreamAsyncResult.Count = numBytes;
			fileStreamAsyncResult.OriginalCount = numBytes;
			if (this.buf_dirty)
			{
				MemoryStream memoryStream = new MemoryStream();
				this.FlushBuffer(memoryStream);
				memoryStream.Write(array, offset, numBytes);
				offset = 0;
				numBytes = (int)memoryStream.Length;
			}
			FileStream.WriteDelegate writeDelegate = new FileStream.WriteDelegate(this.WriteInternal);
			return writeDelegate.BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Ends an asynchronous write, blocking until the I/O operation has completed.</summary>
		/// <param name="asyncResult">The pending asynchronous I/O request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> is called multiple times. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E0B RID: 7691 RVA: 0x0006F3FC File Offset: 0x0006D5FC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				base.EndWrite(asyncResult);
				return;
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Sets the current position of this stream to the given value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point relative to <paramref name="origin" /> from which to begin seeking. </param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="origin" />, using a value of type <see cref="T:System.IO.SeekOrigin" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the FileStream is constructed from a pipe or console output. </exception>
		/// <exception cref="T:System.ArgumentException">Attempted seeking before the beginning of the stream. </exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0C RID: 7692 RVA: 0x0006F474 File Offset: 0x0006D674
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin", "Invalid SeekOrigin");
			}
			if (num < 0L)
			{
				throw new IOException("Attempted to Seek before the beginning of the stream");
			}
			if (num < this.append_startpos)
			{
				throw new IOException("Can't seek back over pre-existing data in append mode");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			this.buf_start = MonoIO.Seek(this.handle, num, SeekOrigin.Begin, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			return this.buf_start;
		}

		/// <summary>Sets the length of this stream to the given value.</summary>
		/// <param name="value">The new length of the stream. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred. </exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the <paramref name="value" /> parameter to less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E0D RID: 7693 RVA: 0x0006F568 File Offset: 0x0006D768
		public override void SetLength(long value)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value is less than 0");
			}
			this.Flush();
			MonoIOError monoIOError;
			MonoIO.SetLength(this.handle, value, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (this.Position > value)
			{
				this.Position = value;
			}
		}

		/// <summary>Clears all buffers for this stream and causes any buffered data to be written to the file system.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E0E RID: 7694 RVA: 0x0006F618 File Offset: 0x0006D818
		public override void Flush()
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
		}

		/// <summary>Prevents other processes from changing the <see cref="T:System.IO.FileStream" />.</summary>
		/// <param name="position">The beginning of the range to lock. The value of this parameter must be equal to or greater than zero (0). </param>
		/// <param name="length">The range to be locked. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed. </exception>
		/// <exception cref="T:System.IO.IOException">The process cannot access the file because another process has locked a portion of the file.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E0F RID: 7695 RVA: 0x0006F64C File Offset: 0x0006D84C
		public virtual void Lock(long position, long length)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			MonoIOError monoIOError;
			MonoIO.Lock(this.handle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		/// <summary>Allows access by other processes to all or part of a file that was previously locked.</summary>
		/// <param name="position">The beginning of the range to unlock. </param>
		/// <param name="length">The range to be unlocked. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001E10 RID: 7696 RVA: 0x0006F6E8 File Offset: 0x0006D8E8
		public virtual void Unlock(long position, long length)
		{
			if (this.handle == MonoIO.InvalidHandle)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			MonoIOError monoIOError;
			MonoIO.Unlock(this.handle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the FileStream.</summary>
		// Token: 0x06001E11 RID: 7697 RVA: 0x0006F764 File Offset: 0x0006D964
		~FileStream()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001E12 RID: 7698 RVA: 0x0006F7A0 File Offset: 0x0006D9A0
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (this.handle != MonoIO.InvalidHandle)
			{
				try
				{
					this.FlushBuffer();
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				if (this.owner)
				{
					MonoIOError monoIOError;
					MonoIO.Close(this.handle, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					this.handle = MonoIO.InvalidHandle;
				}
			}
			this.canseek = false;
			this.access = (FileAccess)0;
			if (disposing)
			{
				this.buf = null;
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <returns>An object that encapsulates the access control settings for the file described by the current <see cref="T:System.IO.FileStream" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.-or- The caller does not have the required permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E13 RID: 7699 RVA: 0x0006F860 File Offset: 0x0006DA60
		public FileSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <param name="fileSecurity">An object that describes an ACL entry to apply to the current file.</param>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is null.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E14 RID: 7700 RVA: 0x0006F868 File Offset: 0x0006DA68
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0006F870 File Offset: 0x0006DA70
		private int ReadSegment(byte[] dest, int dest_offset, int count)
		{
			if (count > this.buf_length - this.buf_offset)
			{
				count = this.buf_length - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(this.buf, this.buf_offset, dest, dest_offset, count);
				this.buf_offset += count;
			}
			return count;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x0006F8CC File Offset: 0x0006DACC
		private int WriteSegment(byte[] src, int src_offset, int count)
		{
			if (count > this.buf_size - this.buf_offset)
			{
				count = this.buf_size - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(src, src_offset, this.buf, this.buf_offset, count);
				this.buf_offset += count;
				if (this.buf_offset > this.buf_length)
				{
					this.buf_length = this.buf_offset;
				}
				this.buf_dirty = true;
			}
			return count;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0006F94C File Offset: 0x0006DB4C
		private void FlushBuffer(Stream st)
		{
			if (this.buf_dirty)
			{
				if (this.CanSeek)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.handle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				if (st == null)
				{
					MonoIOError monoIOError;
					MonoIO.Write(this.handle, this.buf, 0, this.buf_length, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				else
				{
					st.Write(this.buf, 0, this.buf_length);
				}
			}
			this.buf_start += (long)this.buf_offset;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0006FA1C File Offset: 0x0006DC1C
		private void FlushBuffer()
		{
			this.FlushBuffer(null);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0006FA28 File Offset: 0x0006DC28
		private void FlushBufferIfDirty()
		{
			if (this.buf_dirty)
			{
				this.FlushBuffer(null);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0006FA3C File Offset: 0x0006DC3C
		private void RefillBuffer()
		{
			this.FlushBuffer(null);
			this.buf_length = this.ReadData(this.handle, this.buf, 0, this.buf_size);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006FA70 File Offset: 0x0006DC70
		private int ReadData(IntPtr handle, byte[] buf, int offset, int count)
		{
			MonoIOError monoIOError;
			int num = MonoIO.Read(handle, buf, offset, count, out monoIOError);
			if (monoIOError == MonoIOError.ERROR_BROKEN_PIPE)
			{
				num = 0;
			}
			else if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (num == -1)
			{
				throw new IOException();
			}
			return num;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0006FAC4 File Offset: 0x0006DCC4
		private void InitBuffer(int size, bool noBuffering)
		{
			if (noBuffering)
			{
				size = 0;
				this.buf = new byte[1];
			}
			else
			{
				if (size <= 0)
				{
					throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
				}
				if (size < 8)
				{
					size = 8;
				}
				this.buf = new byte[size];
			}
			this.buf_size = size;
			this.buf_start = 0L;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0006FB40 File Offset: 0x0006DD40
		private string GetSecureFileName(string filename)
		{
			return (!this.anonymous) ? Path.GetFullPath(filename) : Path.GetFileName(filename);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0006FB60 File Offset: 0x0006DD60
		private string GetSecureFileName(string filename, bool full)
		{
			return (!this.anonymous) ? ((!full) ? filename : Path.GetFullPath(filename)) : Path.GetFileName(filename);
		}

		// Token: 0x04000B33 RID: 2867
		internal const int DefaultBufferSize = 8192;

		// Token: 0x04000B34 RID: 2868
		private FileAccess access;

		// Token: 0x04000B35 RID: 2869
		private bool owner;

		// Token: 0x04000B36 RID: 2870
		private bool async;

		// Token: 0x04000B37 RID: 2871
		private bool canseek;

		// Token: 0x04000B38 RID: 2872
		private long append_startpos;

		// Token: 0x04000B39 RID: 2873
		private bool anonymous;

		// Token: 0x04000B3A RID: 2874
		private byte[] buf;

		// Token: 0x04000B3B RID: 2875
		private int buf_size;

		// Token: 0x04000B3C RID: 2876
		private int buf_length;

		// Token: 0x04000B3D RID: 2877
		private int buf_offset;

		// Token: 0x04000B3E RID: 2878
		private bool buf_dirty;

		// Token: 0x04000B3F RID: 2879
		private long buf_start;

		// Token: 0x04000B40 RID: 2880
		private string name;

		// Token: 0x04000B41 RID: 2881
		private IntPtr handle;

		// Token: 0x04000B42 RID: 2882
		private SafeFileHandle safeHandle;

		// Token: 0x020006DA RID: 1754
		// (Invoke) Token: 0x0600435C RID: 17244
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x020006DB RID: 1755
		// (Invoke) Token: 0x06004360 RID: 17248
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
