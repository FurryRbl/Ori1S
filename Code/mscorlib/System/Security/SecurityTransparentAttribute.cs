using System;

namespace System.Security
{
	/// <summary>Specifies that an assembly cannot cause an elevation of privilege. </summary>
	// Token: 0x0200054D RID: 1357
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[MonoTODO("Only supported by the runtime when CoreCLR is enabled")]
	public sealed class SecurityTransparentAttribute : Attribute
	{
	}
}
