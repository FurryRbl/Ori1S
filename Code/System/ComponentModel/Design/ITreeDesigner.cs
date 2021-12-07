﻿using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for building a set of related custom designers.</summary>
	// Token: 0x02000120 RID: 288
	public interface ITreeDesigner : IDisposable, IDesigner
	{
		/// <summary>Gets a collection of child designers.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" />, containing the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> child objects of the current designer. </returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000B20 RID: 2848
		ICollection Children { get; }

		/// <summary>Gets the parent designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or null if there is no parent.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B21 RID: 2849
		IDesigner Parent { get; }
	}
}
