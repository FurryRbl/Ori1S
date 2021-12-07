using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000C1 RID: 193
	public sealed class LocationService
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B58 RID: 2904
		public extern bool isEnabledByUser { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B59 RID: 2905
		public extern LocationServiceStatus status { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B5A RID: 2906
		public extern LocationInfo lastData { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000B5B RID: 2907
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Start([DefaultValue("10f")] float desiredAccuracyInMeters, [DefaultValue("10f")] float updateDistanceInMeters);

		// Token: 0x06000B5C RID: 2908 RVA: 0x0000F288 File Offset: 0x0000D488
		[ExcludeFromDocs]
		public void Start(float desiredAccuracyInMeters)
		{
			float updateDistanceInMeters = 10f;
			this.Start(desiredAccuracyInMeters, updateDistanceInMeters);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		[ExcludeFromDocs]
		public void Start()
		{
			float updateDistanceInMeters = 10f;
			float desiredAccuracyInMeters = 10f;
			this.Start(desiredAccuracyInMeters, updateDistanceInMeters);
		}

		// Token: 0x06000B5E RID: 2910
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();
	}
}
