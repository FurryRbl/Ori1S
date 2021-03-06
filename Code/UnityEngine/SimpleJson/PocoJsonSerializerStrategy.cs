using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using SimpleJson.Reflection;

namespace SimpleJson
{
	// Token: 0x02000260 RID: 608
	[GeneratedCode("simple-json", "1.0.0")]
	internal class PocoJsonSerializerStrategy : IJsonSerializerStrategy
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		public PocoJsonSerializerStrategy()
		{
			this.ConstructorCache = new ReflectionUtils.ThreadSafeDictionary<Type, ReflectionUtils.ConstructorDelegate>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, ReflectionUtils.ConstructorDelegate>(this.ContructorDelegateFactory));
			this.GetCache = new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(this.GetterValueFactory));
			this.SetCache = new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(this.SetterValueFactory));
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0002EF94 File Offset: 0x0002D194
		protected virtual string MapClrMemberNameToJsonFieldName(string clrPropertyName)
		{
			return clrPropertyName;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0002EF98 File Offset: 0x0002D198
		internal virtual ReflectionUtils.ConstructorDelegate ContructorDelegateFactory(Type key)
		{
			return ReflectionUtils.GetContructor(key, (!key.IsArray) ? PocoJsonSerializerStrategy.EmptyTypes : PocoJsonSerializerStrategy.ArrayConstructorParameterTypes);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0002EFC8 File Offset: 0x0002D1C8
		internal virtual IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
		{
			IDictionary<string, ReflectionUtils.GetDelegate> dictionary = new Dictionary<string, ReflectionUtils.GetDelegate>();
			foreach (PropertyInfo propertyInfo in ReflectionUtils.GetProperties(type))
			{
				if (propertyInfo.CanRead)
				{
					MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
					if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
					{
						dictionary[this.MapClrMemberNameToJsonFieldName(propertyInfo.Name)] = ReflectionUtils.GetGetMethod(propertyInfo);
					}
				}
			}
			foreach (FieldInfo fieldInfo in ReflectionUtils.GetFields(type))
			{
				if (!fieldInfo.IsStatic && fieldInfo.IsPublic)
				{
					dictionary[this.MapClrMemberNameToJsonFieldName(fieldInfo.Name)] = ReflectionUtils.GetGetMethod(fieldInfo);
				}
			}
			return dictionary;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(Type type)
		{
			IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> dictionary = new Dictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>();
			foreach (PropertyInfo propertyInfo in ReflectionUtils.GetProperties(type))
			{
				if (propertyInfo.CanWrite)
				{
					MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
					if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
					{
						dictionary[this.MapClrMemberNameToJsonFieldName(propertyInfo.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(propertyInfo.PropertyType, ReflectionUtils.GetSetMethod(propertyInfo));
					}
				}
			}
			foreach (FieldInfo fieldInfo in ReflectionUtils.GetFields(type))
			{
				if (!fieldInfo.IsInitOnly && !fieldInfo.IsStatic && fieldInfo.IsPublic)
				{
					dictionary[this.MapClrMemberNameToJsonFieldName(fieldInfo.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(fieldInfo.FieldType, ReflectionUtils.GetSetMethod(fieldInfo));
				}
			}
			return dictionary;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0002F24C File Offset: 0x0002D44C
		public virtual bool TrySerializeNonPrimitiveObject(object input, out object output)
		{
			return this.TrySerializeKnownTypes(input, out output) || this.TrySerializeUnknownTypes(input, out output);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0002F268 File Offset: 0x0002D468
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public virtual object DeserializeObject(object value, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = value as string;
			if (type == typeof(Guid) && string.IsNullOrEmpty(text))
			{
				return default(Guid);
			}
			if (value == null)
			{
				return null;
			}
			object obj = null;
			if (text != null)
			{
				if (text.Length != 0)
				{
					if (type == typeof(DateTime) || (ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(DateTime)))
					{
						return DateTime.ParseExact(text, PocoJsonSerializerStrategy.Iso8601Format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
					}
					if (type == typeof(DateTimeOffset) || (ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(DateTimeOffset)))
					{
						return DateTimeOffset.ParseExact(text, PocoJsonSerializerStrategy.Iso8601Format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
					}
					if (type == typeof(Guid) || (ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(Guid)))
					{
						return new Guid(text);
					}
					return text;
				}
				else
				{
					if (type == typeof(Guid))
					{
						obj = default(Guid);
					}
					else if (ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(Guid))
					{
						obj = null;
					}
					else
					{
						obj = text;
					}
					if (!ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof(Guid))
					{
						return text;
					}
				}
			}
			else if (value is bool)
			{
				return value;
			}
			bool flag = value is long;
			bool flag2 = value is double;
			if ((flag && type == typeof(long)) || (flag2 && type == typeof(double)))
			{
				return value;
			}
			if ((!flag2 || type == typeof(double)) && (!flag || type == typeof(long)))
			{
				IDictionary<string, object> dictionary = value as IDictionary<string, object>;
				if (dictionary != null)
				{
					IDictionary<string, object> dictionary2 = dictionary;
					if (ReflectionUtils.IsTypeDictionary(type))
					{
						Type[] genericTypeArguments = ReflectionUtils.GetGenericTypeArguments(type);
						Type type2 = genericTypeArguments[0];
						Type type3 = genericTypeArguments[1];
						Type key = typeof(Dictionary<, >).MakeGenericType(new Type[]
						{
							type2,
							type3
						});
						IDictionary dictionary3 = (IDictionary)this.ConstructorCache[key](null);
						foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
						{
							dictionary3.Add(keyValuePair.Key, this.DeserializeObject(keyValuePair.Value, type3));
						}
						obj = dictionary3;
					}
					else if (type == typeof(object))
					{
						obj = value;
					}
					else
					{
						obj = this.ConstructorCache[type](null);
						foreach (KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> keyValuePair2 in this.SetCache[type])
						{
							object value2;
							if (dictionary2.TryGetValue(keyValuePair2.Key, out value2))
							{
								value2 = this.DeserializeObject(value2, keyValuePair2.Value.Key);
								keyValuePair2.Value.Value(obj, value2);
							}
						}
					}
				}
				else
				{
					IList<object> list = value as IList<object>;
					if (list != null)
					{
						IList<object> list2 = list;
						IList list3 = null;
						if (type.IsArray)
						{
							list3 = (IList)this.ConstructorCache[type](new object[]
							{
								list2.Count
							});
							int num = 0;
							foreach (object value3 in list2)
							{
								list3[num++] = this.DeserializeObject(value3, type.GetElementType());
							}
						}
						else if (ReflectionUtils.IsTypeGenericeCollectionInterface(type) || ReflectionUtils.IsAssignableFrom(typeof(IList), type))
						{
							Type type4 = ReflectionUtils.GetGenericTypeArguments(type)[0];
							Type key2 = typeof(List<>).MakeGenericType(new Type[]
							{
								type4
							});
							list3 = (IList)this.ConstructorCache[key2](new object[]
							{
								list2.Count
							});
							foreach (object value4 in list2)
							{
								list3.Add(this.DeserializeObject(value4, type4));
							}
						}
						obj = list3;
					}
				}
				return obj;
			}
			obj = ((!typeof(IConvertible).IsAssignableFrom(type)) ? value : Convert.ChangeType(value, type, CultureInfo.InvariantCulture));
			if (ReflectionUtils.IsNullableType(type))
			{
				return ReflectionUtils.ToNullableType(obj, type);
			}
			return obj;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0002F81C File Offset: 0x0002DA1C
		protected virtual object SerializeEnum(Enum p)
		{
			return Convert.ToDouble(p, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0002F830 File Offset: 0x0002DA30
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Need to support .NET 2")]
		protected virtual bool TrySerializeKnownTypes(object input, out object output)
		{
			bool result = true;
			if (input is DateTime)
			{
				output = ((DateTime)input).ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], CultureInfo.InvariantCulture);
			}
			else if (input is DateTimeOffset)
			{
				output = ((DateTimeOffset)input).ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], CultureInfo.InvariantCulture);
			}
			else if (input is Guid)
			{
				output = ((Guid)input).ToString("D");
			}
			else if (input is Uri)
			{
				output = input.ToString();
			}
			else
			{
				Enum @enum = input as Enum;
				if (@enum != null)
				{
					output = this.SerializeEnum(@enum);
				}
				else
				{
					result = false;
					output = null;
				}
			}
			return result;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0002F908 File Offset: 0x0002DB08
		[SuppressMessage("Microsoft.Design", "CA1007:UseGenericsWhereAppropriate", Justification = "Need to support .NET 2")]
		protected virtual bool TrySerializeUnknownTypes(object input, out object output)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			output = null;
			Type type = input.GetType();
			if (type.FullName == null)
			{
				return false;
			}
			IDictionary<string, object> dictionary = new JsonObject();
			IDictionary<string, ReflectionUtils.GetDelegate> dictionary2 = this.GetCache[type];
			foreach (KeyValuePair<string, ReflectionUtils.GetDelegate> keyValuePair in dictionary2)
			{
				if (keyValuePair.Value != null)
				{
					dictionary.Add(this.MapClrMemberNameToJsonFieldName(keyValuePair.Key), keyValuePair.Value(input));
				}
			}
			output = dictionary;
			return true;
		}

		// Token: 0x040009A3 RID: 2467
		internal IDictionary<Type, ReflectionUtils.ConstructorDelegate> ConstructorCache;

		// Token: 0x040009A4 RID: 2468
		internal IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>> GetCache;

		// Token: 0x040009A5 RID: 2469
		internal IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> SetCache;

		// Token: 0x040009A6 RID: 2470
		internal static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x040009A7 RID: 2471
		internal static readonly Type[] ArrayConstructorParameterTypes = new Type[]
		{
			typeof(int)
		};

		// Token: 0x040009A8 RID: 2472
		private static readonly string[] Iso8601Format = new string[]
		{
			"yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
			"yyyy-MM-dd\\THH:mm:ss\\Z",
			"yyyy-MM-dd\\THH:mm:ssK"
		};
	}
}
