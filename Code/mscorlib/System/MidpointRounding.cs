using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies how mathematical rounding methods should process a number that is midway between two numbers.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000150 RID: 336
	[ComVisible(true)]
	public enum MidpointRounding
	{
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest even number.</summary>
		// Token: 0x04000530 RID: 1328
		ToEven,
		/// <summary>When a number is halfway between two others, it is rounded toward the nearest number that is away from zero.</summary>
		// Token: 0x04000531 RID: 1329
		AwayFromZero
	}
}
