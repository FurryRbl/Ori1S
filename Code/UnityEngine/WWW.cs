using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A2 RID: 162
	[UsedByNativeCode]
	public sealed class WWW : IDisposable
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		public WWW(string url)
		{
			this.InitWWW(url, null, null);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0000CDC4 File Offset: 0x0000AFC4
		public WWW(string url, WWWForm form)
		{
			string[] iHeaders = WWW.FlattenedHeadersFrom(form.headers);
			this.InitWWW(url, form.data, iHeaders);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0000CDF4 File Offset: 0x0000AFF4
		public WWW(string url, byte[] postData)
		{
			this.InitWWW(url, postData, null);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0000CE08 File Offset: 0x0000B008
		[Obsolete("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead.", true)]
		public WWW(string url, byte[] postData, Hashtable headers)
		{
			Debug.LogError("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead");
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public WWW(string url, byte[] postData, Dictionary<string, string> headers)
		{
			string[] iHeaders = WWW.FlattenedHeadersFrom(headers);
			this.InitWWW(url, postData, iHeaders);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000CE40 File Offset: 0x0000B040
		internal WWW(string url, Hash128 hash, uint crc)
		{
			WWW.INTERNAL_CALL_WWW(this, url, ref hash, crc);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0000CE54 File Offset: 0x0000B054
		public void Dispose()
		{
			this.DestroyWWW(true);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0000CE60 File Offset: 0x0000B060
		~WWW()
		{
			this.DestroyWWW(false);
		}

		// Token: 0x0600093B RID: 2363
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DestroyWWW(bool cancel);

		// Token: 0x0600093C RID: 2364
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InitWWW(string url, byte[] postData, string[] iHeaders);

		// Token: 0x0600093D RID: 2365
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool enforceWebSecurityRestrictions();

		// Token: 0x0600093E RID: 2366 RVA: 0x0000CE9C File Offset: 0x0000B09C
		[ExcludeFromDocs]
		public static string EscapeURL(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWW.EscapeURL(s, utf);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public static string EscapeURL(string s, [DefaultValue("System.Text.Encoding.UTF8")] Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s == string.Empty)
			{
				return string.Empty;
			}
			if (e == null)
			{
				return null;
			}
			return WWWTranscoder.URLEncode(s, e);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		[ExcludeFromDocs]
		public static string UnEscapeURL(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWW.UnEscapeURL(s, utf);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0000CF04 File Offset: 0x0000B104
		public static string UnEscapeURL(string s, [DefaultValue("System.Text.Encoding.UTF8")] Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
			{
				return s;
			}
			return WWWTranscoder.URLDecode(s, e);
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000CF40 File Offset: 0x0000B140
		public Dictionary<string, string> responseHeaders
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not finished downloading yet");
				}
				return WWW.ParseHTTPHeaderString(this.responseHeadersString);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000943 RID: 2371
		private extern string responseHeadersString { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0000CF64 File Offset: 0x0000B164
		public string text
		{
			get
			{
				if (!this.isDone)
				{
					throw new UnityException("WWW is not ready downloading yet");
				}
				byte[] bytes = this.bytes;
				return this.GetTextEncoder().GetString(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		private Encoding GetTextEncoder()
		{
			string text = null;
			if (this.responseHeaders.TryGetValue("CONTENT-TYPE", out text))
			{
				int num = text.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				if (num > -1)
				{
					int num2 = text.IndexOf('=', num);
					if (num2 > -1)
					{
						string text2 = text.Substring(num2 + 1).Trim().Trim(new char[]
						{
							'\'',
							'"'
						}).Trim();
						int num3 = text2.IndexOf(';');
						if (num3 > -1)
						{
							text2 = text2.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text2);
						}
						catch (Exception)
						{
							Debug.Log("Unsupported encoding: '" + text2 + "'");
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0000D08C File Offset: 0x0000B28C
		[Obsolete("Please use WWW.text instead")]
		public string data
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000948 RID: 2376
		public extern byte[] bytes { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000949 RID: 2377
		public extern int size { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600094A RID: 2378
		public extern string error { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600094B RID: 2379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetTexture(bool markNonReadable);

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0000D094 File Offset: 0x0000B294
		public Texture2D texture
		{
			get
			{
				return this.GetTexture(false);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
		public Texture2D textureNonReadable
		{
			get
			{
				return this.GetTexture(true);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public AudioClip audioClip
		{
			get
			{
				return this.GetAudioClip(true);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		public AudioClip GetAudioClip(bool threeD)
		{
			return this.GetAudioClip(threeD, false);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		public AudioClip GetAudioClip(bool threeD, bool stream)
		{
			return this.GetAudioClip(threeD, stream, AudioType.UNKNOWN);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
		{
			return this.GetAudioClipInternal(threeD, stream, false, audioType);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		public AudioClip GetAudioClipCompressed()
		{
			return this.GetAudioClipCompressed(true);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		public AudioClip GetAudioClipCompressed(bool threeD)
		{
			return this.GetAudioClipCompressed(threeD, AudioType.UNKNOWN);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public AudioClip GetAudioClipCompressed(bool threeD, AudioType audioType)
		{
			return this.GetAudioClipInternal(threeD, false, true, audioType);
		}

		// Token: 0x06000955 RID: 2389
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AudioClip GetAudioClipInternal(bool threeD, bool stream, bool compressed, AudioType audioType);

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000956 RID: 2390
		public extern MovieTexture movie { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000957 RID: 2391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void LoadImageIntoTexture(Texture2D tex);

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000958 RID: 2392
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000959 RID: 2393
		[WrapperlessIcall]
		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetURL(string url);

		// Token: 0x0600095A RID: 2394 RVA: 0x0000D100 File Offset: 0x0000B300
		[Obsolete("All blocking WWW functions have been deprecated, please use one of the asynchronous functions instead.", true)]
		public static Texture2D GetTextureFromURL(string url)
		{
			return new WWW(url).texture;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600095B RID: 2395
		public extern float progress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600095C RID: 2396
		public extern float uploadProgress { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600095D RID: 2397
		public extern int bytesDownloaded { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0000D110 File Offset: 0x0000B310
		[Obsolete("Property WWW.oggVorbis has been deprecated. Use WWW.audioClip instead (UnityUpgradable).", true)]
		public AudioClip oggVorbis
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000D114 File Offset: 0x0000B314
		[Obsolete("LoadUnityWeb is no longer supported. Please use javascript to reload the web player on a different url instead", true)]
		public void LoadUnityWeb()
		{
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000960 RID: 2400
		public extern string url { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000961 RID: 2401
		public extern AssetBundle assetBundle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000962 RID: 2402
		// (set) Token: 0x06000963 RID: 2403
		public extern ThreadPriority threadPriority { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000964 RID: 2404
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_WWW(WWW self, string url, ref Hash128 hash, uint crc);

		// Token: 0x06000965 RID: 2405 RVA: 0x0000D118 File Offset: 0x0000B318
		[ExcludeFromDocs]
		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			uint crc = 0U;
			return WWW.LoadFromCacheOrDownload(url, version, crc);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0000D130 File Offset: 0x0000B330
		public static WWW LoadFromCacheOrDownload(string url, int version, [DefaultValue("0")] uint crc)
		{
			Hash128 hash = new Hash128(0U, 0U, 0U, (uint)version);
			return WWW.LoadFromCacheOrDownload(url, hash, crc);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0000D150 File Offset: 0x0000B350
		[ExcludeFromDocs]
		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash)
		{
			uint crc = 0U;
			return WWW.LoadFromCacheOrDownload(url, hash, crc);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0000D168 File Offset: 0x0000B368
		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash, [DefaultValue("0")] uint crc)
		{
			return new WWW(url, hash, crc);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0000D174 File Offset: 0x0000B374
		private static string[] FlattenedHeadersFrom(Dictionary<string, string> headers)
		{
			if (headers == null)
			{
				return null;
			}
			string[] array = new string[headers.Count * 2];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				array[num++] = keyValuePair.Key.ToString();
				array[num++] = keyValuePair.Value.ToString();
			}
			return array;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000D210 File Offset: 0x0000B410
		internal static Dictionary<string, string> ParseHTTPHeaderString(string input)
		{
			if (input == null)
			{
				throw new ArgumentException("input was null to ParseHTTPHeaderString");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StringReader stringReader = new StringReader(input);
			int num = 0;
			for (;;)
			{
				string text = stringReader.ReadLine();
				if (text == null)
				{
					break;
				}
				if (num++ == 0 && text.StartsWith("HTTP"))
				{
					dictionary["STATUS"] = text;
				}
				else
				{
					int num2 = text.IndexOf(": ");
					if (num2 != -1)
					{
						string key = text.Substring(0, num2).ToUpper();
						string value = text.Substring(num2 + 2);
						dictionary[key] = value;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040001F2 RID: 498
		internal IntPtr m_Ptr;
	}
}
