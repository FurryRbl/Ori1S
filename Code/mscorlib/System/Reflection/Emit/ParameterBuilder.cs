﻿using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Creates or associates parameter information.</summary>
	// Token: 0x020002F4 RID: 756
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x060026B9 RID: 9913 RVA: 0x0008A430 File Offset: 0x00088630
		internal ParameterBuilder(MethodBase mb, int pos, ParameterAttributes attributes, string strParamName)
		{
			this.name = strParamName;
			this.position = pos;
			this.attrs = attributes;
			this.methodb = mb;
			if (mb is DynamicMethod)
			{
				this.table_idx = 0;
			}
			else
			{
				this.table_idx = mb.get_next_table_index(this, 8, true);
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026BA RID: 9914 RVA: 0x0008A488 File Offset: 0x00088688
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026BB RID: 9915 RVA: 0x0008A490 File Offset: 0x00088690
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x060026BC RID: 9916 RVA: 0x0008A498 File Offset: 0x00088698
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060026BD RID: 9917 RVA: 0x0008A4A0 File Offset: 0x000886A0
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the attributes for this parameter.</summary>
		/// <returns>Read-only. Retrieves the attributes for this parameter.</returns>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0008A4A8 File Offset: 0x000886A8
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		/// <summary>Retrieves whether this is an input parameter.</summary>
		/// <returns>Read-only. Retrieves whether this is an input parameter.</returns>
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0008A4B0 File Offset: 0x000886B0
		public bool IsIn
		{
			get
			{
				return (this.attrs & ParameterAttributes.In) != ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is an output parameter.</summary>
		/// <returns>Read-only. Retrieves whether this parameter is an output parameter.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0008A4C0 File Offset: 0x000886C0
		public bool IsOut
		{
			get
			{
				return (this.attrs & ParameterAttributes.Out) != ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is optional.</summary>
		/// <returns>Read-only. Specifies whether this parameter is optional.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x0008A4D0 File Offset: 0x000886D0
		public bool IsOptional
		{
			get
			{
				return (this.attrs & ParameterAttributes.Optional) != ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves the name of this parameter.</summary>
		/// <returns>Read-only. Retrieves the name of this parameter.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0008A4E4 File Offset: 0x000886E4
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the signature position for this parameter.</summary>
		/// <returns>Read-only. Retrieves the signature position for this parameter.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x0008A4EC File Offset: 0x000886EC
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>Retrieves the token for this parameter.</summary>
		/// <returns>Returns the token for this parameter.</returns>
		// Token: 0x060026C4 RID: 9924 RVA: 0x0008A4F4 File Offset: 0x000886F4
		public virtual ParameterToken GetToken()
		{
			return new ParameterToken(8 | this.table_idx);
		}

		/// <summary>Sets the default value of the parameter.</summary>
		/// <param name="defaultValue">The default value of this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The parameter is not one of the supported types.-or-The type of <paramref name="defaultValue" /> does not match the type of the parameter.-or-The parameter is of type <see cref="T:System.Object" /> or other reference type, and <paramref name="defaultValue" /> is not null. </exception>
		// Token: 0x060026C5 RID: 9925 RVA: 0x0008A504 File Offset: 0x00088704
		public virtual void SetConstant(object defaultValue)
		{
			this.def_value = defaultValue;
			this.attrs |= ParameterAttributes.HasDefault;
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x060026C6 RID: 9926 RVA: 0x0008A520 File Offset: 0x00088720
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.InAttribute")
			{
				this.attrs |= ParameterAttributes.In;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OutAttribute")
			{
				this.attrs |= ParameterAttributes.Out;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OptionalAttribute")
			{
				this.attrs |= ParameterAttributes.Optional;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= ParameterAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, false);
				return;
			}
			if (fullName == "System.Runtime.InteropServices.DefaultParameterValueAttribute")
			{
				this.SetConstant(CustomAttributeBuilder.decode_cattr(customBuilder).ctorArgs[0]);
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
		// Token: 0x060026C7 RID: 9927 RVA: 0x0008A648 File Offset: 0x00088848
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Specifies the marshaling for this parameter.</summary>
		/// <param name="unmanagedMarshal">The marshaling information for this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unmanagedMarshal" /> is null. </exception>
		// Token: 0x060026C8 RID: 9928 RVA: 0x0008A658 File Offset: 0x00088858
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.marshal_info = unmanagedMarshal;
			this.attrs |= ParameterAttributes.HasFieldMarshal;
		}

		// Token: 0x04000F8E RID: 3982
		private MethodBase methodb;

		// Token: 0x04000F8F RID: 3983
		private string name;

		// Token: 0x04000F90 RID: 3984
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04000F91 RID: 3985
		private UnmanagedMarshal marshal_info;

		// Token: 0x04000F92 RID: 3986
		private ParameterAttributes attrs;

		// Token: 0x04000F93 RID: 3987
		private int position;

		// Token: 0x04000F94 RID: 3988
		private int table_idx;

		// Token: 0x04000F95 RID: 3989
		private object def_value;
	}
}
