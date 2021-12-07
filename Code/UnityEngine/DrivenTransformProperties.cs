using System;

namespace UnityEngine
{
	// Token: 0x0200007D RID: 125
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x0400015F RID: 351
		None = 0,
		// Token: 0x04000160 RID: 352
		All = -1,
		// Token: 0x04000161 RID: 353
		AnchoredPositionX = 2,
		// Token: 0x04000162 RID: 354
		AnchoredPositionY = 4,
		// Token: 0x04000163 RID: 355
		AnchoredPositionZ = 8,
		// Token: 0x04000164 RID: 356
		Rotation = 16,
		// Token: 0x04000165 RID: 357
		ScaleX = 32,
		// Token: 0x04000166 RID: 358
		ScaleY = 64,
		// Token: 0x04000167 RID: 359
		ScaleZ = 128,
		// Token: 0x04000168 RID: 360
		AnchorMinX = 256,
		// Token: 0x04000169 RID: 361
		AnchorMinY = 512,
		// Token: 0x0400016A RID: 362
		AnchorMaxX = 1024,
		// Token: 0x0400016B RID: 363
		AnchorMaxY = 2048,
		// Token: 0x0400016C RID: 364
		SizeDeltaX = 4096,
		// Token: 0x0400016D RID: 365
		SizeDeltaY = 8192,
		// Token: 0x0400016E RID: 366
		PivotX = 16384,
		// Token: 0x0400016F RID: 367
		PivotY = 32768,
		// Token: 0x04000170 RID: 368
		AnchoredPosition = 6,
		// Token: 0x04000171 RID: 369
		AnchoredPosition3D = 14,
		// Token: 0x04000172 RID: 370
		Scale = 224,
		// Token: 0x04000173 RID: 371
		AnchorMin = 768,
		// Token: 0x04000174 RID: 372
		AnchorMax = 3072,
		// Token: 0x04000175 RID: 373
		Anchors = 3840,
		// Token: 0x04000176 RID: 374
		SizeDelta = 12288,
		// Token: 0x04000177 RID: 375
		Pivot = 49152
	}
}
