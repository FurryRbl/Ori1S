using System;
using Game;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class Letterbox : MonoBehaviour
{
	// Token: 0x06001150 RID: 4432 RVA: 0x0004F722 File Offset: 0x0004D922
	public void Awake()
	{
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0004F755 File Offset: 0x0004D955
	public void OnDestroy()
	{
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0004F788 File Offset: 0x0004D988
	public void OnRestoreCheckpoint()
	{
		Letterbox.ShowLetterboxes = false;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0004F790 File Offset: 0x0004D990
	public void OnGameReset()
	{
		Letterbox.ShowLetterboxes = false;
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0004F798 File Offset: 0x0004D998
	private void Start()
	{
		Letterbox.Instance = this;
	}

	// Token: 0x04000EED RID: 3821
	public static bool ShowLetterboxes;

	// Token: 0x04000EEE RID: 3822
	public static AnimationCurve AnimationCurve;

	// Token: 0x04000EEF RID: 3823
	public static Letterbox Instance;
}
