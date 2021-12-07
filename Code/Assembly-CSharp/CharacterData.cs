using System;
using System.IO;
using Game;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class CharacterData : IFrameData
{
	// Token: 0x06000F67 RID: 3943 RVA: 0x000471E0 File Offset: 0x000453E0
	public CharacterData()
	{
		if (Characters.Current as Component)
		{
			PlatformBehaviour currentPlatformBehaviour = CharacterData.CurrentPlatformBehaviour;
			if (currentPlatformBehaviour != null)
			{
				this.Position = currentPlatformBehaviour.PlatformMovement.Position;
				this.Velocity = currentPlatformBehaviour.PlatformMovement.WorldSpeed;
			}
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0004723B File Offset: 0x0004543B
	public CharacterData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0004724A File Offset: 0x0004544A
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.CharacterData;
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00047250 File Offset: 0x00045450
	public static PlatformBehaviour CurrentPlatformBehaviour
	{
		get
		{
			PlatformBehaviour result = null;
			if (Characters.Sein)
			{
				result = Characters.Sein.PlatformBehaviour;
			}
			else if (Characters.BabySein)
			{
				result = Characters.BabySein.PlatformBehaviour;
			}
			else if (Characters.Naru)
			{
				result = Characters.Naru.PlatformBehaviour;
			}
			return result;
		}
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x000472B8 File Offset: 0x000454B8
	public static void Record(BinaryWriter binaryWriter)
	{
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (Characters.Current as Component)
		{
			PlatformBehaviour currentPlatformBehaviour = CharacterData.CurrentPlatformBehaviour;
			if (currentPlatformBehaviour != null)
			{
				vector = currentPlatformBehaviour.PlatformMovement.Position;
				vector2 = currentPlatformBehaviour.PlatformMovement.WorldSpeed;
			}
		}
		binaryWriter.Write(3);
		binaryWriter.Write(vector.x);
		binaryWriter.Write(vector.y);
		binaryWriter.Write(vector.z);
		binaryWriter.Write(vector2.x);
		binaryWriter.Write(vector2.y);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x0004735C File Offset: 0x0004555C
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.Position.x);
		binaryWriter.Write(this.Position.y);
		binaryWriter.Write(this.Position.z);
		binaryWriter.Write(this.Velocity.x);
		binaryWriter.Write(this.Velocity.y);
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x000473C0 File Offset: 0x000455C0
	public void Load(BinaryReader binaryReader)
	{
		this.Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
		this.Velocity = new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
	}

	// Token: 0x04000C4B RID: 3147
	public Vector3 Position;

	// Token: 0x04000C4C RID: 3148
	public Vector2 Velocity;
}
