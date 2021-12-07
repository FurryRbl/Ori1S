using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Provides a wrapper class for pointers.</summary>
	// Token: 0x020002B2 RID: 690
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x0007DF98 File Offset: 0x0007C198
		private Pointer()
		{
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name, fusion log, and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06002318 RID: 8984 RVA: 0x0007DFA0 File Offset: 0x0007C1A0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException("Pointer deserializatioon not supported.");
		}

		/// <summary>Boxes the supplied unmanaged memory pointer and the type associated with that pointer into a managed <see cref="T:System.Reflection.Pointer" /> wrapper object. The value and the type are saved so they can be accessed from the native code during an invocation.</summary>
		/// <returns>A pointer object.</returns>
		/// <param name="ptr">The supplied unmanaged memory pointer. </param>
		/// <param name="type">The type associated with the <paramref name="ptr" /> parameter. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a pointer. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null. </exception>
		// Token: 0x06002319 RID: 8985 RVA: 0x0007DFAC File Offset: 0x0007C1AC
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException("type");
			}
			return new Pointer
			{
				data = ptr,
				type = type
			};
		}

		/// <summary>Returns the stored pointer.</summary>
		/// <returns>This method returns void.</returns>
		/// <param name="ptr">The stored pointer. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is not a pointer. </exception>
		// Token: 0x0600231A RID: 8986 RVA: 0x0007DFF8 File Offset: 0x0007C1F8
		public unsafe static void* Unbox(object ptr)
		{
			Pointer pointer = ptr as Pointer;
			if (pointer == null)
			{
				throw new ArgumentException("ptr");
			}
			return pointer.data;
		}

		// Token: 0x04000D1E RID: 3358
		private unsafe void* data;

		// Token: 0x04000D1F RID: 3359
		private Type type;
	}
}
