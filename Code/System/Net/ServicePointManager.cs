using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Mono.Security.Protocol.Tls;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x020003E5 RID: 997
	public class ServicePointManager
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x000642AC File Offset: 0x000624AC
		private ServicePointManager()
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000642B4 File Offset: 0x000624B4
		static ServicePointManager()
		{
			object section = ConfigurationManager.GetSection("system.net/connectionManagement");
			System.Net.Configuration.ConnectionManagementSection connectionManagementSection = section as System.Net.Configuration.ConnectionManagementSection;
			if (connectionManagementSection != null)
			{
				ServicePointManager.manager = new ConnectionManagementData(null);
				foreach (object obj in connectionManagementSection.ConnectionManagement)
				{
					System.Net.Configuration.ConnectionManagementElement connectionManagementElement = (System.Net.Configuration.ConnectionManagementElement)obj;
					ServicePointManager.manager.Add(connectionManagementElement.Address, connectionManagementElement.MaxConnection);
				}
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
				return;
			}
			ServicePointManager.manager = (ConnectionManagementData)System.Configuration.ConfigurationSettings.GetConfig("system.net/connectionManagement");
			if (ServicePointManager.manager != null)
			{
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
			}
		}

		/// <summary>Gets or sets policy for server certificates.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICertificatePolicy" /> interface.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x000643E0 File Offset: 0x000625E0
		// (set) Token: 0x06002270 RID: 8816 RVA: 0x000643E8 File Offset: 0x000625E8
		[Obsolete("Use ServerCertificateValidationCallback instead", false)]
		public static ICertificatePolicy CertificatePolicy
		{
			get
			{
				return ServicePointManager.policy;
			}
			set
			{
				ServicePointManager.policy = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x000643F0 File Offset: 0x000625F0
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x000643F8 File Offset: 0x000625F8
		[MonoTODO("CRL checks not implemented")]
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return ServicePointManager._checkCRL;
			}
			set
			{
				ServicePointManager._checkCRL = false;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object. The default value is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> is less than or equal to 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x00064400 File Offset: 0x00062600
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x00064408 File Offset: 0x00062608
		public static int DefaultConnectionLimit
		{
			get
			{
				return ServicePointManager.defaultConnectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.defaultConnectionLimit = value;
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x00064424 File Offset: 0x00062624
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0006442C File Offset: 0x0006262C
		// (set) Token: 0x06002277 RID: 8823 RVA: 0x00064434 File Offset: 0x00062634
		[MonoTODO]
		public static int DnsRefreshTimeout
		{
			get
			{
				throw ServicePointManager.GetMustImplement();
			}
			set
			{
				throw ServicePointManager.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a value that indicates whether a Domain Name Service (DNS) resolution rotates among the applicable Internet Protocol (IP) addresses.</summary>
		/// <returns>false if a DNS resolution always returns the first IP address for a particular host; otherwise true. The default is false.</returns>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x0006443C File Offset: 0x0006263C
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x00064444 File Offset: 0x00062644
		[MonoTODO]
		public static bool EnableDnsRoundRobin
		{
			get
			{
				throw ServicePointManager.GetMustImplement();
			}
			set
			{
				throw ServicePointManager.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum idle time of a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum idle time, in milliseconds, of a <see cref="T:System.Net.ServicePoint" /> object. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePointIdleTime" /> is less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0006444C File Offset: 0x0006264C
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x00064454 File Offset: 0x00062654
		public static int MaxServicePointIdleTime
		{
			get
			{
				return ServicePointManager.maxServicePointIdleTime;
			}
			set
			{
				if (value < -2 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.maxServicePointIdleTime = value;
			}
		}

		/// <summary>Gets or sets the maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain at any time.</summary>
		/// <returns>The maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain. The default value is 0, which means there is no limit to the number of <see cref="T:System.Net.ServicePoint" /> objects.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00064488 File Offset: 0x00062688
		// (set) Token: 0x0600227D RID: 8829 RVA: 0x00064490 File Offset: 0x00062690
		public static int MaxServicePoints
		{
			get
			{
				return ServicePointManager.maxServicePoints;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				ServicePointManager.maxServicePoints = value;
				ServicePointManager.RecycleServicePoints();
			}
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value. </exception>
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000644B0 File Offset: 0x000626B0
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x000644B8 File Offset: 0x000626B8
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				return ServicePointManager._securityProtocol;
			}
			set
			{
				ServicePointManager._securityProtocol = value;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" /> The default value is null.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000644C0 File Offset: 0x000626C0
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x000644C8 File Offset: 0x000626C8
		public static System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				return ServicePointManager.server_cert_cb;
			}
			set
			{
				ServicePointManager.server_cert_cb = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to enable 100-Continue behavior. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000644D0 File Offset: 0x000626D0
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x000644D8 File Offset: 0x000626D8
		public static bool Expect100Continue
		{
			get
			{
				return ServicePointManager.expectContinue;
			}
			set
			{
				ServicePointManager.expectContinue = value;
			}
		}

		/// <summary>Determines whether the Nagle algorithm is used by the service points managed by this <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000644E0 File Offset: 0x000626E0
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x000644E8 File Offset: 0x000626E8
		public static bool UseNagleAlgorithm
		{
			get
			{
				return ServicePointManager.useNagle;
			}
			set
			{
				ServicePointManager.useNagle = value;
			}
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">The <see cref="T:System.Uri" /> object of the Internet resource to contact. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002286 RID: 8838 RVA: 0x000644F0 File Offset: 0x000626F0
		public static ServicePoint FindServicePoint(System.Uri address)
		{
			return ServicePointManager.FindServicePoint(address, GlobalProxySelection.Select);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified Uniform Resource Identifier (URI).</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="uriString">The URI of the Internet resource to be contacted. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uriString" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002287 RID: 8839 RVA: 0x00064500 File Offset: 0x00062700
		public static ServicePoint FindServicePoint(string uriString, IWebProxy proxy)
		{
			return ServicePointManager.FindServicePoint(new System.Uri(uriString), proxy);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002288 RID: 8840 RVA: 0x00064510 File Offset: 0x00062710
		public static ServicePoint FindServicePoint(System.Uri address, IWebProxy proxy)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			ServicePointManager.RecycleServicePoints();
			bool usesProxy = false;
			bool flag = false;
			if (proxy != null && !proxy.IsBypassed(address))
			{
				usesProxy = true;
				bool flag2 = address.Scheme == "https";
				address = proxy.GetProxy(address);
				if (address.Scheme != "http" && !flag2)
				{
					throw new NotSupportedException("Proxy scheme not supported.");
				}
				if (flag2 && address.Scheme == "http")
				{
					flag = true;
				}
			}
			address = new System.Uri(address.Scheme + "://" + address.Authority);
			ServicePoint servicePoint = null;
			System.Collections.Specialized.HybridDictionary obj = ServicePointManager.servicePoints;
			lock (obj)
			{
				ServicePointManager.SPKey key = new ServicePointManager.SPKey(address, flag);
				servicePoint = (ServicePointManager.servicePoints[key] as ServicePoint);
				if (servicePoint != null)
				{
					return servicePoint;
				}
				if (ServicePointManager.maxServicePoints > 0 && ServicePointManager.servicePoints.Count >= ServicePointManager.maxServicePoints)
				{
					throw new InvalidOperationException("maximum number of service points reached");
				}
				string hostOrIP = address.ToString();
				int maxConnections = (int)ServicePointManager.manager.GetMaxConnections(hostOrIP);
				servicePoint = new ServicePoint(address, maxConnections, ServicePointManager.maxServicePointIdleTime);
				servicePoint.Expect100Continue = ServicePointManager.expectContinue;
				servicePoint.UseNagleAlgorithm = ServicePointManager.useNagle;
				servicePoint.UsesProxy = usesProxy;
				servicePoint.UseConnect = flag;
				ServicePointManager.servicePoints.Add(key, servicePoint);
			}
			return servicePoint;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000646B0 File Offset: 0x000628B0
		internal static void RecycleServicePoints()
		{
			ArrayList arrayList = new ArrayList();
			System.Collections.Specialized.HybridDictionary obj = ServicePointManager.servicePoints;
			lock (obj)
			{
				IDictionaryEnumerator enumerator = ServicePointManager.servicePoints.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ServicePoint servicePoint = (ServicePoint)enumerator.Value;
					if (servicePoint.AvailableForRecycling)
					{
						arrayList.Add(enumerator.Key);
					}
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					ServicePointManager.servicePoints.Remove(arrayList[i]);
				}
				if (ServicePointManager.maxServicePoints != 0 && ServicePointManager.servicePoints.Count > ServicePointManager.maxServicePoints)
				{
					SortedList sortedList = new SortedList(ServicePointManager.servicePoints.Count);
					enumerator = ServicePointManager.servicePoints.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ServicePoint servicePoint2 = (ServicePoint)enumerator.Value;
						if (servicePoint2.CurrentConnections == 0)
						{
							while (sortedList.ContainsKey(servicePoint2.IdleSince))
							{
								servicePoint2.IdleSince = servicePoint2.IdleSince.AddMilliseconds(1.0);
							}
							sortedList.Add(servicePoint2.IdleSince, servicePoint2.Address);
						}
					}
					int num = 0;
					while (num < sortedList.Count && ServicePointManager.servicePoints.Count > ServicePointManager.maxServicePoints)
					{
						ServicePointManager.servicePoints.Remove(sortedList.GetByIndex(num));
						num++;
					}
				}
			}
		}

		/// <summary>The default number of non-persistent connections (4) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.0 or later server. This field is constant but is no longer used in the .NET Framework 2.0.</summary>
		// Token: 0x0400151B RID: 5403
		public const int DefaultNonPersistentConnectionLimit = 4;

		/// <summary>The default number of persistent connections (2) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.1 or later server. This field is constant and is used to initialize the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property if the value of the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property has not been set either directly or through configuration.</summary>
		// Token: 0x0400151C RID: 5404
		public const int DefaultPersistentConnectionLimit = 2;

		// Token: 0x0400151D RID: 5405
		private const string configKey = "system.net/connectionManagement";

		// Token: 0x0400151E RID: 5406
		private static System.Collections.Specialized.HybridDictionary servicePoints = new System.Collections.Specialized.HybridDictionary();

		// Token: 0x0400151F RID: 5407
		private static ICertificatePolicy policy = new DefaultCertificatePolicy();

		// Token: 0x04001520 RID: 5408
		private static int defaultConnectionLimit = 2;

		// Token: 0x04001521 RID: 5409
		private static int maxServicePointIdleTime = 900000;

		// Token: 0x04001522 RID: 5410
		private static int maxServicePoints = 0;

		// Token: 0x04001523 RID: 5411
		private static bool _checkCRL = false;

		// Token: 0x04001524 RID: 5412
		private static SecurityProtocolType _securityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

		// Token: 0x04001525 RID: 5413
		private static bool expectContinue = true;

		// Token: 0x04001526 RID: 5414
		private static bool useNagle;

		// Token: 0x04001527 RID: 5415
		private static System.Net.Security.RemoteCertificateValidationCallback server_cert_cb;

		// Token: 0x04001528 RID: 5416
		private static ConnectionManagementData manager;

		// Token: 0x020003E6 RID: 998
		private class SPKey
		{
			// Token: 0x0600228A RID: 8842 RVA: 0x00064868 File Offset: 0x00062A68
			public SPKey(System.Uri uri, bool use_connect)
			{
				this.uri = uri;
				this.use_connect = use_connect;
			}

			// Token: 0x17000A04 RID: 2564
			// (get) Token: 0x0600228B RID: 8843 RVA: 0x00064880 File Offset: 0x00062A80
			public System.Uri Uri
			{
				get
				{
					return this.uri;
				}
			}

			// Token: 0x17000A05 RID: 2565
			// (get) Token: 0x0600228C RID: 8844 RVA: 0x00064888 File Offset: 0x00062A88
			public bool UseConnect
			{
				get
				{
					return this.use_connect;
				}
			}

			// Token: 0x0600228D RID: 8845 RVA: 0x00064890 File Offset: 0x00062A90
			public override int GetHashCode()
			{
				return this.uri.GetHashCode() + ((!this.use_connect) ? 0 : 1);
			}

			// Token: 0x0600228E RID: 8846 RVA: 0x000648B0 File Offset: 0x00062AB0
			public override bool Equals(object obj)
			{
				ServicePointManager.SPKey spkey = obj as ServicePointManager.SPKey;
				return obj != null && this.uri.Equals(spkey.uri) && spkey.use_connect == this.use_connect;
			}

			// Token: 0x04001529 RID: 5417
			private System.Uri uri;

			// Token: 0x0400152A RID: 5418
			private bool use_connect;
		}

		// Token: 0x020003E7 RID: 999
		internal class ChainValidationHelper
		{
			// Token: 0x0600228F RID: 8847 RVA: 0x000648F4 File Offset: 0x00062AF4
			public ChainValidationHelper(object sender)
			{
				this.sender = sender;
			}

			// Token: 0x17000A06 RID: 2566
			// (get) Token: 0x06002291 RID: 8849 RVA: 0x00064920 File Offset: 0x00062B20
			// (set) Token: 0x06002292 RID: 8850 RVA: 0x0006496C File Offset: 0x00062B6C
			public string Host
			{
				get
				{
					if (this.host == null && this.sender is HttpWebRequest)
					{
						this.host = ((HttpWebRequest)this.sender).Address.Host;
					}
					return this.host;
				}
				set
				{
					this.host = value;
				}
			}

			// Token: 0x06002293 RID: 8851 RVA: 0x00064978 File Offset: 0x00062B78
			internal ValidationResult ValidateChain(Mono.Security.X509.X509CertificateCollection certs)
			{
				bool user_denied = false;
				if (certs == null || certs.Count == 0)
				{
					return null;
				}
				ICertificatePolicy certificatePolicy = ServicePointManager.CertificatePolicy;
				System.Net.Security.RemoteCertificateValidationCallback serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
				System.Security.Cryptography.X509Certificates.X509Chain x509Chain = new System.Security.Cryptography.X509Certificates.X509Chain();
				x509Chain.ChainPolicy = new System.Security.Cryptography.X509Certificates.X509ChainPolicy();
				for (int i = 1; i < certs.Count; i++)
				{
					System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certs[i].RawData);
					x509Chain.ChainPolicy.ExtraStore.Add(certificate);
				}
				System.Security.Cryptography.X509Certificates.X509Certificate2 x509Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certs[0].RawData);
				int num = 0;
				System.Net.Security.SslPolicyErrors sslPolicyErrors = System.Net.Security.SslPolicyErrors.None;
				try
				{
					if (!x509Chain.Build(x509Certificate))
					{
						sslPolicyErrors |= ServicePointManager.ChainValidationHelper.GetErrorsFromChain(x509Chain);
					}
				}
				catch (Exception arg)
				{
					Console.Error.WriteLine("ERROR building certificate chain: {0}", arg);
					Console.Error.WriteLine("Please, report this problem to the Mono team");
					sslPolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;
				}
				if (!ServicePointManager.ChainValidationHelper.CheckCertificateUsage(x509Certificate))
				{
					sslPolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;
					num = -2146762490;
				}
				if (!ServicePointManager.ChainValidationHelper.CheckServerIdentity(certs[0], this.Host))
				{
					sslPolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch;
					num = -2146762481;
				}
				bool flag = false;
				if (ServicePointManager.ChainValidationHelper.is_macosx)
				{
					try
					{
						OSX509Certificates.SecTrustResult secTrustResult = OSX509Certificates.TrustEvaluateSsl(certs);
						flag = (secTrustResult == OSX509Certificates.SecTrustResult.Proceed || secTrustResult == OSX509Certificates.SecTrustResult.Unspecified);
					}
					catch
					{
					}
					if (flag)
					{
						num = 0;
						sslPolicyErrors = System.Net.Security.SslPolicyErrors.None;
					}
				}
				if (certificatePolicy != null && (!(certificatePolicy is DefaultCertificatePolicy) || serverCertificateValidationCallback == null))
				{
					ServicePoint srvPoint = null;
					HttpWebRequest httpWebRequest = this.sender as HttpWebRequest;
					if (httpWebRequest != null)
					{
						srvPoint = httpWebRequest.ServicePoint;
					}
					if (num == 0 && sslPolicyErrors != System.Net.Security.SslPolicyErrors.None)
					{
						num = ServicePointManager.ChainValidationHelper.GetStatusFromChain(x509Chain);
					}
					flag = certificatePolicy.CheckValidationResult(srvPoint, x509Certificate, httpWebRequest, num);
					user_denied = (!flag && !(certificatePolicy is DefaultCertificatePolicy));
				}
				if (serverCertificateValidationCallback != null)
				{
					flag = serverCertificateValidationCallback(this.sender, x509Certificate, x509Chain, sslPolicyErrors);
					user_denied = !flag;
				}
				return new ValidationResult(flag, user_denied, num);
			}

			// Token: 0x06002294 RID: 8852 RVA: 0x00064BAC File Offset: 0x00062DAC
			private static int GetStatusFromChain(System.Security.Cryptography.X509Certificates.X509Chain chain)
			{
				long num = 0L;
				foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					System.Security.Cryptography.X509Certificates.X509ChainStatusFlags status = x509ChainStatus.Status;
					if (status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762495);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeNested) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762494);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Revoked) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762484);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotSignatureValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146869244);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotValidForUsage) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762480);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762487);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146885614);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Cyclic) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762486);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidExtension) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762485);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidPolicyConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762483);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146869223);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidNameConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762476);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotSupportedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762476);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotDefinedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762476);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotPermittedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762476);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasExcludedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762476);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762486);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotTimeValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762495);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotSignatureValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146869244);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotValidForUsage) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762480);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.OfflineRevocation) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146885614);
						}
						else if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoIssuanceChainPolicy) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
						{
							num = (long)((ulong)-2146762489);
						}
						else
						{
							num = (long)((ulong)-2146762485);
						}
						break;
					}
				}
				return (int)num;
			}

			// Token: 0x06002295 RID: 8853 RVA: 0x00064E14 File Offset: 0x00063014
			private static System.Net.Security.SslPolicyErrors GetErrorsFromChain(System.Security.Cryptography.X509Certificates.X509Chain chain)
			{
				System.Net.Security.SslPolicyErrors sslPolicyErrors = System.Net.Security.SslPolicyErrors.None;
				foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if (x509ChainStatus.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						sslPolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;
						break;
					}
				}
				return sslPolicyErrors;
			}

			// Token: 0x06002296 RID: 8854 RVA: 0x00064E68 File Offset: 0x00063068
			private static bool CheckCertificateUsage(System.Security.Cryptography.X509Certificates.X509Certificate2 cert)
			{
				bool result;
				try
				{
					if (cert.Version < 3)
					{
						result = true;
					}
					else
					{
						System.Security.Cryptography.X509Certificates.X509KeyUsageExtension x509KeyUsageExtension = (System.Security.Cryptography.X509Certificates.X509KeyUsageExtension)cert.Extensions["2.5.29.15"];
						System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension x509EnhancedKeyUsageExtension = (System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension)cert.Extensions["2.5.29.37"];
						if (x509KeyUsageExtension != null && x509EnhancedKeyUsageExtension != null)
						{
							if ((x509KeyUsageExtension.KeyUsages & ServicePointManager.ChainValidationHelper.s_flags) == System.Security.Cryptography.X509Certificates.X509KeyUsageFlags.None)
							{
								result = false;
							}
							else
							{
								result = (x509EnhancedKeyUsageExtension.EnhancedKeyUsages["1.3.6.1.5.5.7.3.1"] != null || x509EnhancedKeyUsageExtension.EnhancedKeyUsages["2.16.840.1.113730.4.1"] != null);
							}
						}
						else if (x509KeyUsageExtension != null)
						{
							result = ((x509KeyUsageExtension.KeyUsages & ServicePointManager.ChainValidationHelper.s_flags) != System.Security.Cryptography.X509Certificates.X509KeyUsageFlags.None);
						}
						else if (x509EnhancedKeyUsageExtension != null)
						{
							result = (x509EnhancedKeyUsageExtension.EnhancedKeyUsages["1.3.6.1.5.5.7.3.1"] != null || x509EnhancedKeyUsageExtension.EnhancedKeyUsages["2.16.840.1.113730.4.1"] != null);
						}
						else
						{
							System.Security.Cryptography.X509Certificates.X509Extension x509Extension = cert.Extensions["2.16.840.1.113730.1.1"];
							if (x509Extension != null)
							{
								string text = x509Extension.NetscapeCertType(false);
								result = (text.IndexOf("SSL Server Authentication") != -1);
							}
							else
							{
								result = true;
							}
						}
					}
				}
				catch (Exception arg)
				{
					Console.Error.WriteLine("ERROR processing certificate: {0}", arg);
					Console.Error.WriteLine("Please, report this problem to the Mono team");
					result = false;
				}
				return result;
			}

			// Token: 0x06002297 RID: 8855 RVA: 0x00064FFC File Offset: 0x000631FC
			private static bool CheckServerIdentity(Mono.Security.X509.X509Certificate cert, string targetHost)
			{
				bool result;
				try
				{
					Mono.Security.X509.X509Extension x509Extension = cert.Extensions["2.5.29.17"];
					if (x509Extension != null)
					{
						SubjectAltNameExtension subjectAltNameExtension = new SubjectAltNameExtension(x509Extension);
						foreach (string pattern in subjectAltNameExtension.DNSNames)
						{
							if (ServicePointManager.ChainValidationHelper.Match(targetHost, pattern))
							{
								return true;
							}
						}
						foreach (string a in subjectAltNameExtension.IPAddresses)
						{
							if (a == targetHost)
							{
								return true;
							}
						}
					}
					result = ServicePointManager.ChainValidationHelper.CheckDomainName(cert.SubjectName, targetHost);
				}
				catch (Exception arg)
				{
					Console.Error.WriteLine("ERROR processing certificate: {0}", arg);
					Console.Error.WriteLine("Please, report this problem to the Mono team");
					result = false;
				}
				return result;
			}

			// Token: 0x06002298 RID: 8856 RVA: 0x00065104 File Offset: 0x00063304
			private static bool CheckDomainName(string subjectName, string targetHost)
			{
				string pattern = string.Empty;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("CN\\s*=\\s*([^,]*)");
				System.Text.RegularExpressions.MatchCollection matchCollection = regex.Matches(subjectName);
				if (matchCollection.Count == 1 && matchCollection[0].Success)
				{
					pattern = matchCollection[0].Groups[1].Value.ToString();
				}
				return ServicePointManager.ChainValidationHelper.Match(targetHost, pattern);
			}

			// Token: 0x06002299 RID: 8857 RVA: 0x0006516C File Offset: 0x0006336C
			private static bool Match(string hostname, string pattern)
			{
				int num = pattern.IndexOf('*');
				if (num == -1)
				{
					return string.Compare(hostname, pattern, true, CultureInfo.InvariantCulture) == 0;
				}
				if (num != pattern.Length - 1 && pattern[num + 1] != '.')
				{
					return false;
				}
				int num2 = pattern.IndexOf('*', num + 1);
				if (num2 != -1)
				{
					return false;
				}
				string text = pattern.Substring(num + 1);
				int num3 = hostname.Length - text.Length;
				if (num3 <= 0)
				{
					return false;
				}
				if (string.Compare(hostname, num3, text, 0, text.Length, true, CultureInfo.InvariantCulture) != 0)
				{
					return false;
				}
				if (num == 0)
				{
					int num4 = hostname.IndexOf('.');
					return num4 == -1 || num4 >= hostname.Length - text.Length;
				}
				string text2 = pattern.Substring(0, num);
				return string.Compare(hostname, 0, text2, 0, text2.Length, true, CultureInfo.InvariantCulture) == 0;
			}

			// Token: 0x0400152B RID: 5419
			private object sender;

			// Token: 0x0400152C RID: 5420
			private string host;

			// Token: 0x0400152D RID: 5421
			private static bool is_macosx = File.Exists("/System/Library/Frameworks/Security.framework/Security");

			// Token: 0x0400152E RID: 5422
			private static System.Security.Cryptography.X509Certificates.X509KeyUsageFlags s_flags = System.Security.Cryptography.X509Certificates.X509KeyUsageFlags.KeyAgreement | System.Security.Cryptography.X509Certificates.X509KeyUsageFlags.KeyEncipherment | System.Security.Cryptography.X509Certificates.X509KeyUsageFlags.DigitalSignature;
		}
	}
}
