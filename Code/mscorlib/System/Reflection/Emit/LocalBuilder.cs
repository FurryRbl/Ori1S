using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a local variable within a method or constructor.</summary>
	// Token: 0x020002E6 RID: 742
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x060025CD RID: 9677 RVA: 0x00085E74 File Offset: 0x00084074
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025CE RID: 9678 RVA: 0x00085E8C File Offset: 0x0008408C
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025CF RID: 9679 RVA: 0x00085E94 File Offset: 0x00084094
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060025D0 RID: 9680 RVA: 0x00085E9C File Offset: 0x0008409C
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060025D1 RID: 9681 RVA: 0x00085EA4 File Offset: 0x000840A4
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the name and lexical scope of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <param name="startOffset">The beginning offset of the lexical scope of the local variable. </param>
		/// <param name="endOffset">The ending offset of the lexical scope of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x060025D2 RID: 9682 RVA: 0x00085EAC File Offset: 0x000840AC
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		/// <summary>Sets the name of this local variable.</summary>
		/// <param name="name">The name of the local variable. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created with <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- There is no symbolic writer defined for the containing module. </exception>
		/// <exception cref="T:System.NotSupportedException">This local is defined in a dynamic method, rather than in a method of a dynamic type.</exception>
		// Token: 0x060025D3 RID: 9683 RVA: 0x00085EC4 File Offset: 0x000840C4
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the local variable.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x00085ED0 File Offset: 0x000840D0
		public override Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the local variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00085ED8 File Offset: 0x000840D8
		public override bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the zero-based index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x00085EE0 File Offset: 0x000840E0
		public override int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00085EE8 File Offset: 0x000840E8
		internal static int Mono_GetLocalIndex(LocalBuilder builder)
		{
			return (int)builder.position;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060025D8 RID: 9688 RVA: 0x00085EF0 File Offset: 0x000840F0
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00085EF8 File Offset: 0x000840F8
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060025DA RID: 9690 RVA: 0x00085F00 File Offset: 0x00084100
		internal int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x04000E3A RID: 3642
		private string name;

		// Token: 0x04000E3B RID: 3643
		internal ILGenerator ilgen;

		// Token: 0x04000E3C RID: 3644
		private int startOffset;

		// Token: 0x04000E3D RID: 3645
		private int endOffset;
	}
}
