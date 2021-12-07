using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000055 RID: 85
	[DisallowMultipleComponent]
	[AddComponentMenu("Network/NetworkStartPosition")]
	public class NetworkStartPosition : MonoBehaviour
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x00016038 File Offset: 0x00014238
		public void Awake()
		{
			NetworkManager.RegisterStartPosition(base.transform);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00016048 File Offset: 0x00014248
		public void OnDestroy()
		{
			NetworkManager.UnRegisterStartPosition(base.transform);
		}
	}
}
