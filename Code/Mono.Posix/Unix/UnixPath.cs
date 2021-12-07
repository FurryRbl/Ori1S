using System;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x0200001D RID: 29
	public sealed class UnixPath
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00006A38 File Offset: 0x00004C38
		private UnixPath()
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006A6C File Offset: 0x00004C6C
		public static char[] GetInvalidPathChars()
		{
			return (char[])UnixPath._InvalidPathChars.Clone();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006A80 File Offset: 0x00004C80
		public static string Combine(string path1, params string[] paths)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (path1.IndexOfAny(UnixPath._InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path", "path1");
			}
			int num = path1.Length;
			int num2 = -1;
			for (int i = 0; i < paths.Length; i++)
			{
				if (paths[i] == null)
				{
					throw new ArgumentNullException("paths[" + i + "]");
				}
				if (paths[i].IndexOfAny(UnixPath._InvalidPathChars) != -1)
				{
					throw new ArgumentException("Illegal characters in path", "paths[" + i + "]");
				}
				if (UnixPath.IsPathRooted(paths[i]))
				{
					num = 0;
					num2 = i;
				}
				num += paths[i].Length + 1;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (num2 == -1)
			{
				stringBuilder.Append(path1);
				num2 = 0;
			}
			for (int j = num2; j < paths.Length; j++)
			{
				UnixPath.Combine(stringBuilder, paths[j]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006BA4 File Offset: 0x00004DA4
		private static void Combine(StringBuilder path, string part)
		{
			if (path.Length > 0 && part.Length > 0)
			{
				char c = path[path.Length - 1];
				if (c != UnixPath.DirectorySeparatorChar && c != UnixPath.AltDirectorySeparatorChar && c != UnixPath.VolumeSeparatorChar)
				{
					path.Append(UnixPath.DirectorySeparatorChar);
				}
			}
			path.Append(part);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006C10 File Offset: 0x00004E10
		public static string GetDirectoryName(string path)
		{
			UnixPath.CheckPath(path);
			int num = path.LastIndexOf(UnixPath.DirectorySeparatorChar);
			if (num > 0)
			{
				return path.Substring(0, num);
			}
			if (num == 0)
			{
				return "/";
			}
			return string.Empty;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006C50 File Offset: 0x00004E50
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			int num = path.LastIndexOf(UnixPath.DirectorySeparatorChar);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006C90 File Offset: 0x00004E90
		public static string GetFullPath(string path)
		{
			path = UnixPath._GetFullPath(path);
			return UnixPath.GetCanonicalPath(path);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006CA0 File Offset: 0x00004EA0
		private static string _GetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!UnixPath.IsPathRooted(path))
			{
				path = UnixDirectoryInfo.GetCurrentDirectory() + UnixPath.DirectorySeparatorChar + path;
			}
			return path;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static string GetCanonicalPath(string path)
		{
			string[] value;
			int count;
			UnixPath.GetPathComponents(path, out value, out count);
			string text = string.Join("/", value, 0, count);
			return (!UnixPath.IsPathRooted(path)) ? text : ("/" + text);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006D28 File Offset: 0x00004F28
		private static void GetPathComponents(string path, out string[] components, out int lastIndex)
		{
			string[] array = path.Split(new char[]
			{
				UnixPath.DirectorySeparatorChar
			});
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i] == ".") && !(array[i] == string.Empty))
				{
					if (array[i] == "..")
					{
						if (num != 0)
						{
							num--;
						}
						else
						{
							num++;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			components = array;
			lastIndex = num;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (!UnixPath.IsPathRooted(path))
			{
				return string.Empty;
			}
			return "/";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public static string GetCompleteRealPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string[] array;
			int num;
			UnixPath.GetPathComponents(path, out array, out num);
			StringBuilder stringBuilder = new StringBuilder();
			if (array.Length > 0)
			{
				string text = (!UnixPath.IsPathRooted(path)) ? string.Empty : "/";
				text += array[0];
				stringBuilder.Append(UnixPath.GetRealPath(text));
			}
			for (int i = 1; i < num; i++)
			{
				stringBuilder.Append("/").Append(array[i]);
				string realPath = UnixPath.GetRealPath(stringBuilder.ToString());
				stringBuilder.Remove(0, stringBuilder.Length);
				stringBuilder.Append(realPath);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006EA4 File Offset: 0x000050A4
		public static string GetRealPath(string path)
		{
			for (;;)
			{
				string text = UnixPath.ReadSymbolicLink(path);
				if (text == null)
				{
					break;
				}
				if (UnixPath.IsPathRooted(text))
				{
					path = text;
				}
				else
				{
					path = UnixPath.GetDirectoryName(path) + UnixPath.DirectorySeparatorChar + text;
					path = UnixPath.GetCanonicalPath(path);
				}
			}
			return path;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006EF8 File Offset: 0x000050F8
		internal static string ReadSymbolicLink(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			int num;
			for (;;)
			{
				num = Syscall.readlink(path, stringBuilder);
				if (num < 0)
				{
					Errno lastError;
					Errno errno = lastError = Stdlib.GetLastError();
					if (lastError == Errno.EINVAL)
					{
						break;
					}
					UnixMarshal.ThrowExceptionForError(errno);
				}
				else
				{
					if (num != stringBuilder.Capacity)
					{
						goto IL_60;
					}
					stringBuilder.Capacity *= 2;
				}
			}
			return null;
			IL_60:
			return stringBuilder.ToString(0, num);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006F74 File Offset: 0x00005174
		private static string ReadSymbolicLink(string path, out Errno errno)
		{
			errno = (Errno)0;
			StringBuilder stringBuilder = new StringBuilder(256);
			int num;
			for (;;)
			{
				num = Syscall.readlink(path, stringBuilder);
				if (num < 0)
				{
					break;
				}
				if (num != stringBuilder.Capacity)
				{
					goto IL_45;
				}
				stringBuilder.Capacity *= 2;
			}
			errno = Stdlib.GetLastError();
			return null;
			IL_45:
			return stringBuilder.ToString(0, num);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static string TryReadLink(string path)
		{
			Errno errno;
			return UnixPath.ReadSymbolicLink(path, out errno);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006FEC File Offset: 0x000051EC
		public static string ReadLink(string path)
		{
			Errno errno;
			path = UnixPath.ReadSymbolicLink(path, out errno);
			if (errno != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForError(errno);
			}
			return path;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007010 File Offset: 0x00005210
		public static bool IsPathRooted(string path)
		{
			return path != null && path.Length != 0 && path[0] == UnixPath.DirectorySeparatorChar;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007034 File Offset: 0x00005234
		internal static void CheckPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException();
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path cannot contain a zero-length string", "path");
			}
			if (path.IndexOfAny(UnixPath._InvalidPathChars) != -1)
			{
				throw new ArgumentException("Invalid characters in path.", "path");
			}
		}

		// Token: 0x0400006E RID: 110
		public static readonly char DirectorySeparatorChar = '/';

		// Token: 0x0400006F RID: 111
		public static readonly char AltDirectorySeparatorChar = '/';

		// Token: 0x04000070 RID: 112
		public static readonly char PathSeparator = ':';

		// Token: 0x04000071 RID: 113
		public static readonly char VolumeSeparatorChar = '/';

		// Token: 0x04000072 RID: 114
		private static readonly char[] _InvalidPathChars = new char[0];
	}
}
