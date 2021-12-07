using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000761 RID: 1889
public class ScenesToSkip
{
	// Token: 0x06002C1C RID: 11292 RVA: 0x000BD19D File Offset: 0x000BB39D
	public ScenesToSkip()
	{
		ScenesToSkip.Instance = this;
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000BD1B6 File Offset: 0x000BB3B6
	public ScenesToSkip(string inputFilePath)
	{
		ScenesToSkip.Instance = this;
		this.ParseInuptFile(inputFilePath);
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000BD1D8 File Offset: 0x000BB3D8
	public bool ShouldSkipScene(string scene)
	{
		return this.Scenes.Contains(scene);
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000BD1E8 File Offset: 0x000BB3E8
	public void ParseInuptFile(string inputFilePath)
	{
		this.Scenes.Clear();
		if (!File.Exists(inputFilePath))
		{
			return;
		}
		using (StreamReader streamReader = new StreamReader(new FileStream(inputFilePath, FileMode.Open)))
		{
			while (!streamReader.EndOfStream)
			{
				string item = streamReader.ReadLine();
				this.Scenes.Add(item);
			}
		}
	}

	// Token: 0x040027E5 RID: 10213
	public static ScenesToSkip Instance;

	// Token: 0x040027E6 RID: 10214
	public List<string> Scenes = new List<string>();
}
