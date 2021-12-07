using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerCreated" /> and <see cref="E:System.ComponentModel.Design.IDesignerEventService.DesignerDisposed" /> events.</summary>
	// Token: 0x020000FE RID: 254
	public class DesignerEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerEventArgs" /> class.</summary>
		/// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document. </param>
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		public DesignerEventArgs(IDesignerHost host)
		{
			this.host = host;
		}

		/// <summary>Gets the host of the document.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> of the document.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001D2C0 File Offset: 0x0001B4C0
		public IDesignerHost Designer
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x040002C0 RID: 704
		private IDesignerHost host;
	}
}
