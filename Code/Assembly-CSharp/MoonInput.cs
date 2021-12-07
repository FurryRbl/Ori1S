using System;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class MoonInput
{
	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00047B4B File Offset: 0x00045D4B
	public static bool anyKeyDown
	{
		get
		{
			return Input.anyKeyDown;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00047B52 File Offset: 0x00045D52
	public static bool anyKey
	{
		get
		{
			return Input.anyKey;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00047B59 File Offset: 0x00045D59
	public static Vector3 mousePosition
	{
		get
		{
			return Input.mousePosition;
		}
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x00047B60 File Offset: 0x00045D60
	public static float GetAxis(string axisName)
	{
		return Input.GetAxis(axisName);
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x00047B68 File Offset: 0x00045D68
	public static bool GetMouseButton(int button)
	{
		return Input.GetMouseButton(button);
	}

	// Token: 0x06000F99 RID: 3993 RVA: 0x00047B70 File Offset: 0x00045D70
	public static bool GetButton(string buttonName)
	{
		return Input.GetButton(buttonName);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x00047B78 File Offset: 0x00045D78
	public static bool GetButtonDown(string buttonName)
	{
		return Input.GetButtonDown(buttonName);
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x00047B80 File Offset: 0x00045D80
	public static bool GetKey(KeyCode keyCode)
	{
		return Input.GetKey(keyCode);
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x00047B88 File Offset: 0x00045D88
	public static bool GetKeyDown(string name)
	{
		return Input.GetKeyDown(name);
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x00047B90 File Offset: 0x00045D90
	public static bool GetKeyDown(KeyCode keyCode)
	{
		return Input.GetKeyDown(keyCode);
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x00047B98 File Offset: 0x00045D98
	public static bool GetKeyUp(string name)
	{
		return Input.GetKeyUp(name);
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00047BA0 File Offset: 0x00045DA0
	public static bool GetKeyUp(KeyCode keyCode)
	{
		return Input.GetKeyUp(keyCode);
	}

	// Token: 0x06000FA0 RID: 4000 RVA: 0x00047BA8 File Offset: 0x00045DA8
	public static bool GetMouseButtonUp(int button)
	{
		return Input.GetMouseButtonUp(button);
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x00047BB0 File Offset: 0x00045DB0
	public static bool GetMouseButtonDown(int button)
	{
		return Input.GetMouseButtonDown(button);
	}
}
