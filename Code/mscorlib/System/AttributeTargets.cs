using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the application elements on which it is valid to apply an attribute.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000106 RID: 262
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum AttributeTargets
	{
		/// <summary>Attribute can be applied to an assembly.</summary>
		// Token: 0x040003A7 RID: 935
		Assembly = 1,
		/// <summary>Attribute can be applied to a module.</summary>
		// Token: 0x040003A8 RID: 936
		Module = 2,
		/// <summary>Attribute can be applied to a class.</summary>
		// Token: 0x040003A9 RID: 937
		Class = 4,
		/// <summary>Attribute can be applied to a structure; that is, a value type.</summary>
		// Token: 0x040003AA RID: 938
		Struct = 8,
		/// <summary>Attribute can be applied to an enumeration.</summary>
		// Token: 0x040003AB RID: 939
		Enum = 16,
		/// <summary>Attribute can be applied to a constructor.</summary>
		// Token: 0x040003AC RID: 940
		Constructor = 32,
		/// <summary>Attribute can be applied to a method.</summary>
		// Token: 0x040003AD RID: 941
		Method = 64,
		/// <summary>Attribute can be applied to a property.</summary>
		// Token: 0x040003AE RID: 942
		Property = 128,
		/// <summary>Attribute can be applied to a field.</summary>
		// Token: 0x040003AF RID: 943
		Field = 256,
		/// <summary>Attribute can be applied to an event.</summary>
		// Token: 0x040003B0 RID: 944
		Event = 512,
		/// <summary>Attribute can be applied to an interface.</summary>
		// Token: 0x040003B1 RID: 945
		Interface = 1024,
		/// <summary>Attribute can be applied to a parameter.</summary>
		// Token: 0x040003B2 RID: 946
		Parameter = 2048,
		/// <summary>Attribute can be applied to a delegate.</summary>
		// Token: 0x040003B3 RID: 947
		Delegate = 4096,
		/// <summary>Attribute can be applied to a return value.</summary>
		// Token: 0x040003B4 RID: 948
		ReturnValue = 8192,
		/// <summary>Attribute can be applied to a generic parameter.</summary>
		// Token: 0x040003B5 RID: 949
		GenericParameter = 16384,
		/// <summary>Attribute can be applied to any application element.</summary>
		// Token: 0x040003B6 RID: 950
		All = 32767
	}
}
