using System;
using System.IO;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class BuildData : IFrameData
{
	// Token: 0x06000E70 RID: 3696 RVA: 0x0004269C File Offset: 0x0004089C
	public BuildData()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs.Length > 0)
		{
			this.BuildPath = commandLineArgs[0];
		}
		string path = this.BuildPath.Replace(".exe", "_Data");
		string path2 = Path.Combine(path, "svnRevision.txt");
		if (!Application.isEditor && File.Exists(path2))
		{
			using (StreamReader streamReader = new StreamReader(new FileStream(path2, FileMode.Open)))
			{
				this.Revision = streamReader.ReadLine();
			}
		}
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0004275C File Offset: 0x0004095C
	public BuildData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x00042797 File Offset: 0x00040997
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.BuildData;
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0004279C File Offset: 0x0004099C
	public static void Record(BinaryWriter binaryWriter)
	{
		string text = string.Empty;
		string value = string.Empty;
		string empty = string.Empty;
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs.Length > 0)
		{
			text = commandLineArgs[0];
		}
		string path = text.Replace(".exe", "_Data");
		string path2 = Path.Combine(path, "svnRevision.txt");
		if (!Application.isEditor && File.Exists(path2))
		{
			using (StreamReader streamReader = new StreamReader(new FileStream(path2, FileMode.Open)))
			{
				value = streamReader.ReadLine();
			}
		}
		binaryWriter.Write(6);
		binaryWriter.Write(text);
		binaryWriter.Write(value);
		binaryWriter.Write(empty);
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x0004285C File Offset: 0x00040A5C
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.BuildPath);
		binaryWriter.Write(this.Revision);
		binaryWriter.Write(this.BuildID);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0004288D File Offset: 0x00040A8D
	public void Load(BinaryReader binaryReader)
	{
		this.BuildPath = binaryReader.ReadString();
		this.Revision = binaryReader.ReadString();
		this.BuildID = binaryReader.ReadString();
	}

	// Token: 0x04000B97 RID: 2967
	public string BuildPath = string.Empty;

	// Token: 0x04000B98 RID: 2968
	public string Revision = string.Empty;

	// Token: 0x04000B99 RID: 2969
	public string BuildID = string.Empty;
}
