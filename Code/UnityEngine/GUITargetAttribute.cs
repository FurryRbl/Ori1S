using System;
using System.Reflection;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000215 RID: 533
	[AttributeUsage(AttributeTargets.Method)]
	public class GUITargetAttribute : Attribute
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x00027074 File Offset: 0x00025274
		public GUITargetAttribute()
		{
			this.displayMask = -1;
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00027084 File Offset: 0x00025284
		public GUITargetAttribute(int displayIndex)
		{
			this.displayMask = 1 << displayIndex;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00027098 File Offset: 0x00025298
		public GUITargetAttribute(int displayIndex, int displayIndex1)
		{
			this.displayMask = (1 << displayIndex | 1 << displayIndex1);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x000270B4 File Offset: 0x000252B4
		public GUITargetAttribute(int displayIndex, int displayIndex1, params int[] displayIndexList)
		{
			this.displayMask = (1 << displayIndex | 1 << displayIndex1);
			for (int i = 0; i < displayIndexList.Length; i++)
			{
				this.displayMask |= 1 << displayIndexList[i];
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00027108 File Offset: 0x00025308
		[RequiredByNativeCode]
		private static int GetGUITargetAttrValue(Type klass, string methodName)
		{
			MethodInfo method = klass.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null)
			{
				object[] customAttributes = method.GetCustomAttributes(true);
				if (customAttributes != null)
				{
					for (int i = 0; i < customAttributes.Length; i++)
					{
						if (customAttributes[i].GetType() == typeof(GUITargetAttribute))
						{
							GUITargetAttribute guitargetAttribute = customAttributes[i] as GUITargetAttribute;
							return guitargetAttribute.displayMask;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x04000844 RID: 2116
		internal int displayMask;
	}
}
