using System;
using UnityEngine;

// Token: 0x02000738 RID: 1848
public class LeaderboardRowUI : MonoBehaviour
{
	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000BA544 File Offset: 0x000B8744
	public Rect Bounds
	{
		get
		{
			return new Rect(base.transform.position.x, base.transform.position.y - 0.5f, 9f, 0.5f);
		}
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x000BA58C File Offset: 0x000B878C
	public void SetContent(LeaderboardData.Entry entry)
	{
		this.Rank.SetMessage(new MessageDescriptor(entry.Rank.ToString()));
		this.Tag.SetMessage(new MessageDescriptor(entry.UserHandle));
		if (this.Deaths)
		{
			this.Deaths.SetMessage(new MessageDescriptor(entry.DeathCount.ToString()));
		}
		if (this.Time)
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)entry.Time);
			string message = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			this.Time.SetMessage(new MessageDescriptor(message));
		}
		if (this.Completion)
		{
			this.Completion.SetMessage(new MessageDescriptor(entry.CompletionPercentage + "%"));
		}
	}

	// Token: 0x06002B69 RID: 11113 RVA: 0x000BA68D File Offset: 0x000B888D
	public void Awake()
	{
		this.Hide();
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x000BA695 File Offset: 0x000B8895
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x000BA6A3 File Offset: 0x000B88A3
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x000BA6B1 File Offset: 0x000B88B1
	public void Highlight()
	{
		this.Glow.AnimatorDriver.ContinueForward();
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x000BA6C3 File Offset: 0x000B88C3
	public void Unhighlight()
	{
		this.Glow.AnimatorDriver.ContinueBackwards();
	}

	// Token: 0x04002736 RID: 10038
	public MessageBox Rank;

	// Token: 0x04002737 RID: 10039
	public MessageBox Tag;

	// Token: 0x04002738 RID: 10040
	public MessageBox Deaths;

	// Token: 0x04002739 RID: 10041
	public MessageBox Time;

	// Token: 0x0400273A RID: 10042
	public MessageBox Completion;

	// Token: 0x0400273B RID: 10043
	public BaseAnimator Glow;
}
