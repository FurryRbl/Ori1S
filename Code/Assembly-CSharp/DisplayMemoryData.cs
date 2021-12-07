using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000961 RID: 2401
[RequireComponent(typeof(GUIText))]
public class DisplayMemoryData : MonoBehaviour
{
	// Token: 0x060034CF RID: 13519 RVA: 0x000DD851 File Offset: 0x000DBA51
	private void Start()
	{
		this.RefreshStats();
	}

	// Token: 0x060034D0 RID: 13520 RVA: 0x000DD85C File Offset: 0x000DBA5C
	private void Update()
	{
		if (this.m_timeOfLastSample + this.Intervals < Time.time)
		{
			this.m_timeOfLastSample = Time.time;
			this.RefreshStats();
		}
	}

	// Token: 0x060034D1 RID: 13521 RVA: 0x000DD894 File Offset: 0x000DBA94
	public void RefreshStats()
	{
		List<DisplayMemoryData.TextureWithMemory> list = new List<DisplayMemoryData.TextureWithMemory>();
		list.Clear();
		UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(Texture));
		int num = 0;
		foreach (Texture texture in array)
		{
			int runtimeMemorySize = Profiler.GetRuntimeMemorySize(texture);
			list.Add(new DisplayMemoryData.TextureWithMemory(texture, (float)runtimeMemorySize / 1024f / 1024f));
			num += runtimeMemorySize;
		}
		list.Sort((DisplayMemoryData.TextureWithMemory a, DisplayMemoryData.TextureWithMemory b) => a.Memory.CompareTo(b.Memory));
		using (StreamWriter streamWriter = new StreamWriter(new FileStream("TextureMemory.txt", FileMode.Create)))
		{
			foreach (DisplayMemoryData.TextureWithMemory textureWithMemory in list)
			{
				streamWriter.WriteLine(textureWithMemory.Texture.name + "  " + textureWithMemory.Memory);
			}
		}
		HashSet<Texture> hashSet = new HashSet<Texture>();
		int num2 = 0;
		foreach (Renderer renderer in UnityEngine.Object.FindObjectsOfType(typeof(Renderer)))
		{
			foreach (Material material in renderer.sharedMaterials)
			{
				if (material && material.mainTexture)
				{
					hashSet.Add(material.mainTexture);
				}
			}
		}
		foreach (Texture texture2 in hashSet)
		{
			int runtimeMemorySize2 = Profiler.GetRuntimeMemorySize(texture2);
			list.Add(new DisplayMemoryData.TextureWithMemory(texture2, (float)runtimeMemorySize2 / 1024f / 1024f));
			num2 += runtimeMemorySize2;
		}
		using (StreamWriter streamWriter2 = new StreamWriter(new FileStream("UnusedTextureMemory.txt", FileMode.Create)))
		{
			foreach (DisplayMemoryData.TextureWithMemory textureWithMemory2 in list)
			{
				if (!hashSet.Contains(textureWithMemory2.Texture))
				{
					streamWriter2.WriteLine(string.Concat(new object[]
					{
						(!(textureWithMemory2.Texture is RenderTexture)) ? string.Empty : "[RenderTexture]",
						textureWithMemory2.Texture.name,
						"  [",
						textureWithMemory2.Memory,
						" mb]"
					}));
				}
			}
		}
		UnityEngine.Object[] array4 = Resources.FindObjectsOfTypeAll(typeof(Material));
		int num3 = 0;
		foreach (Material o in array4)
		{
			num3 += Profiler.GetRuntimeMemorySize(o);
		}
		UnityEngine.Object[] array6 = Resources.FindObjectsOfTypeAll(typeof(Mesh));
		int num4 = 0;
		foreach (Mesh o2 in array6)
		{
			num4 += Profiler.GetRuntimeMemorySize(o2);
		}
		List<DisplayMemoryData.AudioWithMemory> list2 = new List<DisplayMemoryData.AudioWithMemory>();
		UnityEngine.Object[] array8 = Resources.FindObjectsOfTypeAll(typeof(AudioClip));
		int num5 = 0;
		foreach (AudioClip audioClip in array8)
		{
			int runtimeMemorySize3 = Profiler.GetRuntimeMemorySize(audioClip);
			list2.Add(new DisplayMemoryData.AudioWithMemory(audioClip, (float)runtimeMemorySize3 / 1024f / 1024f));
			num5 += runtimeMemorySize3;
		}
		list2.Sort((DisplayMemoryData.AudioWithMemory a, DisplayMemoryData.AudioWithMemory b) => a.Memory.CompareTo(b.Memory));
		using (StreamWriter streamWriter3 = new StreamWriter(new FileStream("AudioMemory.txt", FileMode.Create)))
		{
			foreach (DisplayMemoryData.AudioWithMemory audioWithMemory in list2)
			{
				streamWriter3.WriteLine(audioWithMemory.Clip.name + "  " + audioWithMemory.Memory);
			}
		}
		UnityEngine.Object[] array10 = Resources.FindObjectsOfTypeAll(typeof(Animation));
		int num6 = 0;
		foreach (Animation o3 in array10)
		{
			num6 += Profiler.GetRuntimeMemorySize(o3);
		}
		base.GetComponent<GUIText>().text = string.Concat(new object[]
		{
			"Textures: ",
			Utility.Round((float)num / 1024f / 1024f, 0.01f),
			"(mb)\nUsed textures: ",
			Utility.Round((float)num2 / 1024f / 1024f, 0.01f),
			"(mb)\nMaterials: ",
			Utility.Round((float)num3 / 1024f / 1024f, 0.01f),
			"(mb)\nMeshes: ",
			Utility.Round((float)num4 / 1024f / 1024f, 0.01f),
			"(mb)\nAudio: ",
			Utility.Round((float)num5 / 1024f / 1024f, 0.01f),
			"(mb)\nAnimation: ",
			Utility.Round((float)num6 / 1024f / 1024f, 0.01f),
			"(mb)"
		});
	}

	// Token: 0x04002F90 RID: 12176
	private float m_timeOfLastSample;

	// Token: 0x04002F91 RID: 12177
	public float Intervals = 2f;

	// Token: 0x02000962 RID: 2402
	public class TextureWithMemory
	{
		// Token: 0x060034D4 RID: 13524 RVA: 0x000DDF06 File Offset: 0x000DC106
		public TextureWithMemory(Texture texture, float memory)
		{
			this.Texture = texture;
			this.Memory = memory;
		}

		// Token: 0x04002F94 RID: 12180
		public Texture Texture;

		// Token: 0x04002F95 RID: 12181
		public float Memory;
	}

	// Token: 0x02000963 RID: 2403
	public class AudioWithMemory
	{
		// Token: 0x060034D5 RID: 13525 RVA: 0x000DDF1C File Offset: 0x000DC11C
		public AudioWithMemory(AudioClip clip, float memory)
		{
			this.Clip = clip;
			this.Memory = memory;
		}

		// Token: 0x04002F96 RID: 12182
		public AudioClip Clip;

		// Token: 0x04002F97 RID: 12183
		public float Memory;
	}
}
