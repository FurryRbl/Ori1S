using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EC RID: 236
[ExecuteInEditMode]
public class UberGhostTrailMeshUpdate : MonoBehaviour
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000967 RID: 2407 RVA: 0x000289B5 File Offset: 0x00026BB5
	private Mesh TrailMesh
	{
		get
		{
			return this.m_trailMesh;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000968 RID: 2408 RVA: 0x000289C0 File Offset: 0x00026BC0
	public Material TargetMat
	{
		get
		{
			return (!(this.TargetRenderer != null)) ? null : this.TargetRenderer.sharedMaterial;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000969 RID: 2409 RVA: 0x000289EF File Offset: 0x00026BEF
	private Renderer TargetRenderer
	{
		get
		{
			if (this.m_animatorTarget == null)
			{
				return null;
			}
			if (this.m_targetRenderer == null)
			{
				this.m_targetRenderer = this.m_animatorTarget.GetComponent<Renderer>();
			}
			return this.m_targetRenderer;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x0600096A RID: 2410 RVA: 0x00028A2C File Offset: 0x00026C2C
	private bool IsDead
	{
		get
		{
			return this.GhostTarget == null || !this.GhostTarget.gameObject.activeInHierarchy || !this.GhostTarget.enabled || (this.m_targetIsSpriteAnim && this.GhostTarget.GetComponent<SpriteAnimator>().AnimationEnded);
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x0600096B RID: 2411 RVA: 0x00028A94 File Offset: 0x00026C94
	// (set) Token: 0x0600096C RID: 2412 RVA: 0x00028A9C File Offset: 0x00026C9C
	public bool Visible { get; private set; }

	// Token: 0x0600096D RID: 2413 RVA: 0x00028AA5 File Offset: 0x00026CA5
	private void OnBecameVisible()
	{
		this.Visible = true;
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00028AAE File Offset: 0x00026CAE
	private void OnBecameInvisible()
	{
		this.Visible = false;
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x0600096F RID: 2415 RVA: 0x00028AB7 File Offset: 0x00026CB7
	private float TrailTime
	{
		get
		{
			return (!Application.isPlaying) ? Time.realtimeSinceStartup : Time.time;
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00028AD4 File Offset: 0x00026CD4
	private void Awake()
	{
		this.m_trailMesh = new Mesh();
		this.m_trailMesh.name = "uberGhostTrail";
		this.m_trailMesh.MarkDynamic();
		this.m_renderer = base.GetComponent<Renderer>();
		this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
		this.m_meshFilter.sharedMesh = this.m_trailMesh;
		if (!Application.isPlaying)
		{
			this.m_trailMesh.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00028B4C File Offset: 0x00026D4C
	public void SetPos(Vector3 pos)
	{
		if (this.m_quads == null)
		{
			this.m_trailMesh.bounds = new Bounds(pos, Vector3.one * 1.5f);
			return;
		}
		if (this.m_posFrame % 2 == 0)
		{
			float num = pos.x - 1f;
			float num2 = pos.x + 1f;
			float num3 = pos.y - 1f;
			float num4 = pos.y + 1f;
			for (int i = 0; i < this.m_quadCount; i += 5)
			{
				if (this.m_active[i])
				{
					UberGhostTrailMeshUpdate.Quad quad = this.m_quads[i];
					float x = quad.X;
					float y = quad.Y;
					num = Mathf.Min(num, x);
					num3 = Mathf.Min(num3, y);
					num2 = Mathf.Max(num2, x);
					num4 = Mathf.Max(num4, y);
				}
			}
			Bounds bounds = default(Bounds);
			bounds.min = new Vector3(num, num3, -1f);
			bounds.max = new Vector3(num2, num4, 1f);
			this.m_trailMesh.bounds = bounds;
			if (pos.z != this.m_lastZ)
			{
				base.transform.position = new Vector3(0f, 0f, this.m_lastZ);
				this.m_lastZ = pos.z;
			}
		}
		this.m_posFrame++;
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00028CC5 File Offset: 0x00026EC5
	private void OnEnable()
	{
		if (!Application.isPlaying)
		{
			this.ReallocateBuffers();
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00028CD7 File Offset: 0x00026ED7
	private void ResizeOrCreate<T>(ref T[] arr, int size)
	{
		if (arr == null)
		{
			arr = new T[size];
		}
		else if (arr.Length != size)
		{
			Array.Resize<T>(ref arr, size);
		}
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00028CFE File Offset: 0x00026EFE
	private T[] EnsureBuffer<T>(T[] arr, int minSize)
	{
		if (arr == null)
		{
			arr = new T[minSize];
		}
		else if (arr.Length < minSize)
		{
			Array.Resize<T>(ref arr, Mathf.Max(arr.Length * 2, minSize));
		}
		return arr;
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00028D30 File Offset: 0x00026F30
	private void ReallocateBuffers()
	{
		this.ResizeOrCreate<Vector3>(ref this.m_vertices, this.m_currentSize * 4);
		this.ResizeOrCreate<Vector2>(ref this.m_uv2, this.m_currentSize * 4);
		this.ResizeOrCreate<Color32>(ref this.m_colors, this.m_currentSize * 4);
		this.ResizeOrCreate<UberGhostTrailMeshUpdate.Quad>(ref this.m_quads, this.m_currentSize);
		this.ResizeOrCreate<bool>(ref this.m_active, this.m_currentSize);
		this.UpdateMaterialsBuf();
		int num = (this.m_mainUvs == null) ? 0 : this.m_mainUvs.Length;
		this.ResizeOrCreate<Vector2>(ref this.m_mainUvs, this.m_currentSize * 4);
		for (int i = num; i < this.m_quadCount; i++)
		{
			int num2 = i * 4;
			this.m_mainUvs[num2] = new Vector2(0f, 0f);
			this.m_mainUvs[num2 + 1] = new Vector2(0.5f, 0f);
			this.m_mainUvs[num2 + 2] = new Vector2(0.5f, 0.5f);
			this.m_mainUvs[num2 + 3] = new Vector2(0f, 0.5f);
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00028E74 File Offset: 0x00027074
	private void UpdateMaterialsBuf()
	{
		this.m_triangles = new int[this.m_materialSize][];
		this.m_setMaterials = new Material[this.m_materialSize][];
		this.m_materials = new Material[this.m_materialSize];
		for (int i = 0; i < this.m_materialSize; i++)
		{
			this.m_setMaterials[i] = new Material[i];
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00028EDC File Offset: 0x000270DC
	public void SetSettings(UberGhostTrail trail)
	{
		this.GhostTarget = trail;
		this.m_scaleCurve = trail.ScaleCurve;
		this.m_constForce = trail.ConstantForce;
		this.m_localConstForce = trail.LocalConstantForce;
		this.m_startSpeed = trail.Startspeed;
		this.m_startRandomSpeed = trail.RandomStartSpeed;
		this.m_localStartSpeed = trail.LocalStartSpeed;
		this.m_localRandomStartSpeed = trail.LocalRandomStartSpeed;
		this.m_alphaCurve = trail.FadeoutCurve;
		this.m_destroyTime = this.m_alphaCurve[Mathf.Max(this.m_alphaCurve.length - 1, 0)].time;
		this.m_destroyTime = Mathf.Min(this.m_scaleCurve[Mathf.Max(this.m_scaleCurve.length - 1, 0)].time, this.m_destroyTime);
		Renderer renderer = trail.Renderer;
		Material sharedMaterial = renderer.sharedMaterial;
		this.m_col = sharedMaterial.GetColor(ShaderProperties.Color);
		this.m_animatorTarget = trail.AnimatorTarget;
		this.m_targetIsSpriteAnim = (trail.GetComponent<SpriteAnimator>() != null);
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00028FF5 File Offset: 0x000271F5
	private void OnDestroy()
	{
		if (!Application.isPlaying)
		{
			UnityEngine.Object.DestroyImmediate(this.m_trailMesh);
		}
		else
		{
			UnityEngine.Object.DestroyObject(this.m_trailMesh);
		}
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0002901C File Offset: 0x0002721C
	public Vector2 RandomVec(Vector2 val)
	{
		return new Vector2(UnityEngine.Random.Range(-val.x, val.x), UnityEngine.Random.Range(-val.y, val.y));
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0002904B File Offset: 0x0002724B
	private void Update()
	{
		if (this.IsDead)
		{
			this.UpdateTrailMesh();
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00029060 File Offset: 0x00027260
	public void SpawnSingleTrailSprite(float posx, float posy, Vector4 size, Vector3 euler, Quaternion rotate, Vector3 scale)
	{
		if (this.GhostTarget == null)
		{
			return;
		}
		Material targetMat = this.TargetMat;
		if (targetMat == null)
		{
			return;
		}
		if (this.m_quads == null)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < this.m_quads.Length; i++)
		{
			int num2 = (this.m_lastIndex + i) % this.m_quads.Length;
			if (!this.m_active[num2])
			{
				num = num2;
				this.m_lastIndex = num2;
				break;
			}
		}
		if (num == -1)
		{
			num = this.m_quads.Length;
			this.m_currentSize += 90;
			this.ReallocateBuffers();
		}
		this.m_quadCount = Mathf.Max(this.m_quadCount, num + 1);
		bool flipped = targetMat.GetVector("_DepthFlipScreen").y > 0.5f;
		size.x = (size.x - 0.5f) * scale.x;
		size.y = (size.y - 0.5f) * scale.y;
		size.z = (size.z - 0.5f) * scale.x;
		size.w = (size.w - 0.5f) * scale.y;
		Texture texture = targetMat.GetTexture(ShaderProperties.MainTexture);
		if (texture == null)
		{
			return;
		}
		Vector2 vector = this.m_startSpeed + this.RandomVec(this.m_startRandomSpeed) + rotate * (this.m_localStartSpeed + this.RandomVec(this.m_localRandomStartSpeed));
		float f = euler.z * 0.017453292f;
		float c = Mathf.Cos(f);
		float s = Mathf.Sin(f);
		Vector4 vector2 = targetMat.GetVector(ShaderProperties.MainTexUSAtlas);
		int instanceID = texture.GetInstanceID();
		UberGhostTrailMeshUpdate.Quad quad = this.m_quads[num];
		quad.EmissionTime = this.TrailTime;
		quad.Vert1 = new Vector2(this.GetVertexX(size.x, size.y, c, s), this.GetVertexY(size.x, size.y, c, s));
		quad.Vert2 = new Vector2(this.GetVertexX(size.z, size.y, c, s), this.GetVertexY(size.z, size.y, c, s));
		quad.Vert3 = new Vector2(this.GetVertexX(size.z, size.w, c, s), this.GetVertexY(size.z, size.w, c, s));
		quad.Vert4 = new Vector2(this.GetVertexX(size.x, size.w, c, s), this.GetVertexY(size.x, size.w, c, s));
		vector2.z += vector2.x;
		vector2.w += vector2.y;
		quad.UvX = vector2.x;
		quad.UvY = vector2.y;
		quad.UvZ = vector2.z;
		quad.UvW = vector2.w;
		quad.Flipped = flipped;
		quad.Texture = instanceID;
		quad.X = posx;
		quad.Y = posy;
		quad.Vx = vector.x;
		quad.Vy = vector.y;
		this.m_quads[num] = quad;
		this.m_active[num] = true;
		int num3 = -1;
		for (int j = 0; j < this.m_textures.Count; j++)
		{
			if (this.m_textures[j].Tex == instanceID)
			{
				num3 = j;
			}
		}
		if (num3 == -1)
		{
			List<int> list = new List<int>();
			list.Add(num);
			this.m_textures.Add(new UberGhostTrailMeshUpdate.TextureUsage
			{
				Count = 1,
				Tex = instanceID,
				Quads = list
			});
			if (!this.m_instToTex.ContainsKey(instanceID))
			{
				UberGhostTrailMeshUpdate.TextureInfo value = new UberGhostTrailMeshUpdate.TextureInfo
				{
					Texture = targetMat.mainTexture,
					Screen = targetMat.GetVector(ShaderProperties.Screen),
					ScreenMask = targetMat.GetVector(ShaderProperties.ScreenMask)
				};
				this.m_instToTex.Add(instanceID, value);
			}
		}
		else
		{
			UberGhostTrailMeshUpdate.TextureUsage value2 = this.m_textures[num3];
			value2.Count++;
			value2.Quads.Add(num);
			this.m_textures[num3] = value2;
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0002953C File Offset: 0x0002773C
	private void UpdateQuads()
	{
		float num;
		if (Application.isPlaying)
		{
			num = Time.deltaTime;
		}
		else
		{
			num = Time.realtimeSinceStartup - this.m_lastTime;
			this.m_lastTime = Time.realtimeSinceStartup;
		}
		if (!this.IsDead)
		{
			this.m_lastRot = this.GhostTarget.transform.rotation;
		}
		Material targetMat = this.TargetMat;
		if (targetMat != null)
		{
			this.m_atlasSettings = targetMat.GetVector(ShaderProperties.MainTexUSAtlas);
		}
		Vector2 v = num * (this.m_constForce + this.m_lastRot * this.m_localConstForce);
		float trailTime = this.TrailTime;
		bool flag = false;
		bool flag2 = false;
		if (v.sqrMagnitude > 0f)
		{
			Vector2 vector = this.m_lastRot * v;
			for (int i = this.m_updateCount; i < this.m_quadCount; i += 5)
			{
				if (this.m_active[i])
				{
					if (trailTime - this.m_quads[i].EmissionTime > this.m_destroyTime)
					{
						this.RemoveQuad(i, this.m_quads[i].Texture);
						this.m_active[i] = false;
						flag2 = true;
					}
					else
					{
						UberGhostTrailMeshUpdate.Quad[] quads = this.m_quads;
						int num2 = i;
						quads[num2].X = quads[num2].X + this.m_quads[i].Vx * num;
						UberGhostTrailMeshUpdate.Quad[] quads2 = this.m_quads;
						int num3 = i;
						quads2[num3].Y = quads2[num3].Y + this.m_quads[i].Vy * num;
						UberGhostTrailMeshUpdate.Quad[] quads3 = this.m_quads;
						int num4 = i;
						quads3[num4].Vx = quads3[num4].Vx + vector.x;
						UberGhostTrailMeshUpdate.Quad[] quads4 = this.m_quads;
						int num5 = i;
						quads4[num5].Vy = quads4[num5].Vy + vector.y;
						flag = true;
					}
				}
			}
		}
		else
		{
			for (int j = this.m_updateCount; j < this.m_quadCount; j += 5)
			{
				if (this.m_active[j])
				{
					if (trailTime - this.m_quads[j].EmissionTime > this.m_destroyTime)
					{
						this.RemoveQuad(j, this.m_quads[j].Texture);
						this.m_active[j] = false;
						flag2 = true;
					}
					else
					{
						UberGhostTrailMeshUpdate.Quad[] quads5 = this.m_quads;
						int num6 = j;
						quads5[num6].X = quads5[num6].X + this.m_quads[j].Vx * num;
						UberGhostTrailMeshUpdate.Quad[] quads6 = this.m_quads;
						int num7 = j;
						quads6[num7].Y = quads6[num7].Y + this.m_quads[j].Vy * num;
						flag = true;
					}
				}
			}
		}
		if (flag2)
		{
			for (int k = this.m_quadCount - 1; k >= 0; k--)
			{
				if (this.m_active[k])
				{
					this.m_quadCount = k + 1;
					break;
				}
			}
		}
		if (!flag && this.IsDead)
		{
			if (Application.isPlaying)
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
			}
		}
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0002988C File Offset: 0x00027A8C
	private void RemoveQuad(int quad, int texture)
	{
		int index = -1;
		for (int i = 0; i < this.m_textures.Count; i++)
		{
			if (this.m_textures[i].Tex == texture)
			{
				index = i;
			}
		}
		UberGhostTrailMeshUpdate.TextureUsage value = this.m_textures[index];
		value.Count--;
		if (value.Count <= 0)
		{
			this.m_textures.RemoveAt(index);
		}
		else
		{
			value.Quads.Remove(quad);
			this.m_textures[index] = value;
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00029928 File Offset: 0x00027B28
	private float GetVertexX(float sizex, float sizey, float c, float s)
	{
		return c * sizex - s * sizey;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00029932 File Offset: 0x00027B32
	private float GetVertexY(float sizex, float sizey, float c, float s)
	{
		return s * sizex + c * sizey;
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0002993C File Offset: 0x00027B3C
	public void UpdateTrailMesh()
	{
		if (this.TrailMesh == null)
		{
			return;
		}
		if (this.m_vertices == null)
		{
			this.ReallocateBuffers();
		}
		if (this.m_textures.Count >= this.m_materialSize)
		{
			this.m_materialSize += 5;
			this.UpdateMaterialsBuf();
		}
		this.UpdateQuads();
		this.m_curTime = this.TrailTime;
		for (int i = 0; i < this.m_textures.Count; i++)
		{
			this.m_triangles[i] = this.EnsureBuffer<int>(this.m_triangles[i], this.m_textures[i].Quads.Count * 4);
		}
		this.UpdateMeshVertPart();
		this.TrailMesh.vertices = this.m_vertices;
		this.TrailMesh.uv = this.m_mainUvs;
		this.TrailMesh.colors32 = this.m_colors;
		this.TrailMesh.subMeshCount = this.m_textures.Count;
		this.TrailMesh.uv2 = this.m_uv2;
		for (int j = 0; j < this.m_textures.Count; j++)
		{
			UberGhostTrailMeshUpdate.TextureUsage textureUsage = this.m_textures[j];
			if (this.GhostTarget != null)
			{
				if (this.m_materials[j] == null)
				{
					Material material = new Material(this.GhostTarget.Renderer.sharedMaterial.shader);
					this.SetPropertiesOnMaterial(material);
					this.m_materials[j] = material;
				}
				Material material2 = this.m_materials[j];
				UberGhostTrailMeshUpdate.TextureInfo textureInfo = this.m_instToTex[textureUsage.Tex];
				material2.mainTexture = textureInfo.Texture;
				material2.SetVector(ShaderProperties.Screen, textureInfo.Screen);
				material2.SetVector(ShaderProperties.ScreenMask, textureInfo.ScreenMask);
				material2.SetVector(ShaderProperties.MainTexUSAtlas, this.m_atlasSettings);
			}
			this.TrailMesh.SetIndices(this.m_triangles[j], MeshTopology.Quads, j);
		}
		Material[] array = this.m_setMaterials[Mathf.Min(this.m_materialSize - 1, this.m_textures.Count)];
		if (array != null)
		{
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = this.m_materials[k];
			}
			this.m_renderer.sharedMaterials = array;
		}
		this.m_meshFilter.sharedMesh = this.TrailMesh;
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00029BB4 File Offset: 0x00027DB4
	private void UpdateMeshVertPart()
	{
		this.m_updateCount = (this.m_updateCount + 1) % 5;
		int num = 0;
		int quadCount = this.m_quadCount;
		Color32 col = this.m_col;
		float num2 = (float)col.a;
		for (int i = num + this.m_updateCount; i < quadCount; i += 5)
		{
			int num3 = i * 4;
			if (this.m_active[i])
			{
				UberGhostTrailMeshUpdate.Quad quad = this.m_quads[i];
				float time = this.m_curTime - quad.EmissionTime;
				float num4 = this.m_scaleCurve.Evaluate(time);
				int num5 = i * 4;
				col.a = (byte)(num2 * this.m_alphaCurve.Evaluate(time));
				this.m_vertices[num3].x = quad.X + quad.Vert1.x * num4;
				this.m_vertices[num3].y = quad.Y + quad.Vert1.y * num4;
				this.m_colors[num3] = col;
				num3++;
				this.m_vertices[num3].x = quad.X + quad.Vert2.x * num4;
				this.m_vertices[num3].y = quad.Y + quad.Vert2.y * num4;
				this.m_colors[num3] = col;
				num3++;
				this.m_vertices[num3].x = quad.X + quad.Vert3.x * num4;
				this.m_vertices[num3].y = quad.Y + quad.Vert3.y * num4;
				this.m_colors[num3] = col;
				num3++;
				this.m_vertices[num3].x = quad.X + quad.Vert4.x * num4;
				this.m_vertices[num3].y = quad.Y + quad.Vert4.y * num4;
				this.m_colors[num3] = col;
				float uvX = quad.UvX;
				float uvY = quad.UvY;
				float uvZ = quad.UvZ;
				float uvW = quad.UvW;
				this.m_uv2[num5].x = uvX;
				this.m_uv2[num5].y = uvY;
				this.m_uv2[num5 + 2].x = uvZ;
				this.m_uv2[num5 + 2].y = uvW;
				if (!quad.Flipped)
				{
					this.m_uv2[num5 + 1].x = uvZ;
					this.m_uv2[num5 + 1].y = uvY;
					this.m_uv2[num5 + 3].x = uvX;
					this.m_uv2[num5 + 3].y = uvW;
				}
				else
				{
					this.m_uv2[num5 + 1].x = uvX;
					this.m_uv2[num5 + 1].y = uvW;
					this.m_uv2[num5 + 3].x = uvZ;
					this.m_uv2[num5 + 3].y = uvY;
				}
			}
		}
		for (int j = 0; j < this.m_textures.Count; j++)
		{
			UberGhostTrailMeshUpdate.TextureUsage textureUsage = this.m_textures[j];
			int num6 = 0;
			int[] array = this.m_triangles[j];
			int count = textureUsage.Quads.Count;
			for (int k = 0; k < count; k++)
			{
				int num7 = textureUsage.Quads[k] * 4;
				array[num6++] = num7;
				array[num6++] = num7 + 1;
				array[num6++] = num7 + 2;
				array[num6++] = num7 + 3;
			}
			Array.Clear(array, num6, array.Length - num6);
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0002A00C File Offset: 0x0002820C
	private void SetPropertiesOnMaterial(Material newMat)
	{
		if (this.GhostTarget != null)
		{
			Material sharedMaterial = this.GhostTarget.Renderer.sharedMaterial;
			newMat.CopyPropertiesFromMaterial(sharedMaterial);
			newMat.renderQueue = sharedMaterial.renderQueue + 2;
			Vector4 vector = newMat.GetVector(ShaderProperties.DepthFlipScreen);
			vector.z = 0f;
			newMat.SetVector(ShaderProperties.DepthFlipScreen, vector);
		}
	}

	// Token: 0x040007A3 RID: 1955
	private const int c_startSize = 90;

	// Token: 0x040007A4 RID: 1956
	private const int c_growSize = 90;

	// Token: 0x040007A5 RID: 1957
	private const int c_materialStartSize = 15;

	// Token: 0x040007A6 RID: 1958
	private const int c_mathSkip = 5;

	// Token: 0x040007A7 RID: 1959
	private Renderer m_targetRenderer;

	// Token: 0x040007A8 RID: 1960
	private GameObject m_animatorTarget;

	// Token: 0x040007A9 RID: 1961
	private Vector3[] m_vertices;

	// Token: 0x040007AA RID: 1962
	private Vector2[] m_uv2;

	// Token: 0x040007AB RID: 1963
	private Vector2[] m_mainUvs;

	// Token: 0x040007AC RID: 1964
	private Color32[] m_colors;

	// Token: 0x040007AD RID: 1965
	private int[][] m_triangles;

	// Token: 0x040007AE RID: 1966
	private Material[] m_materials;

	// Token: 0x040007AF RID: 1967
	private Material[][] m_setMaterials;

	// Token: 0x040007B0 RID: 1968
	private UberGhostTrailMeshUpdate.Quad[] m_quads;

	// Token: 0x040007B1 RID: 1969
	private bool[] m_active;

	// Token: 0x040007B2 RID: 1970
	private List<UberGhostTrailMeshUpdate.TextureUsage> m_textures = new List<UberGhostTrailMeshUpdate.TextureUsage>();

	// Token: 0x040007B3 RID: 1971
	private Dictionary<int, UberGhostTrailMeshUpdate.TextureInfo> m_instToTex = new Dictionary<int, UberGhostTrailMeshUpdate.TextureInfo>();

	// Token: 0x040007B4 RID: 1972
	private Mesh m_trailMesh;

	// Token: 0x040007B5 RID: 1973
	private float m_lastTime;

	// Token: 0x040007B6 RID: 1974
	private AnimationCurve m_scaleCurve;

	// Token: 0x040007B7 RID: 1975
	private AnimationCurve m_alphaCurve;

	// Token: 0x040007B8 RID: 1976
	private Color32 m_col;

	// Token: 0x040007B9 RID: 1977
	private Vector2 m_constForce;

	// Token: 0x040007BA RID: 1978
	private Vector2 m_localConstForce;

	// Token: 0x040007BB RID: 1979
	private Vector2 m_startSpeed;

	// Token: 0x040007BC RID: 1980
	private Vector2 m_startRandomSpeed;

	// Token: 0x040007BD RID: 1981
	private Vector2 m_localStartSpeed;

	// Token: 0x040007BE RID: 1982
	private Vector2 m_localRandomStartSpeed;

	// Token: 0x040007BF RID: 1983
	private Quaternion m_lastRot;

	// Token: 0x040007C0 RID: 1984
	private Vector4 m_atlasSettings;

	// Token: 0x040007C1 RID: 1985
	private int m_currentSize = 90;

	// Token: 0x040007C2 RID: 1986
	private int m_materialSize = 15;

	// Token: 0x040007C3 RID: 1987
	[NonSerialized]
	public UberGhostTrail GhostTarget;

	// Token: 0x040007C4 RID: 1988
	private Renderer m_renderer;

	// Token: 0x040007C5 RID: 1989
	private MeshFilter m_meshFilter;

	// Token: 0x040007C6 RID: 1990
	private float m_destroyTime;

	// Token: 0x040007C7 RID: 1991
	private bool m_targetIsSpriteAnim;

	// Token: 0x040007C8 RID: 1992
	private int m_updateCount;

	// Token: 0x040007C9 RID: 1993
	private float m_lastZ;

	// Token: 0x040007CA RID: 1994
	private int m_quadCount;

	// Token: 0x040007CB RID: 1995
	private int m_lastIndex;

	// Token: 0x040007CC RID: 1996
	private float m_curTime;

	// Token: 0x040007CD RID: 1997
	private int m_posFrame;

	// Token: 0x020000EE RID: 238
	private struct TextureUsage
	{
		// Token: 0x040007D0 RID: 2000
		public int Tex;

		// Token: 0x040007D1 RID: 2001
		public int Count;

		// Token: 0x040007D2 RID: 2002
		public List<int> Quads;
	}

	// Token: 0x020000EF RID: 239
	private struct TextureInfo
	{
		// Token: 0x040007D3 RID: 2003
		public Texture Texture;

		// Token: 0x040007D4 RID: 2004
		public Vector4 Screen;

		// Token: 0x040007D5 RID: 2005
		public Vector4 ScreenMask;
	}

	// Token: 0x020000F0 RID: 240
	private struct Quad
	{
		// Token: 0x040007D6 RID: 2006
		public float X;

		// Token: 0x040007D7 RID: 2007
		public float Y;

		// Token: 0x040007D8 RID: 2008
		public float Vx;

		// Token: 0x040007D9 RID: 2009
		public float Vy;

		// Token: 0x040007DA RID: 2010
		public Vector2 Vert1;

		// Token: 0x040007DB RID: 2011
		public Vector2 Vert2;

		// Token: 0x040007DC RID: 2012
		public Vector2 Vert3;

		// Token: 0x040007DD RID: 2013
		public Vector2 Vert4;

		// Token: 0x040007DE RID: 2014
		public float UvX;

		// Token: 0x040007DF RID: 2015
		public float UvY;

		// Token: 0x040007E0 RID: 2016
		public float UvZ;

		// Token: 0x040007E1 RID: 2017
		public float UvW;

		// Token: 0x040007E2 RID: 2018
		public bool Flipped;

		// Token: 0x040007E3 RID: 2019
		public float EmissionTime;

		// Token: 0x040007E4 RID: 2020
		public int Texture;
	}
}
