using System;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000270 RID: 624
	[UsedByNativeCode]
	internal sealed class _AndroidJNIHelper
	{
		// Token: 0x060024DC RID: 9436 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public static IntPtr CreateJavaProxy(int delegateHandle, AndroidJavaProxy proxy)
		{
			return AndroidReflection.NewProxyInstance(delegateHandle, proxy.javaInterface.GetRawClass());
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000306DC File Offset: 0x0002E8DC
		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return AndroidJNIHelper.CreateJavaProxy(new AndroidJavaRunnableProxy(jrunnable));
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000306EC File Offset: 0x0002E8EC
		public static IntPtr InvokeJavaProxyMethod(AndroidJavaProxy proxy, IntPtr jmethodName, IntPtr jargs)
		{
			int num = 0;
			if (jargs != IntPtr.Zero)
			{
				num = AndroidJNISafe.GetArrayLength(jargs);
			}
			AndroidJavaObject[] array = new AndroidJavaObject[num];
			for (int i = 0; i < num; i++)
			{
				IntPtr objectArrayElement = AndroidJNISafe.GetObjectArrayElement(jargs, i);
				array[i] = ((!(objectArrayElement != IntPtr.Zero)) ? null : new AndroidJavaObject(objectArrayElement));
			}
			IntPtr result;
			using (AndroidJavaObject androidJavaObject = proxy.Invoke(AndroidJNI.GetStringUTFChars(jmethodName), array))
			{
				if (androidJavaObject == null)
				{
					result = IntPtr.Zero;
				}
				else
				{
					result = AndroidJNI.NewLocalRef(androidJavaObject.GetRawObject());
				}
			}
			return result;
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000307BC File Offset: 0x0002E9BC
		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			jvalue[] array = new jvalue[args.GetLength(0)];
			int num = 0;
			foreach (object obj in args)
			{
				if (obj == null)
				{
					array[num].l = IntPtr.Zero;
				}
				else if (AndroidReflection.IsPrimitive(obj.GetType()))
				{
					if (obj is int)
					{
						array[num].i = (int)obj;
					}
					else if (obj is bool)
					{
						array[num].z = (bool)obj;
					}
					else if (obj is byte)
					{
						array[num].b = (byte)obj;
					}
					else if (obj is short)
					{
						array[num].s = (short)obj;
					}
					else if (obj is long)
					{
						array[num].j = (long)obj;
					}
					else if (obj is float)
					{
						array[num].f = (float)obj;
					}
					else if (obj is double)
					{
						array[num].d = (double)obj;
					}
					else if (obj is char)
					{
						array[num].c = (char)obj;
					}
				}
				else if (obj is string)
				{
					array[num].l = AndroidJNISafe.NewStringUTF((string)obj);
				}
				else if (obj is AndroidJavaClass)
				{
					array[num].l = ((AndroidJavaClass)obj).GetRawClass();
				}
				else if (obj is AndroidJavaObject)
				{
					array[num].l = ((AndroidJavaObject)obj).GetRawObject();
				}
				else if (obj is Array)
				{
					array[num].l = _AndroidJNIHelper.ConvertToJNIArray((Array)obj);
				}
				else if (obj is AndroidJavaProxy)
				{
					array[num].l = AndroidJNIHelper.CreateJavaProxy((AndroidJavaProxy)obj);
				}
				else
				{
					if (!(obj is AndroidJavaRunnable))
					{
						throw new Exception("JNI; Unknown argument type '" + obj.GetType() + "'");
					}
					array[num].l = AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj);
				}
				num++;
			}
			return array;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00030A40 File Offset: 0x0002EC40
		public static object UnboxArray(AndroidJavaObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("java/lang/reflect/Array");
			AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", new object[0]);
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getComponentType", new object[0]);
			string text = androidJavaObject2.Call<string>("getName", new object[0]);
			int num = androidJavaClass.Call<int>("getLength", new object[]
			{
				obj
			});
			Array array;
			if (androidJavaObject2.Call<bool>("IsPrimitive", new object[0]))
			{
				if ("I" == text)
				{
					array = new int[num];
				}
				else if ("Z" == text)
				{
					array = new bool[num];
				}
				else if ("B" == text)
				{
					array = new byte[num];
				}
				else if ("S" == text)
				{
					array = new short[num];
				}
				else if ("J" == text)
				{
					array = new long[num];
				}
				else if ("F" == text)
				{
					array = new float[num];
				}
				else if ("D" == text)
				{
					array = new double[num];
				}
				else
				{
					if (!("C" == text))
					{
						throw new Exception("JNI; Unknown argument type '" + text + "'");
					}
					array = new char[num];
				}
			}
			else if ("java.lang.String" == text)
			{
				array = new string[num];
			}
			else if ("java.lang.Class" == text)
			{
				array = new AndroidJavaClass[num];
			}
			else
			{
				array = new AndroidJavaObject[num];
			}
			for (int i = 0; i < num; i++)
			{
				array.SetValue(_AndroidJNIHelper.Unbox(androidJavaClass.CallStatic<AndroidJavaObject>("get", new object[]
				{
					obj,
					i
				})), i);
			}
			return array;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00030C5C File Offset: 0x0002EE5C
		public static object Unbox(AndroidJavaObject obj)
		{
			if (obj == null)
			{
				return null;
			}
			AndroidJavaObject androidJavaObject = obj.Call<AndroidJavaObject>("getClass", new object[0]);
			string b = androidJavaObject.Call<string>("getName", new object[0]);
			if ("java.lang.Integer" == b)
			{
				return obj.Call<int>("intValue", new object[0]);
			}
			if ("java.lang.Boolean" == b)
			{
				return obj.Call<bool>("booleanValue", new object[0]);
			}
			if ("java.lang.Byte" == b)
			{
				return obj.Call<byte>("byteValue", new object[0]);
			}
			if ("java.lang.Short" == b)
			{
				return obj.Call<short>("shortValue", new object[0]);
			}
			if ("java.lang.Long" == b)
			{
				return obj.Call<long>("longValue", new object[0]);
			}
			if ("java.lang.Float" == b)
			{
				return obj.Call<float>("floatValue", new object[0]);
			}
			if ("java.lang.Double" == b)
			{
				return obj.Call<double>("doubleValue", new object[0]);
			}
			if ("java.lang.Character" == b)
			{
				return obj.Call<char>("charValue", new object[0]);
			}
			if ("java.lang.String" == b)
			{
				return obj.Call<string>("toString", new object[0]);
			}
			if ("java.lang.Class" == b)
			{
				return new AndroidJavaClass(obj.GetRawObject());
			}
			if (androidJavaObject.Call<bool>("isArray", new object[0]))
			{
				return _AndroidJNIHelper.UnboxArray(obj);
			}
			return obj;
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00030E2C File Offset: 0x0002F02C
		public static AndroidJavaObject Box(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (AndroidReflection.IsPrimitive(obj.GetType()))
			{
				if (obj is int)
				{
					return new AndroidJavaObject("java.lang.Integer", new object[]
					{
						(int)obj
					});
				}
				if (obj is bool)
				{
					return new AndroidJavaObject("java.lang.Boolean", new object[]
					{
						(bool)obj
					});
				}
				if (obj is byte)
				{
					return new AndroidJavaObject("java.lang.Byte", new object[]
					{
						(byte)obj
					});
				}
				if (obj is short)
				{
					return new AndroidJavaObject("java.lang.Short", new object[]
					{
						(short)obj
					});
				}
				if (obj is long)
				{
					return new AndroidJavaObject("java.lang.Long", new object[]
					{
						(long)obj
					});
				}
				if (obj is float)
				{
					return new AndroidJavaObject("java.lang.Float", new object[]
					{
						(float)obj
					});
				}
				if (obj is double)
				{
					return new AndroidJavaObject("java.lang.Double", new object[]
					{
						(double)obj
					});
				}
				if (obj is char)
				{
					return new AndroidJavaObject("java.lang.Character", new object[]
					{
						(char)obj
					});
				}
				throw new Exception("JNI; Unknown argument type '" + obj.GetType() + "'");
			}
			else
			{
				if (obj is string)
				{
					return new AndroidJavaObject("java.lang.String", new object[]
					{
						(string)obj
					});
				}
				if (obj is AndroidJavaClass)
				{
					return new AndroidJavaObject(((AndroidJavaClass)obj).GetRawClass());
				}
				if (obj is AndroidJavaObject)
				{
					return (AndroidJavaObject)obj;
				}
				if (obj is Array)
				{
					return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(_AndroidJNIHelper.ConvertToJNIArray((Array)obj));
				}
				if (obj is AndroidJavaProxy)
				{
					return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaProxy((AndroidJavaProxy)obj));
				}
				if (obj is AndroidJavaRunnable)
				{
					return AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaRunnable((AndroidJavaRunnable)obj));
				}
				throw new Exception("JNI; Unknown argument type '" + obj.GetType() + "'");
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00031080 File Offset: 0x0002F280
		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
			int num = 0;
			foreach (object obj in args)
			{
				if (obj is string || obj is AndroidJavaRunnable || obj is AndroidJavaProxy || obj is Array)
				{
					AndroidJNISafe.DeleteLocalRef(jniArgs[num].l);
				}
				num++;
			}
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000310EC File Offset: 0x0002F2EC
		public static IntPtr ConvertToJNIArray(Array array)
		{
			Type elementType = array.GetType().GetElementType();
			if (AndroidReflection.IsPrimitive(elementType))
			{
				if (elementType == typeof(int))
				{
					return AndroidJNISafe.ToIntArray((int[])array);
				}
				if (elementType == typeof(bool))
				{
					return AndroidJNISafe.ToBooleanArray((bool[])array);
				}
				if (elementType == typeof(byte))
				{
					return AndroidJNISafe.ToByteArray((byte[])array);
				}
				if (elementType == typeof(short))
				{
					return AndroidJNISafe.ToShortArray((short[])array);
				}
				if (elementType == typeof(long))
				{
					return AndroidJNISafe.ToLongArray((long[])array);
				}
				if (elementType == typeof(float))
				{
					return AndroidJNISafe.ToFloatArray((float[])array);
				}
				if (elementType == typeof(double))
				{
					return AndroidJNISafe.ToDoubleArray((double[])array);
				}
				if (elementType == typeof(char))
				{
					return AndroidJNISafe.ToCharArray((char[])array);
				}
				return IntPtr.Zero;
			}
			else
			{
				if (elementType == typeof(string))
				{
					string[] array2 = (string[])array;
					int length = array.GetLength(0);
					IntPtr intPtr = AndroidJNISafe.FindClass("java/lang/String");
					IntPtr intPtr2 = AndroidJNI.NewObjectArray(length, intPtr, IntPtr.Zero);
					for (int i = 0; i < length; i++)
					{
						IntPtr intPtr3 = AndroidJNISafe.NewStringUTF(array2[i]);
						AndroidJNI.SetObjectArrayElement(intPtr2, i, intPtr3);
						AndroidJNISafe.DeleteLocalRef(intPtr3);
					}
					AndroidJNISafe.DeleteLocalRef(intPtr);
					return intPtr2;
				}
				if (elementType == typeof(AndroidJavaObject))
				{
					AndroidJavaObject[] array3 = (AndroidJavaObject[])array;
					int length2 = array.GetLength(0);
					IntPtr[] array4 = new IntPtr[length2];
					IntPtr intPtr4 = AndroidJNISafe.FindClass("java/lang/Object");
					IntPtr intPtr5 = IntPtr.Zero;
					for (int j = 0; j < length2; j++)
					{
						if (array3[j] != null)
						{
							array4[j] = array3[j].GetRawObject();
							IntPtr rawClass = array3[j].GetRawClass();
							if (intPtr5 != rawClass)
							{
								if (intPtr5 == IntPtr.Zero)
								{
									intPtr5 = rawClass;
								}
								else
								{
									intPtr5 = intPtr4;
								}
							}
						}
						else
						{
							array4[j] = IntPtr.Zero;
						}
					}
					IntPtr result = AndroidJNISafe.ToObjectArray(array4, intPtr5);
					AndroidJNISafe.DeleteLocalRef(intPtr4);
					return result;
				}
				throw new Exception("JNI; Unknown array type '" + elementType + "'");
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x00031360 File Offset: 0x0002F560
		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			Type elementType = typeof(ArrayType).GetElementType();
			if (AndroidReflection.IsPrimitive(elementType))
			{
				if (elementType == typeof(int))
				{
					return (ArrayType)((object)AndroidJNISafe.FromIntArray(array));
				}
				if (elementType == typeof(bool))
				{
					return (ArrayType)((object)AndroidJNISafe.FromBooleanArray(array));
				}
				if (elementType == typeof(byte))
				{
					return (ArrayType)((object)AndroidJNISafe.FromByteArray(array));
				}
				if (elementType == typeof(short))
				{
					return (ArrayType)((object)AndroidJNISafe.FromShortArray(array));
				}
				if (elementType == typeof(long))
				{
					return (ArrayType)((object)AndroidJNISafe.FromLongArray(array));
				}
				if (elementType == typeof(float))
				{
					return (ArrayType)((object)AndroidJNISafe.FromFloatArray(array));
				}
				if (elementType == typeof(double))
				{
					return (ArrayType)((object)AndroidJNISafe.FromDoubleArray(array));
				}
				if (elementType == typeof(char))
				{
					return (ArrayType)((object)AndroidJNISafe.FromCharArray(array));
				}
				return default(ArrayType);
			}
			else
			{
				if (elementType == typeof(string))
				{
					int arrayLength = AndroidJNISafe.GetArrayLength(array);
					string[] array2 = new string[arrayLength];
					for (int i = 0; i < arrayLength; i++)
					{
						IntPtr objectArrayElement = AndroidJNI.GetObjectArrayElement(array, i);
						array2[i] = AndroidJNISafe.GetStringUTFChars(objectArrayElement);
						AndroidJNISafe.DeleteLocalRef(objectArrayElement);
					}
					return (ArrayType)((object)array2);
				}
				if (elementType == typeof(AndroidJavaObject))
				{
					int arrayLength2 = AndroidJNISafe.GetArrayLength(array);
					AndroidJavaObject[] array3 = new AndroidJavaObject[arrayLength2];
					for (int j = 0; j < arrayLength2; j++)
					{
						IntPtr objectArrayElement2 = AndroidJNI.GetObjectArrayElement(array, j);
						array3[j] = new AndroidJavaObject(objectArrayElement2);
						AndroidJNISafe.DeleteLocalRef(objectArrayElement2);
					}
					return (ArrayType)((object)array3);
				}
				throw new Exception("JNI: Unknown generic array type '" + elementType + "'");
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0003153C File Offset: 0x0002F73C
		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return AndroidJNIHelper.GetConstructorID(jclass, _AndroidJNIHelper.GetSignature(args));
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0003154C File Offset: 0x0002F74C
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, _AndroidJNIHelper.GetSignature(args), isStatic);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0003155C File Offset: 0x0002F75C
		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return AndroidJNIHelper.GetMethodID(jclass, methodName, _AndroidJNIHelper.GetSignature<ReturnType>(args), isStatic);
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0003156C File Offset: 0x0002F76C
		public static IntPtr GetFieldID<ReturnType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return AndroidJNIHelper.GetFieldID(jclass, fieldName, _AndroidJNIHelper.GetSignature(typeof(ReturnType)), isStatic);
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00031588 File Offset: 0x0002F788
		public static IntPtr GetConstructorID(IntPtr jclass, string signature)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = AndroidReflection.GetConstructorMember(jclass, signature);
				result = AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr methodID = AndroidJNISafe.GetMethodID(jclass, "<init>", signature);
				if (!(methodID != IntPtr.Zero))
				{
					throw ex;
				}
				result = methodID;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return result;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0003161C File Offset: 0x0002F81C
		public static IntPtr GetMethodID(IntPtr jclass, string methodName, string signature, bool isStatic)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = AndroidReflection.GetMethodMember(jclass, methodName, signature, isStatic);
				result = AndroidJNISafe.FromReflectedMethod(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr intPtr2 = (!isStatic) ? AndroidJNISafe.GetMethodID(jclass, methodName, signature) : AndroidJNISafe.GetStaticMethodID(jclass, methodName, signature);
				if (!(intPtr2 != IntPtr.Zero))
				{
					throw ex;
				}
				result = intPtr2;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return result;
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000316C4 File Offset: 0x0002F8C4
		public static IntPtr GetFieldID(IntPtr jclass, string fieldName, string signature, bool isStatic)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = AndroidReflection.GetFieldMember(jclass, fieldName, signature, isStatic);
				result = AndroidJNISafe.FromReflectedField(intPtr);
			}
			catch (Exception ex)
			{
				IntPtr intPtr2 = (!isStatic) ? AndroidJNISafe.GetFieldID(jclass, fieldName, signature) : AndroidJNISafe.GetStaticFieldID(jclass, fieldName, signature);
				if (!(intPtr2 != IntPtr.Zero))
				{
					throw ex;
				}
				result = intPtr2;
			}
			finally
			{
				AndroidJNISafe.DeleteLocalRef(intPtr);
			}
			return result;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0003176C File Offset: 0x0002F96C
		public static string GetSignature(object obj)
		{
			if (obj == null)
			{
				return "Ljava/lang/Object;";
			}
			Type type = (!(obj is Type)) ? obj.GetType() : ((Type)obj);
			if (AndroidReflection.IsPrimitive(type))
			{
				if (type.Equals(typeof(int)))
				{
					return "I";
				}
				if (type.Equals(typeof(bool)))
				{
					return "Z";
				}
				if (type.Equals(typeof(byte)))
				{
					return "B";
				}
				if (type.Equals(typeof(short)))
				{
					return "S";
				}
				if (type.Equals(typeof(long)))
				{
					return "J";
				}
				if (type.Equals(typeof(float)))
				{
					return "F";
				}
				if (type.Equals(typeof(double)))
				{
					return "D";
				}
				if (type.Equals(typeof(char)))
				{
					return "C";
				}
				return string.Empty;
			}
			else
			{
				if (type.Equals(typeof(string)))
				{
					return "Ljava/lang/String;";
				}
				if (obj is AndroidJavaProxy)
				{
					AndroidJavaObject androidJavaObject = new AndroidJavaObject(((AndroidJavaProxy)obj).javaInterface.GetRawClass());
					return "L" + androidJavaObject.Call<string>("getName", new object[0]) + ";";
				}
				if (type.Equals(typeof(AndroidJavaRunnable)))
				{
					return "Ljava/lang/Runnable;";
				}
				if (type.Equals(typeof(AndroidJavaClass)))
				{
					return "Ljava/lang/Class;";
				}
				if (type.Equals(typeof(AndroidJavaObject)))
				{
					if (obj == type)
					{
						return "Ljava/lang/Object;";
					}
					AndroidJavaObject androidJavaObject2 = (AndroidJavaObject)obj;
					using (AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getClass", new object[0]))
					{
						return "L" + androidJavaObject3.Call<string>("getName", new object[0]) + ";";
					}
				}
				if (!AndroidReflection.IsAssignableFrom(typeof(Array), type))
				{
					throw new Exception(string.Concat(new object[]
					{
						"JNI: Unknown signature for type '",
						type,
						"' (obj = ",
						obj,
						") ",
						(type != obj) ? "instance" : "equal"
					}));
				}
				if (type.GetArrayRank() != 1)
				{
					throw new Exception("JNI: System.Array in n dimensions is not allowed");
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('[');
				stringBuilder.Append(_AndroidJNIHelper.GetSignature(type.GetElementType()));
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00031A58 File Offset: 0x0002FC58
		public static string GetSignature(object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(_AndroidJNIHelper.GetSignature(obj));
			}
			stringBuilder.Append(")V");
			return stringBuilder.ToString();
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00031AB0 File Offset: 0x0002FCB0
		public static string GetSignature<ReturnType>(object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			foreach (object obj in args)
			{
				stringBuilder.Append(_AndroidJNIHelper.GetSignature(obj));
			}
			stringBuilder.Append(')');
			stringBuilder.Append(_AndroidJNIHelper.GetSignature(typeof(ReturnType)));
			return stringBuilder.ToString();
		}
	}
}
