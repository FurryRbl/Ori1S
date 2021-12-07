using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a basic designer loader interface that can be used to implement a custom designer loader.</summary>
	// Token: 0x0200012A RID: 298
	[ComVisible(true)]
	public abstract class DesignerLoader
	{
		/// <summary>Gets a value indicating whether the loader is currently loading a document.</summary>
		/// <returns>true if the loader is currently loading a document; otherwise, false.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001E064 File Offset: 0x0001C264
		public virtual bool Loading
		{
			get
			{
				return false;
			}
		}

		/// <summary>Begins loading a designer.</summary>
		/// <param name="host">The loader host through which this loader loads components. </param>
		// Token: 0x06000B5F RID: 2911
		public abstract void BeginLoad(IDesignerLoaderHost host);

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.DesignerLoader" />.</summary>
		// Token: 0x06000B60 RID: 2912
		public abstract void Dispose();

		/// <summary>Writes cached changes to the location that the designer was loaded from.</summary>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0001E068 File Offset: 0x0001C268
		public virtual void Flush()
		{
		}
	}
}
