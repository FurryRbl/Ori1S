using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FF RID: 1279
	internal enum ObjectRecordStatus : byte
	{
		// Token: 0x04001542 RID: 5442
		Unregistered,
		// Token: 0x04001543 RID: 5443
		ReferenceUnsolved,
		// Token: 0x04001544 RID: 5444
		ReferenceSolvingDelayed,
		// Token: 0x04001545 RID: 5445
		ReferenceSolved
	}
}
