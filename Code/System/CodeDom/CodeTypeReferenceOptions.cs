using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x02000072 RID: 114
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x0400011B RID: 283
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x0400011C RID: 284
		GenericTypeParameter = 2
	}
}
