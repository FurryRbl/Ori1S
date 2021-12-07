using System;

// Token: 0x02000242 RID: 578
public class LeaderboardEntryData
{
	// Token: 0x0600132D RID: 4909 RVA: 0x00059214 File Offset: 0x00057414
	public LeaderboardEntryData(int time, int deaths, int completion)
	{
		if ((long)time > this.m_time.MaxValue)
		{
			time = (int)this.m_time.MaxValue;
		}
		if ((long)deaths > this.m_deathCount.MaxValue)
		{
			deaths = (int)this.m_deathCount.MaxValue;
		}
		if ((long)completion > this.m_incompletionPercentage.MaxValue)
		{
			completion = (int)this.m_incompletionPercentage.MaxValue;
		}
		this.Time = time;
		this.DeathCount = deaths;
		this.CompletionPercentage = completion;
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000592C4 File Offset: 0x000574C4
	public LeaderboardEntryData()
	{
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x0600132F RID: 4911 RVA: 0x000592FD File Offset: 0x000574FD
	// (set) Token: 0x06001330 RID: 4912 RVA: 0x0005930B File Offset: 0x0005750B
	public int Time
	{
		get
		{
			return (int)this.m_time.Value;
		}
		set
		{
			this.m_time.Value = (long)value;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06001331 RID: 4913 RVA: 0x0005931A File Offset: 0x0005751A
	// (set) Token: 0x06001332 RID: 4914 RVA: 0x00059328 File Offset: 0x00057528
	public int DeathCount
	{
		get
		{
			return (int)this.m_deathCount.Value;
		}
		set
		{
			this.m_deathCount.Value = (long)value;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06001333 RID: 4915 RVA: 0x00059337 File Offset: 0x00057537
	// (set) Token: 0x06001334 RID: 4916 RVA: 0x00059348 File Offset: 0x00057548
	public int CompletionPercentage
	{
		get
		{
			return 100 - (int)this.m_incompletionPercentage.Value;
		}
		set
		{
			this.m_incompletionPercentage.Value = (long)(100 - value);
		}
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x0005935C File Offset: 0x0005755C
	public long EncodeExplorer()
	{
		long result = 0L;
		this.m_incompletionPercentage.Encode(ref result);
		this.m_time.Encode(ref result);
		this.m_deathCount.Encode(ref result);
		return result;
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x00059394 File Offset: 0x00057594
	public long DecodeExplorer(long data)
	{
		this.m_deathCount.Decode(ref data);
		this.m_time.Decode(ref data);
		this.m_incompletionPercentage.Decode(ref data);
		return data;
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000593CC File Offset: 0x000575CC
	public long EncodeSpeedRunner()
	{
		long result = 0L;
		this.m_time.Encode(ref result);
		this.m_incompletionPercentage.Encode(ref result);
		this.m_deathCount.Encode(ref result);
		return result;
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x00059404 File Offset: 0x00057604
	public void DecodeSpeedRunner(long data)
	{
		this.m_deathCount.Decode(ref data);
		this.m_incompletionPercentage.Decode(ref data);
		this.m_time.Decode(ref data);
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x00059430 File Offset: 0x00057630
	public long EncodeSurvivor()
	{
		long result = 0L;
		this.m_deathCount.Encode(ref result);
		this.m_time.Encode(ref result);
		this.m_incompletionPercentage.Encode(ref result);
		return result;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x00059468 File Offset: 0x00057668
	public void DecodeSurvivor(long data)
	{
		this.m_incompletionPercentage.Decode(ref data);
		this.m_time.Decode(ref data);
		this.m_deathCount.Decode(ref data);
	}

	// Token: 0x0400111A RID: 4378
	public const int DeathWeight = 60;

	// Token: 0x0400111B RID: 4379
	public const int MaxCompletion = 100;

	// Token: 0x0400111C RID: 4380
	public const int CompletionWeight = 1080;

	// Token: 0x0400111D RID: 4381
	private readonly ExtractedIntFromInt64 m_time = new ExtractedIntFromInt64(19);

	// Token: 0x0400111E RID: 4382
	private readonly ExtractedIntFromInt64 m_deathCount = new ExtractedIntFromInt64(13);

	// Token: 0x0400111F RID: 4383
	private readonly ExtractedIntFromInt64 m_incompletionPercentage = new ExtractedIntFromInt64(7);
}
