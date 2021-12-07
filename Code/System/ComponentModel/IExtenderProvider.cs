using System;

namespace System.ComponentModel
{
	/// <summary>Defines the interface for extending properties to other components in a container.</summary>
	// Token: 0x02000159 RID: 345
	public interface IExtenderProvider
	{
		/// <summary>Specifies whether this object can provide its extender properties to the specified object.</summary>
		/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
		/// <param name="extendee">The <see cref="T:System.Object" /> to receive the extender properties. </param>
		// Token: 0x06000C8D RID: 3213
		bool CanExtend(object extendee);
	}
}
