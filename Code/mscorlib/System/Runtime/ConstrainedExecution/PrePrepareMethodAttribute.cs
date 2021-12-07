﻿using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Instructs the native image generation service to prepare a method for inclusion in a constrained execution region (CER).</summary>
	// Token: 0x0200034A RID: 842
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	public sealed class PrePrepareMethodAttribute : Attribute
	{
	}
}
