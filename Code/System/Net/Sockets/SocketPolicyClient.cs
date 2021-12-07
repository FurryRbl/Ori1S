using System;
using System.IO;
using System.Text;

namespace System.Net.Sockets
{
	// Token: 0x020003FC RID: 1020
	internal static class SocketPolicyClient
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x0006BFF4 File Offset: 0x0006A1F4
		private static void Log(string msg)
		{
			Console.WriteLine(string.Concat(new object[]
			{
				"SocketPolicyClient",
				SocketPolicyClient.session,
				": ",
				msg
			}));
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x0006C028 File Offset: 0x0006A228
		private static Stream GetPolicyStreamForIP(string ip, int policyport, int timeout)
		{
			SocketPolicyClient.session++;
			SocketPolicyClient.Log("Incoming GetPolicyStreamForIP");
			IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Parse(ip), policyport);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			byte[] array = new byte[5000];
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				SocketPolicyClient.Log("About to BeginConnect to " + ipendPoint);
				IAsyncResult asyncResult = socket.BeginConnect(ipendPoint, null, null, false);
				SocketPolicyClient.Log("About to WaitOne");
				DateTime now = DateTime.Now;
				if (!asyncResult.AsyncWaitHandle.WaitOne(timeout))
				{
					SocketPolicyClient.Log("WaitOne timed out. Duration: " + (DateTime.Now - now).TotalMilliseconds);
					socket.Close();
					throw new Exception("BeginConnect timed out");
				}
				socket.EndConnect(asyncResult);
				SocketPolicyClient.Log("Socket connected");
				byte[] bytes = Encoding.ASCII.GetBytes("<policy-file-request/>\0");
				SocketError socketError;
				socket.Send_nochecks(bytes, 0, bytes.Length, SocketFlags.None, out socketError);
				if (socketError != SocketError.Success)
				{
					SocketPolicyClient.Log("Socket error: " + socketError);
					return memoryStream;
				}
				int count = socket.Receive_nochecks(array, 0, array.Length, SocketFlags.None, out socketError);
				if (socketError != SocketError.Success)
				{
					SocketPolicyClient.Log("Socket error: " + socketError);
					return memoryStream;
				}
				try
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}
				catch (SocketException)
				{
				}
				memoryStream = new MemoryStream(array, 0, count);
			}
			catch (Exception ex)
			{
				SocketPolicyClient.Log("Caught exception: " + ex.Message);
				return memoryStream;
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return memoryStream;
		}

		// Token: 0x04001613 RID: 5651
		private const string policy_request = "<policy-file-request/>\0";

		// Token: 0x04001614 RID: 5652
		private static int session;
	}
}
