using System;
using System.Collections;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000018 RID: 24
	public sealed class UnixGroupInfo
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005B70 File Offset: 0x00003D70
		public UnixGroupInfo(string group)
		{
			this.group = new Group();
			Group lhs;
			if (Syscall.getgrnam_r(group, this.group, out lhs) != 0 || lhs == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group name"), "group");
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public UnixGroupInfo(long group)
		{
			this.group = new Group();
			Group lhs;
			if (Syscall.getgrgid_r(Convert.ToUInt32(group), this.group, out lhs) != 0 || lhs == null)
			{
				throw new ArgumentException(Locale.GetText("invalid group id"), "group");
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005C20 File Offset: 0x00003E20
		public UnixGroupInfo(Group group)
		{
			this.group = UnixGroupInfo.CopyGroup(group);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005C34 File Offset: 0x00003E34
		private static Group CopyGroup(Group group)
		{
			return new Group
			{
				gr_gid = group.gr_gid,
				gr_mem = group.gr_mem,
				gr_name = group.gr_name,
				gr_passwd = group.gr_passwd
			};
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005C78 File Offset: 0x00003E78
		public string GroupName
		{
			get
			{
				return this.group.gr_name;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005C88 File Offset: 0x00003E88
		public string Password
		{
			get
			{
				return this.group.gr_passwd;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005C98 File Offset: 0x00003E98
		public long GroupId
		{
			get
			{
				return (long)((ulong)this.group.gr_gid);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005CA8 File Offset: 0x00003EA8
		public UnixUserInfo[] GetMembers()
		{
			ArrayList arrayList = new ArrayList(this.group.gr_mem.Length);
			for (int i = 0; i < this.group.gr_mem.Length; i++)
			{
				try
				{
					arrayList.Add(new UnixUserInfo(this.group.gr_mem[i]));
				}
				catch (ArgumentException)
				{
				}
			}
			return (UnixUserInfo[])arrayList.ToArray(typeof(UnixUserInfo));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005D3C File Offset: 0x00003F3C
		public string[] GetMemberNames()
		{
			return (string[])this.group.gr_mem.Clone();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005D54 File Offset: 0x00003F54
		public override int GetHashCode()
		{
			return this.group.GetHashCode();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005D64 File Offset: 0x00003F64
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.group.Equals(((UnixGroupInfo)obj).group);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005D98 File Offset: 0x00003F98
		public override string ToString()
		{
			return this.group.ToString();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public Group ToGroup()
		{
			return UnixGroupInfo.CopyGroup(this.group);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static UnixGroupInfo[] GetLocalGroups()
		{
			ArrayList arrayList = new ArrayList();
			object grp_lock = Syscall.grp_lock;
			lock (grp_lock)
			{
				if (Syscall.setgrent() != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				try
				{
					Group group;
					while ((group = Syscall.getgrent()) != null)
					{
						arrayList.Add(new UnixGroupInfo(group));
					}
					if (Stdlib.GetLastError() != (Errno)0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
				}
				finally
				{
					Syscall.endgrent();
				}
			}
			return (UnixGroupInfo[])arrayList.ToArray(typeof(UnixGroupInfo));
		}

		// Token: 0x04000067 RID: 103
		private Group group;
	}
}
