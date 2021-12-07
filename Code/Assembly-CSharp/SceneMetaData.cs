using System;
using System.Collections.Generic;
using Game;
using Sein.World;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class SceneMetaData : ScriptableObject
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000645 RID: 1605 RVA: 0x000187F6 File Offset: 0x000169F6
	public string SceneName
	{
		get
		{
			return base.name;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000646 RID: 1606 RVA: 0x00018800 File Offset: 0x00016A00
	// (set) Token: 0x06000647 RID: 1607 RVA: 0x00018869 File Offset: 0x00016A69
	public string SceneGuid
	{
		get
		{
			return string.Format("{0:x}{1:x}{2:x}{3:x}", new object[]
			{
				this.SceneMoonGuid.A,
				this.SceneMoonGuid.B,
				this.SceneMoonGuid.C,
				this.SceneMoonGuid.D
			});
		}
		set
		{
			this.SceneMoonGuid = new MoonGuid(new Guid(value));
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001887C File Offset: 0x00016A7C
	public Rect SceneBounds
	{
		get
		{
			if (this.SceneBoundaries.Count == 0)
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
			return Utility.CombineRects(this.SceneBoundaries);
		}
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x000188B4 File Offset: 0x00016AB4
	public bool AnyPaddingBoundaryOverlaps(Rect rect)
	{
		foreach (Rect rect2 in this.ScenePaddingBoundaries)
		{
			if (rect2.Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00018920 File Offset: 0x00016B20
	public bool IsInsideSceneBounds(Vector3 position)
	{
		int count = this.SceneBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneBoundaries[i].Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00018968 File Offset: 0x00016B68
	public bool IsInsideSceneBounds(Rect rect)
	{
		int count = this.SceneBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x000189B0 File Offset: 0x00016BB0
	public bool IsInsideSceneLoadingZone(Rect rect)
	{
		int count = this.SceneLoadingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneLoadingBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x000189F8 File Offset: 0x00016BF8
	public bool IsInsideSceneLoadingZone(Vector2 position)
	{
		int count = this.SceneLoadingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.SceneLoadingBoundaries[i].Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00018A40 File Offset: 0x00016C40
	public bool IsInsideScenePaddingBounds(Vector3 position)
	{
		int count = this.ScenePaddingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ScenePaddingBoundaries[i].Contains(position))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00018A88 File Offset: 0x00016C88
	public bool IsInsideScenePaddingBounds(Rect rect)
	{
		int count = this.ScenePaddingBoundaries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ScenePaddingBoundaries[i].Overlaps(rect))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000650 RID: 1616 RVA: 0x00018AD0 File Offset: 0x00016CD0
	public string SceneTexturePath
	{
		get
		{
			return string.Empty;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000651 RID: 1617 RVA: 0x00018AD7 File Offset: 0x00016CD7
	public Texture SceneTexture
	{
		get
		{
			return null;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000652 RID: 1618 RVA: 0x00018ADA File Offset: 0x00016CDA
	public bool CanBeLoaded
	{
		get
		{
			return !(this.LoadingCondition != null) || this.LoadingCondition.Validate(null);
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00018AFC File Offset: 0x00016CFC
	public static void CopySceneMetaData(SceneMetaData from, SceneMetaData to)
	{
		to.RootPosition = from.RootPosition;
		to.SceneBoundaries = from.SceneBoundaries;
		to.SceneGuid = from.SceneGuid;
		to.SceneLoadingBoundaries = from.SceneLoadingBoundaries;
		to.SeinPlaceholderPosition = from.SeinPlaceholderPosition;
		to.ReverseLoadingBoundaries = from.ReverseLoadingBoundaries;
		to.ScenePaddingBoundaries = from.ScenePaddingBoundaries;
	}

	// Token: 0x040004D0 RID: 1232
	public const string MetaDataFolderName = "metaData";

	// Token: 0x040004D1 RID: 1233
	public const string ScreenshotsFolderName = "screenshots";

	// Token: 0x040004D2 RID: 1234
	public const string ScenesFolderName = "scenes";

	// Token: 0x040004D3 RID: 1235
	public SceneMetaData.SeinInitialValues InitialValues;

	// Token: 0x040004D4 RID: 1236
	public List<SceneMetaData.Teleporter> Teleporters = new List<SceneMetaData.Teleporter>();

	// Token: 0x040004D5 RID: 1237
	public bool Exclude;

	// Token: 0x040004D6 RID: 1238
	public MoonGuid SceneMoonGuid;

	// Token: 0x040004D7 RID: 1239
	public List<SceneMetaData> IncludedScenes = new List<SceneMetaData>();

	// Token: 0x040004D8 RID: 1240
	public bool DependantScene;

	// Token: 0x040004D9 RID: 1241
	public SceneType SceneType;

	// Token: 0x040004DA RID: 1242
	public float EstimatedPlayDuration;

	// Token: 0x040004DB RID: 1243
	public List<Rect> SceneLoadingBoundaries = new List<Rect>();

	// Token: 0x040004DC RID: 1244
	public List<Rect> SceneBoundaries = new List<Rect>();

	// Token: 0x040004DD RID: 1245
	public List<Rect> ScenePaddingBoundaries = new List<Rect>();

	// Token: 0x040004DE RID: 1246
	public List<float> ScenePaddingWideScreenExpansion = new List<float>();

	// Token: 0x040004DF RID: 1247
	public List<SceneMetaData.ReverseSceneLoadingZoneInfo> ReverseLoadingBoundaries = new List<SceneMetaData.ReverseSceneLoadingZoneInfo>();

	// Token: 0x040004E0 RID: 1248
	public Condition LoadingCondition;

	// Token: 0x040004E1 RID: 1249
	public Vector3 SeinPlaceholderPosition;

	// Token: 0x040004E2 RID: 1250
	public Vector3 RootPosition = new Vector3(0f, 0f, 0f);

	// Token: 0x040004E3 RID: 1251
	public string Notes;

	// Token: 0x040004E4 RID: 1252
	public List<SceneMetaData> ConnectedScenes = new List<SceneMetaData>();

	// Token: 0x040004E5 RID: 1253
	public bool ExcludeIcons;

	// Token: 0x040004E6 RID: 1254
	public List<SceneMetaData.WorldMapIcon> Icons = new List<SceneMetaData.WorldMapIcon>();

	// Token: 0x040004E7 RID: 1255
	public bool ShouldRegenerateAllGUIDs;

	// Token: 0x040004E8 RID: 1256
	public List<Vector3> FPSTestPosition = new List<Vector3>();

	// Token: 0x040004E9 RID: 1257
	public List<ScreenshotIcon> ScreenshotIcons = new List<ScreenshotIcon>();

	// Token: 0x0200071A RID: 1818
	[Serializable]
	public class Teleporter
	{
		// Token: 0x06002B0F RID: 11023 RVA: 0x000B8358 File Offset: 0x000B6558
		public Teleporter(SceneTeleporter teleporter)
		{
			this.Identifier = teleporter.Identifier;
			this.SceneLocalPosition = teleporter.transform.position - teleporter.transform.root.position;
		}

		// Token: 0x0400265F RID: 9823
		public string Identifier;

		// Token: 0x04002660 RID: 9824
		public Vector3 SceneLocalPosition;
	}

	// Token: 0x0200071C RID: 1820
	[Serializable]
	public class SeinInitialValues
	{
		// Token: 0x06002B14 RID: 11028 RVA: 0x000B842C File Offset: 0x000B662C
		public void ApplyInitialValues()
		{
			SeinCharacter sein = Characters.Sein;
			if (!sein)
			{
				return;
			}
			Sein.World.Events.WaterPurified = this.World.WaterCleansed;
			Sein.World.Events.WindRestored = this.World.WindReleased;
			if (sein.Level)
			{
				sein.Level.Current = this.Level;
			}
			if (sein.Mortality.Health)
			{
				sein.Mortality.Health.MaxHealth = this.Hearts * 4;
				sein.Mortality.Health.SetAmount((float)sein.Mortality.Health.MaxHealth);
			}
			if (sein.Energy)
			{
				sein.Energy.Max = (float)this.Energy;
				sein.Energy.SetCurrent((float)this.Energy);
			}
			if (sein.PlayerAbilities)
			{
				PlayerAbilities playerAbilities = sein.PlayerAbilities;
				playerAbilities.SpiritFlame.HasAbility = this.Abilities.SpiritFlame;
				playerAbilities.WallJump.HasAbility = this.Abilities.WallJump;
				playerAbilities.ChargeFlame.HasAbility = this.Abilities.ChargeFlame;
				playerAbilities.DoubleJump.HasAbility = this.Abilities.DoubleJump;
				playerAbilities.Bash.HasAbility = this.Abilities.Bash;
				playerAbilities.Stomp.HasAbility = this.Abilities.Stomp;
				playerAbilities.Glide.HasAbility = this.Abilities.Glide;
				playerAbilities.Climb.HasAbility = this.Abilities.Climb;
				playerAbilities.ChargeJump.HasAbility = this.Abilities.ChargeJump;
				playerAbilities.QuickFlame.HasAbility = this.Abilities.QuickFlame;
				playerAbilities.SparkFlame.HasAbility = this.Abilities.SparkFlame;
				playerAbilities.ChargeFlameBurn.HasAbility = this.Abilities.ChargeFlameBurn;
				playerAbilities.SplitFlameUpgrade.HasAbility = this.Abilities.SplitFlame;
				playerAbilities.BashBuff.HasAbility = this.Abilities.BashBuff;
				playerAbilities.CinderFlame.HasAbility = this.Abilities.CinderFlame;
				playerAbilities.StompUpgrade.HasAbility = this.Abilities.StompUpgrade;
				playerAbilities.RapidFire.HasAbility = this.Abilities.RapidFlame;
				playerAbilities.ChargeFlameBlast.HasAbility = this.Abilities.ChargeFlameBlast;
				playerAbilities.UltraSplitFlame.HasAbility = this.Abilities.UltraSplitFlame;
				playerAbilities.Magnet.HasAbility = this.Abilities.Magnet;
				playerAbilities.MapMarkers.HasAbility = this.Abilities.MapMarkers;
				playerAbilities.HealthEfficiency.HasAbility = this.Abilities.HealthEfficiency;
				playerAbilities.UltraMagnet.HasAbility = this.Abilities.UltraMagnet;
				playerAbilities.EnergyEfficiency.HasAbility = this.Abilities.EnergyEfficiency;
				playerAbilities.AbilityMarkers.HasAbility = this.Abilities.AbilityMarkers;
				playerAbilities.SoulFlameEfficiency.HasAbility = this.Abilities.SoulFlameEfficiency;
				playerAbilities.HealthMarkers.HasAbility = this.Abilities.HealthMarkers;
				playerAbilities.EnergyMarkers.HasAbility = this.Abilities.EnergyMarkers;
				playerAbilities.Sense.HasAbility = this.Abilities.Sense;
				playerAbilities.Rekindle.HasAbility = this.Abilities.Rekindle;
				playerAbilities.Regroup.HasAbility = this.Abilities.Regroup;
				playerAbilities.ChargeFlameEfficiency.HasAbility = this.Abilities.ChargeFlameEfficiency;
				playerAbilities.UltraSoulFlame.HasAbility = this.Abilities.UltraSoulFlame;
				playerAbilities.WaterBreath.HasAbility = this.Abilities.UnlimitedAir;
				playerAbilities.SoulEfficiency.HasAbility = this.Abilities.SoulEfficiency;
				playerAbilities.UltraDefense.HasAbility = this.Abilities.UltraDefense;
				playerAbilities.Dash.HasAbility = this.Abilities.Dash;
				playerAbilities.Grenade.HasAbility = this.Abilities.Grenade;
			}
			if (sein.Prefabs)
			{
				sein.Prefabs.EnsureRightPrefabsAreThereForAbilities();
			}
		}

		// Token: 0x04002664 RID: 9828
		public int Level;

		// Token: 0x04002665 RID: 9829
		public int Hearts = 3;

		// Token: 0x04002666 RID: 9830
		public int Energy = 3;

		// Token: 0x04002667 RID: 9831
		public SceneMetaData.SeinAbilities Abilities;

		// Token: 0x04002668 RID: 9832
		public SceneMetaData.SeinWorldState World;
	}

	// Token: 0x0200071D RID: 1821
	[Serializable]
	public class SeinAbilities
	{
		// Token: 0x04002669 RID: 9833
		public bool SpiritFlame = true;

		// Token: 0x0400266A RID: 9834
		public bool WallJump = true;

		// Token: 0x0400266B RID: 9835
		public bool ChargeFlame = true;

		// Token: 0x0400266C RID: 9836
		public bool DoubleJump = true;

		// Token: 0x0400266D RID: 9837
		public bool Bash = true;

		// Token: 0x0400266E RID: 9838
		public bool Stomp = true;

		// Token: 0x0400266F RID: 9839
		public bool Glide = true;

		// Token: 0x04002670 RID: 9840
		public bool Climb = true;

		// Token: 0x04002671 RID: 9841
		public bool ChargeJump = true;

		// Token: 0x04002672 RID: 9842
		public bool QuickFlame;

		// Token: 0x04002673 RID: 9843
		public bool SparkFlame;

		// Token: 0x04002674 RID: 9844
		public bool ChargeFlameBurn;

		// Token: 0x04002675 RID: 9845
		public bool SplitFlame;

		// Token: 0x04002676 RID: 9846
		public bool BashBuff;

		// Token: 0x04002677 RID: 9847
		public bool CinderFlame;

		// Token: 0x04002678 RID: 9848
		public bool StompUpgrade;

		// Token: 0x04002679 RID: 9849
		public bool RapidFlame = true;

		// Token: 0x0400267A RID: 9850
		public bool ChargeFlameBlast;

		// Token: 0x0400267B RID: 9851
		public bool UltraSplitFlame;

		// Token: 0x0400267C RID: 9852
		public bool Magnet;

		// Token: 0x0400267D RID: 9853
		public bool MapMarkers;

		// Token: 0x0400267E RID: 9854
		public bool HealthEfficiency;

		// Token: 0x0400267F RID: 9855
		public bool UltraMagnet;

		// Token: 0x04002680 RID: 9856
		public bool EnergyEfficiency;

		// Token: 0x04002681 RID: 9857
		public bool AbilityMarkers;

		// Token: 0x04002682 RID: 9858
		public bool SoulFlameEfficiency;

		// Token: 0x04002683 RID: 9859
		public bool HealthMarkers;

		// Token: 0x04002684 RID: 9860
		public bool EnergyMarkers;

		// Token: 0x04002685 RID: 9861
		public bool Sense;

		// Token: 0x04002686 RID: 9862
		public bool Rekindle;

		// Token: 0x04002687 RID: 9863
		public bool Regroup;

		// Token: 0x04002688 RID: 9864
		public bool ChargeFlameEfficiency;

		// Token: 0x04002689 RID: 9865
		public bool UltraSoulFlame;

		// Token: 0x0400268A RID: 9866
		public bool UnlimitedAir = true;

		// Token: 0x0400268B RID: 9867
		public bool SoulEfficiency;

		// Token: 0x0400268C RID: 9868
		public bool DoubleJumpUpgrade;

		// Token: 0x0400268D RID: 9869
		public bool UltraDefense;

		// Token: 0x0400268E RID: 9870
		public bool Grenade;

		// Token: 0x0400268F RID: 9871
		public bool Dash;
	}

	// Token: 0x0200071E RID: 1822
	[Serializable]
	public class SeinWorldState
	{
		// Token: 0x04002690 RID: 9872
		public bool WaterCleansed;

		// Token: 0x04002691 RID: 9873
		public bool WindReleased;
	}

	// Token: 0x0200071F RID: 1823
	[Serializable]
	public class ReverseSceneLoadingZoneInfo
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x000B88EC File Offset: 0x000B6AEC
		public ReverseSceneLoadingZoneInfo(ReverseSceneLoadingZone reverseSceneLoadingZone)
		{
			this.Rectangle = reverseSceneLoadingZone.Rectangle;
			this.SceneToLoad = reverseSceneLoadingZone.SceneToLoad;
		}

		// Token: 0x04002692 RID: 9874
		public Rect Rectangle;

		// Token: 0x04002693 RID: 9875
		public SceneMetaData SceneToLoad;
	}

	// Token: 0x02000720 RID: 1824
	[Serializable]
	public class WorldMapIcon
	{
		// Token: 0x06002B18 RID: 11032 RVA: 0x000B890C File Offset: 0x000B6B0C
		public WorldMapIcon(VisibleOnWorldMap visibleOnWorldMap)
		{
			this.Guid = new MoonGuid(visibleOnWorldMap.MoonGuid);
			this.Position = visibleOnWorldMap.transform.position + visibleOnWorldMap.Offset;
			this.Icon = visibleOnWorldMap.Icon;
			this.IsSecret = visibleOnWorldMap.IsSecret;
		}

		// Token: 0x04002694 RID: 9876
		public MoonGuid Guid;

		// Token: 0x04002695 RID: 9877
		public WorldMapIconType Icon;

		// Token: 0x04002696 RID: 9878
		public Vector2 Position;

		// Token: 0x04002697 RID: 9879
		public bool IsSecret;
	}
}
