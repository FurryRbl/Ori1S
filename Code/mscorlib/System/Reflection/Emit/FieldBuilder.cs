using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a field. This class cannot be inherited.</summary>
	// Token: 0x020002D7 RID: 727
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x060024FA RID: 9466 RVA: 0x00083144 File Offset: 0x00081344
		internal FieldBuilder(TypeBuilder tb, string fieldName, Type type, FieldAttributes attributes, Type[] modReq, Type[] modOpt)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.attrs = attributes;
			this.name = fieldName;
			this.type = type;
			this.modReq = modReq;
			this.modOpt = modOpt;
			this.offset = -1;
			this.typeb = tb;
			this.table_idx = tb.get_next_table_index(this, 4, true);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024FB RID: 9467 RVA: 0x000831AC File Offset: 0x000813AC
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024FC RID: 9468 RVA: 0x000831B4 File Offset: 0x000813B4
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060024FD RID: 9469 RVA: 0x000831BC File Offset: 0x000813BC
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060024FE RID: 9470 RVA: 0x000831C4 File Offset: 0x000813C4
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates the attributes of this field. This property is read-only.</summary>
		/// <returns>The attributes of this field.</returns>
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x000831CC File Offset: 0x000813CC
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Indicates a reference to the <see cref="T:System.Type" /> object for the type that declares this field. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object for the type that declares this field.</returns>
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002500 RID: 9472 RVA: 0x000831D4 File Offset: 0x000813D4
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Indicates the internal metadata handle for this field. This property is read-only.</summary>
		/// <returns>The internal metadata handle for this field.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x000831DC File Offset: 0x000813DC
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw this.CreateNotSupportedException();
			}
		}

		/// <summary>Indicates the <see cref="T:System.Type" /> object that represents the type of this field. This property is read-only.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type of this field.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000831E4 File Offset: 0x000813E4
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Indicates the name of this field. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this field.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x000831EC File Offset: 0x000813EC
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Indicates the reference to the <see cref="T:System.Type" /> object from which this object was obtained. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object from which this instance was obtained.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x000831F4 File Offset: 0x000813F4
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Returns all the custom attributes defined for this field.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06002505 RID: 9477 RVA: 0x000831FC File Offset: 0x000813FC
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		/// <summary>Returns all the custom attributes defined for this field identified by the given type.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06002506 RID: 9478 RVA: 0x0008321C File Offset: 0x0008141C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		/// <summary>Returns the token representing this field.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.Emit.FieldToken" /> object that represents the token for this field.</returns>
		// Token: 0x06002507 RID: 9479 RVA: 0x00083240 File Offset: 0x00081440
		public FieldToken GetToken()
		{
			return new FieldToken(this.MetadataToken);
		}

		/// <summary>Retrieves the value of the field supported by the given object.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object on which to access the field. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06002508 RID: 9480 RVA: 0x00083250 File Offset: 0x00081450
		public override object GetValue(object obj)
		{
			throw this.CreateNotSupportedException();
		}

		/// <summary>Indicates whether an attribute having the specified type is defined on a field.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this field; otherwise, false.</returns>
		/// <param name="attributeType">The type of the attribute. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the field using <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.FieldInfo" />. </exception>
		// Token: 0x06002509 RID: 9481 RVA: 0x00083258 File Offset: 0x00081458
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00083260 File Offset: 0x00081460
		internal override int GetFieldOffset()
		{
			return 0;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00083264 File Offset: 0x00081464
		internal void SetRVAData(byte[] data)
		{
			this.rva_data = (byte[])data.Clone();
		}

		/// <summary>Sets the default value of this field.</summary>
		/// <param name="defaultValue">The new default value for this field. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		/// <exception cref="T:System.ArgumentException">The field is not one of the supported types.-or-The type of <paramref name="defaultValue" /> does not match the type of the field.-or-The field is of type <see cref="T:System.Object" /> or other reference type, and <paramref name="defaultValue" /> is not null.</exception>
		// Token: 0x0600250C RID: 9484 RVA: 0x00083278 File Offset: 0x00081478
		public void SetConstant(object defaultValue)
		{
			this.RejectIfCreated();
			this.def_value = defaultValue;
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The parent type of this field is complete. </exception>
		// Token: 0x0600250D RID: 9485 RVA: 0x00083288 File Offset: 0x00081488
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.RejectIfCreated();
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.FieldOffsetAttribute")
			{
				byte[] data = customBuilder.Data;
				this.offset = (int)data[2];
				this.offset |= (int)data[3] << 8;
				this.offset |= (int)data[4] << 16;
				this.offset |= (int)data[5] << 24;
				return;
			}
			if (fullName == "System.NonSerializedAttribute")
			{
				this.attrs |= FieldAttributes.NotSerialized;
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= FieldAttributes.SpecialName;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= FieldAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, true);
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

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The parent type of this field is complete. </exception>
		// Token: 0x0600250E RID: 9486 RVA: 0x000833D0 File Offset: 0x000815D0
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.RejectIfCreated();
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Describes the native marshaling of the field.</summary>
		/// <param name="unmanagedMarshal">A descriptor specifying the native marshalling of this field. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unmanagedMarshal" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600250F RID: 9487 RVA: 0x000833E8 File Offset: 0x000815E8
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			this.marshal_info = unmanagedMarshal;
			this.attrs |= FieldAttributes.HasFieldMarshal;
		}

		/// <summary>Specifies the field layout.</summary>
		/// <param name="iOffset">The offset of the field within the type containing this field. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="iOffset" /> is less than zero.</exception>
		// Token: 0x06002510 RID: 9488 RVA: 0x0008340C File Offset: 0x0008160C
		public void SetOffset(int iOffset)
		{
			this.RejectIfCreated();
			this.offset = iOffset;
		}

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object on which to access the field. </param>
		/// <param name="val">The value to assign to the field. </param>
		/// <param name="invokeAttr">A member of IBinder that specifies the type of binding that is desired (for example, IBinder.CreateInstance, IBinder.ExactBinding). </param>
		/// <param name="binder">A set of properties and enabling for binding, coercion of argument types, and invocation of members using reflection. If binder is null, then IBinder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x06002511 RID: 9489 RVA: 0x0008341C File Offset: 0x0008161C
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00083424 File Offset: 0x00081624
		internal override UnmanagedMarshal UMarshal
		{
			get
			{
				return this.marshal_info;
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x0008342C File Offset: 0x0008162C
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00083438 File Offset: 0x00081638
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		/// <summary>Gets the module in which the type that contains this field is being defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> that represents the dynamic module in which this field is being defined.</returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x00083458 File Offset: 0x00081658
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x04000DE2 RID: 3554
		private FieldAttributes attrs;

		// Token: 0x04000DE3 RID: 3555
		private Type type;

		// Token: 0x04000DE4 RID: 3556
		private string name;

		// Token: 0x04000DE5 RID: 3557
		private object def_value;

		// Token: 0x04000DE6 RID: 3558
		private int offset;

		// Token: 0x04000DE7 RID: 3559
		private int table_idx;

		// Token: 0x04000DE8 RID: 3560
		internal TypeBuilder typeb;

		// Token: 0x04000DE9 RID: 3561
		private byte[] rva_data;

		// Token: 0x04000DEA RID: 3562
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000DEB RID: 3563
		private UnmanagedMarshal marshal_info;

		// Token: 0x04000DEC RID: 3564
		private RuntimeFieldHandle handle;

		// Token: 0x04000DED RID: 3565
		private Type[] modReq;

		// Token: 0x04000DEE RID: 3566
		private Type[] modOpt;
	}
}
