using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x020002C4 RID: 708
	internal class ChunkStream
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x000431B0 File Offset: 0x000413B0
		public ChunkStream(byte[] buffer, int offset, int size, WebHeaderCollection headers) : this(headers)
		{
			this.Write(buffer, offset, size);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000431C4 File Offset: 0x000413C4
		public ChunkStream(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000431FC File Offset: 0x000413FC
		public void ResetBuffer()
		{
			this.chunkSize = -1;
			this.chunkRead = 0;
			this.chunks.Clear();
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00043218 File Offset: 0x00041418
		public void WriteAndReadBack(byte[] buffer, int offset, int size, ref int read)
		{
			if (offset + read > 0)
			{
				this.Write(buffer, offset, offset + read);
			}
			read = this.Read(buffer, offset, size);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0004324C File Offset: 0x0004144C
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00043258 File Offset: 0x00041458
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				ChunkStream.Chunk chunk = (ChunkStream.Chunk)this.chunks[i];
				if (chunk != null)
				{
					if (chunk.Offset == chunk.Bytes.Length)
					{
						this.chunks[i] = null;
					}
					else
					{
						num += chunk.Read(buffer, offset + num, size - num);
						if (num == size)
						{
							break;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x000432E4 File Offset: 0x000414E4
		public void Write(byte[] buffer, int offset, int size)
		{
			this.InternalWrite(buffer, ref offset, size);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000432F0 File Offset: 0x000414F0
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == ChunkStream.State.None)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == ChunkStream.State.None)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == ChunkStream.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == ChunkStream.State.Body)
				{
					return;
				}
			}
			if (this.state == ChunkStream.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == ChunkStream.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == ChunkStream.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == ChunkStream.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x00043400 File Offset: 0x00041600
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state != ChunkStream.State.None;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001869 RID: 6249 RVA: 0x00043430 File Offset: 0x00041630
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00043440 File Offset: 0x00041640
		private ChunkStream.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return ChunkStream.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new ChunkStream.Chunk(array));
			offset += num;
			this.chunkRead += num;
			return (this.chunkRead != this.chunkSize) ? ChunkStream.State.Body : ChunkStream.State.BodyFinished;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000434D0 File Offset: 0x000416D0
		private ChunkStream.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			char c = '\0';
			while (offset < size)
			{
				c = (char)buffer[offset++];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						ChunkStream.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						ChunkStream.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					ChunkStream.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(ChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					ChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return ChunkStream.State.None;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(ChunkStream.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				ChunkStream.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return ChunkStream.State.Trailer;
			}
			return ChunkStream.State.Body;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00043670 File Offset: 0x00041870
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00043698 File Offset: 0x00041898
		private ChunkStream.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				if ((ushort)buffer[offset++] != 13)
				{
					ChunkStream.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return ChunkStream.State.BodyFinished;
				}
			}
			if (this.sawCR && (ushort)buffer[offset++] != 10)
			{
				ChunkStream.ThrowProtocolViolation("Expecting \\n");
			}
			return ChunkStream.State.None;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00043708 File Offset: 0x00041908
		private ChunkStream.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && (ushort)buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && (ushort)buffer[offset] == 10)
				{
					offset++;
					return ChunkStream.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			string text = "\r\n\r";
			while (offset < size && num < 4)
			{
				char c = (char)buffer[offset++];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num > 0)
				{
					this.saved.Append(text.Substring(0, (this.saved.Length != 0) ? num : (num - 2)));
					num = 0;
					if (this.saved.Length > 4196)
					{
						ChunkStream.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					ChunkStream.ThrowProtocolViolation("Error reading trailer.");
				}
				return ChunkStream.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text2;
			while ((text2 = stringReader.ReadLine()) != null && text2 != string.Empty)
			{
				this.headers.Add(text2);
			}
			return ChunkStream.State.None;
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00043890 File Offset: 0x00041A90
		private static void ThrowProtocolViolation(string message)
		{
			WebException ex = new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
			throw ex;
		}

		// Token: 0x04000FA1 RID: 4001
		internal WebHeaderCollection headers;

		// Token: 0x04000FA2 RID: 4002
		private int chunkSize;

		// Token: 0x04000FA3 RID: 4003
		private int chunkRead;

		// Token: 0x04000FA4 RID: 4004
		private ChunkStream.State state;

		// Token: 0x04000FA5 RID: 4005
		private StringBuilder saved;

		// Token: 0x04000FA6 RID: 4006
		private bool sawCR;

		// Token: 0x04000FA7 RID: 4007
		private bool gotit;

		// Token: 0x04000FA8 RID: 4008
		private int trailerState;

		// Token: 0x04000FA9 RID: 4009
		private ArrayList chunks;

		// Token: 0x020002C5 RID: 709
		private enum State
		{
			// Token: 0x04000FAB RID: 4011
			None,
			// Token: 0x04000FAC RID: 4012
			Body,
			// Token: 0x04000FAD RID: 4013
			BodyFinished,
			// Token: 0x04000FAE RID: 4014
			Trailer
		}

		// Token: 0x020002C6 RID: 710
		private class Chunk
		{
			// Token: 0x06001870 RID: 6256 RVA: 0x000438AC File Offset: 0x00041AAC
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x06001871 RID: 6257 RVA: 0x000438BC File Offset: 0x00041ABC
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = (size <= this.Bytes.Length - this.Offset) ? size : (this.Bytes.Length - this.Offset);
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x04000FAF RID: 4015
			public byte[] Bytes;

			// Token: 0x04000FB0 RID: 4016
			public int Offset;
		}
	}
}
