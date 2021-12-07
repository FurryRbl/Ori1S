using System;

namespace System.Security.AccessControl
{
	/// <summary>Inheritance flags specify the semantics of inheritance for access control entries (ACEs).</summary>
	// Token: 0x0200057C RID: 1404
	[Flags]
	public enum InheritanceFlags
	{
		/// <summary>The ACE is not inherited by child objects.</summary>
		// Token: 0x04001721 RID: 5921
		None = 0,
		/// <summary>The ACE is inherited by child container objects.</summary>
		// Token: 0x04001722 RID: 5922
		ContainerInherit = 1,
		/// <summary>The ACE is inherited by child leaf objects.</summary>
		// Token: 0x04001723 RID: 5923
		ObjectInherit = 2
	}
}
