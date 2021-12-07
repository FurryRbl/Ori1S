using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000054 RID: 84
	public sealed class Passwd : IEquatable<Passwd>
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000B7EC File Offset: 0x000099EC
		public override int GetHashCode()
		{
			return this.pw_name.GetHashCode() ^ this.pw_passwd.GetHashCode() ^ this.pw_uid.GetHashCode() ^ this.pw_gid.GetHashCode() ^ this.pw_gecos.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_dir.GetHashCode() ^ this.pw_shell.GetHashCode();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B858 File Offset: 0x00009A58
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Passwd value = (Passwd)obj;
			return this.Equals(value);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B88C File Offset: 0x00009A8C
		public bool Equals(Passwd value)
		{
			return !(value == null) && (value.pw_uid == this.pw_uid && value.pw_gid == this.pw_gid && value.pw_name == this.pw_name && value.pw_passwd == this.pw_passwd && value.pw_gecos == this.pw_gecos && value.pw_dir == this.pw_dir) && value.pw_shell == this.pw_shell;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B938 File Offset: 0x00009B38
		public override string ToString()
		{
			return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", new object[]
			{
				this.pw_name,
				this.pw_passwd,
				this.pw_uid,
				this.pw_gid,
				this.pw_gecos,
				this.pw_dir,
				this.pw_shell
			});
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public static bool operator ==(Passwd lhs, Passwd rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public static bool operator !=(Passwd lhs, Passwd rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000372 RID: 882
		public string pw_name;

		// Token: 0x04000373 RID: 883
		public string pw_passwd;

		// Token: 0x04000374 RID: 884
		[CLSCompliant(false)]
		public uint pw_uid;

		// Token: 0x04000375 RID: 885
		[CLSCompliant(false)]
		public uint pw_gid;

		// Token: 0x04000376 RID: 886
		public string pw_gecos;

		// Token: 0x04000377 RID: 887
		public string pw_dir;

		// Token: 0x04000378 RID: 888
		public string pw_shell;
	}
}
