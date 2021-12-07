using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	internal class MonoProperty : PropertyInfo, ISerializable
	{
		// Token: 0x060022D0 RID: 8912 RVA: 0x0007D444 File Offset: 0x0007B644
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				MonoPropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0007D470 File Offset: 0x0007B670
		public override PropertyAttributes Attributes
		{
			get
			{
				this.CachePropertyInfo(PInfo.Attributes);
				return this.info.attrs;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0007D484 File Offset: 0x0007B684
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x0007D4A0 File Offset: 0x0007B6A0
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x0007D4BC File Offset: 0x0007B6BC
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parameters = this.info.set_method.GetParameters();
				return parameters[parameters.Length - 1].ParameterType;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x0007D510 File Offset: 0x0007B710
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x0007D524 File Offset: 0x0007B724
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.parent;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0007D53C File Offset: 0x0007B73C
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0007D554 File Offset: 0x0007B754
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			int num = 0;
			int num2 = 0;
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				num2 = 1;
			}
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				num = 1;
			}
			MethodInfo[] array = new MethodInfo[num + num2];
			int num3 = 0;
			if (num2 != 0)
			{
				array[num3++] = this.info.set_method;
			}
			if (num != 0)
			{
				array[num3++] = this.info.get_method;
			}
			return array;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0007D604 File Offset: 0x0007B804
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0007D650 File Offset: 0x0007B850
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParameters();
			}
			else
			{
				if (this.info.set_method == null)
				{
					return new ParameterInfo[0];
				}
				ParameterInfo[] parameters = this.info.set_method.GetParameters();
				array = new ParameterInfo[parameters.Length - 1];
				Array.Copy(parameters, array, array.Length);
			}
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo pinfo = array[i];
				array[i] = new ParameterInfo(pinfo, this);
			}
			return array;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x0007D6F0 File Offset: 0x0007B8F0
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x0007D73C File Offset: 0x0007B93C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x0007D748 File Offset: 0x0007B948
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x0007D754 File Offset: 0x0007B954
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x0007D760 File Offset: 0x0007B960
		private static object GetterAdapterFrame<T, R>(MonoProperty.Getter<T, R> getter, object obj)
		{
			return getter((T)((object)obj));
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x0007D774 File Offset: 0x0007B974
		private static object StaticGetterAdapterFrame<R>(MonoProperty.StaticGetter<R> getter, object obj)
		{
			return getter();
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0007D784 File Offset: 0x0007B984
		private static MonoProperty.GetterAdapter CreateGetterDelegate(MethodInfo method)
		{
			Type[] typeArguments;
			Type typeFromHandle;
			string name;
			if (method.IsStatic)
			{
				typeArguments = new Type[]
				{
					method.ReturnType
				};
				typeFromHandle = typeof(MonoProperty.StaticGetter<>);
				name = "StaticGetterAdapterFrame";
			}
			else
			{
				typeArguments = new Type[]
				{
					method.DeclaringType,
					method.ReturnType
				};
				typeFromHandle = typeof(MonoProperty.Getter<, >);
				name = "GetterAdapterFrame";
			}
			Type type = typeFromHandle.MakeGenericType(typeArguments);
			object firstArgument = Delegate.CreateDelegate(type, method);
			MethodInfo methodInfo = typeof(MonoProperty).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(typeArguments);
			return (MonoProperty.GetterAdapter)Delegate.CreateDelegate(typeof(MonoProperty.GetterAdapter), firstArgument, methodInfo, true);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0007D834 File Offset: 0x0007BA34
		public override object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0007D844 File Offset: 0x0007BA44
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object result = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					result = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					result = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException inner)
			{
				throw new TargetInvocationException(inner);
			}
			return result;
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0007D8DC File Offset: 0x0007BADC
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[]
				{
					value
				};
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0007D95C File Offset: 0x0007BB5C
		public override string ToString()
		{
			return this.PropertyType.ToString() + " " + this.Name;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0007D984 File Offset: 0x0007BB84
		public override Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = MonoPropertyInfo.GetTypeModifiers(this, true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0007D9A8 File Offset: 0x0007BBA8
		public override Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = MonoPropertyInfo.GetTypeModifiers(this, false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007D9CC File Offset: 0x0007BBCC
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Property);
		}

		// Token: 0x04000CFF RID: 3327
		internal IntPtr klass;

		// Token: 0x04000D00 RID: 3328
		internal IntPtr prop;

		// Token: 0x04000D01 RID: 3329
		private MonoPropertyInfo info;

		// Token: 0x04000D02 RID: 3330
		private PInfo cached;

		// Token: 0x04000D03 RID: 3331
		private MonoProperty.GetterAdapter cached_getter;

		// Token: 0x020006DF RID: 1759
		// (Invoke) Token: 0x06004370 RID: 17264
		private delegate object GetterAdapter(object _this);

		// Token: 0x020006E0 RID: 1760
		// (Invoke) Token: 0x06004374 RID: 17268
		private delegate R Getter<T, R>(T _this);

		// Token: 0x020006E1 RID: 1761
		// (Invoke) Token: 0x06004378 RID: 17272
		private delegate R StaticGetter<R>();
	}
}
