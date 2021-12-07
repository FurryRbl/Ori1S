using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the presence of object types for Access Control Entries (ACEs).</summary>
	// Token: 0x02000585 RID: 1413
	[Flags]
	public enum ObjectAceFlags
	{
		/// <summary>No object types are present.</summary>
		// Token: 0x04001736 RID: 5942
		None = 0,
		/// <summary>The type of object that is associated with the ACE is present.</summary>
		// Token: 0x04001737 RID: 5943
		ObjectAceTypePresent = 1,
		/// <summary>The type of object that can inherit the ACE.</summary>
		// Token: 0x04001738 RID: 5944
		InheritedObjectAceTypePresent = 2
	}
}
