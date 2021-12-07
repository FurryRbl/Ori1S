using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies a list of interfaces that are exposed as COM event sources for the attributed class.</summary>
	// Token: 0x0200037F RID: 895
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[ComVisible(true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the name of the event source interface.</summary>
		/// <param name="sourceInterfaces">A null-delimited list of fully qualified event source interface names. </param>
		// Token: 0x06002A34 RID: 10804 RVA: 0x00092358 File Offset: 0x00090558
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this.internalValue = sourceInterfaces;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the type to use as a source interface.</summary>
		/// <param name="sourceInterface">The <see cref="T:System.Type" /> of the source interface. </param>
		// Token: 0x06002A35 RID: 10805 RVA: 0x00092368 File Offset: 0x00090568
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this.internalValue = sourceInterface.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x06002A36 RID: 10806 RVA: 0x0009237C File Offset: 0x0009057C
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this.internalValue = sourceInterface1.ToString() + sourceInterface2.ToString();
		}

		/// <summary>Initializes a new instance of the ComSourceInterfacesAttribute class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x06002A37 RID: 10807 RVA: 0x000923A8 File Offset: 0x000905A8
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this.internalValue = sourceInterface1.ToString() + sourceInterface2.ToString() + sourceInterface3.ToString();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComSourceInterfacesAttribute" /> class with the types to use as source interfaces.</summary>
		/// <param name="sourceInterface1">The <see cref="T:System.Type" /> of the default source interface. </param>
		/// <param name="sourceInterface2">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface3">The <see cref="T:System.Type" /> of a source interface. </param>
		/// <param name="sourceInterface4">The <see cref="T:System.Type" /> of a source interface. </param>
		// Token: 0x06002A38 RID: 10808 RVA: 0x000923D8 File Offset: 0x000905D8
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this.internalValue = sourceInterface1.ToString() + sourceInterface2.ToString() + sourceInterface3.ToString() + sourceInterface4.ToString();
		}

		/// <summary>Gets the fully qualified name of the event source interface.</summary>
		/// <returns>The fully qualified name of the event source interface.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x00092410 File Offset: 0x00090610
		public string Value
		{
			get
			{
				return this.internalValue;
			}
		}

		// Token: 0x040010E6 RID: 4326
		private string internalValue;
	}
}
