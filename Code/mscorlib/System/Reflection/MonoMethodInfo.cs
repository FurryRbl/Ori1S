using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020002A7 RID: 679
	internal struct MonoMethodInfo
	{
		// Token: 0x0600228D RID: 8845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x0600228E RID: 8846 RVA: 0x0007CA18 File Offset: 0x0007AC18
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo result;
			MonoMethodInfo.get_method_info(handle, out result);
			return result;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007CA30 File Offset: 0x0007AC30
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007CA4C File Offset: 0x0007AC4C
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007CA68 File Offset: 0x0007AC68
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).attrs;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007CA84 File Offset: 0x0007AC84
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x06002294 RID: 8852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06002295 RID: 8853 RVA: 0x0007CABC File Offset: 0x0007ACBC
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x06002296 RID: 8854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnmanagedMarshal get_retval_marshal(IntPtr handle);

		// Token: 0x06002297 RID: 8855 RVA: 0x0007CAC8 File Offset: 0x0007ACC8
		internal static ParameterInfo GetReturnParameterInfo(MonoMethod method)
		{
			return new ParameterInfo(MonoMethodInfo.GetReturnType(method.mhandle), method, MonoMethodInfo.get_retval_marshal(method.mhandle));
		}

		// Token: 0x04000CE8 RID: 3304
		private Type parent;

		// Token: 0x04000CE9 RID: 3305
		private Type ret;

		// Token: 0x04000CEA RID: 3306
		internal MethodAttributes attrs;

		// Token: 0x04000CEB RID: 3307
		internal MethodImplAttributes iattrs;

		// Token: 0x04000CEC RID: 3308
		private CallingConventions callconv;
	}
}
