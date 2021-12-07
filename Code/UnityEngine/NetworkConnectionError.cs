using System;

namespace UnityEngine
{
	// Token: 0x0200006D RID: 109
	public enum NetworkConnectionError
	{
		// Token: 0x04000126 RID: 294
		NoError,
		// Token: 0x04000127 RID: 295
		RSAPublicKeyMismatch = 21,
		// Token: 0x04000128 RID: 296
		InvalidPassword = 23,
		// Token: 0x04000129 RID: 297
		ConnectionFailed = 15,
		// Token: 0x0400012A RID: 298
		TooManyConnectedPlayers = 18,
		// Token: 0x0400012B RID: 299
		ConnectionBanned = 22,
		// Token: 0x0400012C RID: 300
		AlreadyConnectedToServer = 16,
		// Token: 0x0400012D RID: 301
		AlreadyConnectedToAnotherServer = -1,
		// Token: 0x0400012E RID: 302
		CreateSocketOrThreadFailure = -2,
		// Token: 0x0400012F RID: 303
		IncorrectParameters = -3,
		// Token: 0x04000130 RID: 304
		EmptyConnectTarget = -4,
		// Token: 0x04000131 RID: 305
		InternalDirectConnectFailed = -5,
		// Token: 0x04000132 RID: 306
		NATTargetNotConnected = 69,
		// Token: 0x04000133 RID: 307
		NATTargetConnectionLost = 71,
		// Token: 0x04000134 RID: 308
		NATPunchthroughFailed = 73
	}
}
