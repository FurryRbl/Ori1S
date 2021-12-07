using System;
using System.IO;

// Token: 0x02000633 RID: 1587
public class HoloLensFileWriteState
{
	// Token: 0x0600270A RID: 9994 RVA: 0x000AABC2 File Offset: 0x000A8DC2
	public HoloLensFileWriteState(FileStream fileStream)
	{
		this.Stream = fileStream;
	}

	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x0600270B RID: 9995 RVA: 0x000AABD1 File Offset: 0x000A8DD1
	// (set) Token: 0x0600270C RID: 9996 RVA: 0x000AABD9 File Offset: 0x000A8DD9
	public FileStream Stream { get; set; }
}
