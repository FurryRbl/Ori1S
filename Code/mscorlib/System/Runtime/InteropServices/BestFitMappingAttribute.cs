using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls whether Unicode characters are converted to the closest matching ANSI characters.</summary>
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> class set to the value of the <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> property.</summary>
		/// <param name="BestFitMapping">true to indicate that best-fit mapping is enabled; otherwise, false. The default is true. </param>
		// Token: 0x06002A19 RID: 10777 RVA: 0x00092190 File Offset: 0x00090390
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this.bfm = BestFitMapping;
		}

		/// <summary>Gets the best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		/// <returns>true if best-fit mapping is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000921A0 File Offset: 0x000903A0
		public bool BestFitMapping
		{
			get
			{
				return this.bfm;
			}
		}

		// Token: 0x040010B1 RID: 4273
		private bool bfm;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI '?' character.</summary>
		// Token: 0x040010B2 RID: 4274
		public bool ThrowOnUnmappableChar;
	}
}
