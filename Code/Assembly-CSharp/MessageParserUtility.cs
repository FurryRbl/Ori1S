using System;
using System.Text;
using Game;
using UnityEngine;

// Token: 0x02000682 RID: 1666
public static class MessageParserUtility
{
	// Token: 0x0600287D RID: 10365 RVA: 0x000AF980 File Offset: 0x000ADB80
	public static string[] ProcessStringArray(string s)
	{
		string[] array = s.Split(new string[]
		{
			"<br/>"
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			array[0] = MessageParserUtility.ProcessString(array[0]);
		}
		return array;
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x000AF9C4 File Offset: 0x000ADBC4
	public static string ProcessString(string s)
	{
		MessageParserUtility.s_currentString = s;
		MessageParserUtility.s_currentBuilder = new StringBuilder(s);
		MessageParserUtility.ProcessColorsInString(MessageParserUtility.s_currentBuilder);
		MessageParserUtility.Replace("[XpBar]", "<icon>j</>");
		MessageParserUtility.Replace("[AbilityPoint]", "<icon>j</>");
		if (Application.isPlaying)
		{
			PlayerInput instance = PlayerInput.Instance;
			MessageParserUtility.Replace("[Up]", ButtonIconUtility.GetAxisIcon(instance.VerticalDigiPad, true));
			MessageParserUtility.Replace("[Down]", ButtonIconUtility.GetAxisIcon(instance.VerticalDigiPad, false));
			MessageParserUtility.Replace("[Left]", ButtonIconUtility.GetAxisIcon(instance.HorizontalDigiPad, false));
			MessageParserUtility.Replace("[Stick]", ButtonIconUtility.GetButtonString(instance.LeftStick));
			MessageParserUtility.Replace("[Right]", ButtonIconUtility.GetAxisIcon(instance.HorizontalDigiPad, true));
			MessageParserUtility.Replace("[Jump]", ButtonIconUtility.GetButtonString(instance.Jump));
			MessageParserUtility.Replace("[Start]", ButtonIconUtility.GetButtonString(instance.Jump));
			MessageParserUtility.Replace("[Accept]", ButtonIconUtility.GetButtonString(instance.Jump));
			MessageParserUtility.Replace("[Rekindle]", ButtonIconUtility.GetButtonString(instance.SoulFlame));
			MessageParserUtility.Replace("[Cancel]", ButtonIconUtility.GetButtonString(instance.Cancel));
			MessageParserUtility.Replace("[SoulFlame]", ButtonIconUtility.GetButtonString(instance.SoulFlame));
			MessageParserUtility.Replace("[Inventory]", ButtonIconUtility.GetButtonString(instance.Start));
			MessageParserUtility.Replace("[SkillTree]", ButtonIconUtility.GetButtonString(instance.Start));
			MessageParserUtility.Replace("[SpiritFlame]", ButtonIconUtility.GetButtonString(instance.SpiritFlame));
			MessageParserUtility.Replace("[Listen]", ButtonIconUtility.GetButtonString(instance.SpiritFlame));
			MessageParserUtility.Replace("[StructureInteraction]", ButtonIconUtility.GetButtonString(instance.SpiritFlame));
			MessageParserUtility.Replace("[Bash]", ButtonIconUtility.GetButtonString(instance.Bash));
			MessageParserUtility.Replace("[Glide]", ButtonIconUtility.GetButtonString(instance.Glide));
			MessageParserUtility.Replace("[Grab]", ButtonIconUtility.GetButtonString(instance.Grab));
			MessageParserUtility.Replace("[Climb]", ButtonIconUtility.GetButtonString(instance.Grab));
			MessageParserUtility.Replace("[PickUp]", ButtonIconUtility.GetButtonString(instance.Grab));
			MessageParserUtility.Replace("[Map]", ButtonIconUtility.GetButtonString(instance.Select));
			MessageParserUtility.Replace("[ChargeJumpCharge]", ButtonIconUtility.GetButtonString(instance.ChargeJump));
			MessageParserUtility.Replace("[MapZoom]", ButtonIconUtility.GetButtonString(instance.ZoomOut) + ButtonIconUtility.GetButtonString(instance.ZoomIn));
			MessageParserUtility.Replace("[MapLegend]", ButtonIconUtility.GetButtonString(instance.Legend));
			MessageParserUtility.Replace("[MapFocus]", ButtonIconUtility.GetButtonString(instance.Focus));
			MessageParserUtility.Replace("[CycleLeaderboard]", ButtonIconUtility.GetButtonString(instance.MenuPageLeft) + ButtonIconUtility.GetButtonString(instance.MenuPageRight));
			MessageParserUtility.Replace("[CycleFilter]", ButtonIconUtility.GetButtonString(instance.Filter));
			MessageParserUtility.Replace("[CycleDifficulty]", ButtonIconUtility.GetButtonString(instance.LeftShoulder) + ButtonIconUtility.GetButtonString(instance.RightShoulder));
			MessageParserUtility.Replace("[SaveSlotCopy]", ButtonIconUtility.GetButtonString(instance.Copy));
			MessageParserUtility.Replace("[SaveSlotDelete]", ButtonIconUtility.GetButtonString(instance.Delete));
			MessageParserUtility.Replace("[SaveSlotBackup]", ButtonIconUtility.GetAxisIcon(instance.VerticalDigiPad, true));
			MessageParserUtility.Replace("[Dash]", ButtonIconUtility.GetButtonString(instance.RightShoulder));
			MessageParserUtility.Replace("[LightSpheres]", ButtonIconUtility.GetButtonString(instance.LeftShoulder));
			MessageParserUtility.Replace("[UnlockFullGame]", ButtonIconUtility.GetButtonString(instance.Copy));
			if (Characters.Sein && Characters.Sein.Level)
			{
				string text = Characters.Sein.Level.ExperienceNeedForNextLevel.ToString();
				if (text.Length > 3)
				{
					Language language = GameSettings.Instance.Language;
					if (language == Language.Japanese || language == Language.Spanish)
					{
						text = text.Insert(text.Length - 3, ",");
					}
					else if (language == Language.German || language == Language.Italian || language == Language.Portuguese)
					{
						text = text.Insert(text.Length - 3, ".");
					}
				}
				MessageParserUtility.Replace("[SpiritLightNextLevel]", text);
			}
		}
		MessageParserUtility.Replace("\\n", "\n");
		return MessageParserUtility.s_currentBuilder.ToString();
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x000AFDF5 File Offset: 0x000ADFF5
	private static void Replace(string replace, string with)
	{
		if (MessageParserUtility.s_currentString.IndexOf(replace, StringComparison.Ordinal) >= 0)
		{
			MessageParserUtility.s_currentBuilder.Replace(replace, with);
		}
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x000AFE18 File Offset: 0x000AE018
	private static void ProcessColorsInString(StringBuilder builder)
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		for (int i = 0; i < builder.Length; i++)
		{
			if (builder[i] == '#')
			{
				flag = !flag;
				string text = (!flag) ? "</>" : "<yellow>";
				builder.Replace("#", text, i, 1);
				i += text.Length - 1;
			}
			else if (builder[i] == '*')
			{
				flag2 = !flag2;
				string text2 = (!flag2) ? "</>" : "<blue>";
				builder.Replace("*", text2, i, 1);
				i += text2.Length - 1;
			}
			else if (builder[i] == '$')
			{
				flag3 = !flag3;
				string text3 = (!flag3) ? "</>" : "<green>";
				builder.Replace("$", text3, i, 1);
				i += text3.Length - 1;
			}
			else if (builder[i] == '@')
			{
				flag4 = !flag4;
				string text4 = (!flag4) ? "</>" : "<red>";
				builder.Replace("@", text4, i, 1);
				i += text4.Length - 1;
			}
			else if (builder[i] == '^')
			{
				flag5 = !flag5;
				string text5 = (!flag5) ? "</>" : "<small>";
				builder.Replace("^", text5, i, 1);
				i += text5.Length - 1;
			}
		}
	}

	// Token: 0x04002400 RID: 9216
	private static string s_currentString;

	// Token: 0x04002401 RID: 9217
	private static StringBuilder s_currentBuilder;
}
