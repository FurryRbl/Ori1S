using System;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public static class EditorInput
{
	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000C2B2C File Offset: 0x000C0D2C
	public static bool LeftMouseDown
	{
		get
		{
			return Event.current.type == EventType.MouseDown && Event.current.button == 0;
		}
	}

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x06002D91 RID: 11665 RVA: 0x000C2B58 File Offset: 0x000C0D58
	public static bool LeftMouseUp
	{
		get
		{
			return Event.current.type == EventType.MouseUp && Event.current.button == 0;
		}
	}

	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000C2B88 File Offset: 0x000C0D88
	public static bool LeftMouseDrag
	{
		get
		{
			return Event.current.type == EventType.MouseDrag && Event.current.button == 0;
		}
	}

	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x06002D93 RID: 11667 RVA: 0x000C2BB8 File Offset: 0x000C0DB8
	public static bool RightMouseDown
	{
		get
		{
			return Event.current.type == EventType.MouseDown && Event.current.button == 1;
		}
	}

	// Token: 0x1700074E RID: 1870
	// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000C2BE4 File Offset: 0x000C0DE4
	public static bool RightMouseUp
	{
		get
		{
			return Event.current.type == EventType.MouseUp && Event.current.button == 1;
		}
	}

	// Token: 0x1700074F RID: 1871
	// (get) Token: 0x06002D95 RID: 11669 RVA: 0x000C2C14 File Offset: 0x000C0E14
	public static bool RightMouseDrag
	{
		get
		{
			return Event.current.type == EventType.MouseDrag && Event.current.button == 1;
		}
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x000C2C44 File Offset: 0x000C0E44
	public static bool KeyDown(KeyCode keyCode)
	{
		return Event.current.type == EventType.KeyDown && Event.current.keyCode == keyCode;
	}

	// Token: 0x17000750 RID: 1872
	// (get) Token: 0x06002D97 RID: 11671 RVA: 0x000C2C71 File Offset: 0x000C0E71
	public static bool AltDown
	{
		get
		{
			return Event.current.alt;
		}
	}

	// Token: 0x17000751 RID: 1873
	// (get) Token: 0x06002D98 RID: 11672 RVA: 0x000C2C7D File Offset: 0x000C0E7D
	public static bool ShiftDown
	{
		get
		{
			return Event.current.shift;
		}
	}

	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x06002D99 RID: 11673 RVA: 0x000C2C89 File Offset: 0x000C0E89
	public static bool ControlDown
	{
		get
		{
			return Event.current.control;
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x06002D9A RID: 11674 RVA: 0x000C2C95 File Offset: 0x000C0E95
	public static Vector2 mousePosition
	{
		get
		{
			return Event.current.mousePosition;
		}
	}

	// Token: 0x17000754 RID: 1876
	// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000C2CA4 File Offset: 0x000C0EA4
	public static bool MiddleMouseDown
	{
		get
		{
			return Event.current.type == EventType.MouseDown && Event.current.button == 2;
		}
	}

	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x06002D9C RID: 11676 RVA: 0x000C2CD0 File Offset: 0x000C0ED0
	public static bool DeletePressed
	{
		get
		{
			return Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Delete;
		}
	}

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x06002D9D RID: 11677 RVA: 0x000C2CFE File Offset: 0x000C0EFE
	public static bool OnLeftMouseDoubleClickDown
	{
		get
		{
			return EditorInput.LeftMouseDown && Event.current.clickCount == 2;
		}
	}
}
