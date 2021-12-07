using System;
using System.IO;

// Token: 0x020001A3 RID: 419
public class FPSData : IFrameData
{
	// Token: 0x0600101A RID: 4122 RVA: 0x000498E0 File Offset: 0x00047AE0
	public FPSData()
	{
	}

	// Token: 0x0600101B RID: 4123 RVA: 0x000498E8 File Offset: 0x00047AE8
	public FPSData(int fps)
	{
		this.FPS = fps;
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000498F7 File Offset: 0x00047AF7
	public FPSData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x00049906 File Offset: 0x00047B06
	public static void Record(BinaryWriter binaryWriter, int fps)
	{
		binaryWriter.Write(10);
		binaryWriter.Write(fps);
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00049917 File Offset: 0x00047B17
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.FPS);
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x00049925 File Offset: 0x00047B25
	public void Load(BinaryReader binaryReader)
	{
		this.FPS = binaryReader.ReadInt32();
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x00049933 File Offset: 0x00047B33
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.FPSData;
	}

	// Token: 0x04000D36 RID: 3382
	public int FPS;
}
