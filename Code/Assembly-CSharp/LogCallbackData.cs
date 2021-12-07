using System;
using System.IO;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class LogCallbackData : IFrameData
{
	// Token: 0x06000E81 RID: 3713 RVA: 0x00042BD2 File Offset: 0x00040DD2
	public LogCallbackData()
	{
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00042BDA File Offset: 0x00040DDA
	public LogCallbackData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x00042BE9 File Offset: 0x00040DE9
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.LogCallbackData;
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00042BEC File Offset: 0x00040DEC
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.LogString);
		binaryWriter.Write(this.StackTrace);
		binaryWriter.Write((int)this.LogType);
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00042C1D File Offset: 0x00040E1D
	public void Load(BinaryReader binaryReader)
	{
		this.LogString = binaryReader.ReadString();
		this.StackTrace = binaryReader.ReadString();
		this.LogType = (LogType)binaryReader.ReadInt32();
	}

	// Token: 0x04000BA9 RID: 2985
	public string LogString;

	// Token: 0x04000BAA RID: 2986
	public string StackTrace;

	// Token: 0x04000BAB RID: 2987
	public LogType LogType;
}
