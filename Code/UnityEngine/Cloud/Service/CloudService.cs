using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Cloud.Service
{
	// Token: 0x0200026B RID: 619
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class CloudService : IDisposable
	{
		// Token: 0x060024B5 RID: 9397 RVA: 0x00030034 File Offset: 0x0002E234
		public CloudService(CloudServiceType serviceType)
		{
			this.InternalCreate(serviceType);
		}

		// Token: 0x060024B6 RID: 9398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalCreate(CloudServiceType serviceType);

		// Token: 0x060024B7 RID: 9399
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InternalDestroy();

		// Token: 0x060024B8 RID: 9400 RVA: 0x00030044 File Offset: 0x0002E244
		~CloudService()
		{
			this.InternalDestroy();
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x00030080 File Offset: 0x0002E280
		public void Dispose()
		{
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060024BA RID: 9402
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Initialize(string projectId);

		// Token: 0x060024BB RID: 9403
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool StartEventHandler(string sessionInfo, int maxNumberOfEventInQueue, int maxEventTimeoutInSec);

		// Token: 0x060024BC RID: 9404
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool PauseEventHandler(bool flushEvents);

		// Token: 0x060024BD RID: 9405
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool StopEventHandler();

		// Token: 0x060024BE RID: 9406 RVA: 0x00030090 File Offset: 0x0002E290
		public bool StartEventDispatcher(CloudServiceConfig serviceConfig, Dictionary<string, string> headers)
		{
			return this.InternalStartEventDispatcher(serviceConfig, CloudService.FlattenedHeadersFrom(headers));
		}

		// Token: 0x060024BF RID: 9407
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalStartEventDispatcher(CloudServiceConfig serviceConfig, string[] headers);

		// Token: 0x060024C0 RID: 9408
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool PauseEventDispatcher();

		// Token: 0x060024C1 RID: 9409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool StopEventDispatcher();

		// Token: 0x060024C2 RID: 9410
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetNetworkRetryIndex();

		// Token: 0x060024C3 RID: 9411
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool QueueEvent(string eventData, CloudEventFlags flags);

		// Token: 0x060024C4 RID: 9412 RVA: 0x000300A0 File Offset: 0x0002E2A0
		public bool SaveFileFromServer(string fileName, string url, Dictionary<string, string> headers, object d, string methodName)
		{
			if (methodName == null)
			{
				methodName = string.Empty;
			}
			return this.InternalSaveFileFromServer(fileName, url, CloudService.FlattenedHeadersFrom(headers), d, methodName);
		}

		// Token: 0x060024C5 RID: 9413
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalSaveFileFromServer(string fileName, string url, string[] headers, object d, string methodName);

		// Token: 0x060024C6 RID: 9414
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SaveFile(string fileName, string data);

		// Token: 0x060024C7 RID: 9415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string RestoreFile(string fileName);

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060024C8 RID: 9416
		public extern string serviceFolderName { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060024C9 RID: 9417 RVA: 0x000300D0 File Offset: 0x0002E2D0
		private static string[] FlattenedHeadersFrom(Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return null;
			}
			string[] array = new string[headers.Count * 2];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				array[num++] = keyValuePair.Key.ToString();
				array[num++] = keyValuePair.Value.ToString();
			}
			return array;
		}

		// Token: 0x040009C8 RID: 2504
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
