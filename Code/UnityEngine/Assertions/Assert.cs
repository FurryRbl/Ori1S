using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.Assertions.Comparers;

namespace UnityEngine.Assertions
{
	// Token: 0x02000322 RID: 802
	[DebuggerStepThrough]
	public static class Assert
	{
		// Token: 0x060027B1 RID: 10161 RVA: 0x00038CA0 File Offset: 0x00036EA0
		private static void Fail(string message, string userMessage)
		{
			if (Debugger.IsAttached)
			{
				throw new AssertionException(message, userMessage);
			}
			if (Assert.raiseExceptions)
			{
				throw new AssertionException(message, userMessage);
			}
			if (message == null)
			{
				message = "Assertion has failed\n";
			}
			if (userMessage != null)
			{
				message = userMessage + '\n' + message;
			}
			Debug.LogAssertion(message);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00038CFC File Offset: 0x00036EFC
		[Obsolete("Assert.Equals should not be used for Assertions", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00038D08 File Offset: 0x00036F08
		[Obsolete("Assert.ReferenceEquals should not be used for Assertions", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x00038D14 File Offset: 0x00036F14
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			Assert.IsTrue(condition, null);
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x00038D20 File Offset: 0x00036F20
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			if (!condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(true), message);
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00038D34 File Offset: 0x00036F34
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			Assert.IsFalse(condition, null);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00038D40 File Offset: 0x00036F40
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(false), message);
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00038D54 File Offset: 0x00036F54
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual)
		{
			Assert.AreEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00038D64 File Offset: 0x00036F64
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00038D74 File Offset: 0x00036F74
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00038D80 File Offset: 0x00036F80
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00038D90 File Offset: 0x00036F90
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual)
		{
			Assert.AreNotEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00038DA0 File Offset: 0x00036FA0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00038DB0 File Offset: 0x00036FB0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00038DBC File Offset: 0x00036FBC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00038DCC File Offset: 0x00036FCC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, null);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00038DD8 File Offset: 0x00036FD8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00038DE8 File Offset: 0x00036FE8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			if (!comparer.Equals(actual, expected))
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00038E10 File Offset: 0x00037010
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual)
		{
			Assert.AreNotEqual<T>(expected, actual, null);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00038E1C File Offset: 0x0003701C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x00038E2C File Offset: 0x0003702C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			if (comparer.Equals(actual, expected))
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
			}
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x00038E54 File Offset: 0x00037054
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			Assert.IsNull<T>(value, null);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00038E60 File Offset: 0x00037060
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			if (value != null)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00038E80 File Offset: 0x00037080
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			Assert.IsNotNull<T>(value, null);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00038E8C File Offset: 0x0003708C
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			if (value == null)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
			}
		}

		// Token: 0x04000C44 RID: 3140
		internal const string UNITY_ASSERTIONS = "UNITY_ASSERTIONS";

		// Token: 0x04000C45 RID: 3141
		public static bool raiseExceptions;
	}
}
