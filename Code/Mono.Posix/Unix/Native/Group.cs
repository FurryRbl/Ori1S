using System;
using System.Text;

namespace Mono.Unix.Native
{
	// Token: 0x02000053 RID: 83
	public sealed class Group : IEquatable<Group>
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000B59C File Offset: 0x0000979C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				num ^= this.gr_mem[i].GetHashCode();
			}
			return this.gr_name.GetHashCode() ^ this.gr_passwd.GetHashCode() ^ this.gr_gid.GetHashCode() ^ num;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B5FC File Offset: 0x000097FC
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Group value = (Group)obj;
			return this.Equals(value);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B630 File Offset: 0x00009830
		public bool Equals(Group value)
		{
			if (value == null)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid)
			{
				return false;
			}
			if (value.gr_gid != this.gr_gid || !(value.gr_name == this.gr_name) || !(value.gr_passwd == this.gr_passwd))
			{
				return false;
			}
			if (value.gr_mem == this.gr_mem)
			{
				return true;
			}
			if (value.gr_mem == null || this.gr_mem == null)
			{
				return false;
			}
			if (value.gr_mem.Length != this.gr_mem.Length)
			{
				return false;
			}
			for (int i = 0; i < this.gr_mem.Length; i++)
			{
				if (this.gr_mem[i] != value.gr_mem[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B718 File Offset: 0x00009918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.gr_name).Append(":").Append(this.gr_passwd).Append(":");
			stringBuilder.Append(this.gr_gid).Append(":");
			Group.GetMembers(stringBuilder, this.gr_mem);
			return stringBuilder.ToString();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B780 File Offset: 0x00009980
		private static void GetMembers(StringBuilder sb, string[] members)
		{
			if (members.Length > 0)
			{
				sb.Append(members[0]);
			}
			for (int i = 1; i < members.Length; i++)
			{
				sb.Append(",");
				sb.Append(members[i]);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B7CC File Offset: 0x000099CC
		public static bool operator ==(Group lhs, Group rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public static bool operator !=(Group lhs, Group rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x0400036E RID: 878
		public string gr_name;

		// Token: 0x0400036F RID: 879
		public string gr_passwd;

		// Token: 0x04000370 RID: 880
		[CLSCompliant(false)]
		public uint gr_gid;

		// Token: 0x04000371 RID: 881
		public string[] gr_mem;
	}
}
