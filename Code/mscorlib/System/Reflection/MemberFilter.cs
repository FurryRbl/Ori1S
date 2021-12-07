using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a delegate that is used to filter a list of members represented in an array of <see cref="T:System.Reflection.MemberInfo" /> objects.</summary>
	/// <returns>true to include the member in the filtered list; otherwise false.</returns>
	/// <param name="m">The <see cref="T:System.Reflection.MemberInfo" /> object to which the filter is applied. </param>
	/// <param name="filterCriteria">An arbitrary object used to filter the list. </param>
	// Token: 0x020006EF RID: 1775
	// (Invoke) Token: 0x060043B0 RID: 17328
	[ComVisible(true)]
	[Serializable]
	public delegate bool MemberFilter(MemberInfo m, object filterCriteria);
}
