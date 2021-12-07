using System;

// Token: 0x0200067F RID: 1663
public class WriteOutTextBox : BaseAnimator
{
	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x06002864 RID: 10340 RVA: 0x000AF16A File Offset: 0x000AD36A
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x000AF16D File Offset: 0x000AD36D
	public new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x000AF17B File Offset: 0x000AD37B
	public new void Start()
	{
		base.Start();
		this.m_lastLetterIndex = (float)TextBoxExtended.CountLetters(this.MessageBox.TextBox);
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x000AF19C File Offset: 0x000AD39C
	public void OnTextChange()
	{
		this.m_lastLetterIndex = (float)TextBoxExtended.CountLetters(this.MessageBox.TextBox);
		base.AnimatorDriver.Sample();
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x000AF1CB File Offset: 0x000AD3CB
	public new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06002869 RID: 10345 RVA: 0x000AF1D9 File Offset: 0x000AD3D9
	public override void CacheOriginals()
	{
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x000AF1DB File Offset: 0x000AD3DB
	public override void SampleValue(float value, bool forceSample)
	{
		this.SetValue(base.TimeToAnimationCurveTime(value * this.LettersPerSecond));
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x000AF1F1 File Offset: 0x000AD3F1
	public void SetValue(float time)
	{
		this.MessageBox.SetMessageFade(time);
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x000AF1FF File Offset: 0x000AD3FF
	public void Stop()
	{
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x0600286D RID: 10349 RVA: 0x000AF204 File Offset: 0x000AD404
	public override float Duration
	{
		get
		{
			if (this.LettersPerSecond == 0f)
			{
				return 0f;
			}
			return (this.m_lastLetterIndex + this.MessageBox.FadeSpread) / this.LettersPerSecond / this.Speed;
		}
	}

	// Token: 0x0600286E RID: 10350 RVA: 0x000AF247 File Offset: 0x000AD447
	public override void RestoreToOriginalState()
	{
		this.MessageBox.RemoveMessageFade();
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x0600286F RID: 10351 RVA: 0x000AF254 File Offset: 0x000AD454
	public bool AtEnd
	{
		get
		{
			return base.AnimatorDriver.CurrentTime + 0.001f >= this.Duration;
		}
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x000AF27D File Offset: 0x000AD47D
	public void GoToStart()
	{
		base.AnimatorDriver.RestartForward();
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x000AF28A File Offset: 0x000AD48A
	public void GoToEnd()
	{
		base.AnimatorDriver.GoToEnd();
	}

	// Token: 0x040023EF RID: 9199
	public MessageBox MessageBox;

	// Token: 0x040023F0 RID: 9200
	public float LettersPerSecond = 10f;

	// Token: 0x040023F1 RID: 9201
	private float m_lastLetterIndex;
}
