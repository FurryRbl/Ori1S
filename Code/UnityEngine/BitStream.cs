using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000078 RID: 120
	[RequiredByNativeCode]
	public sealed class BitStream
	{
		// Token: 0x06000753 RID: 1875
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializeb(ref int value);

		// Token: 0x06000754 RID: 1876
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializec(ref char value);

		// Token: 0x06000755 RID: 1877
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializes(ref short value);

		// Token: 0x06000756 RID: 1878
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializei(ref int value);

		// Token: 0x06000757 RID: 1879
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serializef(ref float value, float maximumDelta);

		// Token: 0x06000758 RID: 1880 RVA: 0x0000A8EC File Offset: 0x00008AEC
		private void Serializeq(ref Quaternion value, float maximumDelta)
		{
			BitStream.INTERNAL_CALL_Serializeq(this, ref value, maximumDelta);
		}

		// Token: 0x06000759 RID: 1881
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializeq(BitStream self, ref Quaternion value, float maximumDelta);

		// Token: 0x0600075A RID: 1882 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		private void Serializev(ref Vector3 value, float maximumDelta)
		{
			BitStream.INTERNAL_CALL_Serializev(this, ref value, maximumDelta);
		}

		// Token: 0x0600075B RID: 1883
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializev(BitStream self, ref Vector3 value, float maximumDelta);

		// Token: 0x0600075C RID: 1884 RVA: 0x0000A904 File Offset: 0x00008B04
		private void Serializen(ref NetworkViewID viewID)
		{
			BitStream.INTERNAL_CALL_Serializen(this, ref viewID);
		}

		// Token: 0x0600075D RID: 1885
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Serializen(BitStream self, ref NetworkViewID viewID);

		// Token: 0x0600075E RID: 1886 RVA: 0x0000A910 File Offset: 0x00008B10
		public void Serialize(ref bool value)
		{
			int num = (!value) ? 0 : 1;
			this.Serializeb(ref num);
			value = (num != 0);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000A944 File Offset: 0x00008B44
		public void Serialize(ref char value)
		{
			this.Serializec(ref value);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000A950 File Offset: 0x00008B50
		public void Serialize(ref short value)
		{
			this.Serializes(ref value);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000A95C File Offset: 0x00008B5C
		public void Serialize(ref int value)
		{
			this.Serializei(ref value);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000A968 File Offset: 0x00008B68
		[ExcludeFromDocs]
		public void Serialize(ref float value)
		{
			float maxDelta = 1E-05f;
			this.Serialize(ref value, maxDelta);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000A984 File Offset: 0x00008B84
		public void Serialize(ref float value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializef(ref value, maxDelta);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000A990 File Offset: 0x00008B90
		[ExcludeFromDocs]
		public void Serialize(ref Quaternion value)
		{
			float maxDelta = 1E-05f;
			this.Serialize(ref value, maxDelta);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public void Serialize(ref Quaternion value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializeq(ref value, maxDelta);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		[ExcludeFromDocs]
		public void Serialize(ref Vector3 value)
		{
			float maxDelta = 1E-05f;
			this.Serialize(ref value, maxDelta);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000A9D4 File Offset: 0x00008BD4
		public void Serialize(ref Vector3 value, [DefaultValue("0.00001F")] float maxDelta)
		{
			this.Serializev(ref value, maxDelta);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public void Serialize(ref NetworkPlayer value)
		{
			int index = value.index;
			this.Serializei(ref index);
			value.index = index;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000AA04 File Offset: 0x00008C04
		public void Serialize(ref NetworkViewID viewID)
		{
			this.Serializen(ref viewID);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600076A RID: 1898
		public extern bool isReading { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600076B RID: 1899
		public extern bool isWriting { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600076C RID: 1900
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Serialize(ref string value);

		// Token: 0x04000150 RID: 336
		internal IntPtr m_Ptr;
	}
}
