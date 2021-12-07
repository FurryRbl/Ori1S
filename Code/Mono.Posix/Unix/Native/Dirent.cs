using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000051 RID: 81
	public sealed class Dirent : IEquatable<Dirent>
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0000B324 File Offset: 0x00009524
		public override int GetHashCode()
		{
			return this.d_ino.GetHashCode() ^ this.d_off.GetHashCode() ^ this.d_reclen.GetHashCode() ^ this.d_type.GetHashCode() ^ this.d_name.GetHashCode();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B36C File Offset: 0x0000956C
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Dirent value = (Dirent)obj;
			return this.Equals(value);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B3A0 File Offset: 0x000095A0
		public bool Equals(Dirent value)
		{
			return !(value == null) && (value.d_ino == this.d_ino && value.d_off == this.d_off && value.d_reclen == this.d_reclen && value.d_type == this.d_type) && value.d_name == this.d_name;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B414 File Offset: 0x00009614
		public override string ToString()
		{
			return this.d_name;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B41C File Offset: 0x0000961C
		public static bool operator ==(Dirent lhs, Dirent rhs)
		{
			return object.Equals(lhs, rhs);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B428 File Offset: 0x00009628
		public static bool operator !=(Dirent lhs, Dirent rhs)
		{
			return !object.Equals(lhs, rhs);
		}

		// Token: 0x04000362 RID: 866
		[CLSCompliant(false)]
		public ulong d_ino;

		// Token: 0x04000363 RID: 867
		public long d_off;

		// Token: 0x04000364 RID: 868
		[CLSCompliant(false)]
		public ushort d_reclen;

		// Token: 0x04000365 RID: 869
		public byte d_type;

		// Token: 0x04000366 RID: 870
		public string d_name;
	}
}
