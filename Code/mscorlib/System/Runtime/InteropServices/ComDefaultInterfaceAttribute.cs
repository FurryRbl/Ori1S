using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies a default interface to expose to COM. This class cannot be inherited.</summary>
	// Token: 0x02000379 RID: 889
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComDefaultInterfaceAttribute" /> class with the specified <see cref="T:System.Type" /> object as the default interface exposed to COM.</summary>
		/// <param name="defaultInterface">A <see cref="T:System.Type" /> value indicating the default interface to expose to COM. </param>
		// Token: 0x06002A28 RID: 10792 RVA: 0x00092250 File Offset: 0x00090450
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this._type = defaultInterface;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that specifies the default interface to expose to COM.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x00092260 File Offset: 0x00090460
		public Type Value
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040010DB RID: 4315
		private Type _type;
	}
}
