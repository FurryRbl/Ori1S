using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a destination <see cref="T:System.Type" /> in another assembly.  This class cannot be inherited. </summary>
	// Token: 0x0200004F RID: 79
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedToAttribute" /> class specifying a destination <see cref="T:System.Type" />. </summary>
		/// <param name="destination">The destination <see cref="T:System.Type" /> in another assembly.</param>
		// Token: 0x06000676 RID: 1654 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public TypeForwardedToAttribute(Type destination)
		{
			this.destination = destination;
		}

		/// <summary>Gets the destination <see cref="T:System.Type" /> in another assembly.</summary>
		/// <returns>The destination <see cref="T:System.Type" /> in another assembly.</returns>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public Type Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x040000A2 RID: 162
		private Type destination;
	}
}
