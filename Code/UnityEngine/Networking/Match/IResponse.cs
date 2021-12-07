using System;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200022F RID: 559
	public interface IResponse
	{
		// Token: 0x06002250 RID: 8784
		void SetSuccess();

		// Token: 0x06002251 RID: 8785
		void SetFailure(string info);
	}
}
