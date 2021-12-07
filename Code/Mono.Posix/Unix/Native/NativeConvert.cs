using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000028 RID: 40
	[CLSCompliant(false)]
	public sealed class NativeConvert
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00008898 File Offset: 0x00006A98
		private NativeConvert()
		{
		}

		// Token: 0x06000210 RID: 528
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromRealTimeSignum")]
		private static extern int FromRealTimeSignum(int offset, out int rval);

		// Token: 0x06000211 RID: 529 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static int FromRealTimeSignum(RealTimeSignum sig)
		{
			int result;
			if (NativeConvert.FromRealTimeSignum(sig.Offset, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(sig.Offset);
			}
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000089D8 File Offset: 0x00006BD8
		public static RealTimeSignum ToRealTimeSignum(int offset)
		{
			return new RealTimeSignum(offset);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000089E0 File Offset: 0x00006BE0
		public static FilePermissions FromOctalPermissionString(string value)
		{
			uint value2 = Convert.ToUInt32(value, 8);
			return NativeConvert.ToFilePermissions(value2);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000089FC File Offset: 0x00006BFC
		public static string ToOctalPermissionString(FilePermissions value)
		{
			string text = Convert.ToString((int)(value & ~FilePermissions.S_IFMT), 8);
			return new string('0', 4 - text.Length) + text;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008A2C File Offset: 0x00006C2C
		public static FilePermissions FromUnixPermissionString(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 9 && value.Length != 10)
			{
				throw new ArgumentException("value", "must contain 9 or 10 characters");
			}
			int num = 0;
			FilePermissions filePermissions = (FilePermissions)0U;
			if (value.Length == 10)
			{
				filePermissions |= NativeConvert.GetUnixPermissionDevice(value[num]);
				num++;
			}
			filePermissions |= NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IRUSR, value[num++], FilePermissions.S_IWUSR, value[num++], FilePermissions.S_IXUSR, 's', 'S', FilePermissions.S_ISUID);
			filePermissions |= NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IRGRP, value[num++], FilePermissions.S_IWGRP, value[num++], FilePermissions.S_IXGRP, 's', 'S', FilePermissions.S_ISGID);
			return filePermissions | NativeConvert.GetUnixPermissionGroup(value[num++], FilePermissions.S_IROTH, value[num++], FilePermissions.S_IWOTH, value[num++], FilePermissions.S_IXOTH, 't', 'T', FilePermissions.S_ISVTX);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008B44 File Offset: 0x00006D44
		private static FilePermissions GetUnixPermissionDevice(char value)
		{
			switch (value)
			{
			case 'p':
				return FilePermissions.S_IFIFO;
			default:
				switch (value)
				{
				case 'b':
					return FilePermissions.S_IFBLK;
				case 'c':
					return FilePermissions.S_IFCHR;
				case 'd':
					return FilePermissions.S_IFDIR;
				default:
					if (value == '-')
					{
						return FilePermissions.S_IFREG;
					}
					if (value != 'l')
					{
						throw new ArgumentException("value", "invalid device specification: " + value);
					}
					return FilePermissions.S_IFLNK;
				}
				break;
			case 's':
				return FilePermissions.S_IFSOCK;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008BDC File Offset: 0x00006DDC
		private static FilePermissions GetUnixPermissionGroup(char read, FilePermissions readb, char write, FilePermissions writeb, char exec, FilePermissions execb, char xboth, char xbitonly, FilePermissions xbit)
		{
			FilePermissions filePermissions = (FilePermissions)0U;
			if (read == 'r')
			{
				filePermissions |= readb;
			}
			if (write == 'w')
			{
				filePermissions |= writeb;
			}
			if (exec == 'x')
			{
				filePermissions |= execb;
			}
			else if (exec == xbitonly)
			{
				filePermissions |= xbit;
			}
			else if (exec == xboth)
			{
				filePermissions |= (execb | xbit);
			}
			return filePermissions;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008C3C File Offset: 0x00006E3C
		public static string ToUnixPermissionString(FilePermissions value)
		{
			char[] array = new char[]
			{
				'-',
				'-',
				'-',
				'-',
				'-',
				'-',
				'-',
				'-',
				'-',
				'-'
			};
			bool flag = true;
			FilePermissions filePermissions = value & FilePermissions.S_IFMT;
			if (filePermissions != FilePermissions.S_IFIFO)
			{
				if (filePermissions != FilePermissions.S_IFCHR)
				{
					if (filePermissions != FilePermissions.S_IFDIR)
					{
						if (filePermissions != FilePermissions.S_IFBLK)
						{
							if (filePermissions != FilePermissions.S_IFREG)
							{
								if (filePermissions != FilePermissions.S_IFLNK)
								{
									if (filePermissions != FilePermissions.S_IFSOCK)
									{
										flag = false;
									}
									else
									{
										array[0] = 's';
									}
								}
								else
								{
									array[0] = 'l';
								}
							}
							else
							{
								array[0] = '-';
							}
						}
						else
						{
							array[0] = 'b';
						}
					}
					else
					{
						array[0] = 'd';
					}
				}
				else
				{
					array[0] = 'c';
				}
			}
			else
			{
				array[0] = 'p';
			}
			NativeConvert.SetUnixPermissionGroup(value, array, 1, FilePermissions.S_IRUSR, FilePermissions.S_IWUSR, FilePermissions.S_IXUSR, 's', 'S', FilePermissions.S_ISUID);
			NativeConvert.SetUnixPermissionGroup(value, array, 4, FilePermissions.S_IRGRP, FilePermissions.S_IWGRP, FilePermissions.S_IXGRP, 's', 'S', FilePermissions.S_ISGID);
			NativeConvert.SetUnixPermissionGroup(value, array, 7, FilePermissions.S_IROTH, FilePermissions.S_IWOTH, FilePermissions.S_IXOTH, 't', 'T', FilePermissions.S_ISVTX);
			return (!flag) ? new string(array, 1, 9) : new string(array);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008D68 File Offset: 0x00006F68
		private static void SetUnixPermissionGroup(FilePermissions value, char[] access, int index, FilePermissions read, FilePermissions write, FilePermissions exec, char both, char setonly, FilePermissions setxbit)
		{
			if (UnixFileSystemInfo.IsSet(value, read))
			{
				access[index] = 'r';
			}
			if (UnixFileSystemInfo.IsSet(value, write))
			{
				access[index + 1] = 'w';
			}
			access[index + 2] = NativeConvert.GetSymbolicMode(value, exec, both, setonly, setxbit);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008DB0 File Offset: 0x00006FB0
		private static char GetSymbolicMode(FilePermissions value, FilePermissions xbit, char both, char setonly, FilePermissions setxbit)
		{
			bool flag = UnixFileSystemInfo.IsSet(value, xbit);
			bool flag2 = UnixFileSystemInfo.IsSet(value, setxbit);
			if (flag && flag2)
			{
				return both;
			}
			if (flag2)
			{
				return setonly;
			}
			if (flag)
			{
				return 'x';
			}
			return '-';
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008DF0 File Offset: 0x00006FF0
		public static DateTime ToDateTime(long time)
		{
			return NativeConvert.FromTimeT(time);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008DF8 File Offset: 0x00006FF8
		public static long FromDateTime(DateTime time)
		{
			return NativeConvert.ToTimeT(time);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008E00 File Offset: 0x00007000
		public static DateTime FromTimeT(long time)
		{
			return NativeConvert.LocalUnixEpoch.AddSeconds((double)time + NativeConvert.LocalUtcOffset.TotalSeconds);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008E2C File Offset: 0x0000702C
		public static long ToTimeT(DateTime time)
		{
			return (long)(time.Subtract(NativeConvert.LocalUnixEpoch) - NativeConvert.LocalUtcOffset).TotalSeconds;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008E58 File Offset: 0x00007058
		public static OpenFlags ToOpenFlags(FileMode mode, FileAccess access)
		{
			OpenFlags openFlags = OpenFlags.O_RDONLY;
			switch (mode)
			{
			case FileMode.CreateNew:
				openFlags = (OpenFlags.O_CREAT | OpenFlags.O_EXCL);
				break;
			case FileMode.Create:
				openFlags = (OpenFlags.O_CREAT | OpenFlags.O_TRUNC);
				break;
			case FileMode.Open:
				break;
			case FileMode.OpenOrCreate:
				openFlags = OpenFlags.O_CREAT;
				break;
			case FileMode.Truncate:
				openFlags = OpenFlags.O_TRUNC;
				break;
			case FileMode.Append:
				openFlags = OpenFlags.O_APPEND;
				break;
			default:
				throw new ArgumentException(Locale.GetText("Unsupported mode value"), "mode");
			}
			int num;
			if (NativeConvert.TryFromOpenFlags(OpenFlags.O_LARGEFILE, out num))
			{
				openFlags |= OpenFlags.O_LARGEFILE;
			}
			switch (access)
			{
			case FileAccess.Read:
				openFlags |= OpenFlags.O_RDONLY;
				break;
			case FileAccess.Write:
				openFlags |= OpenFlags.O_WRONLY;
				break;
			case FileAccess.ReadWrite:
				openFlags |= OpenFlags.O_RDWR;
				break;
			default:
				throw new ArgumentOutOfRangeException(Locale.GetText("Unsupported access value"), "access");
			}
			return openFlags;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008F44 File Offset: 0x00007144
		public static string ToFopenMode(FileAccess access)
		{
			switch (access)
			{
			case FileAccess.Read:
				return "rb";
			case FileAccess.Write:
				return "wb";
			case FileAccess.ReadWrite:
				return "r+b";
			default:
				throw new ArgumentOutOfRangeException("access");
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008F88 File Offset: 0x00007188
		public static string ToFopenMode(FileMode mode)
		{
			switch (mode)
			{
			case FileMode.CreateNew:
			case FileMode.Create:
				return "w+b";
			case FileMode.Open:
			case FileMode.OpenOrCreate:
				return "r+b";
			case FileMode.Truncate:
				return "w+b";
			case FileMode.Append:
				return "a+b";
			default:
				throw new ArgumentOutOfRangeException("mode");
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008FE0 File Offset: 0x000071E0
		public static string ToFopenMode(FileMode mode, FileAccess access)
		{
			int num = -1;
			int num2 = -1;
			switch (mode)
			{
			case FileMode.CreateNew:
				num = 0;
				break;
			case FileMode.Create:
				num = 1;
				break;
			case FileMode.Open:
				num = 2;
				break;
			case FileMode.OpenOrCreate:
				num = 3;
				break;
			case FileMode.Truncate:
				num = 4;
				break;
			case FileMode.Append:
				num = 5;
				break;
			}
			switch (access)
			{
			case FileAccess.Read:
				num2 = 0;
				break;
			case FileAccess.Write:
				num2 = 1;
				break;
			case FileAccess.ReadWrite:
				num2 = 2;
				break;
			}
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			if (num2 == -1)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			string text = NativeConvert.fopen_modes[num][num2];
			if (text[0] != 'r' && text[0] != 'w' && text[0] != 'a')
			{
				throw new ArgumentException(text);
			}
			return text;
		}

		// Token: 0x06000223 RID: 547
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromStatvfs")]
		private static extern int FromStatvfs(ref Statvfs source, IntPtr destination);

		// Token: 0x06000224 RID: 548 RVA: 0x000090D4 File Offset: 0x000072D4
		public static bool TryCopy(ref Statvfs source, IntPtr destination)
		{
			return NativeConvert.FromStatvfs(ref source, destination) == 0;
		}

		// Token: 0x06000225 RID: 549
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToStatvfs")]
		private static extern int ToStatvfs(IntPtr source, out Statvfs destination);

		// Token: 0x06000226 RID: 550 RVA: 0x000090E0 File Offset: 0x000072E0
		public static bool TryCopy(IntPtr source, out Statvfs destination)
		{
			return NativeConvert.ToStatvfs(source, out destination) == 0;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000090EC File Offset: 0x000072EC
		private static void ThrowArgumentException(object value)
		{
			throw new ArgumentOutOfRangeException("value", value, Locale.GetText("Current platform doesn't support this value."));
		}

		// Token: 0x06000228 RID: 552
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromAccessModes")]
		private static extern int FromAccessModes(AccessModes value, out int rval);

		// Token: 0x06000229 RID: 553 RVA: 0x00009104 File Offset: 0x00007304
		public static bool TryFromAccessModes(AccessModes value, out int rval)
		{
			return NativeConvert.FromAccessModes(value, out rval) == 0;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009110 File Offset: 0x00007310
		public static int FromAccessModes(AccessModes value)
		{
			int result;
			if (NativeConvert.FromAccessModes(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600022B RID: 555
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToAccessModes")]
		private static extern int ToAccessModes(int value, out AccessModes rval);

		// Token: 0x0600022C RID: 556 RVA: 0x00009138 File Offset: 0x00007338
		public static bool TryToAccessModes(int value, out AccessModes rval)
		{
			return NativeConvert.ToAccessModes(value, out rval) == 0;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009144 File Offset: 0x00007344
		public static AccessModes ToAccessModes(int value)
		{
			AccessModes result;
			if (NativeConvert.ToAccessModes(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600022E RID: 558
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromConfstrName")]
		private static extern int FromConfstrName(ConfstrName value, out int rval);

		// Token: 0x0600022F RID: 559 RVA: 0x0000916C File Offset: 0x0000736C
		public static bool TryFromConfstrName(ConfstrName value, out int rval)
		{
			return NativeConvert.FromConfstrName(value, out rval) == 0;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009178 File Offset: 0x00007378
		public static int FromConfstrName(ConfstrName value)
		{
			int result;
			if (NativeConvert.FromConfstrName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000231 RID: 561
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToConfstrName")]
		private static extern int ToConfstrName(int value, out ConfstrName rval);

		// Token: 0x06000232 RID: 562 RVA: 0x000091A0 File Offset: 0x000073A0
		public static bool TryToConfstrName(int value, out ConfstrName rval)
		{
			return NativeConvert.ToConfstrName(value, out rval) == 0;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000091AC File Offset: 0x000073AC
		public static ConfstrName ToConfstrName(int value)
		{
			ConfstrName result;
			if (NativeConvert.ToConfstrName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000234 RID: 564
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromDirectoryNotifyFlags")]
		private static extern int FromDirectoryNotifyFlags(DirectoryNotifyFlags value, out int rval);

		// Token: 0x06000235 RID: 565 RVA: 0x000091D4 File Offset: 0x000073D4
		public static bool TryFromDirectoryNotifyFlags(DirectoryNotifyFlags value, out int rval)
		{
			return NativeConvert.FromDirectoryNotifyFlags(value, out rval) == 0;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000091E0 File Offset: 0x000073E0
		public static int FromDirectoryNotifyFlags(DirectoryNotifyFlags value)
		{
			int result;
			if (NativeConvert.FromDirectoryNotifyFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000237 RID: 567
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToDirectoryNotifyFlags")]
		private static extern int ToDirectoryNotifyFlags(int value, out DirectoryNotifyFlags rval);

		// Token: 0x06000238 RID: 568 RVA: 0x00009208 File Offset: 0x00007408
		public static bool TryToDirectoryNotifyFlags(int value, out DirectoryNotifyFlags rval)
		{
			return NativeConvert.ToDirectoryNotifyFlags(value, out rval) == 0;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009214 File Offset: 0x00007414
		public static DirectoryNotifyFlags ToDirectoryNotifyFlags(int value)
		{
			DirectoryNotifyFlags result;
			if (NativeConvert.ToDirectoryNotifyFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600023A RID: 570
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromErrno")]
		private static extern int FromErrno(Errno value, out int rval);

		// Token: 0x0600023B RID: 571 RVA: 0x0000923C File Offset: 0x0000743C
		public static bool TryFromErrno(Errno value, out int rval)
		{
			return NativeConvert.FromErrno(value, out rval) == 0;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009248 File Offset: 0x00007448
		public static int FromErrno(Errno value)
		{
			int result;
			if (NativeConvert.FromErrno(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600023D RID: 573
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToErrno")]
		private static extern int ToErrno(int value, out Errno rval);

		// Token: 0x0600023E RID: 574 RVA: 0x00009270 File Offset: 0x00007470
		public static bool TryToErrno(int value, out Errno rval)
		{
			return NativeConvert.ToErrno(value, out rval) == 0;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000927C File Offset: 0x0000747C
		public static Errno ToErrno(int value)
		{
			Errno result;
			if (NativeConvert.ToErrno(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000240 RID: 576
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFcntlCommand")]
		private static extern int FromFcntlCommand(FcntlCommand value, out int rval);

		// Token: 0x06000241 RID: 577 RVA: 0x000092A4 File Offset: 0x000074A4
		public static bool TryFromFcntlCommand(FcntlCommand value, out int rval)
		{
			return NativeConvert.FromFcntlCommand(value, out rval) == 0;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000092B0 File Offset: 0x000074B0
		public static int FromFcntlCommand(FcntlCommand value)
		{
			int result;
			if (NativeConvert.FromFcntlCommand(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000243 RID: 579
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFcntlCommand")]
		private static extern int ToFcntlCommand(int value, out FcntlCommand rval);

		// Token: 0x06000244 RID: 580 RVA: 0x000092D8 File Offset: 0x000074D8
		public static bool TryToFcntlCommand(int value, out FcntlCommand rval)
		{
			return NativeConvert.ToFcntlCommand(value, out rval) == 0;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000092E4 File Offset: 0x000074E4
		public static FcntlCommand ToFcntlCommand(int value)
		{
			FcntlCommand result;
			if (NativeConvert.ToFcntlCommand(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000246 RID: 582
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFilePermissions")]
		private static extern int FromFilePermissions(FilePermissions value, out uint rval);

		// Token: 0x06000247 RID: 583 RVA: 0x0000930C File Offset: 0x0000750C
		public static bool TryFromFilePermissions(FilePermissions value, out uint rval)
		{
			return NativeConvert.FromFilePermissions(value, out rval) == 0;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00009318 File Offset: 0x00007518
		public static uint FromFilePermissions(FilePermissions value)
		{
			uint result;
			if (NativeConvert.FromFilePermissions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000249 RID: 585
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFilePermissions")]
		private static extern int ToFilePermissions(uint value, out FilePermissions rval);

		// Token: 0x0600024A RID: 586 RVA: 0x00009340 File Offset: 0x00007540
		public static bool TryToFilePermissions(uint value, out FilePermissions rval)
		{
			return NativeConvert.ToFilePermissions(value, out rval) == 0;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000934C File Offset: 0x0000754C
		public static FilePermissions ToFilePermissions(uint value)
		{
			FilePermissions result;
			if (NativeConvert.ToFilePermissions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600024C RID: 588
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromFlock")]
		private static extern int FromFlock(ref Flock source, IntPtr destination);

		// Token: 0x0600024D RID: 589 RVA: 0x00009374 File Offset: 0x00007574
		public static bool TryCopy(ref Flock source, IntPtr destination)
		{
			return NativeConvert.FromFlock(ref source, destination) == 0;
		}

		// Token: 0x0600024E RID: 590
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToFlock")]
		private static extern int ToFlock(IntPtr source, out Flock destination);

		// Token: 0x0600024F RID: 591 RVA: 0x00009380 File Offset: 0x00007580
		public static bool TryCopy(IntPtr source, out Flock destination)
		{
			return NativeConvert.ToFlock(source, out destination) == 0;
		}

		// Token: 0x06000250 RID: 592
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromLockType")]
		private static extern int FromLockType(LockType value, out short rval);

		// Token: 0x06000251 RID: 593 RVA: 0x0000938C File Offset: 0x0000758C
		public static bool TryFromLockType(LockType value, out short rval)
		{
			return NativeConvert.FromLockType(value, out rval) == 0;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009398 File Offset: 0x00007598
		public static short FromLockType(LockType value)
		{
			short result;
			if (NativeConvert.FromLockType(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000253 RID: 595
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToLockType")]
		private static extern int ToLockType(short value, out LockType rval);

		// Token: 0x06000254 RID: 596 RVA: 0x000093C0 File Offset: 0x000075C0
		public static bool TryToLockType(short value, out LockType rval)
		{
			return NativeConvert.ToLockType(value, out rval) == 0;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000093CC File Offset: 0x000075CC
		public static LockType ToLockType(short value)
		{
			LockType result;
			if (NativeConvert.ToLockType(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000256 RID: 598
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromLockfCommand")]
		private static extern int FromLockfCommand(LockfCommand value, out int rval);

		// Token: 0x06000257 RID: 599 RVA: 0x000093F4 File Offset: 0x000075F4
		public static bool TryFromLockfCommand(LockfCommand value, out int rval)
		{
			return NativeConvert.FromLockfCommand(value, out rval) == 0;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009400 File Offset: 0x00007600
		public static int FromLockfCommand(LockfCommand value)
		{
			int result;
			if (NativeConvert.FromLockfCommand(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000259 RID: 601
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToLockfCommand")]
		private static extern int ToLockfCommand(int value, out LockfCommand rval);

		// Token: 0x0600025A RID: 602 RVA: 0x00009428 File Offset: 0x00007628
		public static bool TryToLockfCommand(int value, out LockfCommand rval)
		{
			return NativeConvert.ToLockfCommand(value, out rval) == 0;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009434 File Offset: 0x00007634
		public static LockfCommand ToLockfCommand(int value)
		{
			LockfCommand result;
			if (NativeConvert.ToLockfCommand(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600025C RID: 604
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMlockallFlags")]
		private static extern int FromMlockallFlags(MlockallFlags value, out int rval);

		// Token: 0x0600025D RID: 605 RVA: 0x0000945C File Offset: 0x0000765C
		public static bool TryFromMlockallFlags(MlockallFlags value, out int rval)
		{
			return NativeConvert.FromMlockallFlags(value, out rval) == 0;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009468 File Offset: 0x00007668
		public static int FromMlockallFlags(MlockallFlags value)
		{
			int result;
			if (NativeConvert.FromMlockallFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600025F RID: 607
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMlockallFlags")]
		private static extern int ToMlockallFlags(int value, out MlockallFlags rval);

		// Token: 0x06000260 RID: 608 RVA: 0x00009490 File Offset: 0x00007690
		public static bool TryToMlockallFlags(int value, out MlockallFlags rval)
		{
			return NativeConvert.ToMlockallFlags(value, out rval) == 0;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000949C File Offset: 0x0000769C
		public static MlockallFlags ToMlockallFlags(int value)
		{
			MlockallFlags result;
			if (NativeConvert.ToMlockallFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000262 RID: 610
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMmapFlags")]
		private static extern int FromMmapFlags(MmapFlags value, out int rval);

		// Token: 0x06000263 RID: 611 RVA: 0x000094C4 File Offset: 0x000076C4
		public static bool TryFromMmapFlags(MmapFlags value, out int rval)
		{
			return NativeConvert.FromMmapFlags(value, out rval) == 0;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000094D0 File Offset: 0x000076D0
		public static int FromMmapFlags(MmapFlags value)
		{
			int result;
			if (NativeConvert.FromMmapFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000265 RID: 613
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMmapFlags")]
		private static extern int ToMmapFlags(int value, out MmapFlags rval);

		// Token: 0x06000266 RID: 614 RVA: 0x000094F8 File Offset: 0x000076F8
		public static bool TryToMmapFlags(int value, out MmapFlags rval)
		{
			return NativeConvert.ToMmapFlags(value, out rval) == 0;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009504 File Offset: 0x00007704
		public static MmapFlags ToMmapFlags(int value)
		{
			MmapFlags result;
			if (NativeConvert.ToMmapFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000268 RID: 616
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMmapProts")]
		private static extern int FromMmapProts(MmapProts value, out int rval);

		// Token: 0x06000269 RID: 617 RVA: 0x0000952C File Offset: 0x0000772C
		public static bool TryFromMmapProts(MmapProts value, out int rval)
		{
			return NativeConvert.FromMmapProts(value, out rval) == 0;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009538 File Offset: 0x00007738
		public static int FromMmapProts(MmapProts value)
		{
			int result;
			if (NativeConvert.FromMmapProts(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600026B RID: 619
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMmapProts")]
		private static extern int ToMmapProts(int value, out MmapProts rval);

		// Token: 0x0600026C RID: 620 RVA: 0x00009560 File Offset: 0x00007760
		public static bool TryToMmapProts(int value, out MmapProts rval)
		{
			return NativeConvert.ToMmapProts(value, out rval) == 0;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000956C File Offset: 0x0000776C
		public static MmapProts ToMmapProts(int value)
		{
			MmapProts result;
			if (NativeConvert.ToMmapProts(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600026E RID: 622
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMountFlags")]
		private static extern int FromMountFlags(MountFlags value, out ulong rval);

		// Token: 0x0600026F RID: 623 RVA: 0x00009594 File Offset: 0x00007794
		public static bool TryFromMountFlags(MountFlags value, out ulong rval)
		{
			return NativeConvert.FromMountFlags(value, out rval) == 0;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000095A0 File Offset: 0x000077A0
		public static ulong FromMountFlags(MountFlags value)
		{
			ulong result;
			if (NativeConvert.FromMountFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000271 RID: 625
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMountFlags")]
		private static extern int ToMountFlags(ulong value, out MountFlags rval);

		// Token: 0x06000272 RID: 626 RVA: 0x000095C8 File Offset: 0x000077C8
		public static bool TryToMountFlags(ulong value, out MountFlags rval)
		{
			return NativeConvert.ToMountFlags(value, out rval) == 0;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000095D4 File Offset: 0x000077D4
		public static MountFlags ToMountFlags(ulong value)
		{
			MountFlags result;
			if (NativeConvert.ToMountFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000274 RID: 628
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMremapFlags")]
		private static extern int FromMremapFlags(MremapFlags value, out ulong rval);

		// Token: 0x06000275 RID: 629 RVA: 0x000095FC File Offset: 0x000077FC
		public static bool TryFromMremapFlags(MremapFlags value, out ulong rval)
		{
			return NativeConvert.FromMremapFlags(value, out rval) == 0;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009608 File Offset: 0x00007808
		public static ulong FromMremapFlags(MremapFlags value)
		{
			ulong result;
			if (NativeConvert.FromMremapFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000277 RID: 631
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMremapFlags")]
		private static extern int ToMremapFlags(ulong value, out MremapFlags rval);

		// Token: 0x06000278 RID: 632 RVA: 0x00009630 File Offset: 0x00007830
		public static bool TryToMremapFlags(ulong value, out MremapFlags rval)
		{
			return NativeConvert.ToMremapFlags(value, out rval) == 0;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000963C File Offset: 0x0000783C
		public static MremapFlags ToMremapFlags(ulong value)
		{
			MremapFlags result;
			if (NativeConvert.ToMremapFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600027A RID: 634
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromMsyncFlags")]
		private static extern int FromMsyncFlags(MsyncFlags value, out int rval);

		// Token: 0x0600027B RID: 635 RVA: 0x00009664 File Offset: 0x00007864
		public static bool TryFromMsyncFlags(MsyncFlags value, out int rval)
		{
			return NativeConvert.FromMsyncFlags(value, out rval) == 0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009670 File Offset: 0x00007870
		public static int FromMsyncFlags(MsyncFlags value)
		{
			int result;
			if (NativeConvert.FromMsyncFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600027D RID: 637
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToMsyncFlags")]
		private static extern int ToMsyncFlags(int value, out MsyncFlags rval);

		// Token: 0x0600027E RID: 638 RVA: 0x00009698 File Offset: 0x00007898
		public static bool TryToMsyncFlags(int value, out MsyncFlags rval)
		{
			return NativeConvert.ToMsyncFlags(value, out rval) == 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000096A4 File Offset: 0x000078A4
		public static MsyncFlags ToMsyncFlags(int value)
		{
			MsyncFlags result;
			if (NativeConvert.ToMsyncFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000280 RID: 640
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromOpenFlags")]
		private static extern int FromOpenFlags(OpenFlags value, out int rval);

		// Token: 0x06000281 RID: 641 RVA: 0x000096CC File Offset: 0x000078CC
		public static bool TryFromOpenFlags(OpenFlags value, out int rval)
		{
			return NativeConvert.FromOpenFlags(value, out rval) == 0;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000096D8 File Offset: 0x000078D8
		public static int FromOpenFlags(OpenFlags value)
		{
			int result;
			if (NativeConvert.FromOpenFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000283 RID: 643
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToOpenFlags")]
		private static extern int ToOpenFlags(int value, out OpenFlags rval);

		// Token: 0x06000284 RID: 644 RVA: 0x00009700 File Offset: 0x00007900
		public static bool TryToOpenFlags(int value, out OpenFlags rval)
		{
			return NativeConvert.ToOpenFlags(value, out rval) == 0;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000970C File Offset: 0x0000790C
		public static OpenFlags ToOpenFlags(int value)
		{
			OpenFlags result;
			if (NativeConvert.ToOpenFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000286 RID: 646
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPathconfName")]
		private static extern int FromPathconfName(PathconfName value, out int rval);

		// Token: 0x06000287 RID: 647 RVA: 0x00009734 File Offset: 0x00007934
		public static bool TryFromPathconfName(PathconfName value, out int rval)
		{
			return NativeConvert.FromPathconfName(value, out rval) == 0;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009740 File Offset: 0x00007940
		public static int FromPathconfName(PathconfName value)
		{
			int result;
			if (NativeConvert.FromPathconfName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000289 RID: 649
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPathconfName")]
		private static extern int ToPathconfName(int value, out PathconfName rval);

		// Token: 0x0600028A RID: 650 RVA: 0x00009768 File Offset: 0x00007968
		public static bool TryToPathconfName(int value, out PathconfName rval)
		{
			return NativeConvert.ToPathconfName(value, out rval) == 0;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009774 File Offset: 0x00007974
		public static PathconfName ToPathconfName(int value)
		{
			PathconfName result;
			if (NativeConvert.ToPathconfName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600028C RID: 652
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPollEvents")]
		private static extern int FromPollEvents(PollEvents value, out short rval);

		// Token: 0x0600028D RID: 653 RVA: 0x0000979C File Offset: 0x0000799C
		public static bool TryFromPollEvents(PollEvents value, out short rval)
		{
			return NativeConvert.FromPollEvents(value, out rval) == 0;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000097A8 File Offset: 0x000079A8
		public static short FromPollEvents(PollEvents value)
		{
			short result;
			if (NativeConvert.FromPollEvents(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600028F RID: 655
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPollEvents")]
		private static extern int ToPollEvents(short value, out PollEvents rval);

		// Token: 0x06000290 RID: 656 RVA: 0x000097D0 File Offset: 0x000079D0
		public static bool TryToPollEvents(short value, out PollEvents rval)
		{
			return NativeConvert.ToPollEvents(value, out rval) == 0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000097DC File Offset: 0x000079DC
		public static PollEvents ToPollEvents(short value)
		{
			PollEvents result;
			if (NativeConvert.ToPollEvents(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000292 RID: 658
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPollfd")]
		private static extern int FromPollfd(ref Pollfd source, IntPtr destination);

		// Token: 0x06000293 RID: 659 RVA: 0x00009804 File Offset: 0x00007A04
		public static bool TryCopy(ref Pollfd source, IntPtr destination)
		{
			return NativeConvert.FromPollfd(ref source, destination) == 0;
		}

		// Token: 0x06000294 RID: 660
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPollfd")]
		private static extern int ToPollfd(IntPtr source, out Pollfd destination);

		// Token: 0x06000295 RID: 661 RVA: 0x00009810 File Offset: 0x00007A10
		public static bool TryCopy(IntPtr source, out Pollfd destination)
		{
			return NativeConvert.ToPollfd(source, out destination) == 0;
		}

		// Token: 0x06000296 RID: 662
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPosixFadviseAdvice")]
		private static extern int FromPosixFadviseAdvice(PosixFadviseAdvice value, out int rval);

		// Token: 0x06000297 RID: 663 RVA: 0x0000981C File Offset: 0x00007A1C
		public static bool TryFromPosixFadviseAdvice(PosixFadviseAdvice value, out int rval)
		{
			return NativeConvert.FromPosixFadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009828 File Offset: 0x00007A28
		public static int FromPosixFadviseAdvice(PosixFadviseAdvice value)
		{
			int result;
			if (NativeConvert.FromPosixFadviseAdvice(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x06000299 RID: 665
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPosixFadviseAdvice")]
		private static extern int ToPosixFadviseAdvice(int value, out PosixFadviseAdvice rval);

		// Token: 0x0600029A RID: 666 RVA: 0x00009850 File Offset: 0x00007A50
		public static bool TryToPosixFadviseAdvice(int value, out PosixFadviseAdvice rval)
		{
			return NativeConvert.ToPosixFadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000985C File Offset: 0x00007A5C
		public static PosixFadviseAdvice ToPosixFadviseAdvice(int value)
		{
			PosixFadviseAdvice result;
			if (NativeConvert.ToPosixFadviseAdvice(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600029C RID: 668
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromPosixMadviseAdvice")]
		private static extern int FromPosixMadviseAdvice(PosixMadviseAdvice value, out int rval);

		// Token: 0x0600029D RID: 669 RVA: 0x00009884 File Offset: 0x00007A84
		public static bool TryFromPosixMadviseAdvice(PosixMadviseAdvice value, out int rval)
		{
			return NativeConvert.FromPosixMadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009890 File Offset: 0x00007A90
		public static int FromPosixMadviseAdvice(PosixMadviseAdvice value)
		{
			int result;
			if (NativeConvert.FromPosixMadviseAdvice(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x0600029F RID: 671
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToPosixMadviseAdvice")]
		private static extern int ToPosixMadviseAdvice(int value, out PosixMadviseAdvice rval);

		// Token: 0x060002A0 RID: 672 RVA: 0x000098B8 File Offset: 0x00007AB8
		public static bool TryToPosixMadviseAdvice(int value, out PosixMadviseAdvice rval)
		{
			return NativeConvert.ToPosixMadviseAdvice(value, out rval) == 0;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000098C4 File Offset: 0x00007AC4
		public static PosixMadviseAdvice ToPosixMadviseAdvice(int value)
		{
			PosixMadviseAdvice result;
			if (NativeConvert.ToPosixMadviseAdvice(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002A2 RID: 674
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSeekFlags")]
		private static extern int FromSeekFlags(SeekFlags value, out short rval);

		// Token: 0x060002A3 RID: 675 RVA: 0x000098EC File Offset: 0x00007AEC
		public static bool TryFromSeekFlags(SeekFlags value, out short rval)
		{
			return NativeConvert.FromSeekFlags(value, out rval) == 0;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000098F8 File Offset: 0x00007AF8
		public static short FromSeekFlags(SeekFlags value)
		{
			short result;
			if (NativeConvert.FromSeekFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002A5 RID: 677
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSeekFlags")]
		private static extern int ToSeekFlags(short value, out SeekFlags rval);

		// Token: 0x060002A6 RID: 678 RVA: 0x00009920 File Offset: 0x00007B20
		public static bool TryToSeekFlags(short value, out SeekFlags rval)
		{
			return NativeConvert.ToSeekFlags(value, out rval) == 0;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000992C File Offset: 0x00007B2C
		public static SeekFlags ToSeekFlags(short value)
		{
			SeekFlags result;
			if (NativeConvert.ToSeekFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002A8 RID: 680
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSignum")]
		private static extern int FromSignum(Signum value, out int rval);

		// Token: 0x060002A9 RID: 681 RVA: 0x00009954 File Offset: 0x00007B54
		public static bool TryFromSignum(Signum value, out int rval)
		{
			return NativeConvert.FromSignum(value, out rval) == 0;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009960 File Offset: 0x00007B60
		public static int FromSignum(Signum value)
		{
			int result;
			if (NativeConvert.FromSignum(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002AB RID: 683
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSignum")]
		private static extern int ToSignum(int value, out Signum rval);

		// Token: 0x060002AC RID: 684 RVA: 0x00009988 File Offset: 0x00007B88
		public static bool TryToSignum(int value, out Signum rval)
		{
			return NativeConvert.ToSignum(value, out rval) == 0;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00009994 File Offset: 0x00007B94
		public static Signum ToSignum(int value)
		{
			Signum result;
			if (NativeConvert.ToSignum(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002AE RID: 686
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromStat")]
		private static extern int FromStat(ref Stat source, IntPtr destination);

		// Token: 0x060002AF RID: 687 RVA: 0x000099BC File Offset: 0x00007BBC
		public static bool TryCopy(ref Stat source, IntPtr destination)
		{
			return NativeConvert.FromStat(ref source, destination) == 0;
		}

		// Token: 0x060002B0 RID: 688
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToStat")]
		private static extern int ToStat(IntPtr source, out Stat destination);

		// Token: 0x060002B1 RID: 689 RVA: 0x000099C8 File Offset: 0x00007BC8
		public static bool TryCopy(IntPtr source, out Stat destination)
		{
			return NativeConvert.ToStat(source, out destination) == 0;
		}

		// Token: 0x060002B2 RID: 690
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSysconfName")]
		private static extern int FromSysconfName(SysconfName value, out int rval);

		// Token: 0x060002B3 RID: 691 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static bool TryFromSysconfName(SysconfName value, out int rval)
		{
			return NativeConvert.FromSysconfName(value, out rval) == 0;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000099E0 File Offset: 0x00007BE0
		public static int FromSysconfName(SysconfName value)
		{
			int result;
			if (NativeConvert.FromSysconfName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002B5 RID: 693
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSysconfName")]
		private static extern int ToSysconfName(int value, out SysconfName rval);

		// Token: 0x060002B6 RID: 694 RVA: 0x00009A08 File Offset: 0x00007C08
		public static bool TryToSysconfName(int value, out SysconfName rval)
		{
			return NativeConvert.ToSysconfName(value, out rval) == 0;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00009A14 File Offset: 0x00007C14
		public static SysconfName ToSysconfName(int value)
		{
			SysconfName result;
			if (NativeConvert.ToSysconfName(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002B8 RID: 696
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogFacility")]
		private static extern int FromSyslogFacility(SyslogFacility value, out int rval);

		// Token: 0x060002B9 RID: 697 RVA: 0x00009A3C File Offset: 0x00007C3C
		public static bool TryFromSyslogFacility(SyslogFacility value, out int rval)
		{
			return NativeConvert.FromSyslogFacility(value, out rval) == 0;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00009A48 File Offset: 0x00007C48
		public static int FromSyslogFacility(SyslogFacility value)
		{
			int result;
			if (NativeConvert.FromSyslogFacility(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002BB RID: 699
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogFacility")]
		private static extern int ToSyslogFacility(int value, out SyslogFacility rval);

		// Token: 0x060002BC RID: 700 RVA: 0x00009A70 File Offset: 0x00007C70
		public static bool TryToSyslogFacility(int value, out SyslogFacility rval)
		{
			return NativeConvert.ToSyslogFacility(value, out rval) == 0;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009A7C File Offset: 0x00007C7C
		public static SyslogFacility ToSyslogFacility(int value)
		{
			SyslogFacility result;
			if (NativeConvert.ToSyslogFacility(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002BE RID: 702
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogLevel")]
		private static extern int FromSyslogLevel(SyslogLevel value, out int rval);

		// Token: 0x060002BF RID: 703 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public static bool TryFromSyslogLevel(SyslogLevel value, out int rval)
		{
			return NativeConvert.FromSyslogLevel(value, out rval) == 0;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public static int FromSyslogLevel(SyslogLevel value)
		{
			int result;
			if (NativeConvert.FromSyslogLevel(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002C1 RID: 705
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogLevel")]
		private static extern int ToSyslogLevel(int value, out SyslogLevel rval);

		// Token: 0x060002C2 RID: 706 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public static bool TryToSyslogLevel(int value, out SyslogLevel rval)
		{
			return NativeConvert.ToSyslogLevel(value, out rval) == 0;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public static SyslogLevel ToSyslogLevel(int value)
		{
			SyslogLevel result;
			if (NativeConvert.ToSyslogLevel(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002C4 RID: 708
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromSyslogOptions")]
		private static extern int FromSyslogOptions(SyslogOptions value, out int rval);

		// Token: 0x060002C5 RID: 709 RVA: 0x00009B0C File Offset: 0x00007D0C
		public static bool TryFromSyslogOptions(SyslogOptions value, out int rval)
		{
			return NativeConvert.FromSyslogOptions(value, out rval) == 0;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009B18 File Offset: 0x00007D18
		public static int FromSyslogOptions(SyslogOptions value)
		{
			int result;
			if (NativeConvert.FromSyslogOptions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002C7 RID: 711
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToSyslogOptions")]
		private static extern int ToSyslogOptions(int value, out SyslogOptions rval);

		// Token: 0x060002C8 RID: 712 RVA: 0x00009B40 File Offset: 0x00007D40
		public static bool TryToSyslogOptions(int value, out SyslogOptions rval)
		{
			return NativeConvert.ToSyslogOptions(value, out rval) == 0;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00009B4C File Offset: 0x00007D4C
		public static SyslogOptions ToSyslogOptions(int value)
		{
			SyslogOptions result;
			if (NativeConvert.ToSyslogOptions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002CA RID: 714
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimespec")]
		private static extern int FromTimespec(ref Timespec source, IntPtr destination);

		// Token: 0x060002CB RID: 715 RVA: 0x00009B74 File Offset: 0x00007D74
		public static bool TryCopy(ref Timespec source, IntPtr destination)
		{
			return NativeConvert.FromTimespec(ref source, destination) == 0;
		}

		// Token: 0x060002CC RID: 716
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimespec")]
		private static extern int ToTimespec(IntPtr source, out Timespec destination);

		// Token: 0x060002CD RID: 717 RVA: 0x00009B80 File Offset: 0x00007D80
		public static bool TryCopy(IntPtr source, out Timespec destination)
		{
			return NativeConvert.ToTimespec(source, out destination) == 0;
		}

		// Token: 0x060002CE RID: 718
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimeval")]
		private static extern int FromTimeval(ref Timeval source, IntPtr destination);

		// Token: 0x060002CF RID: 719 RVA: 0x00009B8C File Offset: 0x00007D8C
		public static bool TryCopy(ref Timeval source, IntPtr destination)
		{
			return NativeConvert.FromTimeval(ref source, destination) == 0;
		}

		// Token: 0x060002D0 RID: 720
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimeval")]
		private static extern int ToTimeval(IntPtr source, out Timeval destination);

		// Token: 0x060002D1 RID: 721 RVA: 0x00009B98 File Offset: 0x00007D98
		public static bool TryCopy(IntPtr source, out Timeval destination)
		{
			return NativeConvert.ToTimeval(source, out destination) == 0;
		}

		// Token: 0x060002D2 RID: 722
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromTimezone")]
		private static extern int FromTimezone(ref Timezone source, IntPtr destination);

		// Token: 0x060002D3 RID: 723 RVA: 0x00009BA4 File Offset: 0x00007DA4
		public static bool TryCopy(ref Timezone source, IntPtr destination)
		{
			return NativeConvert.FromTimezone(ref source, destination) == 0;
		}

		// Token: 0x060002D4 RID: 724
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToTimezone")]
		private static extern int ToTimezone(IntPtr source, out Timezone destination);

		// Token: 0x060002D5 RID: 725 RVA: 0x00009BB0 File Offset: 0x00007DB0
		public static bool TryCopy(IntPtr source, out Timezone destination)
		{
			return NativeConvert.ToTimezone(source, out destination) == 0;
		}

		// Token: 0x060002D6 RID: 726
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromUtimbuf")]
		private static extern int FromUtimbuf(ref Utimbuf source, IntPtr destination);

		// Token: 0x060002D7 RID: 727 RVA: 0x00009BBC File Offset: 0x00007DBC
		public static bool TryCopy(ref Utimbuf source, IntPtr destination)
		{
			return NativeConvert.FromUtimbuf(ref source, destination) == 0;
		}

		// Token: 0x060002D8 RID: 728
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToUtimbuf")]
		private static extern int ToUtimbuf(IntPtr source, out Utimbuf destination);

		// Token: 0x060002D9 RID: 729 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static bool TryCopy(IntPtr source, out Utimbuf destination)
		{
			return NativeConvert.ToUtimbuf(source, out destination) == 0;
		}

		// Token: 0x060002DA RID: 730
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromWaitOptions")]
		private static extern int FromWaitOptions(WaitOptions value, out int rval);

		// Token: 0x060002DB RID: 731 RVA: 0x00009BD4 File Offset: 0x00007DD4
		public static bool TryFromWaitOptions(WaitOptions value, out int rval)
		{
			return NativeConvert.FromWaitOptions(value, out rval) == 0;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public static int FromWaitOptions(WaitOptions value)
		{
			int result;
			if (NativeConvert.FromWaitOptions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002DD RID: 733
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToWaitOptions")]
		private static extern int ToWaitOptions(int value, out WaitOptions rval);

		// Token: 0x060002DE RID: 734 RVA: 0x00009C08 File Offset: 0x00007E08
		public static bool TryToWaitOptions(int value, out WaitOptions rval)
		{
			return NativeConvert.ToWaitOptions(value, out rval) == 0;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009C14 File Offset: 0x00007E14
		public static WaitOptions ToWaitOptions(int value)
		{
			WaitOptions result;
			if (NativeConvert.ToWaitOptions(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_FromXattrFlags")]
		private static extern int FromXattrFlags(XattrFlags value, out int rval);

		// Token: 0x060002E1 RID: 737 RVA: 0x00009C3C File Offset: 0x00007E3C
		public static bool TryFromXattrFlags(XattrFlags value, out int rval)
		{
			return NativeConvert.FromXattrFlags(value, out rval) == 0;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009C48 File Offset: 0x00007E48
		public static int FromXattrFlags(XattrFlags value)
		{
			int result;
			if (NativeConvert.FromXattrFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x060002E3 RID: 739
		[DllImport("MonoPosixHelper", EntryPoint = "Mono_Posix_ToXattrFlags")]
		private static extern int ToXattrFlags(int value, out XattrFlags rval);

		// Token: 0x060002E4 RID: 740 RVA: 0x00009C70 File Offset: 0x00007E70
		public static bool TryToXattrFlags(int value, out XattrFlags rval)
		{
			return NativeConvert.ToXattrFlags(value, out rval) == 0;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00009C7C File Offset: 0x00007E7C
		public static XattrFlags ToXattrFlags(int value)
		{
			XattrFlags result;
			if (NativeConvert.ToXattrFlags(value, out result) == -1)
			{
				NativeConvert.ThrowArgumentException(value);
			}
			return result;
		}

		// Token: 0x04000095 RID: 149
		private const string LIB = "MonoPosixHelper";

		// Token: 0x04000096 RID: 150
		public static readonly DateTime LocalUnixEpoch = new DateTime(1970, 1, 1);

		// Token: 0x04000097 RID: 151
		public static readonly TimeSpan LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.UtcNow);

		// Token: 0x04000098 RID: 152
		private static readonly string[][] fopen_modes = new string[][]
		{
			new string[]
			{
				"Can't Read+Create",
				"wb",
				"w+b"
			},
			new string[]
			{
				"Can't Read+Create",
				"wb",
				"w+b"
			},
			new string[]
			{
				"rb",
				"wb",
				"r+b"
			},
			new string[]
			{
				"rb",
				"wb",
				"r+b"
			},
			new string[]
			{
				"Cannot Truncate and Read",
				"wb",
				"w+b"
			},
			new string[]
			{
				"Cannot Append and Read",
				"ab",
				"a+b"
			}
		};
	}
}
