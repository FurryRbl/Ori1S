using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		/// <summary>Gets or sets a value that indicates whether to wrap exceptions that do not derive from the <see cref="T:System.Exception" /> class with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object.</summary>
		/// <returns>true if exceptions that do not derive from the <see cref="T:System.Exception" /> class should appear wrapped with a <see cref="T:System.Runtime.CompilerServices.RuntimeWrappedException" /> object; otherwise, false.</returns>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00014C3C File Offset: 0x00012E3C
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x00014C44 File Offset: 0x00012E44
		public bool WrapNonExceptionThrows
		{
			get
			{
				return this.wrap_non_exception_throws;
			}
			set
			{
				this.wrap_non_exception_throws = value;
			}
		}

		// Token: 0x040000A7 RID: 167
		private bool wrap_non_exception_throws;
	}
}
