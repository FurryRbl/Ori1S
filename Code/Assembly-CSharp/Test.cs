using System;
using System.IO;

// Token: 0x02000751 RID: 1873
public class Test
{
	// Token: 0x06002BD4 RID: 11220 RVA: 0x000BBF77 File Offset: 0x000BA177
	public Test(string testFilePath)
	{
		this.TestFilePath = testFilePath;
	}

	// Token: 0x06002BD5 RID: 11221 RVA: 0x000BBF91 File Offset: 0x000BA191
	public void TestFinished(bool passed)
	{
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000BBF93 File Offset: 0x000BA193
	public string TestName
	{
		get
		{
			return Path.GetFileName(this.TestFilePath);
		}
	}

	// Token: 0x0400279D RID: 10141
	public string TestFilePath = string.Empty;
}
