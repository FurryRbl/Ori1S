using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.IO
{
	/// <summary>Provides access to information on a drive.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000235 RID: 565
	[ComVisible(true)]
	[Serializable]
	public sealed class DriveInfo : ISerializable
	{
		// Token: 0x06001D5D RID: 7517 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		private DriveInfo(DriveInfo._DriveType _drive_type, string path, string fstype)
		{
			this._drive_type = _drive_type;
			this.drive_format = fstype;
			this.path = path;
		}

		/// <summary>Provides access to information on the specified drive.</summary>
		/// <param name="driveName">A valid drive path or drive letter. This can be either uppercase or lowercase, 'a' to 'z'. A null value is not valid. </param>
		/// <exception cref="T:System.ArgumentNullException">The drive letter cannot be null. </exception>
		/// <exception cref="T:System.ArgumentException">The first letter of <paramref name="driveName" /> is not an uppercase or lowercase letter from 'a' to 'z'.-or-<paramref name="driveName" /> does not refer to a valid drive.</exception>
		// Token: 0x06001D5E RID: 7518 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
		public DriveInfo(string driveName)
		{
			DriveInfo[] drives = DriveInfo.GetDrives();
			foreach (DriveInfo driveInfo in drives)
			{
				if (driveInfo.path == driveName)
				{
					this.path = driveInfo.path;
					this.drive_format = driveInfo.drive_format;
					this.path = driveInfo.path;
					return;
				}
			}
			throw new ArgumentException("The drive name does not exist", "driveName");
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06001D5F RID: 7519 RVA: 0x0006CA70 File Offset: 0x0006AC70
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0006CA78 File Offset: 0x0006AC78
		private static void GetDiskFreeSpace(string path, out ulong availableFreeSpace, out ulong totalSize, out ulong totalFreeSpace)
		{
			MonoIOError error;
			if (!DriveInfo.GetDiskFreeSpaceInternal(path, out availableFreeSpace, out totalSize, out totalFreeSpace, out error))
			{
				throw MonoIO.GetException(path, error);
			}
		}

		/// <summary>Indicates the amount of available free space on a drive.</summary>
		/// <returns>The amount of free space available on the drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		public long AvailableFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				return (long)((num <= 9223372036854775807UL) ? num : 9223372036854775807UL);
			}
		}

		/// <summary>Gets the total amount of free space available on a drive.</summary>
		/// <returns>The total free space available on a drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0006CADC File Offset: 0x0006ACDC
		public long TotalFreeSpace
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				return (long)((num3 <= 9223372036854775807UL) ? num3 : 9223372036854775807UL);
			}
		}

		/// <summary>Gets the total size of storage space on a drive.</summary>
		/// <returns>The total size of the drive, in bytes.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0006CB18 File Offset: 0x0006AD18
		public long TotalSize
		{
			get
			{
				ulong num;
				ulong num2;
				ulong num3;
				DriveInfo.GetDiskFreeSpace(this.path, out num, out num2, out num3);
				return (long)((num2 <= 9223372036854775807UL) ? num2 : 9223372036854775807UL);
			}
		}

		/// <summary>Gets or sets the volume label of a drive.</summary>
		/// <returns>The volume label.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.-or-The volume label is being set on a network or CD-ROM drive.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0006CB54 File Offset: 0x0006AD54
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0006CB70 File Offset: 0x0006AD70
		[MonoTODO("Currently get only works on Mono/Unix; set not implemented")]
		public string VolumeLabel
		{
			get
			{
				if (this._drive_type != DriveInfo._DriveType.Windows)
				{
					return this.path;
				}
				return this.path;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the file system, such as NTFS or FAT32.</summary>
		/// <returns>The name of the file system on the specified drive.</returns>
		/// <exception cref="T:System.UnauthorizedAccessException">Access to the drive information is denied.</exception>
		/// <exception cref="T:System.IO.DriveNotFoundException">The drive does not exist or is not mapped.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0006CB78 File Offset: 0x0006AD78
		public string DriveFormat
		{
			get
			{
				return this.drive_format;
			}
		}

		/// <summary>Gets the drive type.</summary>
		/// <returns>One of the <see cref="T:System.IO.DriveType" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0006CB80 File Offset: 0x0006AD80
		public DriveType DriveType
		{
			get
			{
				return (DriveType)DriveInfo.GetDriveTypeInternal(this.path);
			}
		}

		/// <summary>Gets the name of a drive.</summary>
		/// <returns>The name of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0006CB90 File Offset: 0x0006AD90
		public string Name
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the root directory of a drive.</summary>
		/// <returns>A <see cref="T:System.IO.DirectoryInfo" /> object that contains the root directory of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0006CB98 File Offset: 0x0006AD98
		public DirectoryInfo RootDirectory
		{
			get
			{
				return new DirectoryInfo(this.path);
			}
		}

		/// <summary>Gets a value indicating whether a drive is ready.</summary>
		/// <returns>true if the drive is ready; false if the drive is not ready.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0006CBA8 File Offset: 0x0006ADA8
		[MonoTODO("It always returns true")]
		public bool IsReady
		{
			get
			{
				return this._drive_type == DriveInfo._DriveType.Windows || true;
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0006CBBC File Offset: 0x0006ADBC
		private static StreamReader TryOpen(string name)
		{
			if (File.Exists(name))
			{
				return new StreamReader(name, Encoding.ASCII);
			}
			return null;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006CBD8 File Offset: 0x0006ADD8
		private static DriveInfo[] LinuxGetDrives()
		{
			DriveInfo[] result;
			using (StreamReader streamReader = DriveInfo.TryOpen("/proc/mounts"))
			{
				ArrayList arrayList = new ArrayList();
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					if (!text.StartsWith("rootfs"))
					{
						int num = text.IndexOf(' ');
						if (num != -1)
						{
							string text2 = text.Substring(num + 1);
							num = text2.IndexOf(' ');
							if (num != -1)
							{
								string text3 = text2.Substring(0, num);
								text2 = text2.Substring(num + 1);
								num = text2.IndexOf(' ');
								if (num != -1)
								{
									string fstype = text2.Substring(0, num);
									arrayList.Add(new DriveInfo(DriveInfo._DriveType.Linux, text3, fstype));
								}
							}
						}
					}
				}
				result = (DriveInfo[])arrayList.ToArray(typeof(DriveInfo));
			}
			return result;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006CCE8 File Offset: 0x0006AEE8
		private static DriveInfo[] UnixGetDrives()
		{
			DriveInfo[] array = null;
			try
			{
				using (StreamReader streamReader = DriveInfo.TryOpen("/proc/sys/kernel/ostype"))
				{
					if (streamReader != null)
					{
						string a = streamReader.ReadLine();
						if (a == "Linux")
						{
							array = DriveInfo.LinuxGetDrives();
						}
					}
				}
				if (array != null)
				{
					return array;
				}
			}
			catch (Exception)
			{
			}
			return new DriveInfo[]
			{
				new DriveInfo(DriveInfo._DriveType.GenericUnix, "/", "unixfs")
			};
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0006CDA4 File Offset: 0x0006AFA4
		private static DriveInfo[] WindowsGetDrives()
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the drive names of all logical drives on a computer.</summary>
		/// <returns>An array of type <see cref="T:System.IO.DriveInfo" /> that represents the logical drives on a computer.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred (for example, a disk error or a drive was not ready). </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001D6F RID: 7535 RVA: 0x0006CDAC File Offset: 0x0006AFAC
		[MonoTODO("Currently only implemented on Mono/Linux")]
		public static DriveInfo[] GetDrives()
		{
			int platform = (int)Environment.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return DriveInfo.UnixGetDrives();
			}
			return DriveInfo.WindowsGetDrives();
		}

		/// <summary>Returns a drive name as a string.</summary>
		/// <returns>The name of the drive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001D70 RID: 7536 RVA: 0x0006CDE4 File Offset: 0x0006AFE4
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06001D71 RID: 7537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDiskFreeSpaceInternal(string pathName, out ulong freeBytesAvail, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes, out MonoIOError error);

		// Token: 0x06001D72 RID: 7538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetDriveTypeInternal(string rootPathName);

		// Token: 0x04000AF1 RID: 2801
		private DriveInfo._DriveType _drive_type;

		// Token: 0x04000AF2 RID: 2802
		private string drive_format;

		// Token: 0x04000AF3 RID: 2803
		private string path;

		// Token: 0x02000236 RID: 566
		private enum _DriveType
		{
			// Token: 0x04000AF5 RID: 2805
			GenericUnix,
			// Token: 0x04000AF6 RID: 2806
			Linux,
			// Token: 0x04000AF7 RID: 2807
			Windows
		}
	}
}
