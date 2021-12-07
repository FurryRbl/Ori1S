using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Lifetime;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	// Token: 0x02000424 RID: 1060
	internal class ConfigHandler : SmallXmlParser.IContentHandler
	{
		// Token: 0x06002D1E RID: 11550 RVA: 0x00095318 File Offset: 0x00093518
		public ConfigHandler(bool onlyDelayedChannels)
		{
			this.onlyDelayedChannels = onlyDelayedChannels;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x00095354 File Offset: 0x00093554
		private void ValidatePath(string element, params string[] paths)
		{
			foreach (string path in paths)
			{
				if (this.CheckPath(path))
				{
					return;
				}
			}
			throw new RemotingException("Element " + element + " not allowed in this context");
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000953A0 File Offset: 0x000935A0
		private bool CheckPath(string path)
		{
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			if (compareInfo.IsPrefix(path, "/", CompareOptions.Ordinal))
			{
				return path == this.currentXmlPath;
			}
			return compareInfo.IsSuffix(this.currentXmlPath, path, CompareOptions.Ordinal);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000953F0 File Offset: 0x000935F0
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000953F4 File Offset: 0x000935F4
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000953F8 File Offset: 0x000935F8
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000953FC File Offset: 0x000935FC
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
		{
			try
			{
				if (this.currentXmlPath.StartsWith("/configuration/system.runtime.remoting"))
				{
					this.ParseElement(name, attrs);
				}
				this.currentXmlPath = this.currentXmlPath + "/" + name;
			}
			catch (Exception ex)
			{
				throw new RemotingException("Error in element " + name + ": " + ex.Message, ex);
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x00095484 File Offset: 0x00093684
		public void ParseElement(string name, SmallXmlParser.IAttrList attrs)
		{
			if (this.currentProviderData != null)
			{
				this.ReadCustomProviderData(name, attrs);
				return;
			}
			if (name != null)
			{
				if (ConfigHandler.<>f__switch$map22 == null)
				{
					ConfigHandler.<>f__switch$map22 = new Dictionary<string, int>(19)
					{
						{
							"application",
							0
						},
						{
							"lifetime",
							1
						},
						{
							"channels",
							2
						},
						{
							"channel",
							3
						},
						{
							"serverProviders",
							4
						},
						{
							"clientProviders",
							5
						},
						{
							"provider",
							6
						},
						{
							"formatter",
							6
						},
						{
							"client",
							7
						},
						{
							"service",
							8
						},
						{
							"wellknown",
							9
						},
						{
							"activated",
							10
						},
						{
							"soapInterop",
							11
						},
						{
							"interopXmlType",
							12
						},
						{
							"interopXmlElement",
							13
						},
						{
							"preLoad",
							14
						},
						{
							"debug",
							15
						},
						{
							"channelSinkProviders",
							16
						},
						{
							"customErrors",
							17
						}
					};
				}
				int num;
				if (ConfigHandler.<>f__switch$map22.TryGetValue(name, out num))
				{
					switch (num)
					{
					case 0:
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting"
						});
						if (attrs.Names.Length > 0)
						{
							this.appName = attrs.Values[0];
						}
						break;
					case 1:
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						this.ReadLifetine(attrs);
						break;
					case 2:
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting",
							"application"
						});
						break;
					case 3:
						this.ValidatePath(name, new string[]
						{
							"channels"
						});
						if (this.currentXmlPath.IndexOf("application") != -1)
						{
							this.ReadChannel(attrs, false);
						}
						else
						{
							this.ReadChannel(attrs, true);
						}
						break;
					case 4:
						this.ValidatePath(name, new string[]
						{
							"channelSinkProviders",
							"channel"
						});
						break;
					case 5:
						this.ValidatePath(name, new string[]
						{
							"channelSinkProviders",
							"channel"
						});
						break;
					case 6:
						if (this.CheckPath("application/channels/channel/serverProviders") || this.CheckPath("channels/channel/serverProviders"))
						{
							ProviderData providerData = this.ReadProvider(name, attrs, false);
							this.currentChannel.ServerProviders.Add(providerData);
						}
						else if (this.CheckPath("application/channels/channel/clientProviders") || this.CheckPath("channels/channel/clientProviders"))
						{
							ProviderData providerData = this.ReadProvider(name, attrs, false);
							this.currentChannel.ClientProviders.Add(providerData);
						}
						else if (this.CheckPath("channelSinkProviders/serverProviders"))
						{
							ProviderData providerData = this.ReadProvider(name, attrs, true);
							RemotingConfiguration.RegisterServerProviderTemplate(providerData);
						}
						else if (this.CheckPath("channelSinkProviders/clientProviders"))
						{
							ProviderData providerData = this.ReadProvider(name, attrs, true);
							RemotingConfiguration.RegisterClientProviderTemplate(providerData);
						}
						else
						{
							this.ValidatePath(name, new string[0]);
						}
						break;
					case 7:
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						this.currentClientUrl = attrs.GetValue("url");
						break;
					case 8:
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						break;
					case 9:
						this.ValidatePath(name, new string[]
						{
							"client",
							"service"
						});
						if (this.CheckPath("client"))
						{
							this.ReadClientWellKnown(attrs);
						}
						else
						{
							this.ReadServiceWellKnown(attrs);
						}
						break;
					case 10:
						this.ValidatePath(name, new string[]
						{
							"client",
							"service"
						});
						if (this.CheckPath("client"))
						{
							this.ReadClientActivated(attrs);
						}
						else
						{
							this.ReadServiceActivated(attrs);
						}
						break;
					case 11:
						this.ValidatePath(name, new string[]
						{
							"application"
						});
						break;
					case 12:
						this.ValidatePath(name, new string[]
						{
							"soapInterop"
						});
						this.ReadInteropXml(attrs, false);
						break;
					case 13:
						this.ValidatePath(name, new string[]
						{
							"soapInterop"
						});
						this.ReadInteropXml(attrs, false);
						break;
					case 14:
						this.ValidatePath(name, new string[]
						{
							"soapInterop"
						});
						this.ReadPreload(attrs);
						break;
					case 15:
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting"
						});
						break;
					case 16:
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting"
						});
						break;
					case 17:
						this.ValidatePath(name, new string[]
						{
							"system.runtime.remoting"
						});
						RemotingConfiguration.SetCustomErrorsMode(attrs.GetValue("mode"));
						break;
					default:
						goto IL_512;
					}
					return;
				}
			}
			IL_512:
			throw new RemotingException("Element '" + name + "' is not valid in system.remoting.configuration section");
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000959BC File Offset: 0x00093BBC
		public void OnEndElement(string name)
		{
			if (this.currentProviderData != null)
			{
				this.currentProviderData.Pop();
				if (this.currentProviderData.Count == 0)
				{
					this.currentProviderData = null;
				}
			}
			this.currentXmlPath = this.currentXmlPath.Substring(0, this.currentXmlPath.Length - name.Length - 1);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x00095A20 File Offset: 0x00093C20
		private void ReadCustomProviderData(string name, SmallXmlParser.IAttrList attrs)
		{
			SinkProviderData sinkProviderData = (SinkProviderData)this.currentProviderData.Peek();
			SinkProviderData sinkProviderData2 = new SinkProviderData(name);
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				sinkProviderData2.Properties[attrs.Names[i]] = attrs.GetValue(i);
			}
			sinkProviderData.Children.Add(sinkProviderData2);
			this.currentProviderData.Push(sinkProviderData2);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x00095A94 File Offset: 0x00093C94
		private void ReadLifetine(SmallXmlParser.IAttrList attrs)
		{
			int i = 0;
			while (i < attrs.Names.Length)
			{
				string text = attrs.Names[i];
				if (text != null)
				{
					if (ConfigHandler.<>f__switch$map23 == null)
					{
						ConfigHandler.<>f__switch$map23 = new Dictionary<string, int>(4)
						{
							{
								"leaseTime",
								0
							},
							{
								"sponsorshipTimeout",
								1
							},
							{
								"renewOnCallTime",
								2
							},
							{
								"leaseManagerPollTime",
								3
							}
						};
					}
					int num;
					if (ConfigHandler.<>f__switch$map23.TryGetValue(text, out num))
					{
						switch (num)
						{
						case 0:
							LifetimeServices.LeaseTime = this.ParseTime(attrs.GetValue(i));
							break;
						case 1:
							LifetimeServices.SponsorshipTimeout = this.ParseTime(attrs.GetValue(i));
							break;
						case 2:
							LifetimeServices.RenewOnCallTime = this.ParseTime(attrs.GetValue(i));
							break;
						case 3:
							LifetimeServices.LeaseManagerPollTime = this.ParseTime(attrs.GetValue(i));
							break;
						default:
							goto IL_E6;
						}
						i++;
						continue;
					}
				}
				IL_E6:
				throw new RemotingException("Invalid attribute: " + attrs.Names[i]);
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x00095BB4 File Offset: 0x00093DB4
		private TimeSpan ParseTime(string s)
		{
			if (s == string.Empty || s == null)
			{
				throw new RemotingException("Invalid time value");
			}
			int num = s.IndexOfAny(new char[]
			{
				'D',
				'H',
				'M',
				'S'
			});
			string text;
			if (num == -1)
			{
				text = "S";
			}
			else
			{
				text = s.Substring(num);
				s = s.Substring(0, num);
			}
			double value;
			try
			{
				value = double.Parse(s);
			}
			catch
			{
				throw new RemotingException("Invalid time value: " + s);
			}
			if (text == "D")
			{
				return TimeSpan.FromDays(value);
			}
			if (text == "H")
			{
				return TimeSpan.FromHours(value);
			}
			if (text == "M")
			{
				return TimeSpan.FromMinutes(value);
			}
			if (text == "S")
			{
				return TimeSpan.FromSeconds(value);
			}
			if (text == "MS")
			{
				return TimeSpan.FromMilliseconds(value);
			}
			throw new RemotingException("Invalid time unit: " + text);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x00095CE0 File Offset: 0x00093EE0
		private void ReadChannel(SmallXmlParser.IAttrList attrs, bool isTemplate)
		{
			ChannelData channelData = new ChannelData();
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				string text = attrs.Names[i];
				string text2 = attrs.Values[i];
				if (text == "ref" && !isTemplate)
				{
					channelData.Ref = text2;
				}
				else if (text == "delayLoadAsClientChannel")
				{
					channelData.DelayLoadAsClientChannel = text2;
				}
				else if (text == "id" && isTemplate)
				{
					channelData.Id = text2;
				}
				else if (text == "type")
				{
					channelData.Type = text2;
				}
				else
				{
					channelData.CustomProperties.Add(text, text2);
				}
			}
			if (isTemplate)
			{
				if (channelData.Id == null)
				{
					throw new RemotingException("id attribute is required");
				}
				if (channelData.Type == null)
				{
					throw new RemotingException("id attribute is required");
				}
				RemotingConfiguration.RegisterChannelTemplate(channelData);
			}
			else
			{
				this.channelInstances.Add(channelData);
			}
			this.currentChannel = channelData;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x00095DF8 File Offset: 0x00093FF8
		private ProviderData ReadProvider(string name, SmallXmlParser.IAttrList attrs, bool isTemplate)
		{
			ProviderData providerData = (!(name == "provider")) ? new FormatterData() : new ProviderData();
			SinkProviderData sinkProviderData = new SinkProviderData("root");
			providerData.CustomData = sinkProviderData.Children;
			this.currentProviderData = new Stack();
			this.currentProviderData.Push(sinkProviderData);
			for (int i = 0; i < attrs.Names.Length; i++)
			{
				string text = attrs.Names[i];
				string text2 = attrs.Values[i];
				if (text == "id" && isTemplate)
				{
					providerData.Id = text2;
				}
				else if (text == "type")
				{
					providerData.Type = text2;
				}
				else if (text == "ref" && !isTemplate)
				{
					providerData.Ref = text2;
				}
				else
				{
					providerData.CustomProperties.Add(text, text2);
				}
			}
			if (providerData.Id == null && isTemplate)
			{
				throw new RemotingException("id attribute is required");
			}
			return providerData;
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x00095F10 File Offset: 0x00094110
		private void ReadClientActivated(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull);
			if (this.currentClientUrl == null || this.currentClientUrl == string.Empty)
			{
				throw new RemotingException("url attribute is required in client element when it contains activated entries");
			}
			this.typeEntries.Add(new ActivatedClientTypeEntry(notNull, assemblyName, this.currentClientUrl));
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x00095F78 File Offset: 0x00094178
		private void ReadServiceActivated(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull);
			this.typeEntries.Add(new ActivatedServiceTypeEntry(notNull, assemblyName));
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x00095FB0 File Offset: 0x000941B0
		private void ReadClientWellKnown(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "url");
			string notNull2 = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull2);
			this.typeEntries.Add(new WellKnownClientTypeEntry(notNull2, assemblyName, notNull));
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x00095FF4 File Offset: 0x000941F4
		private void ReadServiceWellKnown(SmallXmlParser.IAttrList attrs)
		{
			string notNull = this.GetNotNull(attrs, "objectUri");
			string notNull2 = this.GetNotNull(attrs, "mode");
			string notNull3 = this.GetNotNull(attrs, "type");
			string assemblyName = this.ExtractAssembly(ref notNull3);
			WellKnownObjectMode mode;
			if (notNull2 == "SingleCall")
			{
				mode = WellKnownObjectMode.SingleCall;
			}
			else
			{
				if (!(notNull2 == "Singleton"))
				{
					throw new RemotingException("wellknown object mode '" + notNull2 + "' is invalid");
				}
				mode = WellKnownObjectMode.Singleton;
			}
			this.typeEntries.Add(new WellKnownServiceTypeEntry(notNull3, assemblyName, notNull, mode));
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x00096090 File Offset: 0x00094290
		private void ReadInteropXml(SmallXmlParser.IAttrList attrs, bool isElement)
		{
			Type type = Type.GetType(this.GetNotNull(attrs, "clr"));
			string[] array = this.GetNotNull(attrs, "xml").Split(new char[]
			{
				','
			});
			string text = array[0].Trim();
			string text2 = (array.Length <= 0) ? null : array[1].Trim();
			if (isElement)
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			else
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x00096108 File Offset: 0x00094308
		private void ReadPreload(SmallXmlParser.IAttrList attrs)
		{
			string value = attrs.GetValue("type");
			string value2 = attrs.GetValue("assembly");
			if (value != null && value2 != null)
			{
				throw new RemotingException("Type and assembly attributes cannot be specified together");
			}
			if (value != null)
			{
				SoapServices.PreLoad(Type.GetType(value));
			}
			else
			{
				if (value2 == null)
				{
					throw new RemotingException("Either type or assembly attributes must be specified");
				}
				SoapServices.PreLoad(Assembly.Load(value2));
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0009617C File Offset: 0x0009437C
		private string GetNotNull(SmallXmlParser.IAttrList attrs, string name)
		{
			string value = attrs.GetValue(name);
			if (value == null || value == string.Empty)
			{
				throw new RemotingException(name + " attribute is required");
			}
			return value;
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000961BC File Offset: 0x000943BC
		private string ExtractAssembly(ref string type)
		{
			int num = type.IndexOf(',');
			if (num == -1)
			{
				return string.Empty;
			}
			string result = type.Substring(num + 1).Trim();
			type = type.Substring(0, num).Trim();
			return result;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x00096204 File Offset: 0x00094404
		public void OnChars(string ch)
		{
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x00096208 File Offset: 0x00094408
		public void OnEndParsing(SmallXmlParser parser)
		{
			RemotingConfiguration.RegisterChannels(this.channelInstances, this.onlyDelayedChannels);
			if (this.appName != null)
			{
				RemotingConfiguration.ApplicationName = this.appName;
			}
			if (!this.onlyDelayedChannels)
			{
				RemotingConfiguration.RegisterTypes(this.typeEntries);
			}
		}

		// Token: 0x0400137C RID: 4988
		private ArrayList typeEntries = new ArrayList();

		// Token: 0x0400137D RID: 4989
		private ArrayList channelInstances = new ArrayList();

		// Token: 0x0400137E RID: 4990
		private ChannelData currentChannel;

		// Token: 0x0400137F RID: 4991
		private Stack currentProviderData;

		// Token: 0x04001380 RID: 4992
		private string currentClientUrl;

		// Token: 0x04001381 RID: 4993
		private string appName;

		// Token: 0x04001382 RID: 4994
		private string currentXmlPath = string.Empty;

		// Token: 0x04001383 RID: 4995
		private bool onlyDelayedChannels;
	}
}
