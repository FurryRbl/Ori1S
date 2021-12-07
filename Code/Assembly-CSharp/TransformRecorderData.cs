using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class TransformRecorderData : IFrameData
{
	// Token: 0x06000F75 RID: 3957 RVA: 0x00047478 File Offset: 0x00045678
	public TransformRecorderData()
	{
		for (int i = 0; i < TransformRecordable.All.Count; i++)
		{
			TransformRecordable transformRecordable = TransformRecordable.All[i];
			TransformRecorderData.Data data = new TransformRecorderData.Data();
			data.UniqueID = transformRecordable.UniqueID;
			data.Position = transformRecordable.transform.position;
			data.Rotation = transformRecordable.transform.rotation;
			if (transformRecordable.GetComponent<Rigidbody>() && !transformRecordable.GetComponent<Rigidbody>().isKinematic)
			{
				data.Velocity = transformRecordable.GetComponent<Rigidbody>().velocity;
				data.AngularVelocity = transformRecordable.GetComponent<Rigidbody>().angularVelocity;
			}
			this.RecorderData.Add(data);
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x0004753F File Offset: 0x0004573F
	public TransformRecorderData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00047559 File Offset: 0x00045759
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.TransformRecorderData;
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x0004755C File Offset: 0x0004575C
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.RecorderData.Count);
		foreach (TransformRecorderData.Data data in this.RecorderData)
		{
			binaryWriter.Write(data.UniqueID);
			binaryWriter.Write(data.Position.x);
			binaryWriter.Write(data.Position.y);
			binaryWriter.Write(data.Position.z);
			binaryWriter.Write(data.Rotation.x);
			binaryWriter.Write(data.Rotation.y);
			binaryWriter.Write(data.Rotation.z);
			binaryWriter.Write(data.Rotation.w);
			binaryWriter.Write(data.Velocity.x);
			binaryWriter.Write(data.Velocity.y);
			binaryWriter.Write(data.Velocity.z);
			binaryWriter.Write(data.AngularVelocity.x);
			binaryWriter.Write(data.AngularVelocity.y);
			binaryWriter.Write(data.AngularVelocity.z);
		}
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x000476B8 File Offset: 0x000458B8
	public void Load(BinaryReader binaryReader)
	{
		int num = binaryReader.ReadInt32();
		this.RecorderData.Clear();
		for (int i = 0; i < num; i++)
		{
			TransformRecorderData.Data data = new TransformRecorderData.Data();
			data.UniqueID = binaryReader.ReadString();
			data.Position.x = binaryReader.ReadSingle();
			data.Position.y = binaryReader.ReadSingle();
			data.Position.z = binaryReader.ReadSingle();
			data.Rotation.x = binaryReader.ReadSingle();
			data.Rotation.y = binaryReader.ReadSingle();
			data.Rotation.z = binaryReader.ReadSingle();
			data.Rotation.w = binaryReader.ReadSingle();
			data.Velocity.x = binaryReader.ReadSingle();
			data.Velocity.y = binaryReader.ReadSingle();
			data.Velocity.z = binaryReader.ReadSingle();
			data.AngularVelocity.x = binaryReader.ReadSingle();
			data.AngularVelocity.y = binaryReader.ReadSingle();
			data.AngularVelocity.z = binaryReader.ReadSingle();
			this.RecorderData.Add(data);
		}
	}

	// Token: 0x04000C4E RID: 3150
	public List<TransformRecorderData.Data> RecorderData = new List<TransformRecorderData.Data>();

	// Token: 0x0200019C RID: 412
	public class Data
	{
		// Token: 0x04000D03 RID: 3331
		public string UniqueID;

		// Token: 0x04000D04 RID: 3332
		public Vector3 Position;

		// Token: 0x04000D05 RID: 3333
		public Quaternion Rotation;

		// Token: 0x04000D06 RID: 3334
		public Vector3 Velocity;

		// Token: 0x04000D07 RID: 3335
		public Vector3 AngularVelocity;
	}
}
