using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class TestSetManager : MonoBehaviour
{
	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00044CF5 File Offset: 0x00042EF5
	public static TestSet CurrentTestSet
	{
		get
		{
			return TestSetManager.TestSets[TestSetManager.m_testSetIndex];
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00044D06 File Offset: 0x00042F06
	public static bool IsPerformingTests
	{
		get
		{
			return TestSetManager.m_testSetIndex != -1;
		}
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x00044D14 File Offset: 0x00042F14
	public void Start()
	{
		string path = Environment.GetCommandLineArgs()[0].Replace(".exe", "_Data");
		TestSetManager.TestsRelativePath = Path.Combine(path, "testingFrameworkTests");
		TestSetManager.Init();
		if (TestSetManager.HaveMoreTestSets())
		{
			TestSetManager.RunNextTestSet();
		}
		else
		{
			Application.Quit();
		}
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x00044D68 File Offset: 0x00042F68
	public static void Init()
	{
		string text = Path.Combine(OutputFolder.BuildOutputPath, "testResults");
		if (Directory.Exists(text))
		{
			Directory.Delete(text, true);
		}
		Directory.CreateDirectory(text);
		string reportPath = Path.Combine(text, "testReport.html");
		TestSetManager.m_testReporter = new TestReporter(reportPath);
		TestSetManager.TestSets = TestSetManager.GetTestSets();
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x00044DC0 File Offset: 0x00042FC0
	public static List<string> GetRequestedTests()
	{
		List<string> list = new List<string>();
		string path = string.Empty;
		path = Path.Combine(TestSetManager.TestsRelativePath, TestSetManager.TestsToRunFileName);
		if (!File.Exists(path))
		{
			return list;
		}
		using (StreamReader streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
		{
			for (string item = streamReader.ReadLine(); item != null; item = streamReader.ReadLine())
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00044E48 File Offset: 0x00043048
	public static List<TestSet> GetTestSets()
	{
		List<TestSet> list = new List<TestSet>();
		foreach (string text in Directory.GetDirectories(TestSetManager.TestsRelativePath))
		{
			TestSet testSet = new TestSet();
			testSet.Init(text, Path.Combine(OutputFolder.BuildOutputPath, Path.Combine("testResults", text.Substring(text.LastIndexOf("\\") + 1))), TestSetManager.GetRequestedTests());
			if (testSet.Tests.Count > 0)
			{
				list.Add(testSet);
			}
		}
		return list;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00044ED4 File Offset: 0x000430D4
	public static void RunNextTestSet()
	{
		TestSetManager.m_testSetIndex++;
		TestSetManager.CurrentTestSet.Run();
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x00044EEC File Offset: 0x000430EC
	private static bool HaveMoreTestSets()
	{
		return TestSetManager.m_testSetIndex + 1 < TestSetManager.TestSets.Count;
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x00044F04 File Offset: 0x00043104
	public static void FinishedTestSet()
	{
		if (TestSetManager.HaveMoreTestSets())
		{
			TestSetManager.RunNextTestSet();
		}
		else
		{
			TestSetManager.m_testReporter.FinalizeReport();
			using (StreamWriter streamWriter = new StreamWriter(new FileStream("testReport.xml", FileMode.Create)))
			{
				streamWriter.Write(TestSetManager.m_testSuite.ToString());
			}
			Application.Quit();
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00044F78 File Offset: 0x00043178
	public static void FinishedTest(bool passed)
	{
		TestSetManager.CurrentTestSet.TestFinished(passed, TestSetManager.m_testReporter);
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x00044F8C File Offset: 0x0004318C
	public static Dictionary<string, bool> GetResults(string reportFilePath)
	{
		Dictionary<string, bool> result = new Dictionary<string, bool>();
		if (!File.Exists(reportFilePath))
		{
			return result;
		}
		return result;
	}

	// Token: 0x04000BE9 RID: 3049
	public const string TestSetManagerSceneGUID = "6ca1f00f1708e184aaed87674e80d419";

	// Token: 0x04000BEA RID: 3050
	public const string UnloadPreviousTestScenePath = "Assets/frameworks/testingFramework/unloadPreviuosTestScene.unity";

	// Token: 0x04000BEB RID: 3051
	public const string UnloadPreviousTestSceneName = "unloadPreviuosTestScene";

	// Token: 0x04000BEC RID: 3052
	public const string TestOutputFolderName = "testResults";

	// Token: 0x04000BED RID: 3053
	public static string TestsRelativePath = "Assets\\scenes\\tests\\testingFrameworkTests";

	// Token: 0x04000BEE RID: 3054
	public static string TestsToRunFileName = "testsToRun.txt";

	// Token: 0x04000BEF RID: 3055
	private static int m_testSetIndex = -1;

	// Token: 0x04000BF0 RID: 3056
	public static List<TestSet> TestSets = new List<TestSet>();

	// Token: 0x04000BF1 RID: 3057
	private static JUnitReporter.TestSuite m_testSuite = new JUnitReporter.TestSuite();

	// Token: 0x04000BF2 RID: 3058
	private static TestReporter m_testReporter = null;
}
