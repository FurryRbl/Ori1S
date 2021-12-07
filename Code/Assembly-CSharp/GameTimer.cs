using System;
using System.Collections;
using System.Text;
using Game;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class GameTimer : SaveSerialize
{
	// Token: 0x06000E07 RID: 3591 RVA: 0x0004163F File Offset: 0x0003F83F
	public override void Awake()
	{
		GameTimer.Instance = this;
		base.Awake();
		Events.Scheduler.OnPlayerDeath.Add(new Action(this.OnDeath));
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x00041668 File Offset: 0x0003F868
	private void OnDeath()
	{
		SaveSceneManager.Master.Save(Game.Checkpoint.SaveGameData.Master, this);
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0004167F File Offset: 0x0003F87F
	public override void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnPlayerDeath.Remove(new Action(this.OnDeath));
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x000416A4 File Offset: 0x0003F8A4
	public void FixedUpdate()
	{
		if (UI.MainMenuVisible)
		{
			return;
		}
		if (GameStateMachine.Instance.IsInExtendedTitleScreen())
		{
			return;
		}
		if (GameController.Instance.IsLoadingGame)
		{
			return;
		}
		this.CurrentTime += Time.deltaTime;
		this.m_waitTillSave -= Time.deltaTime;
		if (this.m_waitTillSave <= 0f)
		{
			this.m_builder.Length = 0;
			string text = this.Seconds.ToString();
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			string text2 = this.Minutes.ToString();
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			this.m_builder.Append(this.Hours.ToString());
			this.m_builder.Append(":");
			this.m_builder.Append(text2);
			this.m_builder.Append(":");
			this.m_builder.Append(text);
			this.m_waitTillSave = 1f;
		}
		this.m_sendTelemetryTimer -= Time.fixedDeltaTime;
		if (this.m_sendTelemetryTimer < 0f)
		{
			base.StartCoroutine(this.SendTelemetry());
			this.m_sendTelemetryTimer = 60f;
		}
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00041808 File Offset: 0x0003FA08
	private IEnumerator SendTelemetry()
	{
		Telemetry.InterMediaMinutesPlayed.SendData(this.TotalMinutes);
		yield return new WaitForSeconds(1f);
		Telemetry.TimeHeroStat.SendData(this.m_builder.ToString());
		yield break;
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00041824 File Offset: 0x0003FA24
	public string DisplayTimeAsString
	{
		get
		{
			int num = Mathf.FloorToInt(this.CurrentTime / 60f);
			int num2 = Mathf.FloorToInt((float)num / 60f);
			if (num2 > 0)
			{
				this.m_displayText = string.Format("{0}h {1}", this.Hours.ToString(), this.Minutes.ToString());
			}
			else
			{
				this.m_displayText = string.Format("{0}", this.Minutes.ToString());
			}
			return this.m_displayText;
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000418AE File Offset: 0x0003FAAE
	public int Hours
	{
		get
		{
			return Mathf.FloorToInt((float)this.TotalMinutes / 60f);
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000418C4 File Offset: 0x0003FAC4
	public string HoursAsString
	{
		get
		{
			return string.Format("{0}", this.Hours.ToString());
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06000E0F RID: 3599 RVA: 0x000418E9 File Offset: 0x0003FAE9
	public int Minutes
	{
		get
		{
			return this.TotalMinutes - this.Hours * 60;
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06000E10 RID: 3600 RVA: 0x000418FC File Offset: 0x0003FAFC
	public string MinutesAsString
	{
		get
		{
			return string.Format("{0}", this.Minutes.ToString());
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00041921 File Offset: 0x0003FB21
	public int TotalMinutes
	{
		get
		{
			return Mathf.FloorToInt(this.CurrentTime / 60f);
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00041934 File Offset: 0x0003FB34
	public int Seconds
	{
		get
		{
			return Mathf.FloorToInt(this.CurrentTime % 60f);
		}
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x00041947 File Offset: 0x0003FB47
	public void Reset()
	{
		this.CurrentTime = 0f;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00041954 File Offset: 0x0003FB54
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.CurrentTime);
	}

	// Token: 0x04000B61 RID: 2913
	public static GameTimer Instance;

	// Token: 0x04000B62 RID: 2914
	public float CurrentTime;

	// Token: 0x04000B63 RID: 2915
	private float m_waitTillSave = 1f;

	// Token: 0x04000B64 RID: 2916
	private StringBuilder m_builder = new StringBuilder();

	// Token: 0x04000B65 RID: 2917
	private float m_sendTelemetryTimer = 60f;

	// Token: 0x04000B66 RID: 2918
	private string m_displayText;
}
