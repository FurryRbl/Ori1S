using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000055 RID: 85
	public sealed class Utsname : IEquatable<Utsname>
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public override int GetHashCode()
		{
			return this.sysname.GetHashCode() ^ this.nodename.GetHashCode() ^ this.release.GetHashCode() ^ this.version.GetHashCode() ^ this.machine.GetHashCode() ^ this.domainname.GetHashCode();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BA14 File Offset: 0x00009C14
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Utsname value = (Utsname)obj;
			return this.Equals(value);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000BA48 File Offset: 0x00009C48
		public bool Equals(Utsname value)
		{
			return value.sysname == this.sysname && value.nodename == this.nodename && value.release == this.release && value.version == this.version && value.machine == this.machine && value.domainname == this.domainname;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3} {4}", new object[]
			{
				this.sysname,
				this.nodename,
				this.release,
				this.version,
				this.machine
			});
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BB18 File Offset: 0x00009D18
		public static bool operator ==(Utsname lhs, Utsname rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BB24 File Offset: 0x00009D24
		public static bool operator !=(Utsname lhs, Utsname rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000379 RID: 889
		public string sysname;

		// Token: 0x0400037A RID: 890
		public string nodename;

		// Token: 0x0400037B RID: 891
		public string release;

		// Token: 0x0400037C RID: 892
		public string version;

		// Token: 0x0400037D RID: 893
		public string machine;

		// Token: 0x0400037E RID: 894
		public string domainname;
	}
}
