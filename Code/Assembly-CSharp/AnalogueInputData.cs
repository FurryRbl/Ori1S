using System;
using System.IO;
using Core;

// Token: 0x02000186 RID: 390
public class AnalogueInputData : IFrameData
{
	// Token: 0x06000F5A RID: 3930 RVA: 0x00046F7B File Offset: 0x0004517B
	public AnalogueInputData()
	{
		this.UpdateInputs();
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00046F8A File Offset: 0x0004518A
	public AnalogueInputData(BinaryReader binaryReader)
	{
		this.Load(binaryReader);
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00046F9C File Offset: 0x0004519C
	public bool UpdateInputs()
	{
		bool result = false;
		if (this.HorizontalAnalogLeft != Input.HorizontalAnalogLeft)
		{
			this.HorizontalAnalogLeft = Input.HorizontalAnalogLeft;
			result = true;
		}
		if (this.VerticalAnalogLeft != Input.VerticalAnalogLeft)
		{
			this.VerticalAnalogLeft = Input.VerticalAnalogLeft;
			result = true;
		}
		if (this.HorizontalAnalogRight != Input.HorizontalAnalogRight)
		{
			this.HorizontalAnalogRight = Input.HorizontalAnalogRight;
			result = true;
		}
		if (this.VerticalAnalogRight != Input.VerticalAnalogRight)
		{
			this.VerticalAnalogRight = Input.VerticalAnalogRight;
			result = true;
		}
		return result;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00047020 File Offset: 0x00045220
	public static void Record(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(11);
		float horizontalAnalogLeft = Input.HorizontalAnalogLeft;
		float verticalAnalogLeft = Input.VerticalAnalogLeft;
		float horizontalAnalogRight = Input.HorizontalAnalogRight;
		float verticalAnalogRight = Input.VerticalAnalogRight;
		binaryWriter.Write(horizontalAnalogLeft);
		binaryWriter.Write(verticalAnalogLeft);
		binaryWriter.Write(horizontalAnalogRight);
		binaryWriter.Write(verticalAnalogRight);
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0004706C File Offset: 0x0004526C
	public void Save(BinaryWriter binaryWriter)
	{
		binaryWriter.Write(this.HorizontalAnalogLeft);
		binaryWriter.Write(this.VerticalAnalogLeft);
		binaryWriter.Write(this.HorizontalAnalogRight);
		binaryWriter.Write(this.VerticalAnalogRight);
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x000470AC File Offset: 0x000452AC
	public void Load(BinaryReader binaryReader)
	{
		this.HorizontalAnalogLeft = binaryReader.ReadSingle();
		this.VerticalAnalogLeft = binaryReader.ReadSingle();
		this.HorizontalAnalogRight = binaryReader.ReadSingle();
		this.VerticalAnalogRight = binaryReader.ReadSingle();
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x000470E9 File Offset: 0x000452E9
	public RecorderFrame.FrameDataTypes FrameType()
	{
		return RecorderFrame.FrameDataTypes.AnalogInputData;
	}

	// Token: 0x04000C46 RID: 3142
	public float HorizontalAnalogLeft;

	// Token: 0x04000C47 RID: 3143
	public float VerticalAnalogLeft;

	// Token: 0x04000C48 RID: 3144
	public float HorizontalAnalogRight;

	// Token: 0x04000C49 RID: 3145
	public float VerticalAnalogRight;
}
