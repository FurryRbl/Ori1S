using System;
using System.IO;
using UnityEngine;

// Token: 0x02000752 RID: 1874
public class TestReporter
{
	// Token: 0x06002BD7 RID: 11223 RVA: 0x000BBFA0 File Offset: 0x000BA1A0
	public TestReporter(string reportPath)
	{
		this.m_reportPath = reportPath;
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(reportPath, FileMode.Create)))
		{
			streamWriter.Write("\r\n<html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8'/>\n\n    <style type='text/css'>\n    <!--\n    @import url('http://54.77.177.236:8080/userContent/style.css');\n    -->\n    </style>\n</head>\n<body>\n\t<a href='buildLog.log'>Build Log</a>\n    <table id='background-image'>\n        <thead>\n            <tr>\n                <th scope='col'>Test</th>\n                <th scope='col'>Passed?</th>\n\t\t\t\t<th scope='col'>Log</th>\n                <th scope='col'>Failure Thumbnail</th>\n            </tr>\n        </thead>\n        <tfoot>\n            <tr>\n                <td colspan='4'></td>\n            </tr>\n        </tfoot>\n        <tbody>\r\n");
		}
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x000BC000 File Offset: 0x000BA200
	public void ReportResult(string testPath, bool result, string logFilePath)
	{
		string path = string.Concat(new string[]
		{
			Environment.MachineName,
			"_",
			Application.loadedLevelName,
			"_",
			DateTime.Now.ToString().Replace("/", "-").Replace(":", "-"),
			".jpg"
		});
		string text = Path.Combine(Path.GetDirectoryName(logFilePath), path);
		if (!result)
		{
			Application.CaptureScreenshot(text);
		}
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(this.m_reportPath, FileMode.Append)))
		{
			streamWriter.Write("\r\n            <tr>\n                <td>\n                    {0}\n                </td>\n                <td>\n                    {1}\n                </td>\r\n                <td>\n                    <a href='{2}'>Log</a>\n                </td>\r\n", testPath, result, logFilePath);
			if (!result)
			{
				streamWriter.Write("\n                <td>\n                    <div id='enlargingImage'>\n                        <a href='{0}' target='_target'>\n                            <img src='{0}' height='36' width='64'/>\n                        </a>\n                    </div>\n                </td>\r\n", text);
			}
			else
			{
				streamWriter.Write("\r\n                <td>\n                </td>\r\n");
			}
			streamWriter.Write("\n            </tr>\r\n");
		}
	}

	// Token: 0x06002BD9 RID: 11225 RVA: 0x000BC0FC File Offset: 0x000BA2FC
	public void FinalizeReport()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream(this.m_reportPath, FileMode.Append)))
		{
			streamWriter.Write("\r\n        </tbody>\n    </table></body></html>\r\n");
		}
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x000BC148 File Offset: 0x000BA348
	public void SaveLastTestsReportPath()
	{
		using (StreamWriter streamWriter = new StreamWriter(new FileStream("moonTemp/lastTestsReport.txt", FileMode.Create)))
		{
			streamWriter.Write(this.m_reportPath);
		}
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x000BC194 File Offset: 0x000BA394
	public static string GetLastTestsReportPath()
	{
		string path = "moonTemp/lastTestsReport.txt";
		if (!File.Exists(path))
		{
			return string.Empty;
		}
		string result;
		using (StreamReader streamReader = new StreamReader(new FileStream(path, FileMode.Open)))
		{
			result = streamReader.ReadLine();
		}
		return result;
	}

	// Token: 0x0400279E RID: 10142
	private string m_reportPath = string.Empty;
}
