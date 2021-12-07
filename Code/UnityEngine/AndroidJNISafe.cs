using System;

namespace UnityEngine
{
	// Token: 0x02000271 RID: 625
	internal class AndroidJNISafe
	{
		// Token: 0x060024F1 RID: 9457 RVA: 0x00031B20 File Offset: 0x0002FD20
		public static void CheckException()
		{
			IntPtr intPtr = AndroidJNI.ExceptionOccurred();
			if (intPtr != IntPtr.Zero)
			{
				AndroidJNI.ExceptionClear();
				IntPtr intPtr2 = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr intPtr3 = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr methodID = AndroidJNI.GetMethodID(intPtr2, "toString", "()Ljava/lang/String;");
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr3, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string message = AndroidJNI.CallStringMethod(intPtr, methodID, new jvalue[0]);
					jvalue[] array = new jvalue[1];
					array[0].l = intPtr;
					string javaStackTrace = AndroidJNI.CallStaticStringMethod(intPtr3, staticMethodID, array);
					throw new AndroidJavaException(message, javaStackTrace);
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr2);
					AndroidJNISafe.DeleteLocalRef(intPtr3);
				}
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00031BF0 File Offset: 0x0002FDF0
		public static void DeleteGlobalRef(IntPtr globalref)
		{
			if (globalref != IntPtr.Zero)
			{
				AndroidJNI.DeleteGlobalRef(globalref);
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00031C08 File Offset: 0x0002FE08
		public static void DeleteLocalRef(IntPtr localref)
		{
			if (localref != IntPtr.Zero)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00031C20 File Offset: 0x0002FE20
		public static IntPtr NewStringUTF(string bytes)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.NewStringUTF(bytes);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00031C64 File Offset: 0x0002FE64
		public static string GetStringUTFChars(IntPtr str)
		{
			string stringUTFChars;
			try
			{
				stringUTFChars = AndroidJNI.GetStringUTFChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringUTFChars;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00031CA8 File Offset: 0x0002FEA8
		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			IntPtr objectClass;
			try
			{
				objectClass = AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectClass;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00031CEC File Offset: 0x0002FEEC
		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticMethodID;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00031D30 File Offset: 0x0002FF30
		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			IntPtr methodID;
			try
			{
				methodID = AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return methodID;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00031D74 File Offset: 0x0002FF74
		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr fieldID;
			try
			{
				fieldID = AndroidJNI.GetFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return fieldID;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00031DB8 File Offset: 0x0002FFB8
		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticFieldID;
			try
			{
				staticFieldID = AndroidJNI.GetStaticFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFieldID;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00031DFC File Offset: 0x0002FFFC
		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00031E40 File Offset: 0x00030040
		public static IntPtr FromReflectedField(IntPtr refField)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FromReflectedField(refField);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00031E84 File Offset: 0x00030084
		public static IntPtr FindClass(string name)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FindClass(name);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00031EC8 File Offset: 0x000300C8
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00031F0C File Offset: 0x0003010C
		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetStaticObjectField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x00031F48 File Offset: 0x00030148
		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStaticStringField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00031F84 File Offset: 0x00030184
		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetStaticCharField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00031FC0 File Offset: 0x000301C0
		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetStaticDoubleField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00031FFC File Offset: 0x000301FC
		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetStaticFloatField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00032038 File Offset: 0x00030238
		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetStaticLongField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00032074 File Offset: 0x00030274
		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetStaticShortField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000320B0 File Offset: 0x000302B0
		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
			try
			{
				AndroidJNI.SetStaticByteField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000320EC File Offset: 0x000302EC
		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetStaticBooleanField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00032128 File Offset: 0x00030328
		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetStaticIntField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00032164 File Offset: 0x00030364
		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			IntPtr staticObjectField;
			try
			{
				staticObjectField = AndroidJNI.GetStaticObjectField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticObjectField;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000321A8 File Offset: 0x000303A8
		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			string staticStringField;
			try
			{
				staticStringField = AndroidJNI.GetStaticStringField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticStringField;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000321EC File Offset: 0x000303EC
		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			char staticCharField;
			try
			{
				staticCharField = AndroidJNI.GetStaticCharField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticCharField;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x00032230 File Offset: 0x00030430
		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			double staticDoubleField;
			try
			{
				staticDoubleField = AndroidJNI.GetStaticDoubleField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticDoubleField;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00032274 File Offset: 0x00030474
		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			float staticFloatField;
			try
			{
				staticFloatField = AndroidJNI.GetStaticFloatField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFloatField;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000322B8 File Offset: 0x000304B8
		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			long staticLongField;
			try
			{
				staticLongField = AndroidJNI.GetStaticLongField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticLongField;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000322FC File Offset: 0x000304FC
		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			short staticShortField;
			try
			{
				staticShortField = AndroidJNI.GetStaticShortField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticShortField;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00032340 File Offset: 0x00030540
		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			byte staticByteField;
			try
			{
				staticByteField = AndroidJNI.GetStaticByteField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticByteField;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00032384 File Offset: 0x00030584
		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			bool staticBooleanField;
			try
			{
				staticBooleanField = AndroidJNI.GetStaticBooleanField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticBooleanField;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000323C8 File Offset: 0x000305C8
		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			int staticIntField;
			try
			{
				staticIntField = AndroidJNI.GetStaticIntField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticIntField;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0003240C File Offset: 0x0003060C
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallStaticVoidMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00032448 File Offset: 0x00030648
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x0003248C File Offset: 0x0003068C
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			string result;
			try
			{
				result = AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000324D0 File Offset: 0x000306D0
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			char result;
			try
			{
				result = AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00032514 File Offset: 0x00030714
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			double result;
			try
			{
				result = AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00032558 File Offset: 0x00030758
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			float result;
			try
			{
				result = AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0003259C File Offset: 0x0003079C
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			long result;
			try
			{
				result = AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000325E0 File Offset: 0x000307E0
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			short result;
			try
			{
				result = AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00032624 File Offset: 0x00030824
		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			byte result;
			try
			{
				result = AndroidJNI.CallStaticByteMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00032668 File Offset: 0x00030868
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			bool result;
			try
			{
				result = AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x000326AC File Offset: 0x000308AC
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			int result;
			try
			{
				result = AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000326F0 File Offset: 0x000308F0
		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetObjectField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0003272C File Offset: 0x0003092C
		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStringField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00032768 File Offset: 0x00030968
		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetCharField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000327A4 File Offset: 0x000309A4
		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetDoubleField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000327E0 File Offset: 0x000309E0
		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetFloatField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0003281C File Offset: 0x00030A1C
		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetLongField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00032858 File Offset: 0x00030A58
		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetShortField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00032894 File Offset: 0x00030A94
		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
			try
			{
				AndroidJNI.SetByteField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000328D0 File Offset: 0x00030AD0
		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetBooleanField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0003290C File Offset: 0x00030B0C
		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetIntField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x00032948 File Offset: 0x00030B48
		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			IntPtr objectField;
			try
			{
				objectField = AndroidJNI.GetObjectField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectField;
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x0003298C File Offset: 0x00030B8C
		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			string stringField;
			try
			{
				stringField = AndroidJNI.GetStringField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringField;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000329D0 File Offset: 0x00030BD0
		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			char charField;
			try
			{
				charField = AndroidJNI.GetCharField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return charField;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00032A14 File Offset: 0x00030C14
		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			double doubleField;
			try
			{
				doubleField = AndroidJNI.GetDoubleField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return doubleField;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00032A58 File Offset: 0x00030C58
		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			float floatField;
			try
			{
				floatField = AndroidJNI.GetFloatField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return floatField;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00032A9C File Offset: 0x00030C9C
		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			long longField;
			try
			{
				longField = AndroidJNI.GetLongField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return longField;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00032AE0 File Offset: 0x00030CE0
		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			short shortField;
			try
			{
				shortField = AndroidJNI.GetShortField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return shortField;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00032B24 File Offset: 0x00030D24
		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			byte byteField;
			try
			{
				byteField = AndroidJNI.GetByteField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return byteField;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00032B68 File Offset: 0x00030D68
		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			bool booleanField;
			try
			{
				booleanField = AndroidJNI.GetBooleanField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return booleanField;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x00032BAC File Offset: 0x00030DAC
		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			int intField;
			try
			{
				intField = AndroidJNI.GetIntField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intField;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00032BF0 File Offset: 0x00030DF0
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallVoidMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00032C2C File Offset: 0x00030E2C
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00032C70 File Offset: 0x00030E70
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			string result;
			try
			{
				result = AndroidJNI.CallStringMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00032CB4 File Offset: 0x00030EB4
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			char result;
			try
			{
				result = AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00032CF8 File Offset: 0x00030EF8
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			double result;
			try
			{
				result = AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00032D3C File Offset: 0x00030F3C
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			float result;
			try
			{
				result = AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00032D80 File Offset: 0x00030F80
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			long result;
			try
			{
				result = AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00032DC4 File Offset: 0x00030FC4
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			short result;
			try
			{
				result = AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00032E08 File Offset: 0x00031008
		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			byte result;
			try
			{
				result = AndroidJNI.CallByteMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x00032E4C File Offset: 0x0003104C
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			bool result;
			try
			{
				result = AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00032E90 File Offset: 0x00031090
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			int result;
			try
			{
				result = AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00032ED4 File Offset: 0x000310D4
		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			IntPtr[] result;
			try
			{
				result = AndroidJNI.FromObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00032F18 File Offset: 0x00031118
		public static char[] FromCharArray(IntPtr array)
		{
			char[] result;
			try
			{
				result = AndroidJNI.FromCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00032F5C File Offset: 0x0003115C
		public static double[] FromDoubleArray(IntPtr array)
		{
			double[] result;
			try
			{
				result = AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00032FA0 File Offset: 0x000311A0
		public static float[] FromFloatArray(IntPtr array)
		{
			float[] result;
			try
			{
				result = AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x00032FE4 File Offset: 0x000311E4
		public static long[] FromLongArray(IntPtr array)
		{
			long[] result;
			try
			{
				result = AndroidJNI.FromLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00033028 File Offset: 0x00031228
		public static short[] FromShortArray(IntPtr array)
		{
			short[] result;
			try
			{
				result = AndroidJNI.FromShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x0003306C File Offset: 0x0003126C
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] result;
			try
			{
				result = AndroidJNI.FromByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x000330B0 File Offset: 0x000312B0
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] result;
			try
			{
				result = AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000330F4 File Offset: 0x000312F4
		public static int[] FromIntArray(IntPtr array)
		{
			int[] result;
			try
			{
				result = AndroidJNI.FromIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00033138 File Offset: 0x00031338
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0003317C File Offset: 0x0003137C
		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x000331C0 File Offset: 0x000313C0
		public static IntPtr ToCharArray(char[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00033204 File Offset: 0x00031404
		public static IntPtr ToDoubleArray(double[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x00033248 File Offset: 0x00031448
		public static IntPtr ToFloatArray(float[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0003328C File Offset: 0x0003148C
		public static IntPtr ToLongArray(long[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000332D0 File Offset: 0x000314D0
		public static IntPtr ToShortArray(short[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00033314 File Offset: 0x00031514
		public static IntPtr ToByteArray(byte[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00033358 File Offset: 0x00031558
		public static IntPtr ToBooleanArray(bool[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x0003339C File Offset: 0x0003159C
		public static IntPtr ToIntArray(int[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000333E0 File Offset: 0x000315E0
		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			IntPtr objectArrayElement;
			try
			{
				objectArrayElement = AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectArrayElement;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x00033424 File Offset: 0x00031624
		public static int GetArrayLength(IntPtr array)
		{
			int arrayLength;
			try
			{
				arrayLength = AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return arrayLength;
		}
	}
}
