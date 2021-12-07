using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the class identifier of a coclass imported from a type library.</summary>
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class CoClassAttribute : Attribute
	{
		/// <summary>Initializes new instance of the <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> with the class identifier of the original coclass.</summary>
		/// <param name="coClass">A <see cref="T:System.Type" /> that contains the class identifier of the original coclass. </param>
		// Token: 0x06000671 RID: 1649 RVA: 0x00014B80 File Offset: 0x00012D80
		public CoClassAttribute(Type coClass)
		{
			this.klass = coClass;
		}

		/// <summary>Gets the class identifier of the original coclass.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the class identifier of the original coclass.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00014B90 File Offset: 0x00012D90
		public Type CoClass
		{
			get
			{
				return this.klass;
			}
		}

		// Token: 0x040000A0 RID: 160
		private Type klass;
	}
}
