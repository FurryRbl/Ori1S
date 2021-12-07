using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Marks each type of member that is defined as a derived class of MemberInfo.</summary>
	// Token: 0x02000298 RID: 664
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MemberTypes
	{
		/// <summary>Specifies that the member is a constructor, representing a <see cref="T:System.Reflection.ConstructorInfo" /> member. Hexadecimal value of 0x01.</summary>
		// Token: 0x04000C8B RID: 3211
		Constructor = 1,
		/// <summary>Specifies that the member is an event, representing an <see cref="T:System.Reflection.EventInfo" /> member. Hexadecimal value of 0x02.</summary>
		// Token: 0x04000C8C RID: 3212
		Event = 2,
		/// <summary>Specifies that the member is a field, representing a <see cref="T:System.Reflection.FieldInfo" /> member. Hexadecimal value of 0x04.</summary>
		// Token: 0x04000C8D RID: 3213
		Field = 4,
		/// <summary>Specifies that the member is a method, representing a <see cref="T:System.Reflection.MethodInfo" /> member. Hexadecimal value of 0x08.</summary>
		// Token: 0x04000C8E RID: 3214
		Method = 8,
		/// <summary>Specifies that the member is a property, representing a <see cref="T:System.Reflection.PropertyInfo" /> member. Hexadecimal value of 0x10.</summary>
		// Token: 0x04000C8F RID: 3215
		Property = 16,
		/// <summary>Specifies that the member is a type, representing a <see cref="F:System.Reflection.MemberTypes.TypeInfo" /> member. Hexadecimal value of 0x20.</summary>
		// Token: 0x04000C90 RID: 3216
		TypeInfo = 32,
		/// <summary>Specifies that the member is a custom member type. Hexadecimal value of 0x40.</summary>
		// Token: 0x04000C91 RID: 3217
		Custom = 64,
		/// <summary>Specifies that the member is a nested type, extending <see cref="T:System.Reflection.MemberInfo" />.</summary>
		// Token: 0x04000C92 RID: 3218
		NestedType = 128,
		/// <summary>Specifies all member types.</summary>
		// Token: 0x04000C93 RID: 3219
		All = 191
	}
}
