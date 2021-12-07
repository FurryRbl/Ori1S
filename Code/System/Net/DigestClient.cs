using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x020002FB RID: 763
	internal class DigestClient : IAuthenticationModule
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A22 RID: 6690 RVA: 0x00048950 File Offset: 0x00046B50
		private static Hashtable Cache
		{
			get
			{
				object syncRoot = DigestClient.cache.SyncRoot;
				lock (syncRoot)
				{
					DigestClient.CheckExpired(DigestClient.cache.Count);
				}
				return DigestClient.cache;
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000489AC File Offset: 0x00046BAC
		private static void CheckExpired(int count)
		{
			if (count < 10)
			{
				return;
			}
			DateTime t = DateTime.MaxValue;
			DateTime now = DateTime.Now;
			ArrayList arrayList = null;
			foreach (object obj in DigestClient.cache.Keys)
			{
				int num = (int)obj;
				DigestSession digestSession = (DigestSession)DigestClient.cache[num];
				if (digestSession.LastUse < t && (digestSession.LastUse - now).Ticks > 6000000000L)
				{
					t = digestSession.LastUse;
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(num);
				}
			}
			if (arrayList != null)
			{
				foreach (object obj2 in arrayList)
				{
					int num2 = (int)obj2;
					DigestClient.cache.Remove(num2);
				}
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00048B18 File Offset: 0x00046D18
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			if (text.ToLower().IndexOf("digest") == -1)
			{
				return null;
			}
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			bool flag = digestSession == null;
			if (flag)
			{
				digestSession = new DigestSession();
			}
			if (!digestSession.Parse(challenge))
			{
				return null;
			}
			if (flag)
			{
				DigestClient.Cache.Add(num, digestSession);
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00048BCC File Offset: 0x00046DCC
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			if (credentials == null)
			{
				return null;
			}
			int num = httpWebRequest.Address.GetHashCode() ^ credentials.GetHashCode();
			DigestSession digestSession = (DigestSession)DigestClient.Cache[num];
			if (digestSession == null)
			{
				return null;
			}
			return digestSession.Authenticate(webRequest, credentials);
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x00048C2C File Offset: 0x00046E2C
		public string AuthenticationType
		{
			get
			{
				return "Digest";
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x00048C34 File Offset: 0x00046E34
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001045 RID: 4165
		private static readonly Hashtable cache = Hashtable.Synchronized(new Hashtable());
	}
}
