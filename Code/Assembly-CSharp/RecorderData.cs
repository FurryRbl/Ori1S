using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000175 RID: 373
[Serializable]
public class RecorderData
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00043997 File Offset: 0x00041B97
	public int FlushedFramesCount
	{
		get
		{
			return this.m_flushedFramesCount;
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x000439A0 File Offset: 0x00041BA0
	public RecorderFrame GetFrame(int index)
	{
		int num = index - this.m_flushedFramesCount;
		if (num < 0)
		{
			return null;
		}
		if (num > this.Frames.Count)
		{
			return null;
		}
		return this.Frames[num];
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000439E0 File Offset: 0x00041BE0
	public RecorderFrame LastFrame
	{
		get
		{
			return (this.Frames.Count != 0) ? this.Frames[this.Frames.Count - 1] : null;
		}
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00043A1C File Offset: 0x00041C1C
	public void LoadFromFile(string filePath)
	{
		this.Frames.Clear();
		this.CurrentReplayPath = filePath;
		using (BinaryReader binaryReader = new BinaryReader(File.Open(filePath, FileMode.Open)))
		{
			this.CurrentFormatIdentifier = binaryReader.ReadString();
			if (!(this.CurrentFormatIdentifier != this.FILE_FORMAT_IDENTIFIER))
			{
				this.CurrentVersion = binaryReader.ReadInt32();
				if (this.CurrentVersion == 2)
				{
					while (binaryReader.PeekChar() != -1)
					{
						RecorderFrame recorderFrame = new RecorderFrame();
						try
						{
							recorderFrame.LoadFromFile(binaryReader);
						}
						catch (Exception ex)
						{
							int num = 0;
							bool flag = false;
							while (!flag)
							{
								try
								{
									binaryReader.ReadByte();
									num++;
								}
								catch (EndOfStreamException ex2)
								{
									flag = true;
								}
							}
							break;
						}
						this.Frames.Add(recorderFrame);
					}
				}
			}
		}
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00043B2C File Offset: 0x00041D2C
	public void WriteSplitReplay(string folderPath)
	{
		List<BuildData> frameData = this.Frames[0].GetFrameData<BuildData>();
		if (frameData.Count == 0)
		{
			return;
		}
		BuildData item = frameData[0];
		BinaryWriter binaryWriter = null;
		for (int i = 0; i < this.Frames.Count; i++)
		{
			if (this.Frames[i].GetFrameData<CheckpointData>().Count == 0)
			{
				this.Frames[i].SaveToFile(binaryWriter);
			}
			else
			{
				if (binaryWriter != null)
				{
					((IDisposable)binaryWriter).Dispose();
				}
				binaryWriter = new BinaryWriter(File.Open(Path.Combine(folderPath, "output" + i + ".replay"), FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite));
				binaryWriter.Write(this.CurrentFormatIdentifier);
				binaryWriter.Write(this.CurrentVersion);
				this.Frames[i].FrameData.Insert(0, item);
				this.Frames[i].SaveToFile(binaryWriter);
			}
		}
		((IDisposable)binaryWriter).Dispose();
	}

	// Token: 0x04000BCD RID: 3021
	public const int DATA_VERSION = 2;

	// Token: 0x04000BCE RID: 3022
	public string FILE_FORMAT_IDENTIFIER = "record";

	// Token: 0x04000BCF RID: 3023
	public string CurrentFormatIdentifier;

	// Token: 0x04000BD0 RID: 3024
	public int CurrentVersion;

	// Token: 0x04000BD1 RID: 3025
	public List<RecorderFrame> Frames = new List<RecorderFrame>();

	// Token: 0x04000BD2 RID: 3026
	public string CurrentReplayPath = string.Empty;

	// Token: 0x04000BD3 RID: 3027
	private int m_flushedFramesCount;
}
