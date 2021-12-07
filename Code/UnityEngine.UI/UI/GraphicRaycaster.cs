using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000048 RID: 72
	[AddComponentMenu("Event/Graphic Raycaster")]
	[RequireComponent(typeof(Canvas))]
	public class GraphicRaycaster : BaseRaycaster
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000094F8 File Offset: 0x000076F8
		protected GraphicRaycaster()
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009538 File Offset: 0x00007738
		public override int sortOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.sortingOrder;
				}
				return base.sortOrderPriority;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009568 File Offset: 0x00007768
		public override int renderOrderPriority
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					return this.canvas.renderOrder;
				}
				return base.renderOrderPriority;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00009598 File Offset: 0x00007798
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000095A0 File Offset: 0x000077A0
		public bool ignoreReversedGraphics
		{
			get
			{
				return this.m_IgnoreReversedGraphics;
			}
			set
			{
				this.m_IgnoreReversedGraphics = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000095AC File Offset: 0x000077AC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000095B4 File Offset: 0x000077B4
		public GraphicRaycaster.BlockingObjects blockingObjects
		{
			get
			{
				return this.m_BlockingObjects;
			}
			set
			{
				this.m_BlockingObjects = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000095C0 File Offset: 0x000077C0
		private Canvas canvas
		{
			get
			{
				if (this.m_Canvas != null)
				{
					return this.m_Canvas;
				}
				this.m_Canvas = base.GetComponent<Canvas>();
				return this.m_Canvas;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000095F8 File Offset: 0x000077F8
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.canvas == null)
			{
				return;
			}
			Vector2 vector;
			if (this.eventCamera == null)
			{
				float num = (float)Screen.width;
				float num2 = (float)Screen.height;
				vector = new Vector2(eventData.position.x / num, eventData.position.y / num2);
			}
			else
			{
				vector = this.eventCamera.ScreenToViewportPoint(eventData.position);
			}
			if (vector.x < 0f || vector.x > 1f || vector.y < 0f || vector.y > 1f)
			{
				return;
			}
			float num3 = float.MaxValue;
			Ray ray = default(Ray);
			if (this.eventCamera != null)
			{
				ray = this.eventCamera.ScreenPointToRay(eventData.position);
			}
			if (this.canvas.renderMode != RenderMode.ScreenSpaceOverlay && this.blockingObjects != GraphicRaycaster.BlockingObjects.None)
			{
				float num4 = 100f;
				if (this.eventCamera != null)
				{
					num4 = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
				}
				RaycastHit raycastHit;
				if ((this.blockingObjects == GraphicRaycaster.BlockingObjects.ThreeD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All) && Physics.Raycast(ray, out raycastHit, num4, this.m_BlockingMask))
				{
					num3 = raycastHit.distance;
				}
				if (this.blockingObjects == GraphicRaycaster.BlockingObjects.TwoD || this.blockingObjects == GraphicRaycaster.BlockingObjects.All)
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, num4, this.m_BlockingMask);
					if (raycastHit2D.collider != null)
					{
						num3 = raycastHit2D.fraction * num4;
					}
				}
			}
			this.m_RaycastResults.Clear();
			GraphicRaycaster.Raycast(this.canvas, this.eventCamera, eventData.position, this.m_RaycastResults);
			for (int i = 0; i < this.m_RaycastResults.Count; i++)
			{
				GameObject gameObject = this.m_RaycastResults[i].gameObject;
				bool flag = true;
				if (this.ignoreReversedGraphics)
				{
					if (this.eventCamera == null)
					{
						Vector3 rhs = gameObject.transform.rotation * Vector3.forward;
						flag = (Vector3.Dot(Vector3.forward, rhs) > 0f);
					}
					else
					{
						Vector3 lhs = this.eventCamera.transform.rotation * Vector3.forward;
						Vector3 rhs2 = gameObject.transform.rotation * Vector3.forward;
						flag = (Vector3.Dot(lhs, rhs2) > 0f);
					}
				}
				if (flag)
				{
					float num5;
					if (this.eventCamera == null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
					{
						num5 = 0f;
					}
					else
					{
						Transform transform = gameObject.transform;
						Vector3 forward = transform.forward;
						num5 = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
						if (num5 < 0f)
						{
							goto IL_3DA;
						}
					}
					if (num5 < num3)
					{
						RaycastResult item = new RaycastResult
						{
							gameObject = gameObject,
							module = this,
							distance = num5,
							screenPosition = eventData.position,
							index = (float)resultAppendList.Count,
							depth = this.m_RaycastResults[i].depth,
							sortingLayer = this.canvas.sortingLayerID,
							sortingOrder = this.canvas.sortingOrder
						};
						resultAppendList.Add(item);
					}
				}
				IL_3DA:;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000099F8 File Offset: 0x00007BF8
		public override Camera eventCamera
		{
			get
			{
				if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || (this.canvas.renderMode == RenderMode.ScreenSpaceCamera && this.canvas.worldCamera == null))
				{
					return null;
				}
				return (!(this.canvas.worldCamera != null)) ? Camera.main : this.canvas.worldCamera;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009A6C File Offset: 0x00007C6C
		private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, List<Graphic> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth != -1 && graphic.raycastTarget)
				{
					if (RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera))
					{
						if (graphic.Raycast(pointerPosition, eventCamera))
						{
							GraphicRaycaster.s_SortedGraphics.Add(graphic);
						}
					}
				}
			}
			GraphicRaycaster.s_SortedGraphics.Sort((Graphic g1, Graphic g2) => g2.depth.CompareTo(g1.depth));
			for (int j = 0; j < GraphicRaycaster.s_SortedGraphics.Count; j++)
			{
				results.Add(GraphicRaycaster.s_SortedGraphics[j]);
			}
			GraphicRaycaster.s_SortedGraphics.Clear();
		}

		// Token: 0x04000100 RID: 256
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000101 RID: 257
		[FormerlySerializedAs("ignoreReversedGraphics")]
		[SerializeField]
		private bool m_IgnoreReversedGraphics = true;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		[FormerlySerializedAs("blockingObjects")]
		private GraphicRaycaster.BlockingObjects m_BlockingObjects;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		// Token: 0x04000104 RID: 260
		private Canvas m_Canvas;

		// Token: 0x04000105 RID: 261
		[NonSerialized]
		private List<Graphic> m_RaycastResults = new List<Graphic>();

		// Token: 0x04000106 RID: 262
		[NonSerialized]
		private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

		// Token: 0x02000049 RID: 73
		public enum BlockingObjects
		{
			// Token: 0x04000109 RID: 265
			None,
			// Token: 0x0400010A RID: 266
			TwoD,
			// Token: 0x0400010B RID: 267
			ThreeD,
			// Token: 0x0400010C RID: 268
			All
		}
	}
}
