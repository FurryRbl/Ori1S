using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x0200016D RID: 365
public class RecorderFrame
{
	// Token: 0x06000E7A RID: 3706 RVA: 0x000428C8 File Offset: 0x00040AC8
	public void SaveToFile(BinaryWriter binaryWriter)
	{
		for (int i = 0; i < this.FrameData.Count; i++)
		{
			IFrameData frameData = this.FrameData[i];
			binaryWriter.Write((int)frameData.FrameType());
			frameData.Save(binaryWriter);
		}
		binaryWriter.Write(0);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00042918 File Offset: 0x00040B18
	public T GetFrameDataOfType<T>() where T : class
	{
		foreach (IFrameData frameData in this.FrameData)
		{
			if (frameData is T)
			{
				return (T)((object)frameData);
			}
		}
		return (T)((object)null);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0004298C File Offset: 0x00040B8C
	public List<T> GetFrameData<T>() where T : class
	{
		List<T> list = new List<T>();
		foreach (IFrameData frameData in this.FrameData)
		{
			if (frameData is T)
			{
				list.Add((T)((object)frameData));
			}
		}
		return list;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x00042A00 File Offset: 0x00040C00
	public void LoadFromFile(BinaryReader binaryReader)
	{
		this.FrameData.Clear();
		int i = 0;
		while (i < 1000)
		{
			RecorderFrame.FrameDataTypes frameDataTypes = (RecorderFrame.FrameDataTypes)binaryReader.ReadInt32();
			switch (frameDataTypes)
			{
			case RecorderFrame.FrameDataTypes.None:
				i = 1000;
				break;
			case RecorderFrame.FrameDataTypes.InputData:
			{
				InputData item = new InputData(binaryReader);
				this.FrameData.Add(item);
				break;
			}
			case RecorderFrame.FrameDataTypes.CameraData:
			{
				CameraData item2 = new CameraData(binaryReader);
				this.FrameData.Add(item2);
				break;
			}
			case RecorderFrame.FrameDataTypes.CharacterData:
			{
				CharacterData item3 = new CharacterData(binaryReader);
				this.FrameData.Add(item3);
				break;
			}
			case RecorderFrame.FrameDataTypes.LogCallbackData:
			{
				LogCallbackData item4 = new LogCallbackData(binaryReader);
				this.FrameData.Add(item4);
				break;
			}
			case RecorderFrame.FrameDataTypes.RecorderMessageData:
			{
				RecorderMessageData item5 = new RecorderMessageData(binaryReader);
				this.FrameData.Add(item5);
				break;
			}
			case RecorderFrame.FrameDataTypes.BuildData:
			{
				BuildData item6 = new BuildData(binaryReader);
				this.FrameData.Add(item6);
				break;
			}
			case RecorderFrame.FrameDataTypes.CheckpointData:
			{
				CheckpointData item7 = new CheckpointData(binaryReader);
				this.FrameData.Add(item7);
				break;
			}
			case RecorderFrame.FrameDataTypes.TransformRecorderData:
			{
				TransformRecorderData item8 = new TransformRecorderData(binaryReader);
				this.FrameData.Add(item8);
				break;
			}
			case RecorderFrame.FrameDataTypes.DeathsData:
			{
				DeathsData item9 = new DeathsData(binaryReader);
				this.FrameData.Add(item9);
				break;
			}
			case RecorderFrame.FrameDataTypes.FPSData:
				goto IL_183;
			case RecorderFrame.FrameDataTypes.AnalogInputData:
			{
				AnalogueInputData item10 = new AnalogueInputData(binaryReader);
				this.FrameData.Add(item10);
				break;
			}
			case RecorderFrame.FrameDataTypes.CursorInputData:
			{
				CursorInputData item11 = new CursorInputData(binaryReader);
				this.FrameData.Add(item11);
				break;
			}
			default:
				goto IL_183;
			}
			i++;
			continue;
			IL_183:
			throw new Exception(string.Concat(new object[]
			{
				"Unknown data type in frame: ",
				i,
				" : ",
				(int)frameDataTypes
			}));
		}
	}

	// Token: 0x04000B9A RID: 2970
	public List<IFrameData> FrameData = new List<IFrameData>();

	// Token: 0x0200016E RID: 366
	public enum FrameDataTypes
	{
		// Token: 0x04000B9C RID: 2972
		None,
		// Token: 0x04000B9D RID: 2973
		InputData,
		// Token: 0x04000B9E RID: 2974
		CameraData,
		// Token: 0x04000B9F RID: 2975
		CharacterData,
		// Token: 0x04000BA0 RID: 2976
		LogCallbackData,
		// Token: 0x04000BA1 RID: 2977
		RecorderMessageData,
		// Token: 0x04000BA2 RID: 2978
		BuildData,
		// Token: 0x04000BA3 RID: 2979
		CheckpointData,
		// Token: 0x04000BA4 RID: 2980
		TransformRecorderData,
		// Token: 0x04000BA5 RID: 2981
		DeathsData,
		// Token: 0x04000BA6 RID: 2982
		FPSData,
		// Token: 0x04000BA7 RID: 2983
		AnalogInputData,
		// Token: 0x04000BA8 RID: 2984
		CursorInputData
	}
}
