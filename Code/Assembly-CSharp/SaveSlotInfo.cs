using System;
using System.IO;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class SaveSlotInfo
{
	// Token: 0x06000AC2 RID: 2754 RVA: 0x0002ED00 File Offset: 0x0002CF00
	public SaveSlotInfo(SaveSlotInfo clone)
	{
		SaveSlotInfo.CurrentInfoVersion = 13;
		this.AreaName = clone.AreaName;
		this.Completion = clone.Completion;
		this.Health = clone.Health;
		this.MaxHealth = clone.MaxHealth;
		this.Energy = clone.Energy;
		this.MaxEnergy = clone.MaxEnergy;
		this.Hours = clone.Hours;
		this.Minutes = clone.Minutes;
		this.Seconds = clone.Seconds;
		this.Order = clone.Order;
		this.Progression = clone.Progression;
		this.Completed = clone.Completed;
		this.Identifier = clone.Identifier;
		this.DebugOn = clone.DebugOn;
		this.CompletedWithEverything = clone.CompletedWithEverything;
		this.WasKilled = clone.WasKilled;
		this.Difficulty = clone.Difficulty;
		this.LowestDifficulty = clone.LowestDifficulty;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0002EE0B File Offset: 0x0002D00B
	public SaveSlotInfo()
	{
		this.Identifier = new MoonGuid(Guid.NewGuid());
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x0002EE44 File Offset: 0x0002D044
	public void SaveToWriter(BinaryWriter writer)
	{
		writer.Write("OriSave");
		writer.Write(13);
		writer.Write(this.AreaName);
		writer.Write(this.Completion);
		writer.Write(this.Health);
		writer.Write(this.MaxHealth);
		writer.Write(this.Energy);
		writer.Write(this.MaxEnergy);
		writer.Write(this.Hours);
		writer.Write(this.Minutes);
		writer.Write(this.Seconds);
		writer.Write((int)this.Progression);
		writer.Write(this.Completed);
		writer.Write(this.Identifier.ToByteArray());
		writer.Write(CheatsHandler.DebugWasEnabled);
		writer.Write(this.Order);
		writer.Write((int)this.Difficulty);
		writer.Write(this.WasKilled);
		writer.Write(this.CompletedWithEverything);
		writer.Write((int)this.LowestDifficulty);
		writer.Write(this.IsTrialSave);
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0002EF4C File Offset: 0x0002D14C
	public bool LoadFromReader(BinaryReader reader)
	{
		if (reader.ReadString() != "OriSave")
		{
			return false;
		}
		SaveSlotInfo.CurrentInfoVersion = reader.ReadInt32();
		if (SaveSlotInfo.CurrentInfoVersion < 10)
		{
			return false;
		}
		this.AreaName = reader.ReadString();
		this.Completion = reader.ReadInt32();
		this.Health = reader.ReadInt32();
		this.MaxHealth = reader.ReadInt32();
		this.Energy = reader.ReadInt32();
		this.MaxEnergy = reader.ReadInt32();
		this.Hours = reader.ReadInt32();
		this.Minutes = reader.ReadInt32();
		this.Seconds = reader.ReadInt32();
		this.Progression = (WorldProgression)reader.ReadInt32();
		this.Completed = reader.ReadBoolean();
		if (SaveSlotInfo.CurrentInfoVersion >= 5)
		{
			this.Identifier = new MoonGuid(reader.ReadBytes(16));
		}
		else
		{
			this.Identifier = new MoonGuid(Guid.NewGuid());
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 6)
		{
			this.DebugOn = reader.ReadBoolean();
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 7)
		{
			this.Order = reader.ReadInt32();
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 8)
		{
			this.Difficulty = (DifficultyMode)reader.ReadInt32();
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 9)
		{
			this.WasKilled = reader.ReadBoolean();
			this.CompletedWithEverything = reader.ReadBoolean();
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 11)
		{
			this.LowestDifficulty = (DifficultyMode)reader.ReadInt32();
		}
		if (SaveSlotInfo.CurrentInfoVersion >= 12)
		{
			this.IsTrialSave = reader.ReadBoolean();
		}
		return true;
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002F0DA File Offset: 0x0002D2DA
	public int TotalSeconds
	{
		get
		{
			return this.Seconds + 60 * this.Minutes + 3600 * this.Hours;
		}
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0002F0FC File Offset: 0x0002D2FC
	public void FillData()
	{
		if (WorldMapLogic.Instance)
		{
			WorldMapLogic.Instance.UpdateCurrentArea();
			if (World.CurrentArea == null)
			{
				this.AreaName = "mountHoru";
			}
			else
			{
				this.AreaName = World.CurrentArea.Area.AreaIdentifier;
			}
			this.Completion = GameWorld.Instance.CompletionPercentage;
		}
		if (Characters.Sein)
		{
			this.Health = Mathf.RoundToInt(Characters.Sein.Mortality.Health.Amount / 4f);
			this.MaxHealth = Characters.Sein.Mortality.Health.MaxHealth / 4;
			this.Energy = Mathf.RoundToInt(Characters.Sein.Energy.Current);
			this.MaxEnergy = Mathf.RoundToInt(Characters.Sein.Energy.Max);
			this.Hours = GameController.Instance.Timer.Hours;
			this.Minutes = GameController.Instance.Timer.Minutes;
			this.Seconds = GameController.Instance.Timer.Seconds;
			this.Progression = Sein.World.Events.Progression;
		}
		this.Difficulty = DifficultyController.Instance.Difficulty;
		this.LowestDifficulty = DifficultyController.Instance.LowestDifficulty;
		this.IsTrialSave = GameController.Instance.IsTrial;
	}

	// Token: 0x040008D7 RID: 2263
	public const int VERSION = 13;

	// Token: 0x040008D8 RID: 2264
	private const int BACKWARDS_COMPATIBLE_VERSION = 10;

	// Token: 0x040008D9 RID: 2265
	private const string FILE_FORMAT_STRING = "OriSave";

	// Token: 0x040008DA RID: 2266
	public static int CurrentInfoVersion = -1;

	// Token: 0x040008DB RID: 2267
	public string AreaName = string.Empty;

	// Token: 0x040008DC RID: 2268
	public int Completion;

	// Token: 0x040008DD RID: 2269
	public int Health;

	// Token: 0x040008DE RID: 2270
	public int MaxHealth;

	// Token: 0x040008DF RID: 2271
	public int Energy;

	// Token: 0x040008E0 RID: 2272
	public int MaxEnergy;

	// Token: 0x040008E1 RID: 2273
	public int Hours;

	// Token: 0x040008E2 RID: 2274
	public int Minutes;

	// Token: 0x040008E3 RID: 2275
	public int Seconds;

	// Token: 0x040008E4 RID: 2276
	public int Order;

	// Token: 0x040008E5 RID: 2277
	public WorldProgression Progression;

	// Token: 0x040008E6 RID: 2278
	public bool Completed;

	// Token: 0x040008E7 RID: 2279
	public bool WasKilled;

	// Token: 0x040008E8 RID: 2280
	public bool CompletedWithEverything;

	// Token: 0x040008E9 RID: 2281
	public MoonGuid Identifier;

	// Token: 0x040008EA RID: 2282
	public DifficultyMode Difficulty = DifficultyMode.Normal;

	// Token: 0x040008EB RID: 2283
	public DifficultyMode LowestDifficulty = DifficultyMode.Normal;

	// Token: 0x040008EC RID: 2284
	public bool IsTrialSave;

	// Token: 0x040008ED RID: 2285
	public bool DebugOn;
}
