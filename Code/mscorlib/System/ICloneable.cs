﻿using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Supports cloning, which creates a new instance of a class with the same value as an existing instance.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001D RID: 29
	[ComVisible(true)]
	public interface ICloneable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600027B RID: 635
		object Clone();
	}
}
