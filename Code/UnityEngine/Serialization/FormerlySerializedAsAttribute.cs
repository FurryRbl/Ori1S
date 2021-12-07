using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x02000330 RID: 816
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	[RequiredByNativeCode]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x0600282F RID: 10287 RVA: 0x00039700 File Offset: 0x00037900
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x00039710 File Offset: 0x00037910
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x04000C57 RID: 3159
		private string m_oldName;
	}
}
