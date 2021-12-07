using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Retrieves the mapping of an interface into the actual methods on a class that implements that interface.</summary>
	// Token: 0x02000293 RID: 659
	[ComVisible(true)]
	public struct InterfaceMapping
	{
		/// <summary>Shows the methods that are defined on the interface.</summary>
		// Token: 0x04000C7A RID: 3194
		[ComVisible(true)]
		public MethodInfo[] InterfaceMethods;

		/// <summary>Shows the type that represents the interface.</summary>
		// Token: 0x04000C7B RID: 3195
		[ComVisible(true)]
		public Type InterfaceType;

		/// <summary>Shows the methods that implement the interface.</summary>
		// Token: 0x04000C7C RID: 3196
		[ComVisible(true)]
		public MethodInfo[] TargetMethods;

		/// <summary>Represents the type that was used to create the interface mapping.</summary>
		// Token: 0x04000C7D RID: 3197
		[ComVisible(true)]
		public Type TargetType;
	}
}
