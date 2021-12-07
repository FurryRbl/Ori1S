using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x02000301 RID: 769
	public static class Types
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x000365A0 File Offset: 0x000347A0
		public static Type GetType(string typeName, string assemblyName)
		{
			Type result;
			try
			{
				result = Assembly.Load(assemblyName).GetType(typeName);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
