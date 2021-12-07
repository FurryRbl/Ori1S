using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000052 RID: 82
	public sealed class Fstab : IEquatable<Fstab>
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000B43C File Offset: 0x0000963C
		public override int GetHashCode()
		{
			return this.fs_spec.GetHashCode() ^ this.fs_file.GetHashCode() ^ this.fs_vfstype.GetHashCode() ^ this.fs_mntops.GetHashCode() ^ this.fs_type.GetHashCode() ^ this.fs_freq ^ this.fs_passno;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B494 File Offset: 0x00009694
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Fstab value = (Fstab)obj;
			return this.Equals(value);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public bool Equals(Fstab value)
		{
			return !(value == null) && (value.fs_spec == this.fs_spec && value.fs_file == this.fs_file && value.fs_vfstype == this.fs_vfstype && value.fs_mntops == this.fs_mntops && value.fs_type == this.fs_type && value.fs_freq == this.fs_freq) && value.fs_passno == this.fs_passno;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B574 File Offset: 0x00009774
		public override string ToString()
		{
			return this.fs_spec;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B57C File Offset: 0x0000977C
		public static bool operator ==(Fstab lhs, Fstab rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B588 File Offset: 0x00009788
		public static bool operator !=(Fstab lhs, Fstab rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000367 RID: 871
		public string fs_spec;

		// Token: 0x04000368 RID: 872
		public string fs_file;

		// Token: 0x04000369 RID: 873
		public string fs_vfstype;

		// Token: 0x0400036A RID: 874
		public string fs_mntops;

		// Token: 0x0400036B RID: 875
		public string fs_type;

		// Token: 0x0400036C RID: 876
		public int fs_freq;

		// Token: 0x0400036D RID: 877
		public int fs_passno;
	}
}
