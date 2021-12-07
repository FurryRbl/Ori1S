using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000D4 RID: 212
	[UsedByNativeCode]
	public sealed class AndroidJNIHelper
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		private AndroidJNIHelper()
		{
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D4F RID: 3407
		// (set) Token: 0x06000D50 RID: 3408
		public static extern bool debug { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000D51 RID: 3409 RVA: 0x00011CD0 File Offset: 0x0000FED0
		[ExcludeFromDocs]
		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			string empty = string.Empty;
			return AndroidJNIHelper.GetConstructorID(javaClass, empty);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("\"\"")] string signature)
		{
			return _AndroidJNIHelper.GetConstructorID(javaClass, signature);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00011CF8 File Offset: 0x0000FEF8
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, string signature)
		{
			bool isStatic = false;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00011D10 File Offset: 0x0000FF10
		[ExcludeFromDocs]
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			bool isStatic = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetMethodID(javaClass, methodName, empty, isStatic);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00011D30 File Offset: 0x0000FF30
		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(javaClass, methodName, signature, isStatic);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00011D3C File Offset: 0x0000FF3C
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, string signature)
		{
			bool isStatic = false;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00011D54 File Offset: 0x0000FF54
		[ExcludeFromDocs]
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			bool isStatic = false;
			string empty = string.Empty;
			return AndroidJNIHelper.GetFieldID(javaClass, fieldName, empty, isStatic);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00011D74 File Offset: 0x0000FF74
		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("\"\"")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID(javaClass, fieldName, signature, isStatic);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00011D80 File Offset: 0x0000FF80
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return _AndroidJNIHelper.CreateJavaRunnable(jrunnable);
		}

		// Token: 0x06000D5A RID: 3418
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateJavaProxy(AndroidJavaProxy proxy);

		// Token: 0x06000D5B RID: 3419 RVA: 0x00011D88 File Offset: 0x0000FF88
		public static IntPtr ConvertToJNIArray(Array array)
		{
			return _AndroidJNIHelper.ConvertToJNIArray(array);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00011D90 File Offset: 0x0000FF90
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return _AndroidJNIHelper.CreateJNIArgArray(args);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00011D98 File Offset: 0x0000FF98
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			_AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return _AndroidJNIHelper.GetConstructorID(jclass, args);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00011DBC File Offset: 0x0000FFBC
		public static string GetSignature(object obj)
		{
			return _AndroidJNIHelper.GetSignature(obj);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00011DC4 File Offset: 0x0000FFC4
		public static string GetSignature(object[] args)
		{
			return _AndroidJNIHelper.GetSignature(args);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return _AndroidJNIHelper.ConvertFromJNIArray<ArrayType>(array);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return _AndroidJNIHelper.GetMethodID<ReturnType>(jclass, methodName, args, isStatic);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return _AndroidJNIHelper.GetFieldID<FieldType>(jclass, fieldName, isStatic);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00011DEC File Offset: 0x0000FFEC
		public static string GetSignature<ReturnType>(object[] args)
		{
			return _AndroidJNIHelper.GetSignature<ReturnType>(args);
		}
	}
}
