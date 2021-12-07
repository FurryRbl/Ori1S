using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Game;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class SteamTelemetry : MonoBehaviour
{
	// Token: 0x0600140C RID: 5132 RVA: 0x0005B3E4 File Offset: 0x000595E4
	public void Awake()
	{
		SteamTelemetry.URL = SteamTelemetry.BaseUrL + SteamTelemetry.SessionGUID;
		SteamTelemetry.m_headers = new Dictionary<string, string>();
		SteamTelemetry.m_headers.Add("Content-Type", "application/ms-maelstrom.v3+json;type=eventbatch;charset=utf-8");
		SteamTelemetry.m_epochTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		SteamTelemetry.Instance = this;
		this.Send(TelemetryEvent.Test, string.Empty);
	}

	// Token: 0x0600140D RID: 5133 RVA: 0x0005B465 File Offset: 0x00059665
	public void Update()
	{
		SteamTelemetry.m_epochTime += (double)Time.deltaTime;
	}

	// Token: 0x0600140E RID: 5134 RVA: 0x0005B478 File Offset: 0x00059678
	public void Send(TelemetryEvent eventId, string body)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(this.GetJson((int)eventId, body));
		base.StartCoroutine(this.SendRoutine(bytes));
	}

	// Token: 0x0600140F RID: 5135 RVA: 0x0005B4A8 File Offset: 0x000596A8
	private IEnumerator SendRoutine(byte[] data)
	{
		WWW www = new WWW(SteamTelemetry.URL, data, SteamTelemetry.m_headers);
		yield return www;
		if (www.error != null)
		{
		}
		yield break;
	}

	// Token: 0x06001410 RID: 5136 RVA: 0x0005B4CC File Offset: 0x000596CC
	private string GetJson(int eventId, string body)
	{
		string text = string.Empty;
		text += "{ \"events\": [ { \"typeId\": ";
		text += eventId.ToString();
		text += ", \"sequenceId\": ";
		text += SteamTelemetry.sequenceId.ToString();
		text += ", \"timestampUtc\": \"";
		DateTime utcNow = DateTime.UtcNow;
		text = text + utcNow.Year + "-";
		text += ((utcNow.Month <= 9) ? ("0" + utcNow.Month) : utcNow.Month.ToString());
		text += "-";
		text += ((utcNow.Day <= 9) ? ("0" + utcNow.Day) : utcNow.Day.ToString());
		text += "T";
		text += ((utcNow.Hour <= 9) ? ("0" + utcNow.Hour) : utcNow.Hour.ToString());
		text += ":";
		text += ((utcNow.Minute <= 9) ? ("0" + utcNow.Minute) : utcNow.Minute.ToString());
		text += ":";
		text += ((utcNow.Second <= 9) ? ("0" + utcNow.Second) : utcNow.Second.ToString());
		text += "Z";
		text += "\", \"body\": { ";
		text += body;
		text += " } } ] }";
		SteamTelemetry.sequenceId++;
		return text;
	}

	// Token: 0x040011A2 RID: 4514
	public static SteamTelemetry Instance = null;

	// Token: 0x040011A3 RID: 4515
	public static Guid SessionGUID = Guid.NewGuid();

	// Token: 0x040011A4 RID: 4516
	public static string BaseUrL = "https://oridefinitiveprod.rtep.msgamestudios.com/tenants/oridefinitiveprod/routes/steam/";

	// Token: 0x040011A5 RID: 4517
	public static string URL = string.Empty;

	// Token: 0x040011A6 RID: 4518
	private static int sequenceId = 1;

	// Token: 0x040011A7 RID: 4519
	private static Dictionary<string, string> m_headers = new Dictionary<string, string>();

	// Token: 0x040011A8 RID: 4520
	private static double m_epochTime = 0.0;

	// Token: 0x02000251 RID: 593
	public class StringData : SteamTelemetry.Data
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x0005B6E7 File Offset: 0x000598E7
		public StringData(string stringValue)
		{
			this.StringValue = stringValue;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005B6F6 File Offset: 0x000598F6
		public new string ToString()
		{
			return string.Format("{0}, \"stringValue\": \"{1}\"", base.ToString(), this.StringValue);
		}

		// Token: 0x040011A9 RID: 4521
		public string StringValue;
	}

	// Token: 0x02000440 RID: 1088
	public class Data
	{
		// Token: 0x06001E55 RID: 7765 RVA: 0x00085C78 File Offset: 0x00083E78
		public new string ToString()
		{
			string text = "uninitialized";
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			string text2 = "uninitialized";
			float num4 = -1f;
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			float num8 = -1f;
			float num9 = -1f;
			float num10 = -1f;
			int num11 = -1;
			string text3 = "unknown";
			if (Steamworks.Instance)
			{
				text = Steamworks.Instance.SteamId.ToString();
			}
			if (GameStateMachine.Instance != null && !GameStateMachine.Instance.IsInExtendedTitleScreen())
			{
				text2 = SaveSlotsManager.CurrentSaveSlot.Identifier.ToString();
				if (SeinDeathCounter.Instance != null)
				{
					num2 = SeinDeathCounter.Count;
				}
				if (GameTimer.Instance != null)
				{
					num = GameTimer.Instance.TotalMinutes;
				}
				if (GameWorld.Instance != null)
				{
					num3 = GameWorld.Instance.CompletionPercentage;
				}
				if (Characters.Sein)
				{
					num4 = Characters.Sein.Energy.Max;
					num5 = Characters.Sein.Mortality.Health.MaxHealth;
					num6 = Characters.Sein.Level.Current;
					num7 = Characters.Sein.Level.Experience;
					text3 = Characters.Sein.Position.ToString();
				}
				if (SkillTreeManager.Instance)
				{
					num8 = SkillTreeManager.Instance.CombatLane.Index;
					num9 = SkillTreeManager.Instance.EnergyLane.Index;
					num10 = SkillTreeManager.Instance.UtilityLane.Index;
				}
				if (DifficultyController.Instance)
				{
					num11 = (int)DifficultyController.Instance.Difficulty;
				}
			}
			string text4 = (!PlayerInput.Instance.WasKeyboardUsedLast) ? "0" : "1";
			return string.Format("\"steamId\": \"{1}\", \"minsPlayed\": {3}, \"deaths\": {4}, \"completion\": {5}, \"saveSlotGuid\": \"{6}\", \"epochTime\": {7}, \"keyboard\": {8}, \"maxEnergy\": {9}, \"maxHealth\": {10}, \"level\": {11}, \"experience\": {12}, \"combatLaneIndex\": {13}, \"energyLaneIndex\": {14}, \"utilityLaneIndex\": {15}, \"debug\": {16}, \"difficulty\": {17}, \"seinPosition\": \"{18}\"", new object[]
			{
				SteamTelemetry.SessionGUID,
				text,
				this.EventId,
				num,
				num2,
				num3,
				text2,
				SteamTelemetry.m_epochTime.ToString("0"),
				text4,
				num4,
				num5,
				num6,
				num7,
				num8,
				num9,
				num10,
				(!CheatsHandler.DebugWasEnabled) ? "0" : "1",
				num11,
				text3
			}) + this.ExtraData;
		}

		// Token: 0x04001A18 RID: 6680
		public int Version = 2;

		// Token: 0x04001A19 RID: 6681
		public TelemetryEvent EventId;

		// Token: 0x04001A1A RID: 6682
		public string ExtraData = string.Empty;
	}

	// Token: 0x02000735 RID: 1845
	public class IntData : SteamTelemetry.Data
	{
		// Token: 0x06002B5C RID: 11100 RVA: 0x000BA437 File Offset: 0x000B8637
		public IntData(int intValue)
		{
			this.IntValue = intValue;
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000BA446 File Offset: 0x000B8646
		public new string ToString()
		{
			return string.Format("{0}, \"intValue\": {1}", base.ToString(), this.IntValue);
		}

		// Token: 0x0400272F RID: 10031
		public int IntValue;
	}

	// Token: 0x02000736 RID: 1846
	public class FloatData : SteamTelemetry.Data
	{
		// Token: 0x06002B5E RID: 11102 RVA: 0x000BA463 File Offset: 0x000B8663
		public FloatData(float floatValue)
		{
			this.FloatValue = floatValue;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000BA472 File Offset: 0x000B8672
		public new string ToString()
		{
			return string.Format("{0}, \"floatValue\": {1}", base.ToString(), this.FloatValue.ToString("0.00"));
		}

		// Token: 0x04002730 RID: 10032
		public float FloatValue;
	}
}
