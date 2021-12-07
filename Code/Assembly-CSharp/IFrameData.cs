using System;
using System.IO;

// Token: 0x0200016C RID: 364
public interface IFrameData
{
	// Token: 0x06000E76 RID: 3702
	void Save(BinaryWriter binaryWriter);

	// Token: 0x06000E77 RID: 3703
	void Load(BinaryReader binaryReader);

	// Token: 0x06000E78 RID: 3704
	RecorderFrame.FrameDataTypes FrameType();
}
