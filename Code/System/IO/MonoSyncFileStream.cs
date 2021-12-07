using System;
using System.Runtime.Remoting.Messaging;

namespace System.IO
{
	// Token: 0x02000293 RID: 659
	internal class MonoSyncFileStream : FileStream
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		public MonoSyncFileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize) : base(handle, access, ownsHandle, bufferSize, false)
		{
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0003F3E0 File Offset: 0x0003D5E0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			MonoSyncFileStream.WriteDelegate writeDelegate = new MonoSyncFileStream.WriteDelegate(this.Write);
			return writeDelegate.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0003F460 File Offset: 0x0003D660
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			MonoSyncFileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as MonoSyncFileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			MonoSyncFileStream.ReadDelegate readDelegate = new MonoSyncFileStream.ReadDelegate(this.Read);
			return readDelegate.BeginInvoke(buffer, offset, count, cback, state);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0003F544 File Offset: 0x0003D744
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			MonoSyncFileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as MonoSyncFileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x020004DF RID: 1247
		// (Invoke) Token: 0x06002C28 RID: 11304
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);

		// Token: 0x020004E0 RID: 1248
		// (Invoke) Token: 0x06002C2C RID: 11308
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);
	}
}
