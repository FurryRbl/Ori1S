using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the default value for the attributed field or parameter is an instance of <see cref="T:System.Runtime.InteropServices.UnknownWrapper" />, where the <see cref="P:System.Runtime.InteropServices.UnknownWrapper.WrappedObject" /> is null. This class cannot be inherited. </summary>
	// Token: 0x02000331 RID: 817
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Gets the IUnknown constant stored in this attribute.</summary>
		/// <returns>The IUnknown constant stored in this attribute. Only null is allowed for an IUnknown constant value.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x00091DF4 File Offset: 0x0008FFF4
		public override object Value
		{
			get
			{
				return null;
			}
		}
	}
}
