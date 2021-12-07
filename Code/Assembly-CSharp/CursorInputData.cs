using System;
using System.IO;
using Core;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class CursorInputData : IFrameData
{
	// Token: 0x06000F80 RID: 3968 RVA: 0x000478B6 File Offset: 0x00045AB6
	public CursorInputData()
	{
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x000478BE File Offset: 0x00045ABE
	public CursorInputData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x000478D0 File Offset: 0x00045AD0
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.Position.x);
		binaryWriter.Write(this.Position.y);
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x00047900 File Offset: 0x00045B00
	public static void Record(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(12);
		binaryWriter.Write(Core.Input.CursorPosition.x);
		binaryWriter.Write(Core.Input.CursorPosition.y);
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x00047938 File Offset: 0x00045B38
	public void Load(BinaryReader binaryReader)
	{
		this.Position.x = binaryReader.ReadSingle();
		this.Position.y = binaryReader.ReadSingle();
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00047967 File Offset: 0x00045B67
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.CursorInputData;
	}

	// Token: 0x04000C50 RID: 3152
	public Vector2 Position;
}
