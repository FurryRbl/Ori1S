using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001D RID: 29
	public struct RaycastResult
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FD8 File Offset: 0x000011D8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002FE0 File Offset: 0x000011E0
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002FEC File Offset: 0x000011EC
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000301C File Offset: 0x0000121C
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector2.zero;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003084 File Offset: 0x00001284
		public override string ToString()
		{
			if (!this.isValid)
			{
				return string.Empty;
			}
			return string.Concat(new object[]
			{
				"Name: ",
				this.gameObject,
				"\nmodule: ",
				this.module,
				"\nmodule camera: ",
				this.module.GetComponent<Camera>(),
				"\ndistance: ",
				this.distance,
				"\nindex: ",
				this.index,
				"\ndepth: ",
				this.depth,
				"\nworldNormal: ",
				this.worldNormal,
				"\nworldPosition: ",
				this.worldPosition,
				"\nscreenPosition: ",
				this.screenPosition,
				"\nmodule.sortOrderPriority: ",
				this.module.sortOrderPriority,
				"\nmodule.renderOrderPriority: ",
				this.module.renderOrderPriority,
				"\nsortingLayer: ",
				this.sortingLayer,
				"\nsortingOrder: ",
				this.sortingOrder
			});
		}

		// Token: 0x0400003F RID: 63
		private GameObject m_GameObject;

		// Token: 0x04000040 RID: 64
		public BaseRaycaster module;

		// Token: 0x04000041 RID: 65
		public float distance;

		// Token: 0x04000042 RID: 66
		public float index;

		// Token: 0x04000043 RID: 67
		public int depth;

		// Token: 0x04000044 RID: 68
		public int sortingLayer;

		// Token: 0x04000045 RID: 69
		public int sortingOrder;

		// Token: 0x04000046 RID: 70
		public Vector3 worldPosition;

		// Token: 0x04000047 RID: 71
		public Vector3 worldNormal;

		// Token: 0x04000048 RID: 72
		public Vector2 screenPosition;
	}
}
