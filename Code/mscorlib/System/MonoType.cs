using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x02000159 RID: 345
	[Serializable]
	internal class MonoType : Type, ISerializable
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x000486CC File Offset: 0x000468CC
		internal MonoType(object obj)
		{
			MonoType.type_from_obj(this, obj);
			throw new NotImplementedException();
		}

		// Token: 0x0600124A RID: 4682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void type_from_obj(MonoType type, object obj);

		// Token: 0x0600124B RID: 4683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TypeAttributes get_attributes(Type type);

		// Token: 0x0600124C RID: 4684 RVA: 0x000486E0 File Offset: 0x000468E0
		internal ConstructorInfo GetDefaultConstructor()
		{
			if (this.type_info == null)
			{
				this.type_info = new MonoTypeInfo();
			}
			ConstructorInfo result;
			if ((result = this.type_info.default_ctor) == null)
			{
				result = (this.type_info.default_ctor = this.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null));
			}
			return result;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00048738 File Offset: 0x00046938
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return MonoType.get_attributes(this);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00048740 File Offset: 0x00046940
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (bindingAttr == BindingFlags.Default)
			{
				bindingAttr = (BindingFlags.Instance | BindingFlags.Public);
			}
			ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
			ConstructorInfo constructorInfo = null;
			int num = 0;
			foreach (ConstructorInfo constructorInfo2 in constructors)
			{
				if (callConvention == CallingConventions.Any || (constructorInfo2.CallingConvention & callConvention) == callConvention)
				{
					constructorInfo = constructorInfo2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorInfo;
				}
				else
				{
					num = 0;
					foreach (ConstructorInfo constructorInfo3 in constructors)
					{
						if (callConvention == CallingConventions.Any || (constructorInfo3.CallingConvention & callConvention) == callConvention)
						{
							array2[num++] = constructorInfo3;
						}
					}
				}
				if (binder == null)
				{
					binder = Binder.DefaultBinder;
				}
				return (ConstructorInfo)this.CheckMethodSecurity(binder.SelectMethod(bindingAttr, array2, types, modifiers));
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return (ConstructorInfo)this.CheckMethodSecurity(constructorInfo);
		}

		// Token: 0x0600124F RID: 4687
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern ConstructorInfo[] GetConstructors_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06001250 RID: 4688 RVA: 0x00048858 File Offset: 0x00046A58
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructors_internal(bindingAttr, this);
		}

		// Token: 0x06001251 RID: 4689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern EventInfo InternalGetEvent(string name, BindingFlags bindingAttr);

		// Token: 0x06001252 RID: 4690 RVA: 0x00048864 File Offset: 0x00046A64
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.InternalGetEvent(name, bindingAttr);
		}

		// Token: 0x06001253 RID: 4691
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern EventInfo[] GetEvents_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06001254 RID: 4692 RVA: 0x00048880 File Offset: 0x00046A80
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEvents_internal(bindingAttr, this);
		}

		// Token: 0x06001255 RID: 4693
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06001256 RID: 4694
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern FieldInfo[] GetFields_internal(BindingFlags bindingAttr, Type reflected_type);

		// Token: 0x06001257 RID: 4695 RVA: 0x0004888C File Offset: 0x00046A8C
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFields_internal(bindingAttr, this);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00048898 File Offset: 0x00046A98
		public override Type GetInterface(string name, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			Type[] interfaces = this.GetInterfaces();
			foreach (Type type in interfaces)
			{
				Type type2 = (!type.IsGenericType) ? type : type.GetGenericTypeDefinition();
				if (string.Compare(type2.Name, name, ignoreCase, CultureInfo.InvariantCulture) == 0)
				{
					return type;
				}
				if (string.Compare(type2.FullName, name, ignoreCase, CultureInfo.InvariantCulture) == 0)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06001259 RID: 4697
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetInterfaces();

		// Token: 0x0600125A RID: 4698 RVA: 0x00048920 File Offset: 0x00046B20
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.FindMembers(MemberTypes.All, bindingAttr, null, null);
		}

		// Token: 0x0600125B RID: 4699
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type);

		// Token: 0x0600125C RID: 4700 RVA: 0x00048930 File Offset: 0x00046B30
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004893C File Offset: 0x00046B3C
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			bool ignoreCase = (bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
			MethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, ignoreCase, this);
			MethodInfo methodInfo = null;
			int num = 0;
			foreach (MethodInfo methodInfo2 in methodsByName)
			{
				if (callConvention == CallingConventions.Any || (methodInfo2.CallingConvention & callConvention) == callConvention)
				{
					methodInfo = methodInfo2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (num == 1 && types == null)
			{
				return (MethodInfo)this.CheckMethodSecurity(methodInfo);
			}
			MethodBase[] array2 = new MethodBase[num];
			if (num == 1)
			{
				array2[0] = methodInfo;
			}
			else
			{
				num = 0;
				foreach (MethodInfo methodInfo3 in methodsByName)
				{
					if (callConvention == CallingConventions.Any || (methodInfo3.CallingConvention & callConvention) == callConvention)
					{
						array2[num++] = methodInfo3;
					}
				}
			}
			if (types == null)
			{
				return (MethodInfo)this.CheckMethodSecurity(Binder.FindMostDerivedMatch(array2));
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			return (MethodInfo)this.CheckMethodSecurity(binder.SelectMethod(bindingAttr, array2, types, modifiers));
		}

		// Token: 0x0600125E RID: 4702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetCorrespondingInflatedMethod(MethodInfo generic);

		// Token: 0x0600125F RID: 4703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ConstructorInfo GetCorrespondingInflatedConstructor(ConstructorInfo generic);

		// Token: 0x06001260 RID: 4704 RVA: 0x00048A78 File Offset: 0x00046C78
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedMethod(fromNoninstanciated);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00048A94 File Offset: 0x00046C94
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedConstructor(fromNoninstanciated);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00048AB0 File Offset: 0x00046CB0
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			BindingFlags bindingFlags = (!fromNoninstanciated.IsStatic) ? BindingFlags.Instance : BindingFlags.Static;
			bindingFlags |= ((!fromNoninstanciated.IsPublic) ? BindingFlags.NonPublic : BindingFlags.Public);
			return this.GetField(fromNoninstanciated.Name, bindingFlags);
		}

		// Token: 0x06001263 RID: 4707
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type GetNestedType(string name, BindingFlags bindingAttr);

		// Token: 0x06001264 RID: 4708
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetNestedTypes(BindingFlags bindingAttr);

		// Token: 0x06001265 RID: 4709
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern PropertyInfo[] GetPropertiesByName(string name, BindingFlags bindingAttr, bool icase, Type reflected_type);

		// Token: 0x06001266 RID: 4710 RVA: 0x00048AF4 File Offset: 0x00046CF4
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertiesByName(null, bindingAttr, false, this);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00048B00 File Offset: 0x00046D00
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			bool icase = (bindingAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
			PropertyInfo[] propertiesByName = this.GetPropertiesByName(name, bindingAttr, icase, this);
			int num = propertiesByName.Length;
			if (num == 0)
			{
				return null;
			}
			if (num == 1 && (types == null || types.Length == 0) && (returnType == null || returnType == propertiesByName[0].PropertyType))
			{
				return propertiesByName[0];
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertiesByName, returnType, types, modifiers);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00048B7C File Offset: 0x00046D7C
		protected override bool HasElementTypeImpl()
		{
			return this.IsArrayImpl() || this.IsByRefImpl() || this.IsPointerImpl();
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00048BA0 File Offset: 0x00046DA0
		protected override bool IsArrayImpl()
		{
			return Type.IsArrayImpl(this);
		}

		// Token: 0x0600126A RID: 4714
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern bool IsByRefImpl();

		// Token: 0x0600126B RID: 4715
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern bool IsCOMObjectImpl();

		// Token: 0x0600126C RID: 4716
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern bool IsPointerImpl();

		// Token: 0x0600126D RID: 4717
		[MethodImpl(MethodImplOptions.InternalCall)]
		protected override extern bool IsPrimitiveImpl();

		// Token: 0x0600126E RID: 4718 RVA: 0x00048BA8 File Offset: 0x00046DA8
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return base.IsSubclassOf(type);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00048BC4 File Offset: 0x00046DC4
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default)
			{
				if ((invokeAttr & (BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException("bindingFlags");
				}
			}
			else if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if ((invokeAttr & BindingFlags.GetField) != BindingFlags.Default && (invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
			{
				throw new ArgumentException("Cannot specify both Get and Set on a field.", "bindingFlags");
			}
			if ((invokeAttr & BindingFlags.GetProperty) != BindingFlags.Default && (invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
			{
				throw new ArgumentException("Cannot specify both Get and Set on a property.", "bindingFlags");
			}
			if ((invokeAttr & BindingFlags.InvokeMethod) != BindingFlags.Default)
			{
				if ((invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
				{
					throw new ArgumentException("Cannot specify Set on a field and Invoke on a method.", "bindingFlags");
				}
				if ((invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException("Cannot specify Set on a property and Invoke on a method.", "bindingFlags");
				}
			}
			if (namedParameters != null && (args == null || args.Length < namedParameters.Length))
			{
				throw new ArgumentException("namedParameters cannot be more than named arguments in number");
			}
			if ((invokeAttr & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException("Must specify binding flags describing the invoke operation required.", "bindingFlags");
			}
			if ((invokeAttr & (BindingFlags.Public | BindingFlags.NonPublic)) == BindingFlags.Default)
			{
				invokeAttr |= BindingFlags.Public;
			}
			if ((invokeAttr & (BindingFlags.Instance | BindingFlags.Static)) == BindingFlags.Default)
			{
				invokeAttr |= (BindingFlags.Instance | BindingFlags.Static);
			}
			if (binder == null)
			{
				binder = Binder.DefaultBinder;
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default)
			{
				invokeAttr |= BindingFlags.DeclaredOnly;
				ConstructorInfo[] constructors = this.GetConstructors(invokeAttr);
				object state = null;
				MethodBase methodBase = binder.BindToMethod(invokeAttr, constructors, ref args, modifiers, culture, namedParameters, out state);
				if (methodBase != null)
				{
					object result = methodBase.Invoke(target, invokeAttr, binder, args, culture);
					binder.ReorderArgumentArray(ref args, state);
					return result;
				}
				if (this.IsValueType && args == null)
				{
					return Activator.CreateInstanceInternal(this);
				}
				throw new MissingMethodException("Constructor on type '" + this.FullName + "' not found.");
			}
			else
			{
				if (name == string.Empty && Attribute.IsDefined(this, typeof(DefaultMemberAttribute)))
				{
					DefaultMemberAttribute defaultMemberAttribute = (DefaultMemberAttribute)Attribute.GetCustomAttribute(this, typeof(DefaultMemberAttribute));
					name = defaultMemberAttribute.MemberName;
				}
				bool flag = (invokeAttr & BindingFlags.IgnoreCase) != BindingFlags.Default;
				string text = null;
				bool flag2 = false;
				if ((invokeAttr & BindingFlags.InvokeMethod) != BindingFlags.Default)
				{
					MethodInfo[] methodsByName = this.GetMethodsByName(name, invokeAttr, flag, this);
					object state2 = null;
					if (args == null)
					{
						args = new object[0];
					}
					MethodBase methodBase2 = binder.BindToMethod(invokeAttr, methodsByName, ref args, modifiers, culture, namedParameters, out state2);
					if (methodBase2 != null)
					{
						ParameterInfo[] parameters = methodBase2.GetParameters();
						for (int i = 0; i < parameters.Length; i++)
						{
							if (System.Reflection.Missing.Value == args[i] && (parameters[i].Attributes & ParameterAttributes.HasDefault) != ParameterAttributes.HasDefault)
							{
								throw new ArgumentException("Used Missing.Value for argument without default value", "parameters");
							}
						}
						bool flag3 = parameters.Length > 0 && Attribute.IsDefined(parameters[parameters.Length - 1], typeof(ParamArrayAttribute));
						if (flag3)
						{
							this.ReorderParamArrayArguments(ref args, methodBase2);
						}
						object result2 = methodBase2.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state2);
						return result2;
					}
					if (methodsByName.Length > 0)
					{
						text = "The best match for method " + name + " has some invalid parameter.";
					}
					else
					{
						text = "Cannot find method " + name + ".";
					}
				}
				if ((invokeAttr & BindingFlags.GetField) != BindingFlags.Default)
				{
					FieldInfo field = this.GetField(name, invokeAttr);
					if (field != null)
					{
						return field.GetValue(target);
					}
					if ((invokeAttr & BindingFlags.GetProperty) == BindingFlags.Default)
					{
						flag2 = true;
					}
				}
				else if ((invokeAttr & BindingFlags.SetField) != BindingFlags.Default)
				{
					FieldInfo field2 = this.GetField(name, invokeAttr);
					if (field2 != null)
					{
						if (args == null)
						{
							throw new ArgumentNullException("providedArgs");
						}
						if (args == null || args.Length != 1)
						{
							throw new ArgumentException("Only the field value can be specified to set a field value.", "bindingFlags");
						}
						field2.SetValue(target, args[0]);
						return null;
					}
					else if ((invokeAttr & BindingFlags.SetProperty) == BindingFlags.Default)
					{
						flag2 = true;
					}
				}
				if ((invokeAttr & BindingFlags.GetProperty) != BindingFlags.Default)
				{
					PropertyInfo[] propertiesByName = this.GetPropertiesByName(name, invokeAttr, flag, this);
					object state3 = null;
					int num = 0;
					for (int j = 0; j < propertiesByName.Length; j++)
					{
						if (propertiesByName[j].GetGetMethod(true) != null)
						{
							num++;
						}
					}
					MethodBase[] array = new MethodBase[num];
					num = 0;
					for (int j = 0; j < propertiesByName.Length; j++)
					{
						MethodBase getMethod = propertiesByName[j].GetGetMethod(true);
						if (getMethod != null)
						{
							array[num++] = getMethod;
						}
					}
					MethodBase methodBase3 = binder.BindToMethod(invokeAttr, array, ref args, modifiers, culture, namedParameters, out state3);
					if (methodBase3 != null)
					{
						ParameterInfo[] parameters2 = methodBase3.GetParameters();
						bool flag4 = parameters2.Length > 0 && Attribute.IsDefined(parameters2[parameters2.Length - 1], typeof(ParamArrayAttribute));
						if (flag4)
						{
							this.ReorderParamArrayArguments(ref args, methodBase3);
						}
						object result3 = methodBase3.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state3);
						return result3;
					}
					flag2 = true;
				}
				else if ((invokeAttr & BindingFlags.SetProperty) != BindingFlags.Default)
				{
					PropertyInfo[] propertiesByName2 = this.GetPropertiesByName(name, invokeAttr, flag, this);
					object state4 = null;
					int num2 = 0;
					for (int k = 0; k < propertiesByName2.Length; k++)
					{
						if (propertiesByName2[k].GetSetMethod(true) != null)
						{
							num2++;
						}
					}
					MethodBase[] array2 = new MethodBase[num2];
					num2 = 0;
					for (int k = 0; k < propertiesByName2.Length; k++)
					{
						MethodBase setMethod = propertiesByName2[k].GetSetMethod(true);
						if (setMethod != null)
						{
							array2[num2++] = setMethod;
						}
					}
					MethodBase methodBase4 = binder.BindToMethod(invokeAttr, array2, ref args, modifiers, culture, namedParameters, out state4);
					if (methodBase4 != null)
					{
						ParameterInfo[] parameters3 = methodBase4.GetParameters();
						bool flag5 = parameters3.Length > 0 && Attribute.IsDefined(parameters3[parameters3.Length - 1], typeof(ParamArrayAttribute));
						if (flag5)
						{
							this.ReorderParamArrayArguments(ref args, methodBase4);
						}
						object result4 = methodBase4.Invoke(target, invokeAttr, binder, args, culture);
						binder.ReorderArgumentArray(ref args, state4);
						return result4;
					}
					flag2 = true;
				}
				if (text != null)
				{
					throw new MissingMethodException(text);
				}
				if (flag2)
				{
					throw new MissingFieldException("Cannot find variable " + name + ".");
				}
				return null;
			}
		}

		// Token: 0x06001270 RID: 4720
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type GetElementType();

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00049228 File Offset: 0x00047428
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001272 RID: 4722
		public override extern Assembly Assembly { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004922C File Offset: 0x0004742C
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.getFullName(true, true);
			}
		}

		// Token: 0x06001274 RID: 4724
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string getFullName(bool full_name, bool assembly_qualified);

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001275 RID: 4725
		public override extern Type BaseType { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00049238 File Offset: 0x00047438
		public override string FullName
		{
			get
			{
				if (this.type_info == null)
				{
					this.type_info = new MonoTypeInfo();
				}
				string result;
				if ((result = this.type_info.full_name) == null)
				{
					result = (this.type_info.full_name = this.getFullName(true, false));
				}
				return result;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00049288 File Offset: 0x00047488
		public override Guid GUID
		{
			get
			{
				object[] customAttributes = this.GetCustomAttributes(typeof(GuidAttribute), true);
				if (customAttributes.Length == 0)
				{
					return Guid.Empty;
				}
				return new Guid(((GuidAttribute)customAttributes[0]).Value);
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000492C8 File Offset: 0x000474C8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000492D4 File Offset: 0x000474D4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000492E0 File Offset: 0x000474E0
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x000492FC File Offset: 0x000474FC
		public override MemberTypes MemberType
		{
			get
			{
				if (this.DeclaringType != null && !this.IsGenericParameter)
				{
					return MemberTypes.NestedType;
				}
				return MemberTypes.TypeInfo;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600127C RID: 4732
		public override extern string Name { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600127D RID: 4733
		public override extern string Namespace { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600127E RID: 4734
		public override extern Module Module { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600127F RID: 4735
		public override extern Type DeclaringType { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004931C File Offset: 0x0004751C
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00049324 File Offset: 0x00047524
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._impl;
			}
		}

		// Token: 0x06001282 RID: 4738
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern int GetArrayRank();

		// Token: 0x06001283 RID: 4739 RVA: 0x0004932C File Offset: 0x0004752C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			UnitySerializationHolder.GetTypeData(this, info, context);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00049338 File Offset: 0x00047538
		public override string ToString()
		{
			return this.getFullName(false, false);
		}

		// Token: 0x06001285 RID: 4741
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00049344 File Offset: 0x00047544
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.IsGenericType)
				{
					foreach (Type type in this.GetGenericArguments())
					{
						if (type.ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.HasElementType && this.GetElementType().ContainsGenericParameters;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001287 RID: 4743
		public override extern bool IsGenericParameter { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001288 RID: 4744
		public override extern MethodBase DeclaringMethod { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001289 RID: 4745 RVA: 0x000493B0 File Offset: 0x000475B0
		public override Type GetGenericTypeDefinition()
		{
			Type genericTypeDefinition_impl = base.GetGenericTypeDefinition_impl();
			if (genericTypeDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericTypeDefinition_impl;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000493D4 File Offset: 0x000475D4
		private MethodBase CheckMethodSecurity(MethodBase mb)
		{
			if (!SecurityManager.SecurityEnabled || mb == null)
			{
				return mb;
			}
			return (!SecurityManager.ReflectedLinkDemandQuery(mb)) ? null : mb;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00049408 File Offset: 0x00047608
		private void ReorderParamArrayArguments(ref object[] args, MethodBase method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			object[] array = new object[parameters.Length];
			Array array2 = Array.CreateInstance(parameters[parameters.Length - 1].ParameterType.GetElementType(), args.Length - (parameters.Length - 1));
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				if (i < parameters.Length - 1)
				{
					array[i] = args[i];
				}
				else
				{
					array2.SetValue(args[i], num);
					num++;
				}
			}
			array[parameters.Length - 1] = array2;
			args = array;
		}

		// Token: 0x04000548 RID: 1352
		[NonSerialized]
		private MonoTypeInfo type_info;
	}
}
