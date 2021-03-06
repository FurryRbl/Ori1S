using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines the properties for a type.</summary>
	// Token: 0x020002F7 RID: 759
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x0008A724 File Offset: 0x00088924
		internal PropertyBuilder(TypeBuilder tb, string name, PropertyAttributes attributes, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.type = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if (parameterTypes != null)
			{
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, this.parameters.Length);
			}
			this.typeb = tb;
			this.table_idx = tb.get_next_table_index(this, 23, true);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026D2 RID: 9938 RVA: 0x0008A7B0 File Offset: 0x000889B0
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026D3 RID: 9939 RVA: 0x0008A7B8 File Offset: 0x000889B8
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026D4 RID: 9940 RVA: 0x0008A7C0 File Offset: 0x000889C0
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026D5 RID: 9941 RVA: 0x0008A7C8 File Offset: 0x000889C8
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>Attributes of this property.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0008A7D0 File Offset: 0x000889D0
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>true if this property can be read; otherwise, false.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x0008A7D8 File Offset: 0x000889D8
		public override bool CanRead
		{
			get
			{
				return this.get_method != null;
			}
		}

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>true if this property can be written to; otherwise, false.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0008A7E8 File Offset: 0x000889E8
		public override bool CanWrite
		{
			get
			{
				return this.set_method != null;
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The Type object for the class that declares this member.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0008A7F8 File Offset: 0x000889F8
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Gets the name of this member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0008A800 File Offset: 0x00088A00
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the token for this property.</summary>
		/// <returns>Read-only. Retrieves the token for this property.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0008A808 File Offset: 0x00088A08
		public PropertyToken PropertyToken
		{
			get
			{
				return default(PropertyToken);
			}
		}

		/// <summary>Gets the type of the field of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0008A820 File Offset: 0x00088A20
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the class object that was used to obtain this instance of MemberInfo.</summary>
		/// <returns>The Type object through which this MemberInfo object was obtained.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x0008A828 File Offset: 0x00088A28
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Adds one of the other methods associated with this property.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the other method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060026DE RID: 9950 RVA: 0x0008A830 File Offset: 0x00088A30
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
		}

		/// <summary>Returns an array of the public and non-public get and set accessors on this property.</summary>
		/// <returns>An array of type MethodInfo containing the matching public or non-public accessors, or an empty array if matching accessors do not exist on this property.</returns>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the MethodInfo array. true if non-public methods are to be included; otherwise, false. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026DF RID: 9951 RVA: 0x0008A834 File Offset: 0x00088A34
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return null;
		}

		/// <summary>Returns an array of all the custom attributes for this property.</summary>
		/// <returns>An array of all the custom attributes.</returns>
		/// <param name="inherit">If true, walks up this property's inheritance chain to find the custom attributes </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E0 RID: 9952 RVA: 0x0008A838 File Offset: 0x00088A38
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns an array of custom attributes identified by <see cref="T:System.Type" />.</summary>
		/// <returns>An array of custom attributes defined on this reflected member, or null if no attributes are defined on this member.</returns>
		/// <param name="attributeType">An array of custom attributes identified by type. </param>
		/// <param name="inherit">If true, walks up this property's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E1 RID: 9953 RVA: 0x0008A840 File Offset: 0x00088A40
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the public and non-public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the get accessor for this property, if <paramref name="nonPublic" /> is true. Returns null if <paramref name="nonPublic" /> is false and the get accessor is non-public, or if <paramref name="nonPublic" /> is true but no get accessors exist.</returns>
		/// <param name="nonPublic">Indicates whether non-public get accessors should be returned. true if non-public methods are to be included; otherwise, false. </param>
		// Token: 0x060026E2 RID: 9954 RVA: 0x0008A848 File Offset: 0x00088A48
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.get_method;
		}

		/// <summary>Returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type ParameterInfo containing the parameters for the indexes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E3 RID: 9955 RVA: 0x0008A850 File Offset: 0x00088A50
		public override ParameterInfo[] GetIndexParameters()
		{
			throw this.not_supported();
		}

		/// <summary>Returns the set accessor for this property.</summary>
		/// <returns>Value Condition A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property. The set accessor is public.<paramref name="nonPublic" /> is true and non-public methods can be returned. null <paramref name="nonPublic" /> is true, but the property is read-only.<paramref name="nonPublic" /> is false and the set accessor is non-public. </returns>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if non-public methods are to be included; otherwise, false. </param>
		// Token: 0x060026E4 RID: 9956 RVA: 0x0008A858 File Offset: 0x00088A58
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.set_method;
		}

		/// <summary>Gets the value of the indexed property by calling the property's getter method.</summary>
		/// <returns>The value of the specified indexed property.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E5 RID: 9957 RVA: 0x0008A860 File Offset: 0x00088A60
		public override object GetValue(object obj, object[] index)
		{
			return null;
		}

		/// <summary>Gets the value of a property having the specified binding, index, and CultureInfo.</summary>
		/// <returns>The property value for <paramref name="obj" />.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. A suitable invocation attribute must be specified. If a static member is to be invoked, the Static flag of BindingFlags must be set. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The CultureInfo object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the CultureInfo.Parent method will be called successively in search of a match. If this value is null, the CultureInfo is obtained from the CultureInfo.CurrentUICulture property. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E6 RID: 9958 RVA: 0x0008A864 File Offset: 0x00088A64
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this property.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this property; otherwise false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to walk up this property's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026E7 RID: 9959 RVA: 0x0008A86C File Offset: 0x00088A6C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Sets the default value of this property.</summary>
		/// <param name="defaultValue">The default value of this property. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		/// <exception cref="T:System.ArgumentException">The property is not one of the supported types.-or-The type of <paramref name="defaultValue" /> does not match the type of the property.-or-The property is of type <see cref="T:System.Object" /> or other reference type, and <paramref name="defaultValue" /> is not null.</exception>
		// Token: 0x060026E8 RID: 9960 RVA: 0x0008A874 File Offset: 0x00088A74
		public void SetConstant(object defaultValue)
		{
			this.def_value = defaultValue;
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">if <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060026E9 RID: 9961 RVA: 0x0008A880 File Offset: 0x00088A80
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= PropertyAttributes.SpecialName;
				return;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
			}
			else
			{
				this.cattrs = new CustomAttributeBuilder[1];
				this.cattrs[0] = customBuilder;
			}
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060026EA RID: 9962 RVA: 0x0008A918 File Offset: 0x00088B18
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Sets the method that gets the property value.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method that gets the property value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060026EB RID: 9963 RVA: 0x0008A928 File Offset: 0x00088B28
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.get_method = mdBuilder;
		}

		/// <summary>Sets the method that sets the property value.</summary>
		/// <param name="mdBuilder">A MethodBuilder object that represents the method that sets the property value. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type. </exception>
		// Token: 0x060026EC RID: 9964 RVA: 0x0008A934 File Offset: 0x00088B34
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.set_method = mdBuilder;
		}

		/// <summary>Sets the value of the property with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new value for this property. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026ED RID: 9965 RVA: 0x0008A940 File Offset: 0x00088B40
		public override void SetValue(object obj, object value, object[] index)
		{
		}

		/// <summary>Sets the property value for the given object to the given value.</summary>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="value">The new value for this property. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags : InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. A suitable invocation attribute must be specified. If a static member is to be invoked, the Static flag of BindingFlags must be set. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The CultureInfo object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the CultureInfo.Parent method will be called successively in search of a match. If this value is null, the CultureInfo is obtained from the CultureInfo.CurrentUICulture property. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x060026EE RID: 9966 RVA: 0x0008A944 File Offset: 0x00088B44
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
		}

		/// <summary>Gets the module in which the type that declares the current property is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the current property is defined.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x0008A948 File Offset: 0x00088B48
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0008A950 File Offset: 0x00088B50
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x04000F9C RID: 3996
		private PropertyAttributes attrs;

		// Token: 0x04000F9D RID: 3997
		private string name;

		// Token: 0x04000F9E RID: 3998
		private Type type;

		// Token: 0x04000F9F RID: 3999
		private Type[] parameters;

		// Token: 0x04000FA0 RID: 4000
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000FA1 RID: 4001
		private object def_value;

		// Token: 0x04000FA2 RID: 4002
		private MethodBuilder set_method;

		// Token: 0x04000FA3 RID: 4003
		private MethodBuilder get_method;

		// Token: 0x04000FA4 RID: 4004
		private int table_idx;

		// Token: 0x04000FA5 RID: 4005
		internal TypeBuilder typeb;

		// Token: 0x04000FA6 RID: 4006
		private Type[] returnModReq;

		// Token: 0x04000FA7 RID: 4007
		private Type[] returnModOpt;

		// Token: 0x04000FA8 RID: 4008
		private Type[][] paramModReq;

		// Token: 0x04000FA9 RID: 4009
		private Type[][] paramModOpt;
	}
}
