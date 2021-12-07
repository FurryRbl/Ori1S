using System;

namespace UnityEngine
{
	// Token: 0x02000205 RID: 517
	public sealed class GUILayoutOption
	{
		// Token: 0x06001FDD RID: 8157 RVA: 0x00024CD4 File Offset: 0x00022ED4
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x040007DB RID: 2011
		internal GUILayoutOption.Type type;

		// Token: 0x040007DC RID: 2012
		internal object value;

		// Token: 0x02000206 RID: 518
		internal enum Type
		{
			// Token: 0x040007DE RID: 2014
			fixedWidth,
			// Token: 0x040007DF RID: 2015
			fixedHeight,
			// Token: 0x040007E0 RID: 2016
			minWidth,
			// Token: 0x040007E1 RID: 2017
			maxWidth,
			// Token: 0x040007E2 RID: 2018
			minHeight,
			// Token: 0x040007E3 RID: 2019
			maxHeight,
			// Token: 0x040007E4 RID: 2020
			stretchWidth,
			// Token: 0x040007E5 RID: 2021
			stretchHeight,
			// Token: 0x040007E6 RID: 2022
			alignStart,
			// Token: 0x040007E7 RID: 2023
			alignMiddle,
			// Token: 0x040007E8 RID: 2024
			alignEnd,
			// Token: 0x040007E9 RID: 2025
			alignJustify,
			// Token: 0x040007EA RID: 2026
			equalSize,
			// Token: 0x040007EB RID: 2027
			spacing
		}
	}
}
