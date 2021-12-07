using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> that were originally imported for this method from the COM type library.</summary>
	// Token: 0x020003C5 RID: 965
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibFuncAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the TypeLibFuncAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from. </param>
		// Token: 0x06002B84 RID: 11140 RVA: 0x00093C28 File Offset: 0x00091E28
		public TypeLibFuncAttribute(short flags)
		{
			this.flags = (TypeLibFuncFlags)flags;
		}

		/// <summary>Initializes a new instance of the TypeLibFuncAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for the attributed method as found in the type library it was imported from. </param>
		// Token: 0x06002B85 RID: 11141 RVA: 0x00093C38 File Offset: 0x00091E38
		public TypeLibFuncAttribute(TypeLibFuncFlags flags)
		{
			this.flags = flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibFuncFlags" /> value for this method.</returns>
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00093C48 File Offset: 0x00091E48
		public TypeLibFuncFlags Value
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x040011D9 RID: 4569
		private TypeLibFuncFlags flags;
	}
}
