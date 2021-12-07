using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FD RID: 1277
	internal class FixupRecord : BaseFixupRecord
	{
		// Token: 0x0600331C RID: 13084 RVA: 0x000A5774 File Offset: 0x000A3974
		public FixupRecord(ObjectRecord objectToBeFixed, MemberInfo member, ObjectRecord objectRequired) : base(objectToBeFixed, objectRequired)
		{
			this._member = member;
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000A5788 File Offset: 0x000A3988
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetMemberValue(manager, this._member, this.ObjectRequired.ObjectInstance);
		}

		// Token: 0x0400153F RID: 5439
		public MemberInfo _member;
	}
}
