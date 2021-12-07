using System;
using System.Collections;
using System.Text;
using Mono.Unix.Native;

namespace Mono.Unix
{
	// Token: 0x02000024 RID: 36
	public sealed class UnixUserInfo
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00008148 File Offset: 0x00006348
		public UnixUserInfo(string user)
		{
			this.passwd = new Passwd();
			Passwd lhs;
			if (Syscall.getpwnam_r(user, this.passwd, out lhs) != 0 || lhs == null)
			{
				throw new ArgumentException(Locale.GetText("invalid username"), "user");
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000819C File Offset: 0x0000639C
		[CLSCompliant(false)]
		public UnixUserInfo(uint user)
		{
			this.passwd = new Passwd();
			Passwd lhs;
			if (Syscall.getpwuid_r(user, this.passwd, out lhs) != 0 || lhs == null)
			{
				throw new ArgumentException(Locale.GetText("invalid user id"), "user");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000081F0 File Offset: 0x000063F0
		public UnixUserInfo(long user)
		{
			this.passwd = new Passwd();
			Passwd lhs;
			if (Syscall.getpwuid_r(Convert.ToUInt32(user), this.passwd, out lhs) != 0 || lhs == null)
			{
				throw new ArgumentException(Locale.GetText("invalid user id"), "user");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000824C File Offset: 0x0000644C
		public UnixUserInfo(Passwd passwd)
		{
			this.passwd = UnixUserInfo.CopyPasswd(passwd);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008260 File Offset: 0x00006460
		private static Passwd CopyPasswd(Passwd pw)
		{
			return new Passwd
			{
				pw_name = pw.pw_name,
				pw_passwd = pw.pw_passwd,
				pw_uid = pw.pw_uid,
				pw_gid = pw.pw_gid,
				pw_gecos = pw.pw_gecos,
				pw_dir = pw.pw_dir,
				pw_shell = pw.pw_shell
			};
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000082C8 File Offset: 0x000064C8
		public string UserName
		{
			get
			{
				return this.passwd.pw_name;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000082D8 File Offset: 0x000064D8
		public string Password
		{
			get
			{
				return this.passwd.pw_passwd;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000082E8 File Offset: 0x000064E8
		public long UserId
		{
			get
			{
				return (long)((ulong)this.passwd.pw_uid);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000082F8 File Offset: 0x000064F8
		public UnixGroupInfo Group
		{
			get
			{
				return new UnixGroupInfo((long)((ulong)this.passwd.pw_gid));
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000830C File Offset: 0x0000650C
		public long GroupId
		{
			get
			{
				return (long)((ulong)this.passwd.pw_gid);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000831C File Offset: 0x0000651C
		public string GroupName
		{
			get
			{
				return this.Group.GroupName;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000832C File Offset: 0x0000652C
		public string RealName
		{
			get
			{
				return this.passwd.pw_gecos;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000833C File Offset: 0x0000653C
		public string HomeDirectory
		{
			get
			{
				return this.passwd.pw_dir;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000834C File Offset: 0x0000654C
		public string ShellProgram
		{
			get
			{
				return this.passwd.pw_shell;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000835C File Offset: 0x0000655C
		public override int GetHashCode()
		{
			return this.passwd.GetHashCode();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000836C File Offset: 0x0000656C
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType() && this.passwd.Equals(((UnixUserInfo)obj).passwd);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000083A0 File Offset: 0x000065A0
		public override string ToString()
		{
			return this.passwd.ToString();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000083B0 File Offset: 0x000065B0
		public static UnixUserInfo GetRealUser()
		{
			return new UnixUserInfo(UnixUserInfo.GetRealUserId());
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000083BC File Offset: 0x000065BC
		public static long GetRealUserId()
		{
			return (long)((ulong)Syscall.getuid());
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000083C4 File Offset: 0x000065C4
		public static string GetLoginName()
		{
			StringBuilder stringBuilder = new StringBuilder(4);
			int num;
			do
			{
				stringBuilder.Capacity *= 2;
				num = Syscall.getlogin_r(stringBuilder, (ulong)((long)stringBuilder.Capacity));
			}
			while (num == -1 && Stdlib.GetLastError() == Errno.ERANGE);
			UnixMarshal.ThrowExceptionForLastErrorIf(num);
			return stringBuilder.ToString();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008414 File Offset: 0x00006614
		public Passwd ToPasswd()
		{
			return UnixUserInfo.CopyPasswd(this.passwd);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008424 File Offset: 0x00006624
		public static UnixUserInfo[] GetLocalUsers()
		{
			ArrayList arrayList = new ArrayList();
			object pwd_lock = Syscall.pwd_lock;
			lock (pwd_lock)
			{
				if (Syscall.setpwent() != 0)
				{
					UnixMarshal.ThrowExceptionForLastError();
				}
				try
				{
					Passwd passwd;
					while ((passwd = Syscall.getpwent()) != null)
					{
						arrayList.Add(new UnixUserInfo(passwd));
					}
					if (Stdlib.GetLastError() != (Errno)0)
					{
						UnixMarshal.ThrowExceptionForLastError();
					}
				}
				finally
				{
					Syscall.endpwent();
				}
			}
			return (UnixUserInfo[])arrayList.ToArray(typeof(UnixUserInfo));
		}

		// Token: 0x0400008A RID: 138
		private Passwd passwd;
	}
}
