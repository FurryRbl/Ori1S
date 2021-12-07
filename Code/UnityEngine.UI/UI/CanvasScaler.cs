using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000086 RID: 134
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[ExecuteInEditMode]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x000161E0 File Offset: 0x000143E0
		protected CanvasScaler()
		{
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001625C File Offset: 0x0001445C
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00016264 File Offset: 0x00014464
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00016270 File Offset: 0x00014470
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00016278 File Offset: 0x00014478
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00016284 File Offset: 0x00014484
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0001628C File Offset: 0x0001448C
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00016298 File Offset: 0x00014498
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x000162A0 File Offset: 0x000144A0
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
				if (this.m_ReferenceResolution.x > -1E-05f && this.m_ReferenceResolution.x < 1E-05f)
				{
					this.m_ReferenceResolution.x = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.x);
				}
				if (this.m_ReferenceResolution.y > -1E-05f && this.m_ReferenceResolution.y < 1E-05f)
				{
					this.m_ReferenceResolution.y = 1E-05f * Mathf.Sign(this.m_ReferenceResolution.y);
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001634C File Offset: 0x0001454C
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00016354 File Offset: 0x00014554
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00016360 File Offset: 0x00014560
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00016368 File Offset: 0x00014568
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00016374 File Offset: 0x00014574
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0001637C File Offset: 0x0001457C
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00016388 File Offset: 0x00014588
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00016390 File Offset: 0x00014590
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001639C File Offset: 0x0001459C
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000163A4 File Offset: 0x000145A4
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000163B0 File Offset: 0x000145B0
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x000163B8 File Offset: 0x000145B8
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000163C4 File Offset: 0x000145C4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000163E0 File Offset: 0x000145E0
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			base.OnDisable();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00016400 File Offset: 0x00014600
		protected virtual void Update()
		{
			this.Handle();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00016408 File Offset: 0x00014608
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				break;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				break;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				break;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00016490 File Offset: 0x00014690
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000164AC File Offset: 0x000146AC
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000164C8 File Offset: 0x000146C8
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			float scaleFactor = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float a = Mathf.Log(vector.x / this.m_ReferenceResolution.x, 2f);
				float b = Mathf.Log(vector.y / this.m_ReferenceResolution.y, 2f);
				float p = Mathf.Lerp(a, b, this.m_MatchWidthOrHeight);
				scaleFactor = Mathf.Pow(2f, p);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				scaleFactor = Mathf.Min(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				scaleFactor = Mathf.Max(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(scaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000165E0 File Offset: 0x000147E0
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = (dpi != 0f) ? dpi : this.m_FallbackScreenDPI;
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001668C File Offset: 0x0001488C
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000166BC File Offset: 0x000148BC
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x0400024A RID: 586
		private const float kLogBase = 2f;

		// Token: 0x0400024B RID: 587
		[SerializeField]
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x0400024C RID: 588
		[SerializeField]
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x0400024D RID: 589
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		[SerializeField]
		protected float m_ScaleFactor = 1f;

		// Token: 0x0400024E RID: 590
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		[SerializeField]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x0400024F RID: 591
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x04000250 RID: 592
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[Range(0f, 1f)]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x04000251 RID: 593
		[SerializeField]
		[Tooltip("The physical unit to specify positions and sizes in.")]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x04000252 RID: 594
		[SerializeField]
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x04000253 RID: 595
		[SerializeField]
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x04000254 RID: 596
		[SerializeField]
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x04000255 RID: 597
		private Canvas m_Canvas;

		// Token: 0x04000256 RID: 598
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x04000257 RID: 599
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x02000087 RID: 135
		public enum ScaleMode
		{
			// Token: 0x04000259 RID: 601
			ConstantPixelSize,
			// Token: 0x0400025A RID: 602
			ScaleWithScreenSize,
			// Token: 0x0400025B RID: 603
			ConstantPhysicalSize
		}

		// Token: 0x02000088 RID: 136
		public enum ScreenMatchMode
		{
			// Token: 0x0400025D RID: 605
			MatchWidthOrHeight,
			// Token: 0x0400025E RID: 606
			Expand,
			// Token: 0x0400025F RID: 607
			Shrink
		}

		// Token: 0x02000089 RID: 137
		public enum Unit
		{
			// Token: 0x04000261 RID: 609
			Centimeters,
			// Token: 0x04000262 RID: 610
			Millimeters,
			// Token: 0x04000263 RID: 611
			Inches,
			// Token: 0x04000264 RID: 612
			Points,
			// Token: 0x04000265 RID: 613
			Picas
		}
	}
}
