using System;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class CameraData : IFrameData
{
	// Token: 0x06000F61 RID: 3937 RVA: 0x000470ED File Offset: 0x000452ED
	public CameraData()
	{
		this.Position = UI.Cameras.Current.Controller.PuppetController.GameplayPuppet.position;
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00047114 File Offset: 0x00045314
	public CameraData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00047123 File Offset: 0x00045323
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.CameraData;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00047126 File Offset: 0x00045326
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.Position.x);
		binaryWriter.Write(this.Position.y);
		binaryWriter.Write(this.Position.z);
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0004715C File Offset: 0x0004535C
	public static void Record(BinaryWriter binaryWriter)
	{
		Vector3 position = UI.Cameras.Current.Controller.PuppetController.GameplayPuppet.position;
		binaryWriter.Write(2);
		binaryWriter.Write(position.x);
		binaryWriter.Write(position.y);
		binaryWriter.Write(position.z);
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x000471B4 File Offset: 0x000453B4
	public void Load(BinaryReader binaryReader)
	{
		this.Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
	}

	// Token: 0x04000C4A RID: 3146
	public Vector3 Position;
}
