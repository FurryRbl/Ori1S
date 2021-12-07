using System;

namespace JetBrains.Annotations
{
	// Token: 0x020002D5 RID: 725
	[Flags]
	public enum ImplicitUseKindFlags
	{
		// Token: 0x04000B99 RID: 2969
		Default = 7,
		// Token: 0x04000B9A RID: 2970
		Access = 1,
		// Token: 0x04000B9B RID: 2971
		Assign = 2,
		// Token: 0x04000B9C RID: 2972
		InstantiatedWithFixedConstructorSignature = 4,
		// Token: 0x04000B9D RID: 2973
		InstantiatedNoFixedConstructorSignature = 8
	}
}
