using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000089 RID: 137
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x0600082A RID: 2090
		void OnBeforeSerialize();

		// Token: 0x0600082B RID: 2091
		void OnAfterDeserialize();
	}
}
