using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace SimpleJson.Reflection
{
	// Token: 0x02000261 RID: 609
	[GeneratedCode("reflection-utils", "1.0.0")]
	internal class ReflectionUtils
	{
		// Token: 0x06002461 RID: 9313 RVA: 0x0002F9E8 File Offset: 0x0002DBE8
		public static Attribute GetAttribute(MemberInfo info, Type type)
		{
			if (info == null || type == null || !Attribute.IsDefined(info, type))
			{
				return null;
			}
			return Attribute.GetCustomAttribute(info, type);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0002FA0C File Offset: 0x0002DC0C
		public static Attribute GetAttribute(Type objectType, Type attributeType)
		{
			if (objectType == null || attributeType == null || !Attribute.IsDefined(objectType, attributeType))
			{
				return null;
			}
			return Attribute.GetCustomAttribute(objectType, attributeType);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0002FA30 File Offset: 0x0002DC30
		public static Type[] GetGenericTypeArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0002FA38 File Offset: 0x0002DC38
		public static bool IsTypeGenericeCollectionInterface(Type type)
		{
			if (!type.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>);
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0002FA8C File Offset: 0x0002DC8C
		public static bool IsAssignableFrom(Type type1, Type type2)
		{
			return type1.IsAssignableFrom(type2);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0002FA98 File Offset: 0x0002DC98
		public static bool IsTypeDictionary(Type type)
		{
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				return true;
			}
			if (!type.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IDictionary<, >);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		public static bool IsNullableType(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x0002FB10 File Offset: 0x0002DD10
		public static object ToNullableType(object obj, Type nullableType)
		{
			return (obj != null) ? Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture) : null;
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x0002FB30 File Offset: 0x0002DD30
		public static bool IsValueType(Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x0002FB38 File Offset: 0x0002DD38
		public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
		{
			return type.GetConstructors();
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0002FB40 File Offset: 0x0002DD40
		public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
		{
			IEnumerable<ConstructorInfo> constructors = ReflectionUtils.GetConstructors(type);
			foreach (ConstructorInfo constructorInfo in constructors)
			{
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (argsType.Length == parameters.Length)
				{
					int num = 0;
					bool flag = true;
					foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
					{
						if (parameterInfo.ParameterType != argsType[num])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return constructorInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x0002FC10 File Offset: 0x0002DE10
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0002FC1C File Offset: 0x0002DE1C
		public static IEnumerable<FieldInfo> GetFields(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x0002FC28 File Offset: 0x0002DE28
		public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
		{
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0002FC34 File Offset: 0x0002DE34
		public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
		{
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x0002FC40 File Offset: 0x0002DE40
		public static ReflectionUtils.ConstructorDelegate GetContructor(ConstructorInfo constructorInfo)
		{
			return ReflectionUtils.GetConstructorByReflection(constructorInfo);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0002FC48 File Offset: 0x0002DE48
		public static ReflectionUtils.ConstructorDelegate GetContructor(Type type, params Type[] argsType)
		{
			return ReflectionUtils.GetConstructorByReflection(type, argsType);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x0002FC54 File Offset: 0x0002DE54
		public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(ConstructorInfo constructorInfo)
		{
			return (object[] args) => constructorInfo.Invoke(args);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0002FC7C File Offset: 0x0002DE7C
		public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(Type type, params Type[] argsType)
		{
			ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
			return (constructorInfo != null) ? ReflectionUtils.GetConstructorByReflection(constructorInfo) : null;
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
		public static ReflectionUtils.GetDelegate GetGetMethod(PropertyInfo propertyInfo)
		{
			return ReflectionUtils.GetGetMethodByReflection(propertyInfo);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0002FCAC File Offset: 0x0002DEAC
		public static ReflectionUtils.GetDelegate GetGetMethod(FieldInfo fieldInfo)
		{
			return ReflectionUtils.GetGetMethodByReflection(fieldInfo);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0002FCB4 File Offset: 0x0002DEB4
		public static ReflectionUtils.GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
			return (object source) => methodInfo.Invoke(source, ReflectionUtils.EmptyObjects);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
		public static ReflectionUtils.GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
		{
			return (object source) => fieldInfo.GetValue(source);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0002FD08 File Offset: 0x0002DF08
		public static ReflectionUtils.SetDelegate GetSetMethod(PropertyInfo propertyInfo)
		{
			return ReflectionUtils.GetSetMethodByReflection(propertyInfo);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0002FD10 File Offset: 0x0002DF10
		public static ReflectionUtils.SetDelegate GetSetMethod(FieldInfo fieldInfo)
		{
			return ReflectionUtils.GetSetMethodByReflection(fieldInfo);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0002FD18 File Offset: 0x0002DF18
		public static ReflectionUtils.SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
			return delegate(object source, object value)
			{
				methodInfo.Invoke(source, new object[]
				{
					value
				});
			};
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0002FD44 File Offset: 0x0002DF44
		public static ReflectionUtils.SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
		{
			return delegate(object source, object value)
			{
				fieldInfo.SetValue(source, value);
			};
		}

		// Token: 0x040009A9 RID: 2473
		private static readonly object[] EmptyObjects = new object[0];

		// Token: 0x02000262 RID: 610
		public sealed class ThreadSafeDictionary<TKey, TValue> : IEnumerable, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600247C RID: 9340 RVA: 0x0002FD6C File Offset: 0x0002DF6C
			public ThreadSafeDictionary(ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
			{
				this._valueFactory = valueFactory;
			}

			// Token: 0x0600247D RID: 9341 RVA: 0x0002FD88 File Offset: 0x0002DF88
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}

			// Token: 0x0600247E RID: 9342 RVA: 0x0002FD9C File Offset: 0x0002DF9C
			private TValue Get(TKey key)
			{
				if (this._dictionary == null)
				{
					return this.AddValue(key);
				}
				TValue result;
				if (!this._dictionary.TryGetValue(key, out result))
				{
					return this.AddValue(key);
				}
				return result;
			}

			// Token: 0x0600247F RID: 9343 RVA: 0x0002FDD8 File Offset: 0x0002DFD8
			private TValue AddValue(TKey key)
			{
				TValue tvalue = this._valueFactory(key);
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._dictionary == null)
					{
						this._dictionary = new Dictionary<TKey, TValue>();
						this._dictionary[key] = tvalue;
					}
					else
					{
						TValue result;
						if (this._dictionary.TryGetValue(key, out result))
						{
							return result;
						}
						Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._dictionary);
						dictionary[key] = tvalue;
						this._dictionary = dictionary;
					}
				}
				return tvalue;
			}

			// Token: 0x06002480 RID: 9344 RVA: 0x0002FE8C File Offset: 0x0002E08C
			public void Add(TKey key, TValue value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002481 RID: 9345 RVA: 0x0002FE94 File Offset: 0x0002E094
			public bool ContainsKey(TKey key)
			{
				return this._dictionary.ContainsKey(key);
			}

			// Token: 0x17000917 RID: 2327
			// (get) Token: 0x06002482 RID: 9346 RVA: 0x0002FEA4 File Offset: 0x0002E0A4
			public ICollection<TKey> Keys
			{
				get
				{
					return this._dictionary.Keys;
				}
			}

			// Token: 0x06002483 RID: 9347 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
			public bool Remove(TKey key)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002484 RID: 9348 RVA: 0x0002FEBC File Offset: 0x0002E0BC
			public bool TryGetValue(TKey key, out TValue value)
			{
				value = this[key];
				return true;
			}

			// Token: 0x17000918 RID: 2328
			// (get) Token: 0x06002485 RID: 9349 RVA: 0x0002FECC File Offset: 0x0002E0CC
			public ICollection<TValue> Values
			{
				get
				{
					return this._dictionary.Values;
				}
			}

			// Token: 0x17000919 RID: 2329
			public TValue this[TKey key]
			{
				get
				{
					return this.Get(key);
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06002488 RID: 9352 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002489 RID: 9353 RVA: 0x0002FEF8 File Offset: 0x0002E0F8
			public void Clear()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600248A RID: 9354 RVA: 0x0002FF00 File Offset: 0x0002E100
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600248B RID: 9355 RVA: 0x0002FF08 File Offset: 0x0002E108
			public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700091A RID: 2330
			// (get) Token: 0x0600248C RID: 9356 RVA: 0x0002FF10 File Offset: 0x0002E110
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x1700091B RID: 2331
			// (get) Token: 0x0600248D RID: 9357 RVA: 0x0002FF20 File Offset: 0x0002E120
			public bool IsReadOnly
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x0002FF28 File Offset: 0x0002E128
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600248F RID: 9359 RVA: 0x0002FF30 File Offset: 0x0002E130
			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}

			// Token: 0x040009AA RID: 2474
			private readonly object _lock = new object();

			// Token: 0x040009AB RID: 2475
			private readonly ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;

			// Token: 0x040009AC RID: 2476
			private Dictionary<TKey, TValue> _dictionary;
		}

		// Token: 0x0200034D RID: 845
		// (Invoke) Token: 0x0600288E RID: 10382
		public delegate object GetDelegate(object source);

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x06002892 RID: 10386
		public delegate void SetDelegate(object source, object value);

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x06002896 RID: 10390
		public delegate object ConstructorDelegate(params object[] args);

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x0600289A RID: 10394
		public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);
	}
}
