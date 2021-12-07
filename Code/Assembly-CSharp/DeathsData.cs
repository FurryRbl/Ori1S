using System;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class DeathsData : IFrameData
{
	// Token: 0x06000F7A RID: 3962 RVA: 0x000477E4 File Offset: 0x000459E4
	public DeathsData()
	{
		this.Position = Characters.Current.Position;
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x000477FC File Offset: 0x000459FC
	public DeathsData(BinaryReader reader)
	{
		this.Load(reader);
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0004780B File Offset: 0x00045A0B
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.DeathsData;
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00047810 File Offset: 0x00045A10
	public static void Record(BinaryWriter binaryWriter)
	{
		Vector3 position = Characters.Current.Position;
		binaryWriter.Write(9);
		binaryWriter.Write(position.x);
		binaryWriter.Write(position.y);
		binaryWriter.Write(position.z);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x00047857 File Offset: 0x00045A57
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.Position.x);
		binaryWriter.Write(this.Position.y);
		binaryWriter.Write(this.Position.z);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x0004788C File Offset: 0x00045A8C
	public void Load(BinaryReader binaryReader)
	{
		this.Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
	}

	// Token: 0x04000C4F RID: 3151
	public Vector3 Position;
}
