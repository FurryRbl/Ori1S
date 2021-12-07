using System;
using System.IO;
using System.Threading;
using Mono.Security.Protocol.Tls.Handshake;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200008E RID: 142
	internal abstract class RecordProtocol
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0001E444 File Offset: 0x0001C644
		public RecordProtocol(Stream innerStream, Context context)
		{
			this.innerStream = innerStream;
			this.context = context;
			this.context.RecordProtocol = this;
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001E484 File Offset: 0x0001C684
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001E48C File Offset: 0x0001C68C
		public Context Context
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

		// Token: 0x06000528 RID: 1320 RVA: 0x0001E498 File Offset: 0x0001C698
		public virtual void SendRecord(HandshakeType type)
		{
			IAsyncResult asyncResult = this.BeginSendRecord(type, null, null);
			this.EndSendRecord(asyncResult);
		}

		// Token: 0x06000529 RID: 1321
		protected abstract void ProcessHandshakeMessage(TlsStream handMsg);

		// Token: 0x0600052A RID: 1322 RVA: 0x0001E4B8 File Offset: 0x0001C6B8
		protected virtual void ProcessChangeCipherSpec()
		{
			Context context = this.Context;
			context.ReadSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.EndSwitchingSecurityParameters(true);
			}
			else
			{
				context.StartSwitchingSecurityParameters(false);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		public virtual HandshakeMessage GetMessage(HandshakeType type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public IAsyncResult BeginReceiveRecord(Stream record, AsyncCallback callback, object state)
		{
			if (this.context.ReceivedConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			RecordProtocol.record_processing.Reset();
			byte[] initialBuffer = new byte[1];
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = new RecordProtocol.ReceiveRecordAsyncResult(callback, state, initialBuffer, record);
			record.BeginRead(receiveRecordAsyncResult.InitialBuffer, 0, receiveRecordAsyncResult.InitialBuffer.Length, new AsyncCallback(this.InternalReceiveRecordCallback), receiveRecordAsyncResult);
			return receiveRecordAsyncResult;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001E568 File Offset: 0x0001C768
		private void InternalReceiveRecordCallback(IAsyncResult asyncResult)
		{
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = asyncResult.AsyncState as RecordProtocol.ReceiveRecordAsyncResult;
			Stream record = receiveRecordAsyncResult.Record;
			try
			{
				if (receiveRecordAsyncResult.Record.EndRead(asyncResult) == 0)
				{
					receiveRecordAsyncResult.SetComplete(null);
				}
				else
				{
					int num = (int)receiveRecordAsyncResult.InitialBuffer[0];
					this.context.LastHandshakeMsg = HandshakeType.ClientHello;
					ContentType contentType = (ContentType)num;
					byte[] array = this.ReadRecordBuffer(num, record);
					if (array == null)
					{
						receiveRecordAsyncResult.SetComplete(null);
					}
					else
					{
						if (contentType != ContentType.Alert || array.Length != 2)
						{
							if (this.Context.Read != null && this.Context.Read.Cipher != null)
							{
								array = this.decryptRecordFragment(contentType, array);
							}
						}
						ContentType contentType2 = contentType;
						switch (contentType2)
						{
						case ContentType.ChangeCipherSpec:
							this.ProcessChangeCipherSpec();
							break;
						case ContentType.Alert:
							this.ProcessAlert((AlertLevel)array[0], (AlertDescription)array[1]);
							if (record.CanSeek)
							{
								record.SetLength(0L);
							}
							array = null;
							break;
						case ContentType.Handshake:
						{
							TlsStream tlsStream = new TlsStream(array);
							while (!tlsStream.EOF)
							{
								this.ProcessHandshakeMessage(tlsStream);
							}
							break;
						}
						case ContentType.ApplicationData:
							break;
						default:
							if (contentType2 != (ContentType)128)
							{
								throw new TlsException(AlertDescription.UnexpectedMessage, "Unknown record received from server.");
							}
							this.context.HandshakeMessages.Write(array);
							break;
						}
						receiveRecordAsyncResult.SetComplete(array);
					}
				}
			}
			catch (Exception complete)
			{
				receiveRecordAsyncResult.SetComplete(complete);
			}
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001E714 File Offset: 0x0001C914
		public byte[] EndReceiveRecord(IAsyncResult asyncResult)
		{
			RecordProtocol.ReceiveRecordAsyncResult receiveRecordAsyncResult = asyncResult as RecordProtocol.ReceiveRecordAsyncResult;
			if (receiveRecordAsyncResult == null)
			{
				throw new ArgumentException("Either the provided async result is null or was not created by this RecordProtocol.");
			}
			if (!receiveRecordAsyncResult.IsCompleted)
			{
				receiveRecordAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (receiveRecordAsyncResult.CompletedWithError)
			{
				throw receiveRecordAsyncResult.AsyncException;
			}
			byte[] resultingBuffer = receiveRecordAsyncResult.ResultingBuffer;
			RecordProtocol.record_processing.Set();
			return resultingBuffer;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001E778 File Offset: 0x0001C978
		public byte[] ReceiveRecord(Stream record)
		{
			IAsyncResult asyncResult = this.BeginReceiveRecord(record, null, null);
			return this.EndReceiveRecord(asyncResult);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001E798 File Offset: 0x0001C998
		private byte[] ReadRecordBuffer(int contentType, Stream record)
		{
			if (contentType == 128)
			{
				return this.ReadClientHelloV2(record);
			}
			if (!Enum.IsDefined(typeof(ContentType), (ContentType)contentType))
			{
				throw new TlsException(AlertDescription.DecodeError);
			}
			return this.ReadStandardRecordBuffer(record);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		private byte[] ReadClientHelloV2(Stream record)
		{
			int num = record.ReadByte();
			if (record.CanSeek && (long)(num + 1) > record.Length)
			{
				return null;
			}
			byte[] array = new byte[num];
			record.Read(array, 0, num);
			int num2 = (int)array[0];
			if (num2 != 1)
			{
				throw new TlsException(AlertDescription.DecodeError);
			}
			int num3 = (int)array[1] << 8 | (int)array[2];
			int num4 = (int)array[3] << 8 | (int)array[4];
			int num5 = (int)array[5] << 8 | (int)array[6];
			int num6 = (int)array[7] << 8 | (int)array[8];
			int num7 = (num6 <= 32) ? num6 : 32;
			byte[] array2 = new byte[num4];
			Buffer.BlockCopy(array, 9, array2, 0, num4);
			byte[] array3 = new byte[num5];
			Buffer.BlockCopy(array, 9 + num4, array3, 0, num5);
			byte[] array4 = new byte[num6];
			Buffer.BlockCopy(array, 9 + num4 + num5, array4, 0, num6);
			if (num6 < 16 || num4 == 0 || num4 % 3 != 0)
			{
				throw new TlsException(AlertDescription.DecodeError);
			}
			if (array3.Length > 0)
			{
				this.context.SessionId = array3;
			}
			this.Context.ChangeProtocol((short)num3);
			this.ProcessCipherSpecV2Buffer(this.Context.SecurityProtocol, array2);
			this.context.ClientRandom = new byte[32];
			Buffer.BlockCopy(array4, array4.Length - num7, this.context.ClientRandom, 32 - num7, num7);
			this.context.LastHandshakeMsg = HandshakeType.ClientHello;
			this.context.ProtocolNegotiated = true;
			return array;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001E970 File Offset: 0x0001CB70
		private byte[] ReadStandardRecordBuffer(Stream record)
		{
			byte[] array = new byte[4];
			if (record.Read(array, 0, 4) != 4)
			{
				throw new TlsException("buffer underrun");
			}
			short num = (short)((int)array[0] << 8 | (int)array[1]);
			short num2 = (short)((int)array[2] << 8 | (int)array[3]);
			if (record.CanSeek && (long)(num2 + 5) > record.Length)
			{
				return null;
			}
			int num3 = 0;
			byte[] array2 = new byte[(int)num2];
			while (num3 != (int)num2)
			{
				int num4 = record.Read(array2, num3, array2.Length - num3);
				if (num4 == 0)
				{
					throw new TlsException(AlertDescription.CloseNotify, "Received 0 bytes from stream. It must be closed.");
				}
				num3 += num4;
			}
			if (num != this.context.Protocol && this.context.ProtocolNegotiated)
			{
				throw new TlsException(AlertDescription.ProtocolVersion, "Invalid protocol version on message received");
			}
			return array2;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private void ProcessAlert(AlertLevel alertLevel, AlertDescription alertDesc)
		{
			if (alertLevel != AlertLevel.Warning)
			{
				if (alertLevel == AlertLevel.Fatal)
				{
					throw new TlsException(alertLevel, alertDesc);
				}
			}
			if (alertDesc == AlertDescription.CloseNotify)
			{
				this.context.ReceivedConnectionEnd = true;
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001EA90 File Offset: 0x0001CC90
		public void SendAlert(AlertDescription description)
		{
			this.SendAlert(new Alert(description));
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public void SendAlert(AlertLevel level, AlertDescription description)
		{
			this.SendAlert(new Alert(level, description));
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		public void SendAlert(Alert alert)
		{
			AlertLevel alertLevel;
			AlertDescription alertDescription;
			bool flag;
			if (alert == null)
			{
				alertLevel = AlertLevel.Fatal;
				alertDescription = AlertDescription.InternalError;
				flag = true;
			}
			else
			{
				alertLevel = alert.Level;
				alertDescription = alert.Description;
				flag = alert.IsCloseNotify;
			}
			this.SendRecord(ContentType.Alert, new byte[]
			{
				(byte)alertLevel,
				(byte)alertDescription
			});
			if (flag)
			{
				this.context.SentConnectionEnd = true;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		public void SendChangeCipherSpec()
		{
			this.SendRecord(ContentType.ChangeCipherSpec, new byte[]
			{
				1
			});
			Context context = this.context;
			context.WriteSequenceNumber = 0UL;
			if (context is ClientContext)
			{
				context.StartSwitchingSecurityParameters(true);
			}
			else
			{
				context.EndSwitchingSecurityParameters(false);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001EB58 File Offset: 0x0001CD58
		public IAsyncResult BeginSendRecord(HandshakeType handshakeType, AsyncCallback callback, object state)
		{
			HandshakeMessage message = this.GetMessage(handshakeType);
			message.Process();
			RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = new RecordProtocol.SendRecordAsyncResult(callback, state, message);
			this.BeginSendRecord(message.ContentType, message.EncodeMessage(), new AsyncCallback(this.InternalSendRecordCallback), sendRecordAsyncResult);
			return sendRecordAsyncResult;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001EBA0 File Offset: 0x0001CDA0
		private void InternalSendRecordCallback(IAsyncResult ar)
		{
			RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = ar.AsyncState as RecordProtocol.SendRecordAsyncResult;
			try
			{
				this.EndSendRecord(ar);
				sendRecordAsyncResult.Message.Update();
				sendRecordAsyncResult.Message.Reset();
				sendRecordAsyncResult.SetComplete();
			}
			catch (Exception complete)
			{
				sendRecordAsyncResult.SetComplete(complete);
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001EC0C File Offset: 0x0001CE0C
		public IAsyncResult BeginSendRecord(ContentType contentType, byte[] recordData, AsyncCallback callback, object state)
		{
			if (this.context.SentConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			byte[] array = this.EncodeRecord(contentType, recordData);
			return this.innerStream.BeginWrite(array, 0, array.Length, callback, state);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001EC54 File Offset: 0x0001CE54
		public void EndSendRecord(IAsyncResult asyncResult)
		{
			if (asyncResult is RecordProtocol.SendRecordAsyncResult)
			{
				RecordProtocol.SendRecordAsyncResult sendRecordAsyncResult = asyncResult as RecordProtocol.SendRecordAsyncResult;
				if (!sendRecordAsyncResult.IsCompleted)
				{
					sendRecordAsyncResult.AsyncWaitHandle.WaitOne();
				}
				if (sendRecordAsyncResult.CompletedWithError)
				{
					throw sendRecordAsyncResult.AsyncException;
				}
			}
			else
			{
				this.innerStream.EndWrite(asyncResult);
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
		public void SendRecord(ContentType contentType, byte[] recordData)
		{
			IAsyncResult asyncResult = this.BeginSendRecord(contentType, recordData, null, null);
			this.EndSendRecord(asyncResult);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		public byte[] EncodeRecord(ContentType contentType, byte[] recordData)
		{
			return this.EncodeRecord(contentType, recordData, 0, recordData.Length);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public byte[] EncodeRecord(ContentType contentType, byte[] recordData, int offset, int count)
		{
			if (this.context.SentConnectionEnd)
			{
				throw new TlsException(AlertDescription.InternalError, "The session is finished and it's no longer valid.");
			}
			TlsStream tlsStream = new TlsStream();
			short num;
			for (int i = offset; i < offset + count; i += (int)num)
			{
				if (count + offset - i > 16384)
				{
					num = 16384;
				}
				else
				{
					num = (short)(count + offset - i);
				}
				byte[] array = new byte[(int)num];
				Buffer.BlockCopy(recordData, i, array, 0, (int)num);
				if (this.Context.Write != null && this.Context.Write.Cipher != null)
				{
					array = this.encryptRecordFragment(contentType, array);
				}
				tlsStream.Write((byte)contentType);
				tlsStream.Write(this.context.Protocol);
				tlsStream.Write((short)array.Length);
				tlsStream.Write(array);
			}
			return tlsStream.ToArray();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		private byte[] encryptRecordFragment(ContentType contentType, byte[] fragment)
		{
			byte[] mac;
			if (this.Context is ClientContext)
			{
				mac = this.context.Write.Cipher.ComputeClientRecordMAC(contentType, fragment);
			}
			else
			{
				mac = this.context.Write.Cipher.ComputeServerRecordMAC(contentType, fragment);
			}
			byte[] result = this.context.Write.Cipher.EncryptRecord(fragment, mac);
			this.context.WriteSequenceNumber += 1UL;
			return result;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001EE3C File Offset: 0x0001D03C
		private byte[] decryptRecordFragment(ContentType contentType, byte[] fragment)
		{
			byte[] array = null;
			byte[] array2 = null;
			try
			{
				this.context.Read.Cipher.DecryptRecord(fragment, out array, out array2);
			}
			catch
			{
				if (this.context is ServerContext)
				{
					this.Context.RecordProtocol.SendAlert(AlertDescription.DecryptionFailed);
				}
				throw;
			}
			byte[] array3;
			if (this.Context is ClientContext)
			{
				array3 = this.context.Read.Cipher.ComputeServerRecordMAC(contentType, array);
			}
			else
			{
				array3 = this.context.Read.Cipher.ComputeClientRecordMAC(contentType, array);
			}
			if (!this.Compare(array3, array2))
			{
				throw new TlsException(AlertDescription.BadRecordMAC, "Bad record MAC");
			}
			this.context.ReadSequenceNumber += 1UL;
			return array;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001EF28 File Offset: 0x0001D128
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null)
			{
				return array2 == null;
			}
			if (array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001EF78 File Offset: 0x0001D178
		private void ProcessCipherSpecV2Buffer(SecurityProtocolType protocol, byte[] buffer)
		{
			TlsStream tlsStream = new TlsStream(buffer);
			string prefix = (protocol != SecurityProtocolType.Ssl3) ? "TLS_" : "SSL_";
			while (tlsStream.Position < tlsStream.Length)
			{
				byte b = tlsStream.ReadByte();
				if (b == 0)
				{
					short code = tlsStream.ReadInt16();
					int num = this.Context.SupportedCiphers.IndexOf(code);
					if (num != -1)
					{
						this.Context.Negotiating.Cipher = this.Context.SupportedCiphers[num];
						break;
					}
				}
				else
				{
					byte[] array = new byte[2];
					tlsStream.Read(array, 0, array.Length);
					int code2 = (int)(b & byte.MaxValue) << 16 | (int)(array[0] & byte.MaxValue) << 8 | (int)(array[1] & byte.MaxValue);
					CipherSuite cipherSuite = this.MapV2CipherCode(prefix, code2);
					if (cipherSuite != null)
					{
						this.Context.Negotiating.Cipher = cipherSuite;
						break;
					}
				}
			}
			if (this.Context.Negotiating == null)
			{
				throw new TlsException(AlertDescription.InsuficientSecurity, "Insuficient Security");
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001F098 File Offset: 0x0001D298
		private CipherSuite MapV2CipherCode(string prefix, int code)
		{
			CipherSuite result;
			try
			{
				if (code != 65664)
				{
					if (code != 131200)
					{
						if (code != 196736)
						{
							if (code != 262272)
							{
								if (code != 327808)
								{
									if (code != 393280)
									{
										if (code != 458944)
										{
											result = null;
										}
										else
										{
											result = null;
										}
									}
									else
									{
										result = null;
									}
								}
								else
								{
									result = null;
								}
							}
							else
							{
								result = this.Context.SupportedCiphers[prefix + "RSA_EXPORT_WITH_RC2_CBC_40_MD5"];
							}
						}
						else
						{
							result = this.Context.SupportedCiphers[prefix + "RSA_EXPORT_WITH_RC2_CBC_40_MD5"];
						}
					}
					else
					{
						result = this.Context.SupportedCiphers[prefix + "RSA_EXPORT_WITH_RC4_40_MD5"];
					}
				}
				else
				{
					result = this.Context.SupportedCiphers[prefix + "RSA_WITH_RC4_128_MD5"];
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000298 RID: 664
		private static ManualResetEvent record_processing = new ManualResetEvent(true);

		// Token: 0x04000299 RID: 665
		protected Stream innerStream;

		// Token: 0x0400029A RID: 666
		protected Context context;

		// Token: 0x0200008F RID: 143
		private class ReceiveRecordAsyncResult : IAsyncResult
		{
			// Token: 0x06000544 RID: 1348 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
			public ReceiveRecordAsyncResult(AsyncCallback userCallback, object userState, byte[] initialBuffer, Stream record)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._initialBuffer = initialBuffer;
				this._record = record;
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001F204 File Offset: 0x0001D404
			public Stream Record
			{
				get
				{
					return this._record;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001F20C File Offset: 0x0001D40C
			public byte[] ResultingBuffer
			{
				get
				{
					return this._resultingBuffer;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001F214 File Offset: 0x0001D414
			public byte[] InitialBuffer
			{
				get
				{
					return this._initialBuffer;
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001F21C File Offset: 0x0001D41C
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001F224 File Offset: 0x0001D424
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001F22C File Offset: 0x0001D42C
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && null != this._asyncException;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001F248 File Offset: 0x0001D448
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001F2AC File Offset: 0x0001D4AC
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool result;
					lock (obj)
					{
						result = this.completed;
					}
					return result;
				}
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x0001F300 File Offset: 0x0001D500
			private void SetComplete(Exception ex, byte[] resultingBuffer)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (!this.completed)
					{
						this.completed = true;
						this._asyncException = ex;
						this._resultingBuffer = resultingBuffer;
						if (this.handle != null)
						{
							this.handle.Set();
						}
						if (this._userCallback != null)
						{
							this._userCallback.BeginInvoke(this, null, null);
						}
					}
				}
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x0001F398 File Offset: 0x0001D598
			public void SetComplete(Exception ex)
			{
				this.SetComplete(ex, null);
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
			public void SetComplete(byte[] resultingBuffer)
			{
				this.SetComplete(null, resultingBuffer);
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
			public void SetComplete()
			{
				this.SetComplete(null, null);
			}

			// Token: 0x0400029B RID: 667
			private object locker = new object();

			// Token: 0x0400029C RID: 668
			private AsyncCallback _userCallback;

			// Token: 0x0400029D RID: 669
			private object _userState;

			// Token: 0x0400029E RID: 670
			private Exception _asyncException;

			// Token: 0x0400029F RID: 671
			private ManualResetEvent handle;

			// Token: 0x040002A0 RID: 672
			private byte[] _resultingBuffer;

			// Token: 0x040002A1 RID: 673
			private Stream _record;

			// Token: 0x040002A2 RID: 674
			private bool completed;

			// Token: 0x040002A3 RID: 675
			private byte[] _initialBuffer;
		}

		// Token: 0x02000090 RID: 144
		private class SendRecordAsyncResult : IAsyncResult
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x0001F3BC File Offset: 0x0001D5BC
			public SendRecordAsyncResult(AsyncCallback userCallback, object userState, HandshakeMessage message)
			{
				this._userCallback = userCallback;
				this._userState = userState;
				this._message = message;
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
			public HandshakeMessage Message
			{
				get
				{
					return this._message;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
			public object AsyncState
			{
				get
				{
					return this._userState;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001F400 File Offset: 0x0001D600
			public Exception AsyncException
			{
				get
				{
					return this._asyncException;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001F408 File Offset: 0x0001D608
			public bool CompletedWithError
			{
				get
				{
					return this.IsCompleted && null != this._asyncException;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001F424 File Offset: 0x0001D624
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					object obj = this.locker;
					lock (obj)
					{
						if (this.handle == null)
						{
							this.handle = new ManualResetEvent(this.completed);
						}
					}
					return this.handle;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000558 RID: 1368 RVA: 0x0001F488 File Offset: 0x0001D688
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001F48C File Offset: 0x0001D68C
			public bool IsCompleted
			{
				get
				{
					object obj = this.locker;
					bool result;
					lock (obj)
					{
						result = this.completed;
					}
					return result;
				}
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x0001F4DC File Offset: 0x0001D6DC
			public void SetComplete(Exception ex)
			{
				object obj = this.locker;
				lock (obj)
				{
					if (!this.completed)
					{
						this.completed = true;
						if (this.handle != null)
						{
							this.handle.Set();
						}
						if (this._userCallback != null)
						{
							this._userCallback.BeginInvoke(this, null, null);
						}
						this._asyncException = ex;
					}
				}
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x0001F570 File Offset: 0x0001D770
			public void SetComplete()
			{
				this.SetComplete(null);
			}

			// Token: 0x040002A4 RID: 676
			private object locker = new object();

			// Token: 0x040002A5 RID: 677
			private AsyncCallback _userCallback;

			// Token: 0x040002A6 RID: 678
			private object _userState;

			// Token: 0x040002A7 RID: 679
			private Exception _asyncException;

			// Token: 0x040002A8 RID: 680
			private ManualResetEvent handle;

			// Token: 0x040002A9 RID: 681
			private HandshakeMessage _message;

			// Token: 0x040002AA RID: 682
			private bool completed;
		}
	}
}
