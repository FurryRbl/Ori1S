using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000079 RID: 121
	internal class UnixBinaryCore
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0000DC14 File Offset: 0x0000BE14
		public UnixBinaryCore(object owner, IDictionary properties, string[] allowedProperties)
		{
			this._properties = properties;
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (Array.IndexOf<string>(allowedProperties, text) == -1)
				{
					throw new RemotingException(owner.GetType().Name + " does not recognize '" + text + "' configuration property");
				}
				string text2 = text;
				if (text2 != null)
				{
					if (UnixBinaryCore.<>f__switch$map0 == null)
					{
						UnixBinaryCore.<>f__switch$map0 = new Dictionary<string, int>(2)
						{
							{
								"includeVersions",
								0
							},
							{
								"strictBinding",
								1
							}
						};
					}
					int num;
					if (UnixBinaryCore.<>f__switch$map0.TryGetValue(text2, out num))
					{
						if (num != 0)
						{
							if (num == 1)
							{
								this._strictBinding = Convert.ToBoolean(dictionaryEntry.Value);
							}
						}
						else
						{
							this._includeVersions = Convert.ToBoolean(dictionaryEntry.Value);
						}
					}
				}
			}
			this.Init();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public UnixBinaryCore()
		{
			this._properties = new Hashtable();
			this.Init();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000DD8C File Offset: 0x0000BF8C
		public void Init()
		{
			RemotingSurrogateSelector selector = new RemotingSurrogateSelector();
			StreamingContext context = new StreamingContext(StreamingContextStates.Remoting, null);
			this._serializationFormatter = new BinaryFormatter(selector, context);
			this._deserializationFormatter = new BinaryFormatter(null, context);
			if (!this._includeVersions)
			{
				this._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				this._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
			}
			if (!this._strictBinding)
			{
				this._serializationFormatter.Binder = SimpleBinder.Instance;
				this._deserializationFormatter.Binder = SimpleBinder.Instance;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000DE14 File Offset: 0x0000C014
		public BinaryFormatter Serializer
		{
			get
			{
				return this._serializationFormatter;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public BinaryFormatter Deserializer
		{
			get
			{
				return this._deserializationFormatter;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000DE24 File Offset: 0x0000C024
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x04000420 RID: 1056
		private BinaryFormatter _serializationFormatter;

		// Token: 0x04000421 RID: 1057
		private BinaryFormatter _deserializationFormatter;

		// Token: 0x04000422 RID: 1058
		private bool _includeVersions = true;

		// Token: 0x04000423 RID: 1059
		private bool _strictBinding;

		// Token: 0x04000424 RID: 1060
		private IDictionary _properties;

		// Token: 0x04000425 RID: 1061
		public static UnixBinaryCore DefaultInstance = new UnixBinaryCore();
	}
}
