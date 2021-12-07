using System;
using Game;

// Token: 0x02000241 RID: 577
public class SeinDeathCounter : SaveSerialize
{
	// Token: 0x06001328 RID: 4904 RVA: 0x00059160 File Offset: 0x00057360
	public static void SendTelemetryData()
	{
		Telemetry.DeathsHeroStat.SendData(SeinDeathCounter.Instance.m_deathCounter);
		SteamTelemetry.Data data = new SteamTelemetry.Data();
		SteamTelemetry.Instance.Send(TelemetryEvent.Death, data.ToString());
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06001329 RID: 4905 RVA: 0x0005919C File Offset: 0x0005739C
	// (set) Token: 0x0600132A RID: 4906 RVA: 0x000591BC File Offset: 0x000573BC
	public static int Count
	{
		get
		{
			if (SeinDeathCounter.Instance == null)
			{
				return 0;
			}
			return SeinDeathCounter.Instance.m_deathCounter;
		}
		set
		{
			if (SeinDeathCounter.Instance != null)
			{
				SeinDeathCounter.Instance.m_deathCounter = value;
				SaveSceneManager.Master.Save(Game.Checkpoint.SaveGameData.Master, SeinDeathCounter.Instance);
			}
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000591FD File Offset: 0x000573FD
	public new void Awake()
	{
		SeinDeathCounter.Instance = this;
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x00059205 File Offset: 0x00057405
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_deathCounter);
	}

	// Token: 0x04001118 RID: 4376
	private int m_deathCounter;

	// Token: 0x04001119 RID: 4377
	public static SeinDeathCounter Instance;
}
