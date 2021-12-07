using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that an importing compiler must fully understand the semantics of a type definition, or refuse to use it.  This class cannot be inherited. </summary>
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> class.</summary>
		/// <param name="requiredContract">A type that an importing compiler must fully understand.This parameter is not supported in the .NET Framework version 2.0 and later. </param>
		// Token: 0x06000668 RID: 1640 RVA: 0x00014B20 File Offset: 0x00012D20
		public RequiredAttributeAttribute(Type requiredContract)
		{
		}

		/// <summary>Gets a type that an importing compiler must fully understand.</summary>
		/// <returns>A type that an importing compiler must fully understand. </returns>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00014B28 File Offset: 0x00012D28
		public Type RequiredContract
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
