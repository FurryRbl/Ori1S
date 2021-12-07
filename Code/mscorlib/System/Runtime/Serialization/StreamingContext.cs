using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Describes the source and destination of a given serialized stream, and provides an additional caller-defined context.</summary>
	// Token: 0x0200050D RID: 1293
	[ComVisible(true)]
	[Serializable]
	public struct StreamingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		// Token: 0x06003383 RID: 13187 RVA: 0x000A6A88 File Offset: 0x000A4C88
		public StreamingContext(StreamingContextStates state)
		{
			this.state = state;
			this.additional = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state, and some additional information.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		/// <param name="additional">Any additional information to be associated with the <see cref="T:System.Runtime.Serialization.StreamingContext" />. This information is available to any object that implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or any serialization surrogate. Most users do not need to set this parameter. </param>
		// Token: 0x06003384 RID: 13188 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.state = state;
			this.additional = additional;
		}

		/// <summary>Gets context specified as part of the additional context.</summary>
		/// <returns>The context specified as part of the additional context.</returns>
		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003385 RID: 13189 RVA: 0x000A6AA8 File Offset: 0x000A4CA8
		public object Context
		{
			get
			{
				return this.additional;
			}
		}

		/// <summary>Gets the source or destination of the transmitted data.</summary>
		/// <returns>During serialization, the destination of the transmitted data. During deserialization, the source of the data.</returns>
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000A6AB0 File Offset: 0x000A4CB0
		public StreamingContextStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances contain the same values.</summary>
		/// <returns>true if the specified object is an instance of <see cref="T:System.Runtime.Serialization.StreamingContext" /> and equals the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with the current instance. </param>
		// Token: 0x06003387 RID: 13191 RVA: 0x000A6AB8 File Offset: 0x000A4CB8
		public override bool Equals(object obj)
		{
			if (!(obj is StreamingContext))
			{
				return false;
			}
			StreamingContext streamingContext = (StreamingContext)obj;
			return streamingContext.state == this.state && streamingContext.additional == this.additional;
		}

		/// <summary>Returns a hash code of this object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> value that contains the source or destination of the serialization for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</returns>
		// Token: 0x06003388 RID: 13192 RVA: 0x000A6B00 File Offset: 0x000A4D00
		public override int GetHashCode()
		{
			return (int)this.state;
		}

		// Token: 0x04001567 RID: 5479
		private StreamingContextStates state;

		// Token: 0x04001568 RID: 5480
		private object additional;
	}
}
