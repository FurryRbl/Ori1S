using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the class that describes how to marshal a field from managed to unmanaged code. This class cannot be inherited.</summary>
	// Token: 0x02000301 RID: 769
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[ComVisible(true)]
	[Serializable]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x060027C7 RID: 10183 RVA: 0x0008DCC0 File Offset: 0x0008BEC0
		private UnmanagedMarshal(UnmanagedType maint, int cnt)
		{
			this.count = cnt;
			this.t = maint;
			this.tbase = maint;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0008DCE0 File Offset: 0x0008BEE0
		private UnmanagedMarshal(UnmanagedType maint, UnmanagedType elemt)
		{
			this.count = 0;
			this.t = maint;
			this.tbase = elemt;
		}

		/// <summary>Gets an unmanaged base type. This property is read-only.</summary>
		/// <returns>An UnmanagedType object.</returns>
		/// <exception cref="T:System.ArgumentException">The unmanaged type is not an LPArray or a SafeArray. </exception>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060027C9 RID: 10185 RVA: 0x0008DD00 File Offset: 0x0008BF00
		public UnmanagedType BaseType
		{
			get
			{
				if (this.t == UnmanagedType.LPArray || this.t == UnmanagedType.SafeArray)
				{
					throw new ArgumentException();
				}
				return this.tbase;
			}
		}

		/// <summary>Gets a number element. This property is read-only.</summary>
		/// <returns>An integer indicating the element count.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not an unmanaged element count. </exception>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x0008DD34 File Offset: 0x0008BF34
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Indicates an unmanaged type. This property is read-only.</summary>
		/// <returns>An <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> object.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x0008DD3C File Offset: 0x0008BF3C
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.t;
			}
		}

		/// <summary>Gets a GUID. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The argument is not a custom marshaler. </exception>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x0008DD44 File Offset: 0x0008BF44
		public Guid IIDGuid
		{
			get
			{
				return new Guid(this.guid);
			}
		}

		/// <summary>Specifies a fixed-length array (ByValArray) to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemCount">The number of elements in the fixed-length array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060027CD RID: 10189 RVA: 0x0008DD54 File Offset: 0x0008BF54
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, elemCount);
		}

		/// <summary>Specifies a string in a fixed array buffer (ByValTStr) to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemCount">The number of elements in the fixed array buffer. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060027CE RID: 10190 RVA: 0x0008DD60 File Offset: 0x0008BF60
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, elemCount);
		}

		/// <summary>Specifies an LPArray to marshal to unmanaged code. The length of an LPArray is determined at runtime by the size of the actual marshaled array.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemType">The unmanaged type to which to marshal the array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060027CF RID: 10191 RVA: 0x0008DD6C File Offset: 0x0008BF6C
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType);
		}

		/// <summary>Specifies a SafeArray to marshal to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="elemType">The base type or the UnmanagedType of each element of the array. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060027D0 RID: 10192 RVA: 0x0008DD78 File Offset: 0x0008BF78
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, elemType);
		}

		/// <summary>Specifies a given type that is to be marshaled to unmanaged code.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.UnmanagedMarshal" /> object.</returns>
		/// <param name="unmanagedType">The unmanaged type to which the type is to be marshaled. </param>
		/// <exception cref="T:System.ArgumentException">The argument is not a simple native type. </exception>
		// Token: 0x060027D1 RID: 10193 RVA: 0x0008DD84 File Offset: 0x0008BF84
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			return new UnmanagedMarshal(unmanagedType, unmanagedType);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0008DD90 File Offset: 0x0008BF90
		public static UnmanagedMarshal DefineCustom(Type typeref, string cookie, string mtype, Guid id)
		{
			UnmanagedMarshal unmanagedMarshal = new UnmanagedMarshal(UnmanagedType.CustomMarshaler, UnmanagedType.CustomMarshaler);
			unmanagedMarshal.mcookie = cookie;
			unmanagedMarshal.marshaltype = mtype;
			unmanagedMarshal.marshaltyperef = typeref;
			if (id == Guid.Empty)
			{
				unmanagedMarshal.guid = string.Empty;
			}
			else
			{
				unmanagedMarshal.guid = id.ToString();
			}
			return unmanagedMarshal;
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0008DDEC File Offset: 0x0008BFEC
		internal static UnmanagedMarshal DefineLPArrayInternal(UnmanagedType elemType, int sizeConst, int sizeParamIndex)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType)
			{
				count = sizeConst,
				param_num = sizeParamIndex,
				has_size = true
			};
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0008DE18 File Offset: 0x0008C018
		internal MarshalAsAttribute ToMarshalAsAttribute()
		{
			MarshalAsAttribute marshalAsAttribute = new MarshalAsAttribute(this.t);
			marshalAsAttribute.ArraySubType = this.tbase;
			marshalAsAttribute.MarshalCookie = this.mcookie;
			marshalAsAttribute.MarshalType = this.marshaltype;
			marshalAsAttribute.MarshalTypeRef = this.marshaltyperef;
			if (this.count == -1)
			{
				marshalAsAttribute.SizeConst = 0;
			}
			else
			{
				marshalAsAttribute.SizeConst = this.count;
			}
			if (this.param_num == -1)
			{
				marshalAsAttribute.SizeParamIndex = 0;
			}
			else
			{
				marshalAsAttribute.SizeParamIndex = (short)this.param_num;
			}
			return marshalAsAttribute;
		}

		// Token: 0x04000FFA RID: 4090
		private int count;

		// Token: 0x04000FFB RID: 4091
		private UnmanagedType t;

		// Token: 0x04000FFC RID: 4092
		private UnmanagedType tbase;

		// Token: 0x04000FFD RID: 4093
		private string guid;

		// Token: 0x04000FFE RID: 4094
		private string mcookie;

		// Token: 0x04000FFF RID: 4095
		private string marshaltype;

		// Token: 0x04001000 RID: 4096
		private Type marshaltyperef;

		// Token: 0x04001001 RID: 4097
		private int param_num;

		// Token: 0x04001002 RID: 4098
		private bool has_size;
	}
}
