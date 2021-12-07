using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Serialization;

namespace UnityEngine
{
	// Token: 0x0200027F RID: 639
	internal static class ClassLibraryInitializer
	{
		// Token: 0x0600256F RID: 9583 RVA: 0x0003378C File Offset: 0x0003198C
		private static void Init()
		{
			UnityLogWriter.Init();
			if (Application.platform.ToString().Contains("WebPlayer"))
			{
				BinaryFormatter.DefaultSurrogateSelector = new UnitySurrogateSelector();
			}
		}
	}
}
