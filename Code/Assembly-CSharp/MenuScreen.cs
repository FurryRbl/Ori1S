using System;
using UnityEngine;

// Token: 0x02000125 RID: 293
public abstract class MenuScreen : MonoBehaviour
{
	// Token: 0x06000BE4 RID: 3044
	public abstract void Show();

	// Token: 0x06000BE5 RID: 3045
	public abstract void Hide();

	// Token: 0x06000BE6 RID: 3046
	public abstract void ShowImmediate();

	// Token: 0x06000BE7 RID: 3047
	public abstract void HideImmediate();
}
