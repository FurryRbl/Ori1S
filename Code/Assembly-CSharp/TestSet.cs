using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x02000753 RID: 1875
public class TestSet
{
	// Token: 0x06002BDD RID: 11229 RVA: 0x000BC21C File Offset: 0x000BA41C
	public void Init(string testSetFolderPath, string testOutputFolderPath, List<string> requestedTests)
	{
		this.TestSetFolderPath = testSetFolderPath;
		this.OutputFolderPath = testOutputFolderPath;
		if (!Directory.Exists(this.OutputFolderPath))
		{
			Directory.CreateDirectory(this.OutputFolderPath);
		}
		this.TestSetConfiguration = new TestSetConfiguration(this.TestSetFolderPath);
		this.InitializeTests();
		this.FilterRequestedTests(requestedTests);
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x000BC271 File Offset: 0x000BA471
	public void Run()
	{
		this.RunTest();
	}

	// Token: 0x06002BDF RID: 11231 RVA: 0x000BC27C File Offset: 0x000BA47C
	public void TestFinished(bool passed, TestReporter testReporter)
	{
		string text = Path.Combine(this.OutputFolderPath, "log_" + this.CurrentTest.TestName);
		this.m_logCallbackHandler.FlushEntriesToFile(text);
		this.m_logCallbackHandler.RemoveHandler();
		this.m_logCallbackHandler = null;
		testReporter.ReportResult(this.CurrentTest.TestFilePath, passed, text);
		this.CurrentTest.TestFinished(passed);
		if (this.HaveMoreTests())
		{
			this.RunNextTest();
		}
		else
		{
			TestSetManager.FinishedTestSet();
		}
	}

	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000BC302 File Offset: 0x000BA502
	public Test CurrentTest
	{
		get
		{
			return this.Tests[this.m_testIndex];
		}
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000BC315 File Offset: 0x000BA515
	public string FirstTestSetSceneAssetPath
	{
		get
		{
			return Path.Combine(Path.Combine(this.TestSetFolderPath, "scenes"), this.TestSetConfiguration.FirstTestSetSceneName + ".unity").Replace("\\", "/");
		}
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x000BC350 File Offset: 0x000BA550
	private void RunNextTest()
	{
		this.m_testIndex++;
		this.RunTest();
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x000BC368 File Offset: 0x000BA568
	private void RunTest()
	{
		RecorderInput.ReplayPath = this.CurrentTest.TestFilePath;
		RecorderInput.Save();
		UnloadPreviousTestComplete.LevelToLoad = this.TestSetConfiguration.FirstTestSetSceneName;
		Application.LoadLevel("unloadPreviuosTestScene");
		this.m_logCallbackHandler = new LogCallbackHandler();
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000BC3AF File Offset: 0x000BA5AF
	private bool HaveMoreTests()
	{
		return this.m_testIndex + 1 < this.Tests.Count;
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x000BC3C8 File Offset: 0x000BA5C8
	private void InitializeTests()
	{
		foreach (string testFilePath in from f in Directory.GetFiles(Path.Combine(this.TestSetFolderPath, "tests"))
		where f.EndsWith(".txt")
		select f)
		{
			Test item = new Test(testFilePath);
			this.Tests.Add(item);
		}
	}

	// Token: 0x06002BE6 RID: 11238 RVA: 0x000BC45C File Offset: 0x000BA65C
	private void FilterRequestedTests(List<string> requestedTests)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		List<Test> list = new List<Test>();
		Test test;
		foreach (Test test2 in this.Tests)
		{
			test = test2;
			if (requestedTests.Any((string path) => path.EndsWith(test.TestFilePath.Substring(test.TestFilePath.IndexOf(TestSetManager.TestsRelativePath.Substring(TestSetManager.TestsRelativePath.LastIndexOf("\\")))))))
			{
				list.Add(test);
			}
		}
		this.Tests = list;
	}

	// Token: 0x0400279F RID: 10143
	public const string ScenesFolderName = "scenes";

	// Token: 0x040027A0 RID: 10144
	public const string TestsFolderName = "tests";

	// Token: 0x040027A1 RID: 10145
	public string TestSetFolderPath = string.Empty;

	// Token: 0x040027A2 RID: 10146
	public List<Test> Tests = new List<Test>();

	// Token: 0x040027A3 RID: 10147
	public TestSetConfiguration TestSetConfiguration;

	// Token: 0x040027A4 RID: 10148
	public string OutputFolderPath = string.Empty;

	// Token: 0x040027A5 RID: 10149
	private LogCallbackHandler m_logCallbackHandler;

	// Token: 0x040027A6 RID: 10150
	private int m_testIndex;
}
