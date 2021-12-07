using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200027A RID: 634
	internal sealed class TypeMember
	{
		// Token: 0x060019A7 RID: 6567 RVA: 0x000870CC File Offset: 0x000852CC
		internal TypeMember(Type type, string member)
		{
			this.type = type;
			this.member = member;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000870E4 File Offset: 0x000852E4
		public override int GetHashCode()
		{
			return this.type.GetHashCode() + this.member.GetHashCode();
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00087100 File Offset: 0x00085300
		public override bool Equals(object obj)
		{
			return obj is TypeMember && TypeMember.Equals(this, (TypeMember)obj);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0008711C File Offset: 0x0008531C
		public static bool Equals(TypeMember tm1, TypeMember tm2)
		{
			return object.ReferenceEquals(tm1, tm2) || (!object.ReferenceEquals(tm1, null) && !object.ReferenceEquals(tm2, null) && (tm1.type == tm2.type && tm1.member == tm2.member));
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0008717C File Offset: 0x0008537C
		public override string ToString()
		{
			return this.type.ToString() + " " + this.member;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0008719C File Offset: 0x0008539C
		public static bool operator ==(TypeMember tm1, TypeMember tm2)
		{
			return TypeMember.Equals(tm1, tm2);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000871A8 File Offset: 0x000853A8
		public static bool operator !=(TypeMember tm1, TypeMember tm2)
		{
			return !TypeMember.Equals(tm1, tm2);
		}

		// Token: 0x04000AA1 RID: 2721
		private Type type;

		// Token: 0x04000AA2 RID: 2722
		private string member;
	}
}
