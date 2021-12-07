using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the type of class interface to be generated for a class exposed to COM, if an interface is generated at all.</summary>
	// Token: 0x02000374 RID: 884
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration value.</summary>
		/// <param name="classInterfaceType">Describes the type of interface that is generated for a class. </param>
		// Token: 0x06002A1D RID: 10781 RVA: 0x000921C0 File Offset: 0x000903C0
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this.ciType = (ClassInterfaceType)classInterfaceType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> enumeration member.</summary>
		/// <param name="classInterfaceType">One of the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> values that describes the type of interface that is generated for a class. </param>
		// Token: 0x06002A1E RID: 10782 RVA: 0x000921D0 File Offset: 0x000903D0
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this.ciType = classInterfaceType;
		}

		/// <summary>Gets the <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</summary>
		/// <returns>The <see cref="T:System.Runtime.InteropServices.ClassInterfaceType" /> value that describes which type of interface should be generated for the class.</returns>
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002A1F RID: 10783 RVA: 0x000921E0 File Offset: 0x000903E0
		public ClassInterfaceType Value
		{
			get
			{
				return this.ciType;
			}
		}

		// Token: 0x040010D1 RID: 4305
		private ClassInterfaceType ciType;
	}
}
