using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters.Binary
{
	/// <summary>Serializes and deserializes an object, or an entire graph of connected objects, in binary format.</summary>
	// Token: 0x0200051A RID: 1306
	[ComVisible(true)]
	public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with default values.</summary>
		// Token: 0x060033C9 RID: 13257 RVA: 0x000A6EF0 File Offset: 0x000A50F0
		public BinaryFormatter()
		{
			this.surrogate_selector = BinaryFormatter.DefaultSurrogateSelector;
			this.context = new StreamingContext(StreamingContextStates.All);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> class with a given surrogate selector and streaming context.</summary>
		/// <param name="selector">The <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to use. Can be null. </param>
		/// <param name="context">The source and destination for the serialized data. </param>
		// Token: 0x060033CA RID: 13258 RVA: 0x000A6F24 File Offset: 0x000A5124
		public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
		{
			this.surrogate_selector = selector;
			this.context = context;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000A6F54 File Offset: 0x000A5154
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000A6F5C File Offset: 0x000A515C
		public static ISurrogateSelector DefaultSurrogateSelector { get; set; }

		/// <summary>Gets or sets the behavior of the deserializer with regards to finding and loading assemblies.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values that specifies the deserializer behavior.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000A6F64 File Offset: 0x000A5164
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x000A6F6C File Offset: 0x000A516C
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.assembly_format;
			}
			set
			{
				this.assembly_format = value;
			}
		}

		/// <summary>Gets or sets an object of type <see cref="T:System.Runtime.Serialization.SerializationBinder" /> that controls the binding of a serialized object to a type.</summary>
		/// <returns>The serialization binder to use with this formatter.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000A6F78 File Offset: 0x000A5178
		// (set) Token: 0x060033D0 RID: 13264 RVA: 0x000A6F80 File Offset: 0x000A5180
		public SerializationBinder Binder
		{
			get
			{
				return this.binder;
			}
			set
			{
				this.binder = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.StreamingContext" /> for this formatter.</summary>
		/// <returns>The streaming context to use with this formatter.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000A6F8C File Offset: 0x000A518C
		// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000A6F94 File Offset: 0x000A5194
		public StreamingContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that controls type substitution during serialization and deserialization.</summary>
		/// <returns>The surrogate selector to use with this formatter.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000A6FA0 File Offset: 0x000A51A0
		// (set) Token: 0x060033D4 RID: 13268 RVA: 0x000A6FA8 File Offset: 0x000A51A8
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.surrogate_selector;
			}
			set
			{
				this.surrogate_selector = value;
			}
		}

		/// <summary>Gets or sets the format in which type descriptions are laid out in the serialized stream.</summary>
		/// <returns>The style of type layouts to use.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000A6FB4 File Offset: 0x000A51B4
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000A6FBC File Offset: 0x000A51BC
		public FormatterTypeStyle TypeFormat
		{
			get
			{
				return this.type_format;
			}
			set
			{
				this.type_format = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> of automatic deserialization the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> performs.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> that represents the current automatic deserialization level.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000A6FC8 File Offset: 0x000A51C8
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000A6FD0 File Offset: 0x000A51D0
		public TypeFilterLevel FilterLevel
		{
			get
			{
				return this.filter_level;
			}
			set
			{
				this.filter_level = value;
			}
		}

		/// <summary>Deserializes the specified stream into an object graph.</summary>
		/// <returns>The top (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060033D9 RID: 13273 RVA: 0x000A6FDC File Offset: 0x000A51DC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public object Deserialize(Stream serializationStream)
		{
			return this.NoCheckDeserialize(serializationStream, null);
		}

		/// <summary>Deserializes the specified stream into an object graph. The provided <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> handles any headers in that stream.</summary>
		/// <returns>The deserialized object or the top object (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060033DA RID: 13274 RVA: 0x000A6FE8 File Offset: 0x000A51E8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public object Deserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.NoCheckDeserialize(serializationStream, handler);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000A6FF4 File Offset: 0x000A51F4
		private object NoCheckDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException("serializationStream supports seeking, but its length is 0");
			}
			BinaryReader binaryReader = new BinaryReader(serializationStream);
			bool flag;
			this.ReadBinaryHeader(binaryReader, out flag);
			BinaryElement binaryElement = (BinaryElement)binaryReader.Read();
			if (binaryElement == BinaryElement.MethodCall)
			{
				return MessageFormatter.ReadMethodCall(binaryElement, binaryReader, flag, handler, this);
			}
			if (binaryElement == BinaryElement.MethodResponse)
			{
				return MessageFormatter.ReadMethodResponse(binaryElement, binaryReader, flag, handler, null, this);
			}
			ObjectReader objectReader = new ObjectReader(this);
			object result;
			Header[] headers;
			objectReader.ReadObjectGraph(binaryElement, binaryReader, flag, out result, out headers);
			if (handler != null)
			{
				handler(headers);
			}
			return result;
		}

		/// <summary>Deserializes a response to a remote method call from the provided <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The deserialized response to the remote method call.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <param name="methodCallMessage">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about where the call came from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060033DC RID: 13276 RVA: 0x000A7098 File Offset: 0x000A5298
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.NoCheckDeserializeMethodResponse(serializationStream, handler, methodCallMessage);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000A70A4 File Offset: 0x000A52A4
		private object NoCheckDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			if (serializationStream.CanSeek && serializationStream.Length == 0L)
			{
				throw new SerializationException("serializationStream supports seeking, but its length is 0");
			}
			BinaryReader reader = new BinaryReader(serializationStream);
			bool hasHeaders;
			this.ReadBinaryHeader(reader, out hasHeaders);
			return MessageFormatter.ReadMethodResponse(reader, hasHeaders, handler, methodCallMessage, this);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream.</summary>
		/// <param name="serializationStream">The stream to which the graph is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. -or-The <paramref name="graph" /> is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060033DE RID: 13278 RVA: 0x000A7100 File Offset: 0x000A5300
		public void Serialize(Stream serializationStream, object graph)
		{
			this.Serialize(serializationStream, graph, null);
		}

		/// <summary>Serializes the object, or graph of objects with the specified top (root), to the given stream attaching the provided headers.</summary>
		/// <param name="serializationStream">The stream to which the object is to be serialized. </param>
		/// <param name="graph">The object at the root of the graph to serialize. </param>
		/// <param name="headers">Remoting headers to include in the serialization. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">An error has occurred during serialization, such as if an object in the <paramref name="graph" /> parameter is not marked as serializable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060033DF RID: 13279 RVA: 0x000A710C File Offset: 0x000A530C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public void Serialize(Stream serializationStream, object graph, Header[] headers)
		{
			if (serializationStream == null)
			{
				throw new ArgumentNullException("serializationStream");
			}
			BinaryWriter binaryWriter = new BinaryWriter(serializationStream);
			this.WriteBinaryHeader(binaryWriter, headers != null);
			if (graph is IMethodCallMessage)
			{
				MessageFormatter.WriteMethodCall(binaryWriter, graph, headers, this.surrogate_selector, this.context, this.assembly_format, this.type_format);
			}
			else if (graph is IMethodReturnMessage)
			{
				MessageFormatter.WriteMethodResponse(binaryWriter, graph, headers, this.surrogate_selector, this.context, this.assembly_format, this.type_format);
			}
			else
			{
				ObjectWriter objectWriter = new ObjectWriter(this.surrogate_selector, this.context, this.assembly_format, this.type_format);
				objectWriter.WriteObjectGraph(binaryWriter, graph, headers);
			}
			binaryWriter.Flush();
		}

		/// <summary>Deserializes the specified stream into an object graph. The provided <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> handles any headers in that stream.</summary>
		/// <returns>The deserialized object or the top object (root) of the object graph.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060033E0 RID: 13280 RVA: 0x000A71CC File Offset: 0x000A53CC
		[ComVisible(false)]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
		{
			return this.NoCheckDeserialize(serializationStream, handler);
		}

		/// <summary>Deserializes a response to a remote method call from the provided <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The deserialized response to the remote method call.</returns>
		/// <param name="serializationStream">The stream from which to deserialize the object graph. </param>
		/// <param name="handler">The <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> that handles any headers in the <paramref name="serializationStream" />. Can be null. </param>
		/// <param name="methodCallMessage">The <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> that contains details about where the call came from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serializationStream" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="serializationStream" /> supports seeking, but its length is 0. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060033E1 RID: 13281 RVA: 0x000A71D8 File Offset: 0x000A53D8
		[ComVisible(false)]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"SerializationFormatter\"/>\n</PermissionSet>\n")]
		public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
		{
			return this.NoCheckDeserializeMethodResponse(serializationStream, handler, methodCallMessage);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000A71E4 File Offset: 0x000A53E4
		private void WriteBinaryHeader(BinaryWriter writer, bool hasHeaders)
		{
			writer.Write(0);
			writer.Write(1);
			if (hasHeaders)
			{
				writer.Write(2);
			}
			else
			{
				writer.Write(-1);
			}
			writer.Write(1);
			writer.Write(0);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000A7228 File Offset: 0x000A5428
		private void ReadBinaryHeader(BinaryReader reader, out bool hasHeaders)
		{
			reader.ReadByte();
			reader.ReadInt32();
			int num = reader.ReadInt32();
			hasHeaders = (num == 2);
			reader.ReadInt32();
			reader.ReadInt32();
		}

		// Token: 0x0400158D RID: 5517
		private FormatterAssemblyStyle assembly_format;

		// Token: 0x0400158E RID: 5518
		private SerializationBinder binder;

		// Token: 0x0400158F RID: 5519
		private StreamingContext context;

		// Token: 0x04001590 RID: 5520
		private ISurrogateSelector surrogate_selector;

		// Token: 0x04001591 RID: 5521
		private FormatterTypeStyle type_format = FormatterTypeStyle.TypesAlways;

		// Token: 0x04001592 RID: 5522
		private TypeFilterLevel filter_level = TypeFilterLevel.Full;
	}
}
