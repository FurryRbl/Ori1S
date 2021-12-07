using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x02000757 RID: 1879
public class JUnitReporter
{
	// Token: 0x02000758 RID: 1880
	public class TestSuite
	{
		// Token: 0x06002BF0 RID: 11248 RVA: 0x000BC608 File Offset: 0x000BA808
		public JUnitReporter.TestCase AddTestCase(string name, string className, JUnitReporter.Failure failure, float duration)
		{
			this.m_testCount++;
			JUnitReporter.TestCase testCase = new JUnitReporter.TestCase(name, className, failure, duration);
			return this.AddTestCase(testCase);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000BC635 File Offset: 0x000BA835
		public JUnitReporter.TestCase AddTestCase(JUnitReporter.TestCase testCase)
		{
			this.m_testCount++;
			this.m_testCases.Add(testCase);
			return this.m_testCases[this.m_testCases.Count - 1];
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000BC66C File Offset: 0x000BA86C
		public void BeginOutputFile(string filePath)
		{
			using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Create)))
			{
				streamWriter.WriteLine("<testsuite>");
			}
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000BC6B4 File Offset: 0x000BA8B4
		public void WriteToFile(string filePath)
		{
			using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Append)))
			{
				for (int i = 0; i < this.m_testCases.Count; i++)
				{
					if (i >= this.m_numberOfTestCasesWrittenToOutput)
					{
						streamWriter.WriteLine(this.m_testCases[i].ToString());
						this.m_numberOfTestCasesWrittenToOutput++;
					}
				}
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000BC744 File Offset: 0x000BA944
		public void FinalizeOutputFile(string filePath)
		{
			using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.Append)))
			{
				streamWriter.WriteLine("</testsuite>");
			}
		}

		// Token: 0x040027AC RID: 10156
		private List<JUnitReporter.TestCase> m_testCases = new List<JUnitReporter.TestCase>();

		// Token: 0x040027AD RID: 10157
		private int m_testCount;

		// Token: 0x040027AE RID: 10158
		private int m_numberOfTestCasesWrittenToOutput;
	}

	// Token: 0x0200075B RID: 1883
	public class Failure
	{
		// Token: 0x06002C02 RID: 11266 RVA: 0x000BCEA9 File Offset: 0x000BB0A9
		public Failure(string message, string type, string stackTrace)
		{
			this.m_message = message;
			this.m_type = type;
			this.m_stackTrace = stackTrace;
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000BCEC6 File Offset: 0x000BB0C6
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000BCECE File Offset: 0x000BB0CE
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000BCED7 File Offset: 0x000BB0D7
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x000BCEDF File Offset: 0x000BB0DF
		public string Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000BCEE8 File Offset: 0x000BB0E8
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		public string StackTrace
		{
			get
			{
				return this.m_stackTrace;
			}
			set
			{
				this.m_stackTrace = value;
			}
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000BCEFC File Offset: 0x000BB0FC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"<failure",
				(this.m_message.Length != 0) ? new JUnitReporter.XmlProperty("message", this.m_message).ToString() : string.Empty,
				new JUnitReporter.XmlProperty("type", this.m_type),
				">",
				this.m_stackTrace,
				"</failure>"
			});
		}

		// Token: 0x040027C6 RID: 10182
		private string m_message;

		// Token: 0x040027C7 RID: 10183
		private string m_type;

		// Token: 0x040027C8 RID: 10184
		private string m_stackTrace;
	}

	// Token: 0x0200075D RID: 1885
	public class TestCase
	{
		// Token: 0x06002C0F RID: 11279 RVA: 0x000BD000 File Offset: 0x000BB200
		public TestCase(string name, string className, JUnitReporter.Failure failure, float duration)
		{
			this.m_name = name;
			this.m_className = className;
			this.m_failure = failure;
			this.m_duration = duration;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000BD025 File Offset: 0x000BB225
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000BD02D File Offset: 0x000BB22D
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000BD036 File Offset: 0x000BB236
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000BD03E File Offset: 0x000BB23E
		public string ClassName
		{
			get
			{
				return this.m_className;
			}
			set
			{
				this.m_className = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000BD047 File Offset: 0x000BB247
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000BD04F File Offset: 0x000BB24F
		public JUnitReporter.Failure Failure
		{
			get
			{
				return this.m_failure;
			}
			set
			{
				this.m_failure = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000BD058 File Offset: 0x000BB258
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000BD060 File Offset: 0x000BB260
		public float Duration
		{
			get
			{
				return this.m_duration;
			}
			set
			{
				this.m_duration = value;
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000BD06C File Offset: 0x000BB26C
		public override string ToString()
		{
			string text = string.Concat(new object[]
			{
				"<testcase",
				new JUnitReporter.XmlProperty("classname", this.m_className),
				new JUnitReporter.XmlProperty("name", this.m_name),
				new JUnitReporter.XmlProperty("time", this.Duration.ToString())
			});
			if (this.m_failure == null)
			{
				text += "/>";
			}
			else
			{
				text += ">";
				text += this.m_failure.ToString();
				text += "</testcase>";
			}
			return text;
		}

		// Token: 0x040027CC RID: 10188
		private string m_name;

		// Token: 0x040027CD RID: 10189
		private string m_className;

		// Token: 0x040027CE RID: 10190
		private JUnitReporter.Failure m_failure;

		// Token: 0x040027CF RID: 10191
		private float m_duration;
	}

	// Token: 0x0200093C RID: 2364
	public class XmlProperty
	{
		// Token: 0x0600343B RID: 13371 RVA: 0x000DBA1D File Offset: 0x000D9C1D
		public XmlProperty(string key, string value)
		{
			this.m_key = key;
			this.m_value = value;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000DBA33 File Offset: 0x000D9C33
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				" ",
				this.m_key,
				"=\"",
				this.m_value,
				"\" "
			});
		}

		// Token: 0x04002F2C RID: 12076
		private string m_key;

		// Token: 0x04002F2D RID: 12077
		private string m_value;
	}
}
