using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000022 RID: 34
	public class PointerEventData : BaseEventData
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000032D4 File Offset: 0x000014D4
		public PointerEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003354 File Offset: 0x00001554
		// (set) Token: 0x06000097 RID: 151 RVA: 0x0000335C File Offset: 0x0000155C
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003370 File Offset: 0x00001570
		public GameObject lastPress { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000337C File Offset: 0x0000157C
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003384 File Offset: 0x00001584
		public GameObject rawPointerPress { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003390 File Offset: 0x00001590
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003398 File Offset: 0x00001598
		public GameObject pointerDrag { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000033A4 File Offset: 0x000015A4
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000033AC File Offset: 0x000015AC
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000033B8 File Offset: 0x000015B8
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000033C0 File Offset: 0x000015C0
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000033CC File Offset: 0x000015CC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000033D4 File Offset: 0x000015D4
		public bool eligibleForClick { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000033E0 File Offset: 0x000015E0
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000033E8 File Offset: 0x000015E8
		public int pointerId { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000033FC File Offset: 0x000015FC
		public Vector2 position { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003408 File Offset: 0x00001608
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003410 File Offset: 0x00001610
		public Vector2 delta { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000341C File Offset: 0x0000161C
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003424 File Offset: 0x00001624
		public Vector2 pressPosition { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003430 File Offset: 0x00001630
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003438 File Offset: 0x00001638
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003444 File Offset: 0x00001644
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000344C File Offset: 0x0000164C
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003460 File Offset: 0x00001660
		public float clickTime { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000346C File Offset: 0x0000166C
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003474 File Offset: 0x00001674
		public int clickCount { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003480 File Offset: 0x00001680
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003488 File Offset: 0x00001688
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003494 File Offset: 0x00001694
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000349C File Offset: 0x0000169C
		public bool useDragThreshold { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000034A8 File Offset: 0x000016A8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000034B0 File Offset: 0x000016B0
		public bool dragging { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000034BC File Offset: 0x000016BC
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000034C4 File Offset: 0x000016C4
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x060000BC RID: 188 RVA: 0x000034D0 File Offset: 0x000016D0
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000034F4 File Offset: 0x000016F4
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003518 File Offset: 0x00001718
		public Camera enterEventCamera
		{
			get
			{
				return (!(this.pointerCurrentRaycast.module == null)) ? this.pointerCurrentRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003558 File Offset: 0x00001758
		public Camera pressEventCamera
		{
			get
			{
				return (!(this.pointerPressRaycast.module == null)) ? this.pointerPressRaycast.module.eventCamera : null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000035A0 File Offset: 0x000017A0
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000035C8 File Offset: 0x000017C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position);
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta);
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick);
			stringBuilder.AppendLine("<b>pointerEnter</b>: " + this.pointerEnter);
			stringBuilder.AppendLine("<b>pointerPress</b>: " + this.pointerPress);
			stringBuilder.AppendLine("<b>lastPointerPress</b>: " + this.lastPress);
			stringBuilder.AppendLine("<b>pointerDrag</b>: " + this.pointerDrag);
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold);
			stringBuilder.AppendLine("<b>Current Rayast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Rayast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0400004D RID: 77
		private GameObject m_PointerPress;

		// Token: 0x0400004E RID: 78
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x02000023 RID: 35
		public enum InputButton
		{
			// Token: 0x04000063 RID: 99
			Left,
			// Token: 0x04000064 RID: 100
			Right,
			// Token: 0x04000065 RID: 101
			Middle
		}

		// Token: 0x02000024 RID: 36
		public enum FramePressState
		{
			// Token: 0x04000067 RID: 103
			Pressed,
			// Token: 0x04000068 RID: 104
			Released,
			// Token: 0x04000069 RID: 105
			PressedAndReleased,
			// Token: 0x0400006A RID: 106
			NotChanged
		}
	}
}
