using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies which <see cref="T:System.Type" /> exclusively uses an interface. This class cannot be inherited.</summary>
	// Token: 0x020003C7 RID: 967
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> class specifying the <see cref="T:System.Type" /> that exclusively uses an interface. </summary>
		/// <param name="importClass">The <see cref="T:System.Type" /> object that exclusively uses an interface.</param>
		// Token: 0x06002B87 RID: 11143 RVA: 0x00093C50 File Offset: 0x00091E50
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClass = importClass.ToString();
		}

		/// <summary>Gets the name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</summary>
		/// <returns>The name of a <see cref="T:System.Type" /> object that exclusively uses an interface.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x00093C64 File Offset: 0x00091E64
		public string Value
		{
			get
			{
				return this._importClass;
			}
		}

		// Token: 0x040011E8 RID: 4584
		private string _importClass;
	}
}
