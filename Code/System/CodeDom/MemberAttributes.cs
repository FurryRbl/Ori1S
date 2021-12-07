using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x02000091 RID: 145
	[ComVisible(true)]
	[Serializable]
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04000191 RID: 401
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04000192 RID: 402
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the Shared keyword.</summary>
		// Token: 0x04000193 RID: 403
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04000194 RID: 404
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x04000195 RID: 405
		Const,
		/// <summary>A scope mask.</summary>
		// Token: 0x04000196 RID: 406
		ScopeMask = 15,
		/// <summary>A new member.</summary>
		// Token: 0x04000197 RID: 407
		New,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04000198 RID: 408
		VTableMask = 240,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x04000199 RID: 409
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x0400019A RID: 410
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x0400019B RID: 411
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x0400019C RID: 412
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x0400019D RID: 413
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x0400019E RID: 414
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x0400019F RID: 415
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x040001A0 RID: 416
		AccessMask = 61440
	}
}
