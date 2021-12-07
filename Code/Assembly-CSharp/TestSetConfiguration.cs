using System;
using System.IO;

// Token: 0x02000755 RID: 1877
public class TestSetConfiguration
{
	// Token: 0x06002BEA RID: 11242 RVA: 0x000BC55C File Offset: 0x000BA75C
	public TestSetConfiguration(string testSetFolderPath)
	{
		string path = Path.Combine(testSetFolderPath, "configuration.cfg");
		if (File.Exists(path))
		{
			using (StreamReader streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
			{
				this.FirstTestSetSceneName = streamReader.ReadLine();
			}
		}
	}

	// Token: 0x040027A9 RID: 10153
	public const string ConfigurationFileName = "configuration.cfg";

	// Token: 0x040027AA RID: 10154
	public string FirstTestSetSceneName = string.Empty;
}
