using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000310 RID: 784
	[Serializable]
	internal class RuntimeResourceSet : ResourceSet
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x00090A98 File Offset: 0x0008EC98
		public RuntimeResourceSet(UnmanagedMemoryStream stream) : base(stream)
		{
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00090AA4 File Offset: 0x0008ECA4
		public RuntimeResourceSet(Stream stream) : base(stream)
		{
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00090AB0 File Offset: 0x0008ECB0
		public RuntimeResourceSet(string fileName) : base(fileName)
		{
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x00090ABC File Offset: 0x0008ECBC
		public override object GetObject(string name)
		{
			if (this.Reader == null)
			{
				throw new ObjectDisposedException("ResourceSet is closed.");
			}
			return this.CloneDisposableObjectIfPossible(base.GetObject(name));
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x00090AE4 File Offset: 0x0008ECE4
		public override object GetObject(string name, bool ignoreCase)
		{
			if (this.Reader == null)
			{
				throw new ObjectDisposedException("ResourceSet is closed.");
			}
			return this.CloneDisposableObjectIfPossible(base.GetObject(name, ignoreCase));
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x00090B18 File Offset: 0x0008ED18
		private object CloneDisposableObjectIfPossible(object value)
		{
			ICloneable cloneable = value as ICloneable;
			return (cloneable == null || !(value is IDisposable)) ? value : cloneable.Clone();
		}
	}
}
