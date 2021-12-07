using System;
using Game;
using UnityEngine;

// Token: 0x020009B9 RID: 2489
public class CatAndMouseRoomTimerController : Suspendable
{
	// Token: 0x17000874 RID: 2164
	// (get) Token: 0x06003649 RID: 13897 RVA: 0x000E3DD4 File Offset: 0x000E1FD4
	public bool Active
	{
		get
		{
			return this.m_active;
		}
	}

	// Token: 0x17000875 RID: 2165
	// (get) Token: 0x0600364A RID: 13898 RVA: 0x000E3DDC File Offset: 0x000E1FDC
	public float TimeNormalized
	{
		get
		{
			return 1f - this.m_time / this.RoomTime;
		}
	}

	// Token: 0x17000876 RID: 2166
	// (get) Token: 0x0600364B RID: 13899 RVA: 0x000E3DF1 File Offset: 0x000E1FF1
	// (set) Token: 0x0600364C RID: 13900 RVA: 0x000E3DF9 File Offset: 0x000E1FF9
	public override bool IsSuspended { get; set; }

	// Token: 0x0600364D RID: 13901 RVA: 0x000E3E02 File Offset: 0x000E2002
	public void Start()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600364E RID: 13902 RVA: 0x000E3E1A File Offset: 0x000E201A
	public new void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x0600364F RID: 13903 RVA: 0x000E3E38 File Offset: 0x000E2038
	public void OnRestoreCheckpoint()
	{
		this.m_active = false;
		this.m_time = 0f;
		this.FeedbackSequence.AnimatorDriver.SetForward();
		this.FeedbackSequence.AnimatorDriver.Stop();
		this.FeedbackSequence.AnimatorDriver.Speed = 1f;
		if (this.KuroAlertSound)
		{
			this.KuroAlertSound.Stop();
		}
		if (this.KuroWingFlapSound)
		{
			this.KuroWingFlapSound.Stop();
		}
		if (this.KuroGettingNearSound)
		{
			this.KuroGettingNearSound.Stop();
		}
		if (this.KuroFlyOffSound)
		{
			this.KuroFlyOffSound.Stop();
		}
	}

	// Token: 0x06003650 RID: 13904 RVA: 0x000E3EF8 File Offset: 0x000E20F8
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float time = this.m_time;
		this.m_time -= Time.fixedDeltaTime;
		if (this.m_active)
		{
			if (this.m_time < this.RoomTime - 1f && time >= this.RoomTime - 1f && this.KuroWingFlapSound)
			{
				this.KuroWingFlapSound.Play();
			}
			if (this.m_time < 4f && time >= 4f && this.KuroGettingNearSound)
			{
				this.KuroGettingNearSound.Play();
			}
			if (this.m_time <= 0f)
			{
				this.m_active = false;
				this.RoomController.SendMessage("Attack");
				if (this.FeedbackSequence)
				{
					this.FeedbackSequence.AnimatorDriver.Speed = 8f;
					this.FeedbackSequence.AnimatorDriver.ContinueBackwards();
				}
			}
		}
		else if (this.m_time <= 0f && time > 0f)
		{
			this.RoomController.SendMessage("KuroAway", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06003651 RID: 13905 RVA: 0x000E403C File Offset: 0x000E223C
	public void Enter()
	{
		this.m_active = true;
		this.m_time = this.RoomTime;
		if (this.KuroAlertSound)
		{
			this.KuroAlertSound.Play();
		}
		if (this.FeedbackSequence)
		{
			this.FeedbackSequence.AnimatorDriver.Speed = 1f;
			this.FeedbackSequence.AnimatorDriver.ContinueForward();
		}
	}

	// Token: 0x06003652 RID: 13906 RVA: 0x000E40AC File Offset: 0x000E22AC
	public void Exit()
	{
		this.m_active = false;
		if (this.KuroFlyOffSound)
		{
			this.KuroFlyOffSound.Play();
		}
		if (this.KuroWingFlapSound && this.KuroWingFlapSound.IsPlaying)
		{
			this.KuroWingFlapSound.StopAndFadeOut(0.3f);
		}
		if (this.KuroGettingNearSound && this.KuroGettingNearSound.IsPlaying)
		{
			this.KuroGettingNearSound.StopAndFadeOut(0.3f);
		}
		if (this.FeedbackSequence)
		{
			this.FeedbackSequence.AnimatorDriver.Speed = 8f;
			this.FeedbackSequence.AnimatorDriver.ContinueBackwards();
		}
		this.RoomController.SendMessage("Escaped");
	}

	// Token: 0x040030EA RID: 12522
	private const float m_kuroWingFlapDelay = 1f;

	// Token: 0x040030EB RID: 12523
	private const float m_kuroGettingNearLength = 4f;

	// Token: 0x040030EC RID: 12524
	public float RoomTime = 5f;

	// Token: 0x040030ED RID: 12525
	public GameObject RoomController;

	// Token: 0x040030EE RID: 12526
	public SoundSource KuroAlertSound;

	// Token: 0x040030EF RID: 12527
	public SoundSource KuroWingFlapSound;

	// Token: 0x040030F0 RID: 12528
	public SoundSource KuroGettingNearSound;

	// Token: 0x040030F1 RID: 12529
	public SoundSource KuroFlyOffSound;

	// Token: 0x040030F2 RID: 12530
	private float m_time;

	// Token: 0x040030F3 RID: 12531
	private bool m_active;

	// Token: 0x040030F4 RID: 12532
	public TimelineSequence FeedbackSequence;
}
