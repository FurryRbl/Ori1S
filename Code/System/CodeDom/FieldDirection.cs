using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines identifiers used to indicate the direction of parameter and argument declarations.</summary>
	// Token: 0x02000090 RID: 144
	[ComVisible(true)]
	[Serializable]
	public enum FieldDirection
	{
		/// <summary>An incoming field.</summary>
		// Token: 0x0400018D RID: 397
		In,
		/// <summary>An outgoing field.</summary>
		// Token: 0x0400018E RID: 398
		Out,
		/// <summary>A field by reference.</summary>
		// Token: 0x0400018F RID: 399
		Ref
	}
}
