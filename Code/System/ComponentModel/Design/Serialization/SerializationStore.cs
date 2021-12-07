using System;
using System.Collections;
using System.IO;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for storing serialization data for the <see cref="T:System.ComponentModel.Design.Serialization.ComponentSerializationService" />.</summary>
	// Token: 0x02000138 RID: 312
	public abstract class SerializationStore : IDisposable
	{
		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" />.</summary>
		// Token: 0x06000BAD RID: 2989 RVA: 0x0001E730 File Offset: 0x0001C930
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets a collection of errors that occurred during serialization or deserialization.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains errors that occurred during serialization or deserialization.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000BAE RID: 2990
		public abstract ICollection Errors { get; }

		/// <summary>Closes the serialization store.</summary>
		// Token: 0x06000BAF RID: 2991
		public abstract void Close();

		/// <summary>Saves the store to the given stream.</summary>
		/// <param name="stream">The stream to which the store will be serialized.</param>
		// Token: 0x06000BB0 RID: 2992
		public abstract void Save(Stream stream);

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.Serialization.SerializationStore" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001E73C File Offset: 0x0001C93C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}
	}
}
