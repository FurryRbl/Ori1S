using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004AB RID: 1195
public static class YouCanLeaveYourHatOn
{
	// Token: 0x0600209A RID: 8346 RVA: 0x0008E198 File Offset: 0x0008C398
	public static string FindName(UnityEngine.Object o)
	{
		if (o is Component)
		{
			Component component = (Component)o;
			Transform parent = component.transform.parent;
			string text = component.name;
			if (component is SoundPlayer)
			{
				try
				{
					text = text + " " + ((SoundPlayer)o).Clip.name;
				}
				catch
				{
				}
			}
			while (parent)
			{
				text = parent.name + "/" + text;
				parent = parent.parent;
			}
			return text;
		}
		if (o is GameObject)
		{
			GameObject gameObject = (GameObject)o;
			Transform parent2 = gameObject.transform.parent;
			string text2 = gameObject.name;
			while (parent2)
			{
				text2 = parent2.name + "/" + text2;
				parent2 = parent2.parent;
			}
			return text2;
		}
		return o.name;
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x0008E298 File Offset: 0x0008C498
	public static bool DebugMenuPrintReport()
	{
		YouCanLeaveYourHatOn.PrintReport("report.txt");
		return true;
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
	public static void PrintReport(string reportName)
	{
		List<YouCanLeaveYourHatOn.Data> list = new List<YouCanLeaveYourHatOn.Data>();
		UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll<UnityEngine.Object>();
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object @object = array[i];
			string type = @object.GetType().Name;
			YouCanLeaveYourHatOn.Data data = list.Find((YouCanLeaveYourHatOn.Data a) => a.Type == type);
			if (data == null)
			{
				data = new YouCanLeaveYourHatOn.Data(type);
				list.Add(data);
			}
			data.Names.Add(YouCanLeaveYourHatOn.FindName(@object));
		}
		list.Sort((YouCanLeaveYourHatOn.Data a, YouCanLeaveYourHatOn.Data b) => a.Type.CompareTo(b.Type));
		foreach (YouCanLeaveYourHatOn.Data data2 in list)
		{
			data2.Names.Sort();
		}
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(reportName, FileMode.Create)))
		{
			foreach (YouCanLeaveYourHatOn.Data data3 in list)
			{
				streamWriter.WriteLine(string.Concat(new object[]
				{
					data3.Type,
					" (",
					data3.Names.Count,
					")"
				}));
			}
			streamWriter.WriteLine(string.Empty);
			streamWriter.WriteLine(string.Empty);
			foreach (YouCanLeaveYourHatOn.Data data4 in list)
			{
				streamWriter.WriteLine(string.Empty);
				streamWriter.WriteLine("-----------------");
				streamWriter.WriteLine(string.Concat(new object[]
				{
					data4.Type,
					" (",
					data4.Names.Count,
					")"
				}));
				streamWriter.WriteLine("-----------------");
				foreach (string value in data4.Names)
				{
					streamWriter.WriteLine(value);
				}
			}
			int num = 0;
			List<YouCanLeaveYourHatOn.AssetWithSize> list2 = new List<YouCanLeaveYourHatOn.AssetWithSize>();
			foreach (Texture texture in Resources.FindObjectsOfTypeAll<Texture>())
			{
				YouCanLeaveYourHatOn.AssetWithSize assetWithSize = new YouCanLeaveYourHatOn.AssetWithSize(texture, Profiler.GetRuntimeMemorySize(texture));
				num += assetWithSize.Size;
				list2.Add(assetWithSize);
			}
			list2.Sort((YouCanLeaveYourHatOn.AssetWithSize a, YouCanLeaveYourHatOn.AssetWithSize b) => a.Size.CompareTo(b.Size));
			list2.Reverse();
			streamWriter.WriteLine(string.Empty);
			streamWriter.WriteLine("-----------------");
			streamWriter.WriteLine("Textures (" + Utility.Round((float)num / 1024f / 1024f, 0.01f) + "mb)");
			streamWriter.WriteLine("-----------------");
			foreach (YouCanLeaveYourHatOn.AssetWithSize assetWithSize2 in list2)
			{
				string text = assetWithSize2.Asset.name;
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" [",
					Utility.Round((float)assetWithSize2.Size / 1024f / 1024f, 0.01f),
					"mb ",
					Utility.Round((float)assetWithSize2.Size / (float)num * 100f, 0.01f),
					"%]"
				});
				streamWriter.WriteLine(text);
			}
			int num2 = 0;
			List<YouCanLeaveYourHatOn.AssetWithSize> list3 = new List<YouCanLeaveYourHatOn.AssetWithSize>();
			foreach (AudioClip audioClip in Resources.FindObjectsOfTypeAll<AudioClip>())
			{
				YouCanLeaveYourHatOn.AssetWithSize assetWithSize3 = new YouCanLeaveYourHatOn.AssetWithSize(audioClip, Profiler.GetRuntimeMemorySize(audioClip));
				num2 += assetWithSize3.Size;
				list3.Add(assetWithSize3);
			}
			list3.Sort((YouCanLeaveYourHatOn.AssetWithSize a, YouCanLeaveYourHatOn.AssetWithSize b) => a.Size.CompareTo(b.Size));
			list3.Reverse();
			streamWriter.WriteLine(string.Empty);
			streamWriter.WriteLine("-----------------");
			streamWriter.WriteLine("AudioClips (" + Utility.Round((float)num2 / 1024f / 1024f, 0.01f) + "mb)");
			streamWriter.WriteLine("-----------------");
			foreach (YouCanLeaveYourHatOn.AssetWithSize assetWithSize4 in list3)
			{
				string text3 = assetWithSize4.Asset.name;
				string text2 = text3;
				text3 = string.Concat(new object[]
				{
					text2,
					" [",
					Utility.Round((float)assetWithSize4.Size / 1024f / 1024f, 0.01f),
					"mb ",
					Utility.Round((float)assetWithSize4.Size / (float)num2 * 100f, 0.01f),
					"%]"
				});
				streamWriter.WriteLine(text3);
			}
		}
	}

	// Token: 0x020004DE RID: 1246
	public class Data
	{
		// Token: 0x060021BD RID: 8637 RVA: 0x0009399F File Offset: 0x00091B9F
		public Data(string type)
		{
			this.Type = type;
		}

		// Token: 0x04001C53 RID: 7251
		public string Type;

		// Token: 0x04001C54 RID: 7252
		public List<string> Names = new List<string>();
	}

	// Token: 0x020004DF RID: 1247
	public class AssetWithSize
	{
		// Token: 0x060021BE RID: 8638 RVA: 0x000939B9 File Offset: 0x00091BB9
		public AssetWithSize(UnityEngine.Object asset, int size)
		{
			this.Asset = asset;
			this.Size = size;
		}

		// Token: 0x04001C55 RID: 7253
		public UnityEngine.Object Asset;

		// Token: 0x04001C56 RID: 7254
		public int Size;
	}
}
