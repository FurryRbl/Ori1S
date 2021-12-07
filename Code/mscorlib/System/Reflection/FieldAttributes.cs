using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags that describe the attributes of a field.</summary>
	// Token: 0x0200028F RID: 655
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FieldAttributes
	{
		/// <summary>Specifies the access level of a given field.</summary>
		// Token: 0x04000C5A RID: 3162
		FieldAccessMask = 7,
		/// <summary>Specifies that the field cannot be referenced.</summary>
		// Token: 0x04000C5B RID: 3163
		PrivateScope = 0,
		/// <summary>Specifies that the field is accessible only by the parent type.</summary>
		// Token: 0x04000C5C RID: 3164
		Private = 1,
		/// <summary>Specifies that the field is accessible only by subtypes in this assembly.</summary>
		// Token: 0x04000C5D RID: 3165
		FamANDAssem = 2,
		/// <summary>Specifies that the field is accessible throughout the assembly.</summary>
		// Token: 0x04000C5E RID: 3166
		Assembly = 3,
		/// <summary>Specifies that the field is accessible only by type and subtypes.</summary>
		// Token: 0x04000C5F RID: 3167
		Family = 4,
		/// <summary>Specifies that the field is accessible by subtypes anywhere, as well as throughout this assembly.</summary>
		// Token: 0x04000C60 RID: 3168
		FamORAssem = 5,
		/// <summary>Specifies that the field is accessible by any member for whom this scope is visible.</summary>
		// Token: 0x04000C61 RID: 3169
		Public = 6,
		/// <summary>Specifies that the field represents the defined type, or else it is per-instance.</summary>
		// Token: 0x04000C62 RID: 3170
		Static = 16,
		/// <summary>Specifies that the field is initialized only, and can be set only in the body of a constructor. </summary>
		// Token: 0x04000C63 RID: 3171
		InitOnly = 32,
		/// <summary>Specifies that the field's value is a compile-time (static or early bound) constant. Any attempt to set it throws <see cref="T:System.FieldAccessException" />.</summary>
		// Token: 0x04000C64 RID: 3172
		Literal = 64,
		/// <summary>Specifies that the field does not have to be serialized when the type is remoted.</summary>
		// Token: 0x04000C65 RID: 3173
		NotSerialized = 128,
		/// <summary>Specifies that the field has a relative virtual address (RVA). The RVA is the location of the method body in the current image, as an address relative to the start of the image file in which it is located.</summary>
		// Token: 0x04000C66 RID: 3174
		HasFieldRVA = 256,
		/// <summary>Specifies a special method, with the name describing how the method is special.</summary>
		// Token: 0x04000C67 RID: 3175
		SpecialName = 512,
		/// <summary>Specifies that the common language runtime (metadata internal APIs) should check the name encoding.</summary>
		// Token: 0x04000C68 RID: 3176
		RTSpecialName = 1024,
		/// <summary>Specifies that the field has marshaling information.</summary>
		// Token: 0x04000C69 RID: 3177
		HasFieldMarshal = 4096,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04000C6A RID: 3178
		PinvokeImpl = 8192,
		/// <summary>Specifies that the field has a default value.</summary>
		// Token: 0x04000C6B RID: 3179
		HasDefault = 32768,
		/// <summary>Reserved.</summary>
		// Token: 0x04000C6C RID: 3180
		ReservedMask = 38144
	}
}
