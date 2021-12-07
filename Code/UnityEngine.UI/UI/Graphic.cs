using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x02000047 RID: 71
	[DisallowMultipleComponent]
	[RequireComponent(typeof(CanvasRenderer))]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public abstract class Graphic : UIBehaviour, ICanvasElement
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00008904 File Offset: 0x00006B04
		protected Graphic()
		{
			if (this.m_ColorTweenRunner == null)
			{
				this.m_ColorTweenRunner = new TweenRunner<ColorTween>();
			}
			this.m_ColorTweenRunner.Init(this);
			this.useLegacyMeshGeneration = true;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000896C File Offset: 0x00006B6C
		public static Material defaultGraphicMaterial
		{
			get
			{
				if (Graphic.s_DefaultUI == null)
				{
					Graphic.s_DefaultUI = Canvas.GetDefaultCanvasMaterial();
				}
				return Graphic.s_DefaultUI;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008990 File Offset: 0x00006B90
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008998 File Offset: 0x00006B98
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_Color, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000089B4 File Offset: 0x00006BB4
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000089BC File Offset: 0x00006BBC
		public bool raycastTarget
		{
			get
			{
				return this.m_RaycastTarget;
			}
			set
			{
				this.m_RaycastTarget = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000089C8 File Offset: 0x00006BC8
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected bool useLegacyMeshGeneration { get; set; }

		// Token: 0x0600020E RID: 526 RVA: 0x000089DC File Offset: 0x00006BDC
		public virtual void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000089F0 File Offset: 0x00006BF0
		public virtual void SetLayoutDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			if (this.m_OnDirtyLayoutCallback != null)
			{
				this.m_OnDirtyLayoutCallback();
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008A20 File Offset: 0x00006C20
		public virtual void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_VertsDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyVertsCallback != null)
			{
				this.m_OnDirtyVertsCallback();
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008A54 File Offset: 0x00006C54
		public virtual void SetMaterialDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_MaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008A88 File Offset: 0x00006C88
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					this.SetVerticesDirty();
				}
				else
				{
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008AC8 File Offset: 0x00006CC8
		protected override void OnBeforeTransformParentChanged()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008AE4 File Offset: 0x00006CE4
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			if (!this.IsActive())
			{
				return;
			}
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			this.SetAllDirty();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008B1C File Offset: 0x00006D1C
		public int depth
		{
			get
			{
				return this.canvasRenderer.absoluteDepth;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008B2C File Offset: 0x00006D2C
		public RectTransform rectTransform
		{
			get
			{
				RectTransform result;
				if ((result = this.m_RectTransform) == null)
				{
					result = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return result;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008B58 File Offset: 0x00006D58
		public Canvas canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					this.CacheCanvas();
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008B78 File Offset: 0x00006D78
		private void CacheCanvas()
		{
			List<Canvas> list = ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].isActiveAndEnabled)
					{
						this.m_Canvas = list[i];
						break;
					}
				}
			}
			else
			{
				this.m_Canvas = null;
			}
			ListPool<Canvas>.Release(list);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRender == null)
				{
					this.m_CanvasRender = base.GetComponent<CanvasRenderer>();
				}
				return this.m_CanvasRender;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008C18 File Offset: 0x00006E18
		public virtual Material defaultMaterial
		{
			get
			{
				return Graphic.defaultGraphicMaterial;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008C20 File Offset: 0x00006E20
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008C50 File Offset: 0x00006E50
		public virtual Material material
		{
			get
			{
				return (!(this.m_Material != null)) ? this.defaultMaterial : this.m_Material;
			}
			set
			{
				if (this.m_Material == value)
				{
					return;
				}
				this.m_Material = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008C74 File Offset: 0x00006E74
		public virtual Material materialForRendering
		{
			get
			{
				List<Component> list = ListPool<Component>.Get();
				base.GetComponents(typeof(IMaterialModifier), list);
				Material material = this.material;
				for (int i = 0; i < list.Count; i++)
				{
					material = (list[i] as IMaterialModifier).GetModifiedMaterial(material);
				}
				ListPool<Component>.Release(list);
				return material;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public virtual Texture mainTexture
		{
			get
			{
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008CD8 File Offset: 0x00006ED8
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			if (Graphic.s_WhiteTexture == null)
			{
				Graphic.s_WhiteTexture = Texture2D.whiteTexture;
			}
			this.SetAllDirty();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008D20 File Offset: 0x00006F20
		protected override void OnDisable()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008D6C File Offset: 0x00006F6C
		protected override void OnCanvasHierarchyChanged()
		{
			Canvas canvas = this.m_Canvas;
			this.CacheCanvas();
			if (canvas != this.m_Canvas)
			{
				GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008DAC File Offset: 0x00006FAC
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (this.canvasRenderer.cull)
			{
				return;
			}
			if (update == CanvasUpdate.PreRender)
			{
				if (this.m_VertsDirty)
				{
					this.UpdateGeometry();
					this.m_VertsDirty = false;
				}
				if (this.m_MaterialDirty)
				{
					this.UpdateMaterial();
					this.m_MaterialDirty = false;
				}
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00008E10 File Offset: 0x00007010
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00008E14 File Offset: 0x00007014
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008E18 File Offset: 0x00007018
		protected virtual void UpdateMaterial()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(this.materialForRendering, 0);
			this.canvasRenderer.SetTexture(this.mainTexture);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008E60 File Offset: 0x00007060
		protected virtual void UpdateGeometry()
		{
			if (this.useLegacyMeshGeneration)
			{
				this.DoLegacyMeshGeneration();
			}
			else
			{
				this.DoMeshGeneration();
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008E80 File Offset: 0x00007080
		private void DoMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.s_VertexHelper);
			}
			else
			{
				Graphic.s_VertexHelper.Clear();
			}
			List<Component> list = ListPool<Component>.Get();
			base.GetComponents(typeof(IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IMeshModifier)list[i]).ModifyMesh(Graphic.s_VertexHelper);
			}
			ListPool<Component>.Release(list);
			Graphic.s_VertexHelper.FillMesh(Graphic.workerMesh);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008F5C File Offset: 0x0000715C
		private void DoLegacyMeshGeneration()
		{
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnPopulateMesh(Graphic.workerMesh);
			}
			else
			{
				Graphic.workerMesh.Clear();
			}
			List<Component> list = ListPool<Component>.Get();
			base.GetComponents(typeof(IMeshModifier), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IMeshModifier)list[i]).ModifyMesh(Graphic.workerMesh);
			}
			ListPool<Component>.Release(list);
			this.canvasRenderer.SetMesh(Graphic.workerMesh);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009028 File Offset: 0x00007228
		protected static Mesh workerMesh
		{
			get
			{
				if (Graphic.s_Mesh == null)
				{
					Graphic.s_Mesh = new Mesh();
					Graphic.s_Mesh.name = "Shared UI Mesh";
					Graphic.s_Mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return Graphic.s_Mesh;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009070 File Offset: 0x00007270
		[Obsolete("Use OnPopulateMesh instead.", true)]
		protected virtual void OnFillVBO(List<UIVertex> vbo)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009074 File Offset: 0x00007274
		[Obsolete("Use OnPopulateMesh(VertexHelper vh) instead.", false)]
		protected virtual void OnPopulateMesh(Mesh m)
		{
			this.OnPopulateMesh(Graphic.s_VertexHelper);
			Graphic.s_VertexHelper.FillMesh(m);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000908C File Offset: 0x0000728C
		protected virtual void OnPopulateMesh(VertexHelper vh)
		{
			Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			Color32 color = this.color;
			vh.Clear();
			vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(0f, 0f));
			vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(0f, 1f));
			vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(1f, 1f));
			vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(1f, 0f));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000919C File Offset: 0x0000739C
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetAllDirty();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000091A4 File Offset: 0x000073A4
		public virtual void SetNativeSize()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000091A8 File Offset: 0x000073A8
		public virtual bool Raycast(Vector2 sp, Camera eventCamera)
		{
			if (!base.isActiveAndEnabled)
			{
				return false;
			}
			Transform transform = base.transform;
			List<Component> list = ListPool<Component>.Get();
			bool flag = false;
			while (transform != null)
			{
				transform.GetComponents<Component>(list);
				for (int i = 0; i < list.Count; i++)
				{
					ICanvasRaycastFilter canvasRaycastFilter = list[i] as ICanvasRaycastFilter;
					if (canvasRaycastFilter != null)
					{
						bool flag2 = true;
						CanvasGroup canvasGroup = list[i] as CanvasGroup;
						if (canvasGroup != null)
						{
							if (!flag && canvasGroup.ignoreParentGroups)
							{
								flag = true;
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
							else if (!flag)
							{
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
						}
						else
						{
							flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
						}
						if (!flag2)
						{
							ListPool<Component>.Release(list);
							return false;
						}
					}
				}
				transform = transform.parent;
			}
			ListPool<Component>.Release(list);
			return true;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000929C File Offset: 0x0000749C
		public Vector2 PixelAdjustPoint(Vector2 point)
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return point;
			}
			return RectTransformUtility.PixelAdjustPoint(point, base.transform, this.canvas);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000092E0 File Offset: 0x000074E0
		public Rect GetPixelAdjustedRect()
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return this.rectTransform.rect;
			}
			return RectTransformUtility.PixelAdjustRect(this.rectTransform, this.canvas);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000932C File Offset: 0x0000752C
		public void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			this.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, true);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000933C File Offset: 0x0000753C
		private void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (this.canvasRenderer == null || (!useRGB && !useAlpha))
			{
				return;
			}
			if (this.canvasRenderer.GetColor().Equals(targetColor))
			{
				return;
			}
			ColorTween.ColorTweenMode tweenMode = (!useRGB || !useAlpha) ? ((!useRGB) ? ColorTween.ColorTweenMode.Alpha : ColorTween.ColorTweenMode.RGB) : ColorTween.ColorTweenMode.All;
			ColorTween info = new ColorTween
			{
				duration = duration,
				startColor = this.canvasRenderer.GetColor(),
				targetColor = targetColor
			};
			info.AddOnChangedCallback(new UnityAction<Color>(this.canvasRenderer.SetColor));
			info.ignoreTimeScale = ignoreTimeScale;
			info.tweenMode = tweenMode;
			this.m_ColorTweenRunner.StartTween(info);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009410 File Offset: 0x00007610
		private static Color CreateColorFromAlpha(float alpha)
		{
			Color black = Color.black;
			black.a = alpha;
			return black;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000942C File Offset: 0x0000762C
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this.CrossFadeColor(Graphic.CreateColorFromAlpha(alpha), duration, ignoreTimeScale, true, false);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009440 File Offset: 0x00007640
		public void RegisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000945C File Offset: 0x0000765C
		public void UnregisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009478 File Offset: 0x00007678
		public void RegisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009494 File Offset: 0x00007694
		public void UnregisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000094B0 File Offset: 0x000076B0
		public void RegisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000094CC File Offset: 0x000076CC
		public void UnregisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000094E8 File Offset: 0x000076E8
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000094F0 File Offset: 0x000076F0
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040000EF RID: 239
		protected static Material s_DefaultUI = null;

		// Token: 0x040000F0 RID: 240
		protected static Texture2D s_WhiteTexture = null;

		// Token: 0x040000F1 RID: 241
		[FormerlySerializedAs("m_Mat")]
		[SerializeField]
		protected Material m_Material;

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		private bool m_RaycastTarget = true;

		// Token: 0x040000F4 RID: 244
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040000F5 RID: 245
		[NonSerialized]
		private CanvasRenderer m_CanvasRender;

		// Token: 0x040000F6 RID: 246
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x040000F7 RID: 247
		[NonSerialized]
		private bool m_VertsDirty;

		// Token: 0x040000F8 RID: 248
		[NonSerialized]
		private bool m_MaterialDirty;

		// Token: 0x040000F9 RID: 249
		[NonSerialized]
		protected UnityAction m_OnDirtyLayoutCallback;

		// Token: 0x040000FA RID: 250
		[NonSerialized]
		protected UnityAction m_OnDirtyVertsCallback;

		// Token: 0x040000FB RID: 251
		[NonSerialized]
		protected UnityAction m_OnDirtyMaterialCallback;

		// Token: 0x040000FC RID: 252
		[NonSerialized]
		protected static Mesh s_Mesh;

		// Token: 0x040000FD RID: 253
		[NonSerialized]
		private static readonly VertexHelper s_VertexHelper = new VertexHelper();

		// Token: 0x040000FE RID: 254
		[NonSerialized]
		private readonly TweenRunner<ColorTween> m_ColorTweenRunner;
	}
}
