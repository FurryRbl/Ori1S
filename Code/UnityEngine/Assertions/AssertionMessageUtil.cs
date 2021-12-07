using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000324 RID: 804
	internal class AssertionMessageUtil
	{
		// Token: 0x060027CD RID: 10189 RVA: 0x00038EF8 File Offset: 0x000370F8
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[]
			{
				"Assertion failed.",
				failureMessage
			});
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00038F18 File Offset: 0x00037118
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00038F48 File Offset: 0x00037148
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[]
			{
				(!expectEqual) ? string.Empty : "not "
			}), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				(!expectEqual) ? "!=" : "=="
			}));
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00038FB4 File Offset: 0x000371B4
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[]
			{
				(!expectNull) ? string.Empty : "not "
			}), UnityString.Format("Value was {0}Null", new object[]
			{
				(!expectNull) ? "not " : string.Empty
			}));
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x00039018 File Offset: 0x00037218
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + !expected, expected.ToString());
		}

		// Token: 0x04000C47 RID: 3143
		private const string k_Expected = "Expected:";

		// Token: 0x04000C48 RID: 3144
		private const string k_AssertionFailed = "Assertion failed.";
	}
}
