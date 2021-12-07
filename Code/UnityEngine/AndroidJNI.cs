using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000D5 RID: 213
	public sealed class AndroidJNI
	{
		// Token: 0x06000D66 RID: 3430 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		private AndroidJNI()
		{
		}

		// Token: 0x06000D67 RID: 3431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AttachCurrentThread();

		// Token: 0x06000D68 RID: 3432
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int DetachCurrentThread();

		// Token: 0x06000D69 RID: 3433
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersion();

		// Token: 0x06000D6A RID: 3434
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FindClass(string name);

		// Token: 0x06000D6B RID: 3435
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		// Token: 0x06000D6C RID: 3436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		// Token: 0x06000D6D RID: 3437
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		// Token: 0x06000D6E RID: 3438
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		// Token: 0x06000D6F RID: 3439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		// Token: 0x06000D70 RID: 3440
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		// Token: 0x06000D71 RID: 3441
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Throw(IntPtr obj);

		// Token: 0x06000D72 RID: 3442
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ThrowNew(IntPtr clazz, string message);

		// Token: 0x06000D73 RID: 3443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ExceptionOccurred();

		// Token: 0x06000D74 RID: 3444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionDescribe();

		// Token: 0x06000D75 RID: 3445
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionClear();

		// Token: 0x06000D76 RID: 3446
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FatalError(string message);

		// Token: 0x06000D77 RID: 3447
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PushLocalFrame(int capacity);

		// Token: 0x06000D78 RID: 3448
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr PopLocalFrame(IntPtr result);

		// Token: 0x06000D79 RID: 3449
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		// Token: 0x06000D7A RID: 3450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteGlobalRef(IntPtr obj);

		// Token: 0x06000D7B RID: 3451
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		// Token: 0x06000D7C RID: 3452
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteLocalRef(IntPtr obj);

		// Token: 0x06000D7D RID: 3453
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		// Token: 0x06000D7E RID: 3454
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int EnsureLocalCapacity(int capacity);

		// Token: 0x06000D7F RID: 3455
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocObject(IntPtr clazz);

		// Token: 0x06000D80 RID: 3456
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D81 RID: 3457
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		// Token: 0x06000D82 RID: 3458
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		// Token: 0x06000D83 RID: 3459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x06000D84 RID: 3460
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x06000D85 RID: 3461
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x06000D86 RID: 3462
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x06000D87 RID: 3463
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewStringUTF(string bytes);

		// Token: 0x06000D88 RID: 3464
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringUTFLength(IntPtr str);

		// Token: 0x06000D89 RID: 3465
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringUTFChars(IntPtr str);

		// Token: 0x06000D8A RID: 3466
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D8B RID: 3467
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D8C RID: 3468
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D8D RID: 3469
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D8E RID: 3470
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D8F RID: 3471
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D90 RID: 3472
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D91 RID: 3473
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D92 RID: 3474
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D93 RID: 3475
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D94 RID: 3476
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x06000D95 RID: 3477
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D96 RID: 3478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D97 RID: 3479
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D98 RID: 3480
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte GetByteField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D99 RID: 3481
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9A RID: 3482
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9B RID: 3483
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9C RID: 3484
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9D RID: 3485
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9E RID: 3486
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		// Token: 0x06000D9F RID: 3487
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStringField(IntPtr obj, IntPtr fieldID, string val);

		// Token: 0x06000DA0 RID: 3488
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		// Token: 0x06000DA1 RID: 3489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		// Token: 0x06000DA2 RID: 3490
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetByteField(IntPtr obj, IntPtr fieldID, byte val);

		// Token: 0x06000DA3 RID: 3491
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		// Token: 0x06000DA4 RID: 3492
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		// Token: 0x06000DA5 RID: 3493
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		// Token: 0x06000DA6 RID: 3494
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		// Token: 0x06000DA7 RID: 3495
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		// Token: 0x06000DA8 RID: 3496
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		// Token: 0x06000DA9 RID: 3497
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAA RID: 3498
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAB RID: 3499
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAC RID: 3500
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAD RID: 3501
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAE RID: 3502
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DAF RID: 3503
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DB0 RID: 3504
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DB1 RID: 3505
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DB2 RID: 3506
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DB3 RID: 3507
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x06000DB4 RID: 3508
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStaticStringField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DB5 RID: 3509
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DB6 RID: 3510
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DB7 RID: 3511
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte GetStaticByteField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DB8 RID: 3512
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DB9 RID: 3513
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DBA RID: 3514
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DBB RID: 3515
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DBC RID: 3516
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DBD RID: 3517
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x06000DBE RID: 3518
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val);

		// Token: 0x06000DBF RID: 3519
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		// Token: 0x06000DC0 RID: 3520
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		// Token: 0x06000DC1 RID: 3521
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val);

		// Token: 0x06000DC2 RID: 3522
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		// Token: 0x06000DC3 RID: 3523
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		// Token: 0x06000DC4 RID: 3524
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		// Token: 0x06000DC5 RID: 3525
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		// Token: 0x06000DC6 RID: 3526
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		// Token: 0x06000DC7 RID: 3527
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		// Token: 0x06000DC8 RID: 3528
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToBooleanArray(bool[] array);

		// Token: 0x06000DC9 RID: 3529
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToByteArray(byte[] array);

		// Token: 0x06000DCA RID: 3530
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToCharArray(char[] array);

		// Token: 0x06000DCB RID: 3531
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToShortArray(short[] array);

		// Token: 0x06000DCC RID: 3532
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToIntArray(int[] array);

		// Token: 0x06000DCD RID: 3533
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToLongArray(long[] array);

		// Token: 0x06000DCE RID: 3534
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToFloatArray(float[] array);

		// Token: 0x06000DCF RID: 3535
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToDoubleArray(double[] array);

		// Token: 0x06000DD0 RID: 3536
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass);

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00011DFC File Offset: 0x0000FFFC
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return AndroidJNI.ToObjectArray(array, IntPtr.Zero);
		}

		// Token: 0x06000DD2 RID: 3538
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool[] FromBooleanArray(IntPtr array);

		// Token: 0x06000DD3 RID: 3539
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] FromByteArray(IntPtr array);

		// Token: 0x06000DD4 RID: 3540
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char[] FromCharArray(IntPtr array);

		// Token: 0x06000DD5 RID: 3541
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short[] FromShortArray(IntPtr array);

		// Token: 0x06000DD6 RID: 3542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int[] FromIntArray(IntPtr array);

		// Token: 0x06000DD7 RID: 3543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long[] FromLongArray(IntPtr array);

		// Token: 0x06000DD8 RID: 3544
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float[] FromFloatArray(IntPtr array);

		// Token: 0x06000DD9 RID: 3545
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double[] FromDoubleArray(IntPtr array);

		// Token: 0x06000DDA RID: 3546
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr[] FromObjectArray(IntPtr array);

		// Token: 0x06000DDB RID: 3547
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetArrayLength(IntPtr array);

		// Token: 0x06000DDC RID: 3548
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewBooleanArray(int size);

		// Token: 0x06000DDD RID: 3549
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewByteArray(int size);

		// Token: 0x06000DDE RID: 3550
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewCharArray(int size);

		// Token: 0x06000DDF RID: 3551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewShortArray(int size);

		// Token: 0x06000DE0 RID: 3552
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewIntArray(int size);

		// Token: 0x06000DE1 RID: 3553
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLongArray(int size);

		// Token: 0x06000DE2 RID: 3554
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewFloatArray(int size);

		// Token: 0x06000DE3 RID: 3555
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewDoubleArray(int size);

		// Token: 0x06000DE4 RID: 3556
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		// Token: 0x06000DE5 RID: 3557
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		// Token: 0x06000DE6 RID: 3558
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte GetByteArrayElement(IntPtr array, int index);

		// Token: 0x06000DE7 RID: 3559
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		// Token: 0x06000DE8 RID: 3560
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		// Token: 0x06000DE9 RID: 3561
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		// Token: 0x06000DEA RID: 3562
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		// Token: 0x06000DEB RID: 3563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		// Token: 0x06000DEC RID: 3564
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		// Token: 0x06000DED RID: 3565
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		// Token: 0x06000DEE RID: 3566
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, byte val);

		// Token: 0x06000DEF RID: 3567
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetByteArrayElement(IntPtr array, int index, sbyte val);

		// Token: 0x06000DF0 RID: 3568
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		// Token: 0x06000DF1 RID: 3569
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		// Token: 0x06000DF2 RID: 3570
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		// Token: 0x06000DF3 RID: 3571
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		// Token: 0x06000DF4 RID: 3572
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		// Token: 0x06000DF5 RID: 3573
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		// Token: 0x06000DF6 RID: 3574
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);
	}
}
