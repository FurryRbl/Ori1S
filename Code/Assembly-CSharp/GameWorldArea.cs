using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D3 RID: 467
[ExecuteInEditMode]
public class GameWorldArea : MonoBehaviour
{
	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0004CB7B File Offset: 0x0004AD7B
	public Bounds Bounds
	{
		get
		{
			return new Bounds(this.BoundingTransform.position, this.BoundingTransform.localScale);
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0004CB98 File Offset: 0x0004AD98
	public Rect BoundingRect
	{
		get
		{
			return new Rect
			{
				width = this.BoundingTransform.lossyScale.x,
				height = this.BoundingTransform.lossyScale.y,
				center = this.BoundingTransform.position
			};
		}
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0004CBFC File Offset: 0x0004ADFC
	public bool InsideFace(Vector3 worldPosition)
	{
		Vector3 position = this.BoundaryCage.transform.InverseTransformPoint(worldPosition);
		CageStructureTool.Face face = this.BoundaryCage.FindFaceAtPositionFaster(position);
		return face != null;
	}

	// Token: 0x04000E46 RID: 3654
	private const float PIXELS_PER_UNIT = 5f;

	// Token: 0x04000E47 RID: 3655
	public List<GameWorldArea.WorldMapIcon> Icons = new List<GameWorldArea.WorldMapIcon>();

	// Token: 0x04000E48 RID: 3656
	public MessageProvider AreaName;

	// Token: 0x04000E49 RID: 3657
	public MessageProvider LowerAreaName;

	// Token: 0x04000E4A RID: 3658
	public string AreaNameString;

	// Token: 0x04000E4B RID: 3659
	public CageStructureTool CageStructureTool;

	// Token: 0x04000E4C RID: 3660
	public Transform BoundingTransform;

	// Token: 0x04000E4D RID: 3661
	public Texture WorldMapTexture;

	// Token: 0x04000E4E RID: 3662
	public string AreaIdentifier = string.Empty;

	// Token: 0x04000E4F RID: 3663
	public CageStructureTool BoundaryCage;

	// Token: 0x04000E50 RID: 3664
	public Condition VisitableCondition;

	// Token: 0x02000885 RID: 2181
	[Serializable]
	public class WorldMapIcon
	{
		// Token: 0x06003123 RID: 12579 RVA: 0x000D1605 File Offset: 0x000CF805
		public WorldMapIcon(SceneMetaData.WorldMapIcon worldMapIcon)
		{
			this.Guid = new MoonGuid(worldMapIcon.Guid);
			this.Position = worldMapIcon.Position;
			this.Icon = worldMapIcon.Icon;
			this.IsSecret = worldMapIcon.IsSecret;
		}

		// Token: 0x04002C73 RID: 11379
		public MoonGuid Guid;

		// Token: 0x04002C74 RID: 11380
		public WorldMapIconType Icon;

		// Token: 0x04002C75 RID: 11381
		public Vector2 Position;

		// Token: 0x04002C76 RID: 11382
		public bool IsSecret;
	}
}
