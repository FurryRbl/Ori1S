using System;
using System.Collections;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000014 RID: 20
	public sealed class UnixEnvironment
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004CD0 File Offset: 0x00002ED0
		private UnixEnvironment()
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004CD8 File Offset: 0x00002ED8
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public static string CurrentDirectory
		{
			get
			{
				return UnixDirectoryInfo.GetCurrentDirectory();
			}
			set
			{
				UnixDirectoryInfo.SetCurrentDirectory(value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004CE8 File Offset: 0x00002EE8
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00004D10 File Offset: 0x00002F10
		public static string MachineName
		{
			get
			{
				Utsname utsname;
				if (Syscall.uname(out utsname) != 0)
				{
					throw UnixMarshal.CreateExceptionForLastError();
				}
				return utsname.nodename;
			}
			set
			{
				int retval = Syscall.sethostname(value);
				UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004D2C File Offset: 0x00002F2C
		public static string UserName
		{
			get
			{
				return UnixUserInfo.GetRealUser().UserName;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004D38 File Offset: 0x00002F38
		public static UnixGroupInfo RealGroup
		{
			get
			{
				return new UnixGroupInfo(UnixEnvironment.RealGroupId);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004D44 File Offset: 0x00002F44
		public static long RealGroupId
		{
			get
			{
				return (long)((ulong)Syscall.getgid());
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static UnixUserInfo RealUser
		{
			get
			{
				return new UnixUserInfo(UnixEnvironment.RealUserId);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004D58 File Offset: 0x00002F58
		public static long RealUserId
		{
			get
			{
				return (long)((ulong)Syscall.getuid());
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004D60 File Offset: 0x00002F60
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004D6C File Offset: 0x00002F6C
		public static UnixGroupInfo EffectiveGroup
		{
			get
			{
				return new UnixGroupInfo(UnixEnvironment.EffectiveGroupId);
			}
			set
			{
				UnixEnvironment.EffectiveGroupId = value.GroupId;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004D7C File Offset: 0x00002F7C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004D84 File Offset: 0x00002F84
		public static long EffectiveGroupId
		{
			get
			{
				return (long)((ulong)Syscall.getegid());
			}
			set
			{
				Syscall.setegid(Convert.ToUInt32(value));
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004D94 File Offset: 0x00002F94
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static UnixUserInfo EffectiveUser
		{
			get
			{
				return new UnixUserInfo(UnixEnvironment.EffectiveUserId);
			}
			set
			{
				UnixEnvironment.EffectiveUserId = value.UserId;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004DB0 File Offset: 0x00002FB0
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public static long EffectiveUserId
		{
			get
			{
				return (long)((ulong)Syscall.geteuid());
			}
			set
			{
				Syscall.seteuid(Convert.ToUInt32(value));
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004DC8 File Offset: 0x00002FC8
		public static string Login
		{
			get
			{
				return UnixUserInfo.GetRealUser().UserName;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004DD4 File Offset: 0x00002FD4
		[CLSCompliant(false)]
		public static long GetConfigurationValue(SysconfName name)
		{
			long num = Syscall.sysconf(name);
			if (num == -1L && Stdlib.GetLastError() != (Errno)0)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return num;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004E00 File Offset: 0x00003000
		[CLSCompliant(false)]
		public static string GetConfigurationString(ConfstrName name)
		{
			ulong num = Syscall.confstr(name, null, 0UL);
			if (num == 18446744073709551615UL)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			if (num == 0UL)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder((int)num + 1);
			num = Syscall.confstr(name, stringBuilder, num);
			if (num == 18446744073709551615UL)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004E58 File Offset: 0x00003058
		public static void SetNiceValue(int inc)
		{
			int retval = Syscall.nice(inc);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004E74 File Offset: 0x00003074
		public static int CreateSession()
		{
			int num = Syscall.setsid();
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return num;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004E90 File Offset: 0x00003090
		public static void SetProcessGroup()
		{
			int retval = Syscall.setpgrp();
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004EAC File Offset: 0x000030AC
		public static int GetProcessGroup()
		{
			return Syscall.getpgrp();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004EB4 File Offset: 0x000030B4
		public static UnixGroupInfo[] GetSupplementaryGroups()
		{
			uint[] array = UnixEnvironment._GetSupplementaryGroupIds();
			UnixGroupInfo[] array2 = new UnixGroupInfo[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new UnixGroupInfo((long)((ulong)array[i]));
			}
			return array2;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004EF4 File Offset: 0x000030F4
		private static uint[] _GetSupplementaryGroupIds()
		{
			int num = Syscall.getgroups(0, new uint[0]);
			if (num == -1)
			{
				UnixMarshal.ThrowExceptionForLastError();
			}
			uint[] array = new uint[num];
			int retval = Syscall.getgroups(array);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
			return array;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004F30 File Offset: 0x00003130
		public static void SetSupplementaryGroups(UnixGroupInfo[] groups)
		{
			uint[] array = new uint[groups.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToUInt32(groups[i].GroupId);
			}
			int retval = Syscall.setgroups(array);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004F78 File Offset: 0x00003178
		public static long[] GetSupplementaryGroupIds()
		{
			uint[] array = UnixEnvironment._GetSupplementaryGroupIds();
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (long)((ulong)array[i]);
			}
			return array2;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004FB0 File Offset: 0x000031B0
		public static void SetSupplementaryGroupIds(long[] list)
		{
			uint[] array = new uint[list.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToUInt32(list[i]);
			}
			int retval = Syscall.setgroups(array);
			UnixMarshal.ThrowExceptionForLastErrorIf(retval);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004FF4 File Offset: 0x000031F4
		public static int GetParentProcessId()
		{
			return Syscall.getppid();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004FFC File Offset: 0x000031FC
		public static UnixProcess GetParentProcess()
		{
			return new UnixProcess(UnixEnvironment.GetParentProcessId());
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005008 File Offset: 0x00003208
		public static string[] GetUserShells()
		{
			ArrayList arrayList = new ArrayList();
			object usershell_lock = Syscall.usershell_lock;
			lock (usershell_lock)
			{
				try
				{
					if (Syscall.setusershell() != 0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
					string value;
					while ((value = Syscall.getusershell()) != null)
					{
						arrayList.Add(value);
					}
				}
				finally
				{
					Syscall.endusershell();
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}
}
