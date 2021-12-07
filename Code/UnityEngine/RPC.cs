using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000079 RID: 121
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	[RequiredByNativeCode]
	[Obsolete("NetworkView RPC functions are deprecated. Refer to the new Multiplayer Networking system.")]
	public sealed class RPC : Attribute
	{
	}
}
