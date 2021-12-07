using System;
using System.Reflection;
using System.Security;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200008A RID: 138
	public sealed class Security
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x0000B754 File Offset: 0x00009954
		[ExcludeFromDocs]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int timeout = 3000;
			return Security.PrefetchSocketPolicy(ip, atPort, timeout);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0000B770 File Offset: 0x00009970
		public static bool PrefetchSocketPolicy(string ip, int atPort, [DefaultValue("3000")] int timeout)
		{
			return true;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0000B774 File Offset: 0x00009974
		private static MethodInfo GetUnityCrossDomainHelperMethod(string methodname)
		{
			Type type = Types.GetType("UnityEngine.UnityCrossDomainHelper", "CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
			if (type == null)
			{
				throw new SecurityException("Cant find type UnityCrossDomainHelper");
			}
			MethodInfo method = type.GetMethod(methodname);
			if (method == null)
			{
				throw new SecurityException("Cant find " + methodname);
			}
			return method;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0000B7C4 File Offset: 0x000099C4
		internal static string TokenToHex(byte[] token)
		{
			if (token == null || 8 > token.Length)
			{
				return string.Empty;
			}
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}{6:x2}{7:x2}", new object[]
			{
				token[0],
				token[1],
				token[2],
				token[3],
				token[4],
				token[5],
				token[6],
				token[7]
			});
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0000B850 File Offset: 0x00009A50
		[SecuritySafeCritical]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0000B854 File Offset: 0x00009A54
		[SecuritySafeCritical]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0000B858 File Offset: 0x00009A58
		[SecuritySafeCritical]
		private static Assembly LoadAndVerifyAssemblyInternal(byte[] assemblyData)
		{
			return null;
		}
	}
}
