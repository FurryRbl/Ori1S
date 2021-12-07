using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies the source interface and the class that implements the methods of the event interface that is generated when a coclass is imported from a COM type library.</summary>
	// Token: 0x0200037B RID: 891
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComEventInterfaceAttribute" /> class with the source interface and event provider class.</summary>
		/// <param name="SourceInterface">A <see cref="T:System.Type" /> that contains the original source interface from the type library. COM uses this interface to call back to the managed class. </param>
		/// <param name="EventProvider">A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface. </param>
		// Token: 0x06002A30 RID: 10800 RVA: 0x00092328 File Offset: 0x00090528
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this.si = SourceInterface;
			this.ep = EventProvider;
		}

		/// <summary>Gets the class that implements the methods of the event interface.</summary>
		/// <returns>A <see cref="T:System.Type" /> that contains the class that implements the methods of the event interface.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x00092340 File Offset: 0x00090540
		public Type EventProvider
		{
			get
			{
				return this.ep;
			}
		}

		/// <summary>Gets the original source interface from the type library.</summary>
		/// <returns>A <see cref="T:System.Type" /> containing the source interface.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x00092348 File Offset: 0x00090548
		public Type SourceInterface
		{
			get
			{
				return this.si;
			}
		}

		// Token: 0x040010DC RID: 4316
		private Type si;

		// Token: 0x040010DD RID: 4317
		private Type ep;
	}
}
