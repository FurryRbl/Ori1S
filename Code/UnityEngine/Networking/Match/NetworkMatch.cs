using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJson;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000249 RID: 585
	public class NetworkMatch : MonoBehaviour
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0002C3BC File Offset: 0x0002A5BC
		public Uri baseUri
		{
			get
			{
				return this.m_BaseUri;
			}
			set
			{
				this.m_BaseUri = value;
			}
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
		public void SetProgramAppID(AppID programAppID)
		{
			Utility.SetAppID(programAppID);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		public Coroutine CreateMatch(string matchName, uint matchSize, bool matchAdvertise, string matchPassword, NetworkMatch.ResponseDelegate<CreateMatchResponse> callback)
		{
			return this.CreateMatch(new CreateMatchRequest
			{
				name = matchName,
				size = matchSize,
				advertise = matchAdvertise,
				password = matchPassword
			}, callback);
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0002C40C File Offset: 0x0002A60C
		public Coroutine CreateMatch(CreateMatchRequest req, NetworkMatch.ResponseDelegate<CreateMatchResponse> callback)
		{
			Uri uri = new Uri(this.baseUri, "/json/reply/CreateMatchRequest");
			Debug.Log("MatchMakingClient Create :" + uri);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("projectId", Application.cloudProjectId);
			wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
			wwwform.AddField("appId", Utility.GetAppID().ToString());
			wwwform.AddField("accessTokenString", 0);
			wwwform.AddField("domain", 0);
			wwwform.AddField("name", req.name);
			wwwform.AddField("size", req.size.ToString());
			wwwform.AddField("advertise", req.advertise.ToString());
			wwwform.AddField("password", req.password);
			wwwform.headers["Accept"] = "application/json";
			WWW client = new WWW(uri.ToString(), wwwform);
			return base.StartCoroutine(this.ProcessMatchResponse<CreateMatchResponse>(client, callback));
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0002C524 File Offset: 0x0002A724
		public Coroutine JoinMatch(NetworkID netId, string matchPassword, NetworkMatch.ResponseDelegate<JoinMatchResponse> callback)
		{
			return this.JoinMatch(new JoinMatchRequest
			{
				networkId = netId,
				password = matchPassword
			}, callback);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0002C550 File Offset: 0x0002A750
		public Coroutine JoinMatch(JoinMatchRequest req, NetworkMatch.ResponseDelegate<JoinMatchResponse> callback)
		{
			Uri uri = new Uri(this.baseUri, "/json/reply/JoinMatchRequest");
			Debug.Log("MatchMakingClient Join :" + uri);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("projectId", Application.cloudProjectId);
			wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
			wwwform.AddField("appId", Utility.GetAppID().ToString());
			wwwform.AddField("accessTokenString", 0);
			wwwform.AddField("domain", 0);
			wwwform.AddField("networkId", req.networkId.ToString());
			wwwform.AddField("password", req.password);
			wwwform.headers["Accept"] = "application/json";
			WWW client = new WWW(uri.ToString(), wwwform);
			return base.StartCoroutine(this.ProcessMatchResponse<JoinMatchResponse>(client, callback));
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0002C63C File Offset: 0x0002A83C
		public Coroutine DestroyMatch(NetworkID netId, NetworkMatch.ResponseDelegate<BasicResponse> callback)
		{
			return this.DestroyMatch(new DestroyMatchRequest
			{
				networkId = netId
			}, callback);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0002C660 File Offset: 0x0002A860
		public Coroutine DestroyMatch(DestroyMatchRequest req, NetworkMatch.ResponseDelegate<BasicResponse> callback)
		{
			Uri uri = new Uri(this.baseUri, "/json/reply/DestroyMatchRequest");
			Debug.Log("MatchMakingClient Destroy :" + uri.ToString());
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("projectId", Application.cloudProjectId);
			wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
			wwwform.AddField("appId", Utility.GetAppID().ToString());
			wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
			wwwform.AddField("domain", 0);
			wwwform.AddField("networkId", req.networkId.ToString());
			wwwform.headers["Accept"] = "application/json";
			WWW client = new WWW(uri.ToString(), wwwform);
			return base.StartCoroutine(this.ProcessMatchResponse<BasicResponse>(client, callback));
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x0002C750 File Offset: 0x0002A950
		public Coroutine DropConnection(NetworkID netId, NodeID dropNodeId, NetworkMatch.ResponseDelegate<BasicResponse> callback)
		{
			return this.DropConnection(new DropConnectionRequest
			{
				networkId = netId,
				nodeId = dropNodeId
			}, callback);
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x0002C77C File Offset: 0x0002A97C
		public Coroutine DropConnection(DropConnectionRequest req, NetworkMatch.ResponseDelegate<BasicResponse> callback)
		{
			Uri uri = new Uri(this.baseUri, "/json/reply/DropConnectionRequest");
			Debug.Log("MatchMakingClient DropConnection :" + uri);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("projectId", Application.cloudProjectId);
			wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
			wwwform.AddField("appId", Utility.GetAppID().ToString());
			wwwform.AddField("accessTokenString", Utility.GetAccessTokenForNetwork(req.networkId).GetByteString());
			wwwform.AddField("domain", 0);
			wwwform.AddField("networkId", req.networkId.ToString());
			wwwform.AddField("nodeId", req.nodeId.ToString());
			wwwform.headers["Accept"] = "application/json";
			WWW client = new WWW(uri.ToString(), wwwform);
			return base.StartCoroutine(this.ProcessMatchResponse<BasicResponse>(client, callback));
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x0002C884 File Offset: 0x0002AA84
		public Coroutine ListMatches(int startPageNumber, int resultPageSize, string matchNameFilter, NetworkMatch.ResponseDelegate<ListMatchResponse> callback)
		{
			return this.ListMatches(new ListMatchRequest
			{
				pageNum = startPageNumber,
				pageSize = resultPageSize,
				nameFilter = matchNameFilter
			}, callback);
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		public Coroutine ListMatches(ListMatchRequest req, NetworkMatch.ResponseDelegate<ListMatchResponse> callback)
		{
			Uri uri = new Uri(this.baseUri, "/json/reply/ListMatchRequest");
			Debug.Log("MatchMakingClient ListMatches :" + uri);
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("projectId", Application.cloudProjectId);
			wwwform.AddField("sourceId", Utility.GetSourceID().ToString());
			wwwform.AddField("appId", Utility.GetAppID().ToString());
			wwwform.AddField("includePasswordMatches", req.includePasswordMatches.ToString());
			wwwform.AddField("accessTokenString", 0);
			wwwform.AddField("domain", 0);
			wwwform.AddField("pageSize", req.pageSize);
			wwwform.AddField("pageNum", req.pageNum);
			wwwform.AddField("nameFilter", req.nameFilter);
			wwwform.headers["Accept"] = "application/json";
			WWW client = new WWW(uri.ToString(), wwwform);
			return base.StartCoroutine(this.ProcessMatchResponse<ListMatchResponse>(client, callback));
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x0002C9C4 File Offset: 0x0002ABC4
		private IEnumerator ProcessMatchResponse<JSONRESPONSE>(WWW client, NetworkMatch.ResponseDelegate<JSONRESPONSE> callback) where JSONRESPONSE : Response, new()
		{
			yield return client;
			JSONRESPONSE jsonInterface = (JSONRESPONSE)((object)null);
			if (string.IsNullOrEmpty(client.error))
			{
				object o;
				if (SimpleJson.TryDeserializeObject(client.text, out o))
				{
					IDictionary<string, object> dictJsonObj = o as IDictionary<string, object>;
					if (dictJsonObj != null)
					{
						try
						{
							jsonInterface = Activator.CreateInstance<JSONRESPONSE>();
							jsonInterface.Parse(o);
						}
						catch (FormatException ex)
						{
							FormatException exception = ex;
							Debug.Log(exception);
						}
					}
				}
				if (jsonInterface == null)
				{
					Debug.LogError("Could not parse: " + client.text);
				}
				else
				{
					Debug.Log("JSON Response: " + jsonInterface.ToString());
				}
			}
			else
			{
				Debug.LogError("Request error: " + client.error);
				Debug.LogError("Raw response: " + client.text);
			}
			if (jsonInterface == null)
			{
				jsonInterface = Activator.CreateInstance<JSONRESPONSE>();
			}
			callback(jsonInterface);
			yield break;
		}

		// Token: 0x04000947 RID: 2375
		private const string kMultiplayerNetworkingIdKey = "CloudNetworkingId";

		// Token: 0x04000948 RID: 2376
		private Uri m_BaseUri = new Uri("https://mm.unet.unity3d.com");

		// Token: 0x0200034C RID: 844
		// (Invoke) Token: 0x0600288A RID: 10378
		public delegate void ResponseDelegate<T>(T response);
	}
}
