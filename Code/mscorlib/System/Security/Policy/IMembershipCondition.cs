﻿using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Defines the test to determine whether a code assembly is a member of a code group.</summary>
	// Token: 0x02000646 RID: 1606
	[ComVisible(true)]
	public interface IMembershipCondition : ISecurityEncodable, ISecurityPolicyEncodable
	{
		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		// Token: 0x06003D17 RID: 15639
		bool Check(Evidence evidence);

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		// Token: 0x06003D18 RID: 15640
		IMembershipCondition Copy();

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
		// Token: 0x06003D19 RID: 15641
		bool Equals(object obj);

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the current membership condition.</returns>
		// Token: 0x06003D1A RID: 15642
		string ToString();
	}
}
