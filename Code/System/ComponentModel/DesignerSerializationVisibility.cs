using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x02000108 RID: 264
	[ComVisible(true)]
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x040002D6 RID: 726
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x040002D7 RID: 727
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x040002D8 RID: 728
		Content
	}
}
