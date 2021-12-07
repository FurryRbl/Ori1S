using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace System.Diagnostics
{
	/// <summary>Handles the diagnostics section of configuration files.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021A RID: 538
	[Obsolete("This class is obsoleted")]
	public class DiagnosticsConfigurationHandler : System.Configuration.IConfigurationSectionHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DiagnosticsConfigurationHandler" /> class.</summary>
		// Token: 0x06001213 RID: 4627 RVA: 0x0002FF48 File Offset: 0x0002E148
		public DiagnosticsConfigurationHandler()
		{
			this.elementHandlers["assert"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddAssertNode);
			this.elementHandlers["switches"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSwitchesNode);
			this.elementHandlers["trace"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddTraceNode);
			this.elementHandlers["sources"] = new DiagnosticsConfigurationHandler.ElementHandler(this.AddSourcesNode);
		}

		/// <summary>Parses the configuration settings for the &lt;system.diagnostics&gt; Element section of configuration files.</summary>
		/// <returns>A new configuration object, in the form of a <see cref="T:System.Collections.Hashtable" />.</returns>
		/// <param name="parent">The object inherited from the parent path</param>
		/// <param name="configContext">Reserved. Used in ASP.NET to convey the virtual path of the configuration being evaluated.</param>
		/// <param name="section">The root XML node at the section to handle.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Switches could not be found.-or-Assert could not be found.-or-Trace could not be found.-or-Performance counters could not be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001214 RID: 4628 RVA: 0x0002FFD8 File Offset: 0x0002E1D8
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			IDictionary dictionary;
			if (parent == null)
			{
				dictionary = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				dictionary = (IDictionary)((ICloneable)parent).Clone();
			}
			if (dictionary.Contains(".__TraceInfoSettingsKey__."))
			{
				this.configValues = (TraceImplSettings)dictionary[".__TraceInfoSettingsKey__."];
			}
			else
			{
				dictionary.Add(".__TraceInfoSettingsKey__.", this.configValues = new TraceImplSettings());
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType xmlNodeType = xmlNode.NodeType;
				if (xmlNodeType == XmlNodeType.Element)
				{
					if (!(xmlNode.LocalName != "sharedListeners"))
					{
						this.AddTraceListeners(dictionary, xmlNode, this.GetSharedListeners(dictionary));
					}
				}
			}
			foreach (object obj2 in section.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode2.NodeType;
				XmlNodeType xmlNodeType = nodeType;
				if (xmlNodeType != XmlNodeType.Element)
				{
					if (xmlNodeType != XmlNodeType.Comment && xmlNodeType != XmlNodeType.Whitespace)
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
				else if (!(xmlNode2.LocalName == "sharedListeners"))
				{
					DiagnosticsConfigurationHandler.ElementHandler elementHandler = (DiagnosticsConfigurationHandler.ElementHandler)this.elementHandlers[xmlNode2.Name];
					if (elementHandler != null)
					{
						elementHandler(dictionary, xmlNode2);
					}
					else
					{
						this.ThrowUnrecognizedElement(xmlNode2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x000301E8 File Offset: 0x0002E3E8
		private void AddAssertNode(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "assertuienabled", false, node);
			string attribute2 = this.GetAttribute(attributes, "logfilename", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				try
				{
					d["assertuienabled"] = bool.Parse(attribute);
				}
				catch (Exception inner)
				{
					throw new System.Configuration.ConfigurationException("The `assertuienabled' attribute must be `true' or `false'", inner, node);
				}
			}
			if (attribute2 != null)
			{
				d["logfilename"] = attribute2;
			}
			DefaultTraceListener defaultTraceListener = (DefaultTraceListener)this.configValues.Listeners["Default"];
			if (defaultTraceListener != null)
			{
				if (attribute != null)
				{
					defaultTraceListener.AssertUiEnabled = (bool)d["assertuienabled"];
				}
				if (attribute2 != null)
				{
					defaultTraceListener.LogFileName = attribute2;
				}
			}
			if (node.ChildNodes.Count > 0)
			{
				this.ThrowUnrecognizedElement(node.ChildNodes[0]);
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x000302F8 File Offset: 0x0002E4F8
		private void AddSwitchesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			IDictionary dictionary = new Hashtable();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (name == null)
						{
							goto IL_13B;
						}
						if (DiagnosticsConfigurationHandler.<>f__switch$map5 == null)
						{
							DiagnosticsConfigurationHandler.<>f__switch$map5 = new Dictionary<string, int>(3)
							{
								{
									"add",
									0
								},
								{
									"remove",
									1
								},
								{
									"clear",
									2
								}
							};
						}
						int num;
						if (!DiagnosticsConfigurationHandler.<>f__switch$map5.TryGetValue(name, out num))
						{
							goto IL_13B;
						}
						switch (num)
						{
						case 0:
						{
							string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
							string attribute2 = this.GetAttribute(attributes, "value", true, xmlNode);
							dictionary[attribute] = DiagnosticsConfigurationHandler.GetSwitchValue(attribute, attribute2);
							break;
						}
						case 1:
						{
							string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
							dictionary.Remove(attribute);
							break;
						}
						case 2:
							dictionary.Clear();
							break;
						default:
							goto IL_13B;
						}
						IL_147:
						this.ValidateInvalidAttributes(attributes, xmlNode);
						continue;
						IL_13B:
						this.ThrowUnrecognizedElement(xmlNode);
						goto IL_147;
					}
					this.ThrowUnrecognizedNode(xmlNode);
				}
			}
			d[node.Name] = dictionary;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x000304B0 File Offset: 0x0002E6B0
		private static object GetSwitchValue(string name, string value)
		{
			return value;
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000304B4 File Offset: 0x0002E6B4
		private void AddTraceNode(IDictionary d, XmlNode node)
		{
			this.AddTraceAttributes(d, node);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, this.configValues.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00030594 File Offset: 0x0002E794
		private void AddTraceAttributes(IDictionary d, XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			string attribute = this.GetAttribute(attributes, "autoflush", false, node);
			string attribute2 = this.GetAttribute(attributes, "indentsize", false, node);
			this.ValidateInvalidAttributes(attributes, node);
			if (attribute != null)
			{
				bool flag = false;
				try
				{
					flag = bool.Parse(attribute);
					d["autoflush"] = flag;
				}
				catch (Exception inner)
				{
					throw new System.Configuration.ConfigurationException("The `autoflush' attribute must be `true' or `false'", inner, node);
				}
				this.configValues.AutoFlush = flag;
			}
			if (attribute2 != null)
			{
				int num = 0;
				try
				{
					num = int.Parse(attribute2);
					d["indentsize"] = num;
				}
				catch (Exception inner2)
				{
					throw new System.Configuration.ConfigurationException("The `indentsize' attribute must be an integral value.", inner2, node);
				}
				this.configValues.IndentSize = num;
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00030694 File Offset: 0x0002E894
		private TraceListenerCollection GetSharedListeners(IDictionary d)
		{
			TraceListenerCollection traceListenerCollection = d["sharedListeners"] as TraceListenerCollection;
			if (traceListenerCollection == null)
			{
				traceListenerCollection = new TraceListenerCollection();
				d["sharedListeners"] = traceListenerCollection;
			}
			return traceListenerCollection;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000306CC File Offset: 0x0002E8CC
		private void AddSourcesNode(IDictionary d, XmlNode node)
		{
			this.ValidateInvalidAttributes(node.Attributes, node);
			Hashtable hashtable = d["sources"] as Hashtable;
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				d["sources"] = hashtable;
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "source")
						{
							this.AddTraceSource(d, hashtable, xmlNode);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000307C8 File Offset: 0x0002E9C8
		private void AddTraceSource(IDictionary d, Hashtable sources, XmlNode node)
		{
			string text = null;
			SourceLevels levels = SourceLevels.Error;
			System.Collections.Specialized.StringDictionary stringDictionary = new System.Collections.Specialized.StringDictionary();
			foreach (object obj in node.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				if (name != null)
				{
					if (DiagnosticsConfigurationHandler.<>f__switch$map6 == null)
					{
						DiagnosticsConfigurationHandler.<>f__switch$map6 = new Dictionary<string, int>(2)
						{
							{
								"name",
								0
							},
							{
								"switchValue",
								1
							}
						};
					}
					int num;
					if (DiagnosticsConfigurationHandler.<>f__switch$map6.TryGetValue(name, out num))
					{
						if (num == 0)
						{
							text = xmlAttribute.Value;
							continue;
						}
						if (num == 1)
						{
							levels = (SourceLevels)((int)Enum.Parse(typeof(SourceLevels), xmlAttribute.Value));
							continue;
						}
					}
				}
				stringDictionary[xmlAttribute.Name] = xmlAttribute.Value;
			}
			if (text == null)
			{
				throw new System.Configuration.ConfigurationException("Mandatory attribute 'name' is missing in 'source' element.");
			}
			if (sources.ContainsKey(text))
			{
				return;
			}
			TraceSourceInfo traceSourceInfo = new TraceSourceInfo(text, levels, this.configValues);
			sources.Add(traceSourceInfo.Name, traceSourceInfo);
			foreach (object obj2 in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						if (xmlNode.Name == "listeners")
						{
							this.AddTraceListeners(d, xmlNode, traceSourceInfo.Listeners);
						}
						else
						{
							this.ThrowUnrecognizedElement(xmlNode);
						}
						this.ValidateInvalidAttributes(xmlNode.Attributes, xmlNode);
					}
					else
					{
						this.ThrowUnrecognizedNode(xmlNode);
					}
				}
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00030A00 File Offset: 0x0002EC00
		private void AddTraceListeners(IDictionary d, XmlNode listenersNode, TraceListenerCollection listeners)
		{
			this.ValidateInvalidAttributes(listenersNode.Attributes, listenersNode);
			foreach (object obj in listenersNode.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType == XmlNodeType.Element)
					{
						XmlAttributeCollection attributes = xmlNode.Attributes;
						string name = xmlNode.Name;
						if (name == null)
						{
							goto IL_111;
						}
						if (DiagnosticsConfigurationHandler.<>f__switch$map7 == null)
						{
							DiagnosticsConfigurationHandler.<>f__switch$map7 = new Dictionary<string, int>(3)
							{
								{
									"add",
									0
								},
								{
									"remove",
									1
								},
								{
									"clear",
									2
								}
							};
						}
						int num;
						if (!DiagnosticsConfigurationHandler.<>f__switch$map7.TryGetValue(name, out num))
						{
							goto IL_111;
						}
						switch (num)
						{
						case 0:
							this.AddTraceListener(d, xmlNode, attributes, listeners);
							break;
						case 1:
						{
							string attribute = this.GetAttribute(attributes, "name", true, xmlNode);
							this.RemoveTraceListener(attribute);
							break;
						}
						case 2:
							this.configValues.Listeners.Clear();
							break;
						default:
							goto IL_111;
						}
						IL_11D:
						this.ValidateInvalidAttributes(attributes, xmlNode);
						continue;
						IL_111:
						this.ThrowUnrecognizedElement(xmlNode);
						goto IL_11D;
					}
					this.ThrowUnrecognizedNode(xmlNode);
				}
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00030B80 File Offset: 0x0002ED80
		private void AddTraceListener(IDictionary d, XmlNode child, XmlAttributeCollection attributes, TraceListenerCollection listeners)
		{
			string attribute = this.GetAttribute(attributes, "name", true, child);
			string attribute2 = this.GetAttribute(attributes, "type", false, child);
			if (attribute2 == null)
			{
				TraceListener traceListener = this.GetSharedListeners(d)[attribute];
				if (traceListener == null)
				{
					throw new System.Configuration.ConfigurationException(string.Format("Shared trace listener {0} does not exist.", attribute));
				}
				if (attributes.Count != 0)
				{
					throw new ConfigurationErrorsException(string.Format("Listener '{0}' references a shared listener and can only have a 'Name' attribute.", attribute));
				}
				listeners.Add(traceListener, this.configValues);
				return;
			}
			else
			{
				Type type = Type.GetType(attribute2);
				if (type == null)
				{
					throw new System.Configuration.ConfigurationException(string.Format("Invalid Type Specified: {0}", attribute2));
				}
				string attribute3 = this.GetAttribute(attributes, "initializeData", false, child);
				object[] parameters;
				Type[] types;
				if (attribute3 != null)
				{
					parameters = new object[]
					{
						attribute3
					};
					types = new Type[]
					{
						typeof(string)
					};
				}
				else
				{
					parameters = null;
					types = Type.EmptyTypes;
				}
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
				if (type.Assembly == base.GetType().Assembly)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, types, null);
				if (constructor == null)
				{
					throw new System.Configuration.ConfigurationException("Couldn't find constructor for class " + attribute2);
				}
				TraceListener traceListener2 = (TraceListener)constructor.Invoke(parameters);
				traceListener2.Name = attribute;
				string attribute4 = this.GetAttribute(attributes, "traceOutputOptions", false, child);
				if (attribute4 != null)
				{
					if (attribute4 != attribute4.Trim())
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					TraceOptions traceOutputOptions;
					try
					{
						traceOutputOptions = (TraceOptions)((int)Enum.Parse(typeof(TraceOptions), attribute4));
					}
					catch (ArgumentException)
					{
						throw new ConfigurationErrorsException(string.Format("Invalid value '{0}' for 'traceOutputOptions'.", attribute4), child);
					}
					traceListener2.TraceOutputOptions = traceOutputOptions;
				}
				string[] supportedAttributes = traceListener2.GetSupportedAttributes();
				if (supportedAttributes != null)
				{
					foreach (string text in supportedAttributes)
					{
						string attribute5 = this.GetAttribute(attributes, text, false, child);
						if (attribute5 != null)
						{
							traceListener2.Attributes.Add(text, attribute5);
						}
					}
				}
				listeners.Add(traceListener2, this.configValues);
				return;
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		private void RemoveTraceListener(string name)
		{
			try
			{
				this.configValues.Listeners.Remove(name);
			}
			catch (ArgumentException)
			{
			}
			catch (Exception inner)
			{
				throw new System.Configuration.ConfigurationException(string.Format("Unknown error removing listener: {0}", name), inner);
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00030E3C File Offset: 0x0002F03C
		private string GetAttribute(XmlAttributeCollection attrs, string attr, bool required, XmlNode node)
		{
			XmlAttribute xmlAttribute = attrs[attr];
			string text = null;
			if (xmlAttribute != null)
			{
				text = xmlAttribute.Value;
				if (required)
				{
					this.ValidateAttribute(attr, text, node);
				}
				attrs.Remove(xmlAttribute);
			}
			else if (required)
			{
				this.ThrowMissingAttribute(attr, node);
			}
			return text;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00030E90 File Offset: 0x0002F090
		private void ValidateAttribute(string attribute, string value, XmlNode node)
		{
			if (value == null || value.Length == 0)
			{
				throw new System.Configuration.ConfigurationException(string.Format("Required attribute '{0}' cannot be empty.", attribute), node);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00030EB8 File Offset: 0x0002F0B8
		private void ValidateInvalidAttributes(XmlAttributeCollection c, XmlNode node)
		{
			if (c.Count != 0)
			{
				this.ThrowUnrecognizedAttribute(c[0].Name, node);
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00030EE4 File Offset: 0x0002F0E4
		private void ThrowMissingAttribute(string attribute, XmlNode node)
		{
			throw new System.Configuration.ConfigurationException(string.Format("Required attribute '{0}' not found.", attribute), node);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		private void ThrowUnrecognizedNode(XmlNode node)
		{
			throw new System.Configuration.ConfigurationException(string.Format("Unrecognized node `{0}'; nodeType={1}", node.Name, node.NodeType), node);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00030F1C File Offset: 0x0002F11C
		private void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new System.Configuration.ConfigurationException(string.Format("Unrecognized element '{0}'.", node.Name), node);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00030F34 File Offset: 0x0002F134
		private void ThrowUnrecognizedAttribute(string attribute, XmlNode node)
		{
			throw new System.Configuration.ConfigurationException(string.Format("Unrecognized attribute '{0}' on element <{1}/>.", attribute, node.Name), node);
		}

		// Token: 0x04000529 RID: 1321
		private TraceImplSettings configValues;

		// Token: 0x0400052A RID: 1322
		private IDictionary elementHandlers = new Hashtable();

		// Token: 0x020004DA RID: 1242
		// (Invoke) Token: 0x06002C14 RID: 11284
		private delegate void ElementHandler(IDictionary d, XmlNode node);
	}
}
