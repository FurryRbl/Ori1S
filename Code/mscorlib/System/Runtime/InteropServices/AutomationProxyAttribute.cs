using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies whether the type should be marshaled using the Automation marshaler or a custom proxy and stub.</summary>
	// Token: 0x0200036C RID: 876
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	public sealed class AutomationProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.AutomationProxyAttribute" /> class.</summary>
		/// <param name="val">true if the class should be marshaled using the Automation Marshaler; false if a proxy stub marshaler should be used. </param>
		// Token: 0x06002A17 RID: 10775 RVA: 0x00092178 File Offset: 0x00090378
		public AutomationProxyAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating the type of marshaler to use.</summary>
		/// <returns>true if the class should be marshaled using the Automation Marshaler; false if a proxy stub marshaler should be used.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x00092188 File Offset: 0x00090388
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040010B0 RID: 4272
		private bool val;
	}
}
