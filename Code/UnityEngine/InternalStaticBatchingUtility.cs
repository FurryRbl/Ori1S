using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	// Token: 0x02000334 RID: 820
	internal class InternalStaticBatchingUtility
	{
		// Token: 0x0600283E RID: 10302 RVA: 0x00039978 File Offset: 0x00037B78
		public static void CombineRoot(GameObject staticBatchRoot)
		{
			InternalStaticBatchingUtility.Combine(staticBatchRoot, false, false);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00039984 File Offset: 0x00037B84
		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic, bool isEditorPostprocessScene)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			foreach (GameObject gameObject in array)
			{
				if (!(staticBatchRoot != null) || gameObject.transform.IsChildOf(staticBatchRoot.transform))
				{
					if (!combineOnlyStatic || gameObject.isStaticBatchable)
					{
						list.Add(gameObject);
					}
				}
			}
			array = list.ToArray();
			InternalStaticBatchingUtility.CombineGameObjects(array, staticBatchRoot, isEditorPostprocessScene);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00039A20 File Offset: 0x00037C20
		public static void CombineGameObjects(GameObject[] gos, GameObject staticBatchRoot, bool isEditorPostprocessScene)
		{
			Matrix4x4 lhs = Matrix4x4.identity;
			Transform staticBatchRootTransform = null;
			if (staticBatchRoot)
			{
				lhs = staticBatchRoot.transform.worldToLocalMatrix;
				staticBatchRootTransform = staticBatchRoot.transform;
			}
			int batchIndex = 0;
			int num = 0;
			List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
			List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
			List<GameObject> list3 = new List<GameObject>();
			Array.Sort(gos, new InternalStaticBatchingUtility.SortGO());
			foreach (GameObject gameObject in gos)
			{
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (!(meshFilter == null))
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					if (!(sharedMesh == null) && (isEditorPostprocessScene || sharedMesh.canAccess))
					{
						Renderer component = meshFilter.GetComponent<Renderer>();
						if (!(component == null) && component.enabled)
						{
							if (component.staticBatchIndex == 0)
							{
								Material[] array = meshFilter.GetComponent<Renderer>().sharedMaterials;
								if (!array.Any((Material m) => m != null && m.shader != null && m.shader.disableBatching != DisableBatchingType.False))
								{
									if (num + meshFilter.sharedMesh.vertexCount > 64000)
									{
										InternalStaticBatchingUtility.MakeBatch(list, list2, list3, staticBatchRootTransform, batchIndex++);
										list.Clear();
										list2.Clear();
										list3.Clear();
										num = 0;
									}
									MeshSubsetCombineUtility.MeshInstance item = default(MeshSubsetCombineUtility.MeshInstance);
									item.meshInstanceID = sharedMesh.GetInstanceID();
									item.rendererInstanceID = component.GetInstanceID();
									MeshRenderer meshRenderer = component as MeshRenderer;
									if (meshRenderer != null && meshRenderer.additionalVertexStreams != null)
									{
										item.additionalVertexStreamsMeshInstanceID = meshRenderer.additionalVertexStreams.GetInstanceID();
									}
									item.transform = lhs * meshFilter.transform.localToWorldMatrix;
									item.lightmapScaleOffset = component.lightmapScaleOffset;
									item.realtimeLightmapScaleOffset = component.realtimeLightmapScaleOffset;
									list.Add(item);
									if (array.Length > sharedMesh.subMeshCount)
									{
										Debug.LogWarning(string.Concat(new object[]
										{
											"Mesh has more materials (",
											array.Length,
											") than subsets (",
											sharedMesh.subMeshCount,
											")"
										}), meshFilter.GetComponent<Renderer>());
										Material[] array2 = new Material[sharedMesh.subMeshCount];
										for (int j = 0; j < sharedMesh.subMeshCount; j++)
										{
											array2[j] = meshFilter.GetComponent<Renderer>().sharedMaterials[j];
										}
										meshFilter.GetComponent<Renderer>().sharedMaterials = array2;
										array = array2;
									}
									for (int k = 0; k < Math.Min(array.Length, sharedMesh.subMeshCount); k++)
									{
										list2.Add(new MeshSubsetCombineUtility.SubMeshInstance
										{
											meshInstanceID = meshFilter.sharedMesh.GetInstanceID(),
											vertexOffset = num,
											subMeshIndex = k,
											gameObjectInstanceID = gameObject.GetInstanceID(),
											transform = item.transform
										});
										list3.Add(gameObject);
									}
									num += sharedMesh.vertexCount;
								}
							}
						}
					}
				}
			}
			InternalStaticBatchingUtility.MakeBatch(list, list2, list3, staticBatchRootTransform, batchIndex);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x00039D84 File Offset: 0x00037F84
		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshInstance> meshes, List<MeshSubsetCombineUtility.SubMeshInstance> subsets, List<GameObject> subsetGOs, Transform staticBatchRootTransform, int batchIndex)
		{
			if (meshes.Count < 2)
			{
				return;
			}
			MeshSubsetCombineUtility.MeshInstance[] meshes2 = meshes.ToArray();
			MeshSubsetCombineUtility.SubMeshInstance[] array = subsets.ToArray();
			string text = "Combined Mesh";
			text = text + " (root: " + ((!(staticBatchRootTransform != null)) ? "scene" : staticBatchRootTransform.name) + ")";
			if (batchIndex > 0)
			{
				text = text + " " + (batchIndex + 1);
			}
			Mesh mesh = StaticBatchingUtility.InternalCombineVertices(meshes2, text);
			StaticBatchingUtility.InternalCombineIndices(array, mesh);
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				MeshSubsetCombineUtility.SubMeshInstance subMeshInstance = array[i];
				GameObject gameObject = subsetGOs[i];
				Mesh sharedMesh = mesh;
				MeshFilter meshFilter = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
				meshFilter.sharedMesh = sharedMesh;
				Renderer component = gameObject.GetComponent<Renderer>();
				component.SetSubsetIndex(subMeshInstance.subMeshIndex, num);
				component.staticBatchRootTransform = staticBatchRootTransform;
				component.enabled = false;
				component.enabled = true;
				MeshRenderer meshRenderer = component as MeshRenderer;
				if (meshRenderer != null)
				{
					meshRenderer.additionalVertexStreams = null;
				}
				num++;
			}
		}

		// Token: 0x04000C59 RID: 3161
		private const int MaxVerticesInBatch = 64000;

		// Token: 0x04000C5A RID: 3162
		private const string CombinedMeshPrefix = "Combined Mesh";

		// Token: 0x02000335 RID: 821
		internal class SortGO : IComparer
		{
			// Token: 0x06002844 RID: 10308 RVA: 0x00039F00 File Offset: 0x00038100
			int IComparer.Compare(object a, object b)
			{
				if (a == b)
				{
					return 0;
				}
				Renderer renderer = InternalStaticBatchingUtility.SortGO.GetRenderer(a as GameObject);
				Renderer renderer2 = InternalStaticBatchingUtility.SortGO.GetRenderer(b as GameObject);
				int num = InternalStaticBatchingUtility.SortGO.GetMaterialId(renderer).CompareTo(InternalStaticBatchingUtility.SortGO.GetMaterialId(renderer2));
				if (num == 0)
				{
					num = InternalStaticBatchingUtility.SortGO.GetLightmapIndex(renderer).CompareTo(InternalStaticBatchingUtility.SortGO.GetLightmapIndex(renderer2));
				}
				return num;
			}

			// Token: 0x06002845 RID: 10309 RVA: 0x00039F60 File Offset: 0x00038160
			private static int GetMaterialId(Renderer renderer)
			{
				if (renderer == null || renderer.sharedMaterial == null)
				{
					return 0;
				}
				return renderer.sharedMaterial.GetInstanceID();
			}

			// Token: 0x06002846 RID: 10310 RVA: 0x00039F98 File Offset: 0x00038198
			private static int GetLightmapIndex(Renderer renderer)
			{
				if (renderer == null)
				{
					return -1;
				}
				return renderer.lightmapIndex;
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x00039FB0 File Offset: 0x000381B0
			private static Renderer GetRenderer(GameObject go)
			{
				if (go == null)
				{
					return null;
				}
				MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (meshFilter == null)
				{
					return null;
				}
				return meshFilter.GetComponent<Renderer>();
			}
		}
	}
}
