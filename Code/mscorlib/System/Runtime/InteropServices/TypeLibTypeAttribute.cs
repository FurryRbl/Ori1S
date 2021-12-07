using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Contains the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> that were originally imported for this type from the COM type library.</summary>
	// Token: 0x020003C9 RID: 969
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the TypeLibTypeAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from. </param>
		// Token: 0x06002B89 RID: 11145 RVA: 0x00093C6C File Offset: 0x00091E6C
		public TypeLibTypeAttribute(short flags)
		{
			this.flags = (TypeLibTypeFlags)flags;
		}

		/// <summary>Initializes a new instance of the TypeLibTypeAttribute class with the specified <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value.</summary>
		/// <param name="flags">The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for the attributed type as found in the type library it was imported from. </param>
		// Token: 0x06002B8A RID: 11146 RVA: 0x00093C7C File Offset: 0x00091E7C
		public TypeLibTypeAttribute(TypeLibTypeFlags flags)
		{
			this.flags = flags;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.TypeLibTypeFlags" /> value for this type.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x00093C8C File Offset: 0x00091E8C
		public TypeLibTypeFlags Value
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x040011F6 RID: 4598
		private TypeLibTypeFlags flags;
	}
}
