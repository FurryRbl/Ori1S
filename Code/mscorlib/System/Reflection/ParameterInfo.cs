using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a parameter and provides access to parameter metadata.</summary>
	// Token: 0x020002B0 RID: 688
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[Serializable]
	public class ParameterInfo : ICustomAttributeProvider, _ParameterInfo
	{
		/// <summary>Initializes a new instance of the ParameterInfo class.</summary>
		// Token: 0x060022F6 RID: 8950 RVA: 0x0007DAAC File Offset: 0x0007BCAC
		protected ParameterInfo()
		{
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0007DAB4 File Offset: 0x0007BCB4
		internal ParameterInfo(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			if (pb != null)
			{
				this.NameImpl = pb.Name;
				this.PositionImpl = pb.Position - 1;
				this.AttrsImpl = (ParameterAttributes)pb.Attributes;
			}
			else
			{
				this.NameImpl = null;
				this.PositionImpl = position - 1;
				this.AttrsImpl = ParameterAttributes.None;
			}
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0007DB20 File Offset: 0x0007BD20
		internal ParameterInfo(ParameterInfo pinfo, MemberInfo member)
		{
			this.ClassImpl = pinfo.ParameterType;
			this.MemberImpl = member;
			this.NameImpl = pinfo.Name;
			this.PositionImpl = pinfo.Position;
			this.AttrsImpl = pinfo.Attributes;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0007DB6C File Offset: 0x0007BD6C
		internal ParameterInfo(Type type, MemberInfo member, UnmanagedMarshal marshalAs)
		{
			this.ClassImpl = type;
			this.MemberImpl = member;
			this.NameImpl = string.Empty;
			this.PositionImpl = -1;
			this.AttrsImpl = ParameterAttributes.Retval;
			this.marshalAs = marshalAs;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022FA RID: 8954 RVA: 0x0007DBB0 File Offset: 0x0007BDB0
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022FB RID: 8955 RVA: 0x0007DBB8 File Offset: 0x0007BDB8
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022FC RID: 8956 RVA: 0x0007DBC0 File Offset: 0x0007BDC0
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022FD RID: 8957 RVA: 0x0007DBC8 File Offset: 0x0007BDC8
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the parameter type and name represented as a string.</summary>
		/// <returns>A string containing the type and the name of the parameter.</returns>
		// Token: 0x060022FE RID: 8958 RVA: 0x0007DBD0 File Offset: 0x0007BDD0
		public override string ToString()
		{
			Type type = this.ClassImpl;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			bool flag = type.IsPrimitive || this.ClassImpl == typeof(void) || this.ClassImpl.Namespace == this.MemberImpl.DeclaringType.Namespace;
			string text = (!flag) ? this.ClassImpl.FullName : this.ClassImpl.Name;
			if (!this.IsRetval)
			{
				text += ' ';
				text += this.NameImpl;
			}
			return text;
		}

		/// <summary>Gets the Type of this parameter.</summary>
		/// <returns>The Type object that represents the Type of this parameter.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0007DC88 File Offset: 0x0007BE88
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		/// <summary>Gets the attributes for this parameter.</summary>
		/// <returns>A ParameterAttributes object representing the attributes for this parameter.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x0007DC90 File Offset: 0x0007BE90
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0007DC98 File Offset: 0x0007BE98
		public virtual object DefaultValue
		{
			get
			{
				if (this.ClassImpl == typeof(decimal))
				{
					DecimalConstantAttribute[] array = (DecimalConstantAttribute[])this.GetCustomAttributes(typeof(DecimalConstantAttribute), false);
					if (array.Length > 0)
					{
						return array[0].Value;
					}
				}
				else if (this.ClassImpl == typeof(DateTime))
				{
					DateTimeConstantAttribute[] array2 = (DateTimeConstantAttribute[])this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (array2.Length > 0)
					{
						return new DateTime(array2[0].Ticks);
					}
				}
				return this.DefaultValueImpl;
			}
		}

		/// <summary>Gets a value indicating whether this is an input parameter.</summary>
		/// <returns>true if the parameter is an input parameter; otherwise, false.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0007DD3C File Offset: 0x0007BF3C
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) != ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is a locale identifier (lcid).</summary>
		/// <returns>true if the parameter is a locale identifier; otherwise, false.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0007DD4C File Offset: 0x0007BF4C
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) != ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is optional.</summary>
		/// <returns>true if the parameter is optional; otherwise, false.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0007DD5C File Offset: 0x0007BF5C
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) != ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is an output parameter.</summary>
		/// <returns>true if the parameter is an output parameter; otherwise, false.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0007DD70 File Offset: 0x0007BF70
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) != ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is a Retval parameter.</summary>
		/// <returns>true if the parameter is a Retval; otherwise, false.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007DD80 File Offset: 0x0007BF80
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) != ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating the member in which the parameter is implemented.</summary>
		/// <returns>A MemberInfo object.</returns>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0007DD90 File Offset: 0x0007BF90
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>A String containing the simple name of this parameter.</returns>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0007DD98 File Offset: 0x0007BF98
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		/// <summary>Gets the zero-based position of the parameter in the formal parameter list.</summary>
		/// <returns>An integer representing the position this parameter occupies in the parameter list.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0007DDA0 File Offset: 0x0007BFA0
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x0600230A RID: 8970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetMetadataToken();

		/// <summary>Gets a value that identifies this parameter in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this parameter in metadata.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x0007DDA8 File Offset: 0x0007BFA8
		public int MetadataToken
		{
			get
			{
				if (this.MemberImpl is PropertyInfo)
				{
					PropertyInfo propertyInfo = (PropertyInfo)this.MemberImpl;
					MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
					if (methodInfo == null)
					{
						methodInfo = propertyInfo.GetSetMethod(true);
					}
					return methodInfo.GetParameters()[this.PositionImpl].MetadataToken;
				}
				if (this.MemberImpl is MethodBase)
				{
					return this.GetMetadataToken();
				}
				throw new ArgumentException("Can't produce MetadataToken for member of type " + this.MemberImpl.GetType());
			}
		}

		/// <summary>Gets all the custom attributes applied to this parameter.</summary>
		/// <returns>An array that contains all the custom attributes applied to this parameter.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x0600230C RID: 8972 RVA: 0x0007DE2C File Offset: 0x0007C02C
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		/// <summary>Gets the custom attributes of the specified type or its derived types that are applied to this parameter.</summary>
		/// <returns>An array that contains the custom attributes of the specified type or its derived types.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x0600230D RID: 8973 RVA: 0x0007DE38 File Offset: 0x0007C038
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		/// <summary>Determines whether the custom attribute of the specified type or its derived types is applied to this parameter.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> or its derived types are applied to this parameter; otherwise, false.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the common language runtime.</exception>
		// Token: 0x0600230E RID: 8974 RVA: 0x0007DE44 File Offset: 0x0007C044
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x0007DE50 File Offset: 0x0007C050
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsIn)
			{
				num++;
			}
			if (this.IsOut)
			{
				num++;
			}
			if (this.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsIn)
			{
				array[num++] = new InAttribute();
			}
			if (this.IsOptional)
			{
				array[num++] = new OptionalAttribute();
			}
			if (this.IsOut)
			{
				array[num++] = new OutAttribute();
			}
			if (this.marshalAs != null)
			{
				array[num++] = this.marshalAs.ToMarshalAsAttribute();
			}
			return array;
		}

		// Token: 0x06002310 RID: 8976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		/// <summary>Gets the optional custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002311 RID: 8977 RVA: 0x0007DF10 File Offset: 0x0007C110
		public virtual Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Gets the required custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002312 RID: 8978 RVA: 0x0007DF34 File Offset: 0x0007C134
		public virtual Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0007DF58 File Offset: 0x0007C158
		public virtual object RawDefaultValue
		{
			get
			{
				return this.DefaultValue;
			}
		}

		/// <summary>The Type of the parameter.</summary>
		// Token: 0x04000D16 RID: 3350
		protected Type ClassImpl;

		/// <summary>The default value of the parameter.</summary>
		// Token: 0x04000D17 RID: 3351
		protected object DefaultValueImpl;

		/// <summary>The member in which the field is implemented.</summary>
		// Token: 0x04000D18 RID: 3352
		protected MemberInfo MemberImpl;

		/// <summary>The name of the parameter.</summary>
		// Token: 0x04000D19 RID: 3353
		protected string NameImpl;

		/// <summary>The zero-based position of the parameter in the parameter list.</summary>
		// Token: 0x04000D1A RID: 3354
		protected int PositionImpl;

		/// <summary>The attributes of the parameter.</summary>
		// Token: 0x04000D1B RID: 3355
		protected ParameterAttributes AttrsImpl;

		// Token: 0x04000D1C RID: 3356
		private UnmanagedMarshal marshalAs;
	}
}
