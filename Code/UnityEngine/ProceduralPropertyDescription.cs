using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000097 RID: 151
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProceduralPropertyDescription
	{
		// Token: 0x040001D0 RID: 464
		public string name;

		// Token: 0x040001D1 RID: 465
		public string label;

		// Token: 0x040001D2 RID: 466
		public string group;

		// Token: 0x040001D3 RID: 467
		public ProceduralPropertyType type;

		// Token: 0x040001D4 RID: 468
		public bool hasRange;

		// Token: 0x040001D5 RID: 469
		public float minimum;

		// Token: 0x040001D6 RID: 470
		public float maximum;

		// Token: 0x040001D7 RID: 471
		public float step;

		// Token: 0x040001D8 RID: 472
		public string[] enumOptions;

		// Token: 0x040001D9 RID: 473
		public string[] componentLabels;
	}
}
