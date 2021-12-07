using System;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020002A1 RID: 673
	internal class MonoGenericClass : MonoType
	{
		// Token: 0x0600222F RID: 8751 RVA: 0x0007B58C File Offset: 0x0007978C
		internal MonoGenericClass() : base(null)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0007B59C File Offset: 0x0007979C
		internal MonoGenericClass(TypeBuilder tb, Type[] args) : base(null)
		{
			this.generic_type = tb;
			this.type_arguments = args;
		}

		// Token: 0x06002231 RID: 8753
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void initialize(MethodInfo[] methods, ConstructorInfo[] ctors, FieldInfo[] fields, PropertyInfo[] properties, EventInfo[] events);

		// Token: 0x06002232 RID: 8754 RVA: 0x0007B5B4 File Offset: 0x000797B4
		private void initialize()
		{
			if (this.initialized)
			{
				return;
			}
			MonoGenericClass monoGenericClass = this.GetParentType() as MonoGenericClass;
			if (monoGenericClass != null)
			{
				monoGenericClass.initialize();
			}
			EventInfo[] events_internal = this.generic_type.GetEvents_internal(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			this.event_count = events_internal.Length;
			this.initialize(this.generic_type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetConstructorsInternal(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), this.generic_type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), events_internal);
			this.initialized = true;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0007B640 File Offset: 0x00079840
		private Type GetParentType()
		{
			return this.InflateType(this.generic_type.BaseType);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0007B654 File Offset: 0x00079854
		internal Type InflateType(Type type)
		{
			return this.InflateType(type, null);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0007B660 File Offset: 0x00079860
		internal Type InflateType(Type type, Type[] method_args)
		{
			if (type == null)
			{
				return null;
			}
			if (!type.IsGenericParameter && !type.ContainsGenericParameters)
			{
				return type;
			}
			if (type.IsGenericParameter)
			{
				if (type.DeclaringMethod == null)
				{
					return this.type_arguments[type.GenericParameterPosition];
				}
				if (method_args != null)
				{
					return method_args[type.GenericParameterPosition];
				}
				return type;
			}
			else
			{
				if (type.IsPointer)
				{
					return this.InflateType(type.GetElementType(), method_args).MakePointerType();
				}
				if (type.IsByRef)
				{
					return this.InflateType(type.GetElementType(), method_args).MakeByRefType();
				}
				if (!type.IsArray)
				{
					Type[] genericArguments = type.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						genericArguments[i] = this.InflateType(genericArguments[i], method_args);
					}
					Type type2 = (!type.IsGenericTypeDefinition) ? type.GetGenericTypeDefinition() : type;
					return type2.MakeGenericType(genericArguments);
				}
				if (type.GetArrayRank() > 1)
				{
					return this.InflateType(type.GetElementType(), method_args).MakeArrayType(type.GetArrayRank());
				}
				if (type.ToString().EndsWith("[*]", StringComparison.Ordinal))
				{
					return this.InflateType(type.GetElementType(), method_args).MakeArrayType(1);
				}
				return this.InflateType(type.GetElementType(), method_args).MakeArrayType();
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x0007B7B4 File Offset: 0x000799B4
		public override Type BaseType
		{
			get
			{
				Type parentType = this.GetParentType();
				return (parentType == null) ? this.generic_type.BaseType : parentType;
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x0007B7E0 File Offset: 0x000799E0
		private Type[] GetInterfacesInternal()
		{
			if (this.generic_type.interfaces == null)
			{
				return new Type[0];
			}
			Type[] array = new Type[this.generic_type.interfaces.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.InflateType(this.generic_type.interfaces[i]);
			}
			return array;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0007B844 File Offset: 0x00079A44
		public override Type[] GetInterfaces()
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			return this.GetInterfacesInternal();
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0007B864 File Offset: 0x00079A64
		protected override bool IsValueTypeImpl()
		{
			return this.generic_type.IsValueType;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0007B874 File Offset: 0x00079A74
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is MethodBuilder))
			{
				throw new InvalidOperationException("Inflating non MethodBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			MethodBuilder methodBuilder = (MethodBuilder)fromNoninstanciated;
			if (this.methods == null)
			{
				this.methods = new Hashtable();
			}
			if (!this.methods.ContainsKey(methodBuilder))
			{
				this.methods[methodBuilder] = new MethodOnTypeBuilderInst(this, methodBuilder);
			}
			return (MethodInfo)this.methods[methodBuilder];
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0007B8FC File Offset: 0x00079AFC
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is ConstructorBuilder))
			{
				throw new InvalidOperationException("Inflating non ConstructorBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			ConstructorBuilder constructorBuilder = (ConstructorBuilder)fromNoninstanciated;
			if (this.ctors == null)
			{
				this.ctors = new Hashtable();
			}
			if (!this.ctors.ContainsKey(constructorBuilder))
			{
				this.ctors[constructorBuilder] = new ConstructorOnTypeBuilderInst(this, constructorBuilder);
			}
			return (ConstructorInfo)this.ctors[constructorBuilder];
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0007B984 File Offset: 0x00079B84
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			this.initialize();
			if (!(fromNoninstanciated is FieldBuilder))
			{
				throw new InvalidOperationException("Inflating non FieldBuilder objects is not supported: " + fromNoninstanciated.GetType());
			}
			FieldBuilder fieldBuilder = (FieldBuilder)fromNoninstanciated;
			if (this.fields == null)
			{
				this.fields = new Hashtable();
			}
			if (!this.fields.ContainsKey(fieldBuilder))
			{
				this.fields[fieldBuilder] = new FieldOnTypeBuilderInst(this, fieldBuilder);
			}
			return (FieldInfo)this.fields[fieldBuilder];
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0007BA0C File Offset: 0x00079C0C
		public override MethodInfo[] GetMethods(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetMethodsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetMethods(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_91;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetMethodsByName(null, bf, false, this));
			Block_4:
			IL_91:
			MethodInfo[] array = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x0007BAC4 File Offset: 0x00079CC4
		private MethodInfo[] GetMethodsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.num_methods == 0)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.num_methods; i++)
			{
				MethodInfo methodInfo = this.generic_type.methods[i];
				bool flag = false;
				MethodAttributes attributes = methodInfo.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						methodInfo = TypeBuilder.GetMethod(this, methodInfo);
						arrayList.Add(methodInfo);
					}
				}
			}
			MethodInfo[] array = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007BBB8 File Offset: 0x00079DB8
		public override ConstructorInfo[] GetConstructors(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetConstructorsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetConstructors(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8F;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetConstructors_internal(bf, this));
			Block_4:
			IL_8F:
			ConstructorInfo[] array = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0007BC6C File Offset: 0x00079E6C
		private ConstructorInfo[] GetConstructorsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.ctors.Length; i++)
			{
				ConstructorInfo constructorInfo = this.generic_type.ctors[i];
				bool flag = false;
				MethodAttributes attributes = constructorInfo.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(TypeBuilder.GetConstructor(this, constructorInfo));
					}
				}
			}
			ConstructorInfo[] array = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0007BD58 File Offset: 0x00079F58
		public override FieldInfo[] GetFields(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetFieldsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetFields(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8F;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetFields_internal(bf, this));
			Block_4:
			IL_8F:
			FieldInfo[] array = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007BE0C File Offset: 0x0007A00C
		private FieldInfo[] GetFieldsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.num_fields == 0)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			for (int i = 0; i < this.generic_type.num_fields; i++)
			{
				FieldInfo fieldInfo = this.generic_type.fields[i];
				bool flag = false;
				FieldAttributes attributes = fieldInfo.Attributes;
				if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
				{
					if ((bf & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
					{
						if ((bf & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(TypeBuilder.GetField(this, fieldInfo));
					}
				}
			}
			FieldInfo[] array = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0007BEF8 File Offset: 0x0007A0F8
		public override PropertyInfo[] GetProperties(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetPropertiesInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetProperties(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_91;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetPropertiesByName(null, bf, false, this));
			Block_4:
			IL_91:
			PropertyInfo[] array = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0007BFB0 File Offset: 0x0007A1B0
		private PropertyInfo[] GetPropertiesInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			this.initialize();
			foreach (PropertyBuilder propertyInfo in this.generic_type.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (methodInfo != null)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bf & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bf & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(new PropertyOnTypeBuilderInst(reftype, propertyInfo));
						}
					}
				}
			}
			PropertyInfo[] array = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x0007C0C0 File Offset: 0x0007A2C0
		public override EventInfo[] GetEvents(BindingFlags bf)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			Type type = this;
			for (;;)
			{
				MonoGenericClass monoGenericClass = type as MonoGenericClass;
				if (monoGenericClass != null)
				{
					arrayList.AddRange(monoGenericClass.GetEventsInternal(bf, this));
				}
				else
				{
					if (!(type is TypeBuilder))
					{
						break;
					}
					arrayList.AddRange(type.GetEvents(bf));
				}
				if ((bf & BindingFlags.DeclaredOnly) != BindingFlags.Default)
				{
					goto Block_4;
				}
				type = type.BaseType;
				if (type == null)
				{
					goto IL_8E;
				}
			}
			MonoType monoType = (MonoType)type;
			arrayList.AddRange(monoType.GetEvents(bf));
			Block_4:
			IL_8E:
			EventInfo[] array = new EventInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x0007C174 File Offset: 0x0007A374
		private EventInfo[] GetEventsInternal(BindingFlags bf, MonoGenericClass reftype)
		{
			if (this.generic_type.events == null)
			{
				return new EventInfo[0];
			}
			this.initialize();
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.event_count; i++)
			{
				EventBuilder eventBuilder = this.generic_type.events[i];
				bool flag = false;
				MethodInfo methodInfo = eventBuilder.add_method;
				if (methodInfo == null)
				{
					methodInfo = eventBuilder.remove_method;
				}
				if (methodInfo != null)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bf & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bf & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bf & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bf & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(new EventOnTypeBuilderInst(this, eventBuilder));
						}
					}
				}
			}
			EventInfo[] array = new EventInfo[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x0007C280 File Offset: 0x0007A480
		public override Type[] GetNestedTypes(BindingFlags bf)
		{
			return this.generic_type.GetNestedTypes(bf);
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x0007C290 File Offset: 0x0007A490
		public override bool IsAssignableFrom(Type c)
		{
			if (c == this)
			{
				return true;
			}
			Type[] interfacesInternal = this.GetInterfacesInternal();
			if (c.IsInterface)
			{
				if (interfacesInternal == null)
				{
					return false;
				}
				foreach (Type c2 in interfacesInternal)
				{
					if (c.IsAssignableFrom(c2))
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				Type parentType = this.GetParentType();
				if (parentType == null)
				{
					return c == typeof(object);
				}
				return c.IsAssignableFrom(parentType);
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x0007C310 File Offset: 0x0007A510
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x0007C314 File Offset: 0x0007A514
		public override string Name
		{
			get
			{
				return this.generic_type.Name;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007C324 File Offset: 0x0007A524
		public override string Namespace
		{
			get
			{
				return this.generic_type.Namespace;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0007C334 File Offset: 0x0007A534
		public override string FullName
		{
			get
			{
				return this.format_name(true, false);
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0007C340 File Offset: 0x0007A540
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.format_name(true, true);
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0007C34C File Offset: 0x0007A54C
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0007C354 File Offset: 0x0007A554
		private string format_name(bool full_name, bool assembly_qualified)
		{
			StringBuilder stringBuilder = new StringBuilder(this.generic_type.FullName);
			bool isCompilerContext = this.generic_type.IsCompilerContext;
			stringBuilder.Append("[");
			for (int i = 0; i < this.type_arguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				string text = (!full_name) ? this.type_arguments[i].ToString() : this.type_arguments[i].AssemblyQualifiedName;
				if (text == null)
				{
					if (!isCompilerContext || !this.type_arguments[i].IsGenericParameter)
					{
						return null;
					}
					text = this.type_arguments[i].Name;
				}
				if (full_name)
				{
					stringBuilder.Append("[");
				}
				stringBuilder.Append(text);
				if (full_name)
				{
					stringBuilder.Append("]");
				}
			}
			stringBuilder.Append("]");
			if (assembly_qualified)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.generic_type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0007C478 File Offset: 0x0007A678
		public override string ToString()
		{
			return this.format_name(false, false);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0007C484 File Offset: 0x0007A684
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0007C490 File Offset: 0x0007A690
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0007C4A8 File Offset: 0x0007A6A8
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x0007C4B0 File Offset: 0x0007A6B0
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0007C4B8 File Offset: 0x0007A6B8
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0007C4BC File Offset: 0x0007A6BC
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0007C4C0 File Offset: 0x0007A6C0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.generic_type.Attributes;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0007C4D0 File Offset: 0x0007A6D0
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0007C4D8 File Offset: 0x0007A6D8
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (!this.generic_type.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			foreach (EventInfo eventInfo in this.GetEvents(bindingAttr))
			{
				if (eventInfo.Name == name)
				{
					return eventInfo;
				}
			}
			return null;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0007C530 File Offset: 0x0007A730
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0007C538 File Offset: 0x0007A738
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0007C540 File Offset: 0x0007A740
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0007C548 File Offset: 0x0007A748
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007C550 File Offset: 0x0007A750
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0007C558 File Offset: 0x0007A758
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0007C560 File Offset: 0x0007A760
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0007C568 File Offset: 0x0007A768
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0007C570 File Offset: 0x0007A770
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0007C578 File Offset: 0x0007A778
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000CD1 RID: 3281
		private const BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000CD2 RID: 3282
		internal TypeBuilder generic_type;

		// Token: 0x04000CD3 RID: 3283
		private Type[] type_arguments;

		// Token: 0x04000CD4 RID: 3284
		private bool initialized;

		// Token: 0x04000CD5 RID: 3285
		private Hashtable fields;

		// Token: 0x04000CD6 RID: 3286
		private Hashtable ctors;

		// Token: 0x04000CD7 RID: 3287
		private Hashtable methods;

		// Token: 0x04000CD8 RID: 3288
		private int event_count;
	}
}
