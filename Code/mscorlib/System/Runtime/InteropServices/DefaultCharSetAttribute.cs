using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the value of the <see cref="T:System.Runtime.InteropServices.CharSet" /> enumeration. This class cannot be inherited.</summary>
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.CharSet" /> value.</summary>
		/// <param name="charSet">One of the <see cref="T:System.Runtime.InteropServices.CharSet" /> values.</param>
		// Token: 0x06000674 RID: 1652 RVA: 0x00014BA0 File Offset: 0x00012DA0
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._set = charSet;
		}

		/// <summary>Gets the default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</summary>
		/// <returns>The default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</returns>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public CharSet CharSet
		{
			get
			{
				return this._set;
			}
		}

		// Token: 0x040000A1 RID: 161
		private CharSet _set;
	}
}
