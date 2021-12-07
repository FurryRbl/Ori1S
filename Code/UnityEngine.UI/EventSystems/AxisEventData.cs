using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001F RID: 31
	public class AxisEventData : BaseEventData
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003228 File Offset: 0x00001428
		public AxisEventData(EventSystem eventSystem) : base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000324C File Offset: 0x0000144C
		public Vector2 moveVector { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003258 File Offset: 0x00001458
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003260 File Offset: 0x00001460
		public MoveDirection moveDir { get; set; }
	}
}
