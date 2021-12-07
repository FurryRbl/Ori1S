using System;
using System.IO;

// Token: 0x02000189 RID: 393
public class RecorderMessageData : IFrameData
{
	// Token: 0x06000F6E RID: 3950 RVA: 0x00047401 File Offset: 0x00045601
	public RecorderMessageData()
	{
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00047414 File Offset: 0x00045614
	public RecorderMessageData(string text)
	{
		this.Text = text;
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x0004742E File Offset: 0x0004562E
	public RecorderMessageData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00047448 File Offset: 0x00045648
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.RecorderMessageData;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x0004744B File Offset: 0x0004564B
	public static void Record(BinaryWriter binaryWriter, string text)
	{
		binaryWriter.Write(5);
		binaryWriter.Write(text);
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x0004745B File Offset: 0x0004565B
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.Text);
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00047469 File Offset: 0x00045669
	public void Load(BinaryReader binaryReader)
	{
		this.Text = binaryReader.ReadString();
	}

	// Token: 0x04000C4D RID: 3149
	public string Text = string.Empty;
}
