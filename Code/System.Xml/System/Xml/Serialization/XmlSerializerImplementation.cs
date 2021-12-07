using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Defines the reader, writer, and methods for pre-generated, typed serializers.</summary>
	// Token: 0x020002B6 RID: 694
	public abstract class XmlSerializerImplementation
	{
		/// <summary>Gets the XML reader object that is used by the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationReader" /> that is used to read an XML document or data stream.</returns>
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0009B348 File Offset: 0x00099548
		public virtual XmlSerializationReader Reader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of methods that is used to read a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x0009B350 File Offset: 0x00099550
		public virtual Hashtable ReadMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the collection of typed serializers that is found in the assembly.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the typed serializers.</returns>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0009B358 File Offset: 0x00099558
		public virtual Hashtable TypedSerializers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Get the collection of methods that is used to write to a data stream.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the methods.</returns>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0009B360 File Offset: 0x00099560
		public virtual Hashtable WriteMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the XML writer object for the serializer.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlSerializationWriter" /> that is used to write to an XML data stream or document.</returns>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0009B368 File Offset: 0x00099568
		public virtual XmlSerializationWriter Writer
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that determines whether a type can be serialized.</summary>
		/// <returns>true if the type can be serialized; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x0009B370 File Offset: 0x00099570
		public virtual bool CanSerialize(Type type)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a serializer for the specified type.</summary>
		/// <returns>An instance of a type derived from the <see cref="T:System.Xml.Serialization.XmlSerializer" /> class. </returns>
		/// <param name="type">The <see cref="T:System.Type" /> to be serialized.</param>
		// Token: 0x06001D3E RID: 7486 RVA: 0x0009B378 File Offset: 0x00099578
		public virtual XmlSerializer GetSerializer(Type type)
		{
			throw new NotSupportedException();
		}
	}
}
