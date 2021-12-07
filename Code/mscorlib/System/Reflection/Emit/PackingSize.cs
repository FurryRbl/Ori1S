using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies one of two factors that determine the memory alignment of fields when a type is marshaled.</summary>
	// Token: 0x020002F3 RID: 755
	[ComVisible(true)]
	[Serializable]
	public enum PackingSize
	{
		/// <summary>The packing size is not specified.</summary>
		// Token: 0x04000F85 RID: 3973
		Unspecified,
		/// <summary>The packing size is 1 byte.</summary>
		// Token: 0x04000F86 RID: 3974
		Size1,
		/// <summary>The packing size is 2 bytes.</summary>
		// Token: 0x04000F87 RID: 3975
		Size2,
		/// <summary>The packing size is 4 bytes.</summary>
		// Token: 0x04000F88 RID: 3976
		Size4 = 4,
		/// <summary>The packing size is 8 bytes.</summary>
		// Token: 0x04000F89 RID: 3977
		Size8 = 8,
		/// <summary>The packing size is 16 bytes.</summary>
		// Token: 0x04000F8A RID: 3978
		Size16 = 16,
		/// <summary>The packing size is 32 bytes.</summary>
		// Token: 0x04000F8B RID: 3979
		Size32 = 32,
		/// <summary>The packing size is 64 bytes.</summary>
		// Token: 0x04000F8C RID: 3980
		Size64 = 64,
		/// <summary>The packing size is 128 bytes.</summary>
		// Token: 0x04000F8D RID: 3981
		Size128 = 128
	}
}
