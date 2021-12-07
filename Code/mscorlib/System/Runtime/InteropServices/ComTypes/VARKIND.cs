﻿using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the kind of variable.</summary>
	// Token: 0x02000414 RID: 1044
	[Serializable]
	public enum VARKIND
	{
		/// <summary>The variable is a field or member of the type. It exists at a fixed offset within each instance of the type.</summary>
		// Token: 0x04001350 RID: 4944
		VAR_PERINSTANCE,
		/// <summary>There is only one instance of the variable.</summary>
		// Token: 0x04001351 RID: 4945
		VAR_STATIC,
		/// <summary>The VARDESC structure describes a symbolic constant. There is no memory associated with it.</summary>
		// Token: 0x04001352 RID: 4946
		VAR_CONST,
		/// <summary>The variable can be accessed only through IDispatch::Invoke.</summary>
		// Token: 0x04001353 RID: 4947
		VAR_DISPATCH
	}
}
