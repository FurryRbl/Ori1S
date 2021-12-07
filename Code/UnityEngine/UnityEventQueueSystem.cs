using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A7 RID: 167
	public sealed class UnityEventQueueSystem
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		public static string GenerateEventIdForPayload(string eventPayloadName)
		{
			byte[] array = Guid.NewGuid().ToByteArray();
			return string.Format("REGISTER_EVENT_ID(0x{0:X2}{1:X2}{2:X2}{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}ULL,0x{8:X2}{9:X2}{10:X2}{11:X2}{12:X2}{13:X2}{14:X2}{15:X2}ULL,{16})", new object[]
			{
				array[0],
				array[1],
				array[2],
				array[3],
				array[4],
				array[5],
				array[6],
				array[7],
				array[8],
				array[9],
				array[10],
				array[11],
				array[12],
				array[13],
				array[14],
				array[15],
				eventPayloadName
			});
		}

		// Token: 0x06000994 RID: 2452
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetGlobalEventQueue();
	}
}
