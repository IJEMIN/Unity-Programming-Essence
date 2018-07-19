/****************************************
	Simple Mesh Combine
	Copyright Unluck Software	
 	www.chemicalbliss.com 																																													
*****************************************/
//Add script to the parent gameObject, then click combine

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("Simple Mesh Combine")]

public class SimpleMeshCombine:MonoBehaviour{
    public GameObject[] combinedGameOjects;		//Stores gameObjects that has been merged, mesh renderer disabled
    public GameObject combined;					//Stores the combined mesh gameObject
    public string meshName = "Combined_Meshes";	//Asset name when saving as prefab
    public bool _canGenerateLightmapUV;
    public int vCount;
    public bool generateLightmapUV;
    
    public GameObject copyTarget;
    public bool destroyOldColliders;
    public bool keepStructure = true;
    
    public void EnableRenderers(bool e) {	
    	for(int i = 0; i < combinedGameOjects.Length; i++){
    		if(combinedGameOjects[i] == null) break;
    		Renderer renderer = combinedGameOjects[i].GetComponent<Renderer>();
        	if(renderer != null) renderer.enabled = e;
    	}  
    }
    //Returns a meshFilter[] list of all renderer enabled meshfilters(so that it does not merge disabled meshes, useful when there are invisible box colliders)
    public MeshFilter[] FindEnabledMeshes(){
    	MeshFilter[] renderers = null;
    	int count = 0;
    	renderers = transform.GetComponentsInChildren<MeshFilter>();
    	//count all the enabled meshrenderers in children		
    	for(int i = 0; i < renderers.Length; i++)
    	{
    		if((renderers[i].GetComponent<MeshRenderer>() != null) && renderers[i].GetComponent<MeshRenderer>().enabled)
    			count++;
    	}
    	MeshFilter[] meshfilters = new MeshFilter[count];//creates a new array with the correct length
    	count = 0;
    	//adds all enabled meshes to the array
    	for(int ii = 0; ii < renderers.Length; ii++)
    	{
    		if((renderers[ii].GetComponent<MeshRenderer>() != null) && renderers[ii].GetComponent<MeshRenderer>().enabled){
    			meshfilters[count] = renderers[ii];
    			count++;
    		}
    	}
    	return meshfilters;
    }
    
    public void CombineMeshes() {	
    	GameObject combo = new GameObject();
    	combo.name = "_Combined Mesh [" + name + "]";
    	combo.gameObject.AddComponent<MeshFilter>();
    	combo.gameObject.AddComponent<MeshRenderer>();
    	MeshFilter[] meshFilters = null;
    	meshFilters = FindEnabledMeshes();
    	ArrayList materials = new ArrayList();
    	ArrayList combineInstanceArrays = new ArrayList();
    	combinedGameOjects = new GameObject[meshFilters.Length];	
    	for(int i = 0; i < meshFilters.Length; i++) {
    		MeshFilter[] meshFilterss = meshFilters[i].GetComponentsInChildren<MeshFilter>();	
    		combinedGameOjects[i] = meshFilters[i].gameObject;	
    		foreach(MeshFilter meshFilter in meshFilterss) {
    			MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();
    			meshFilters[i].transform.gameObject.GetComponent<Renderer>().enabled = false;
    			if(meshFilters[i].sharedMesh == null){
    					Debug.LogWarning("SimpleMeshCombine : " + meshFilter.gameObject + " [Mesh Filter] has no [Mesh], mesh will not be included in combine..");
    					break;
    				}
    			for(int o = 0; o < meshFilter.sharedMesh.subMeshCount; o++) {
    				if(meshRenderer == null){
    					Debug.LogWarning("SimpleMeshCombine : " + meshFilter.gameObject + "has a [Mesh Filter] but no [Mesh Renderer], mesh will not be included in combine.");
    					break;
    				}
    				if(o < meshRenderer.sharedMaterials.Length && o < meshFilter.sharedMesh.subMeshCount){
    					int materialArrayIndex = Contains(materials, meshRenderer.sharedMaterials[o]);
    					if (materialArrayIndex == -1) {
    						materials.Add(meshRenderer.sharedMaterials[o]);
    						materialArrayIndex = materials.Count - 1;
    					}
    					combineInstanceArrays.Add(new ArrayList());
    					CombineInstance combineInstance = new CombineInstance();
    					combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
    					combineInstance.subMeshIndex = o;
    					combineInstance.mesh = meshFilter.sharedMesh;
    					(combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
    				}
    				#if UNITY_EDITOR
    				else{
    					Debug.LogWarning("Simple Mesh Combine: GameObject [ " +meshRenderer.gameObject.name + " ] is missing a material (Mesh or sub-mesh ignored from combine)");
    				}
    				#endif
    			}
    
    		}
    		#if UNITY_EDITOR      
    		EditorUtility.DisplayProgressBar("Combining", "", (float)i);	
    		#endif
    	}
    	
    	Mesh[] meshes = new Mesh[materials.Count];
    	CombineInstance[] combineInstances = new CombineInstance[materials.Count];
    	for(int m = 0; m < materials.Count; m++) {
    		CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
    		meshes[m] = new Mesh();
    		meshes[m].CombineMeshes(combineInstanceArray, true, true);
    		combineInstances[m] = new CombineInstance();
    		combineInstances[m].mesh = meshes[m];
    		combineInstances[m].subMeshIndex = 0;
    	}		
    	combo.GetComponent<MeshFilter>().sharedMesh = new Mesh();
    	combo.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combineInstances, false, false);
    	foreach(Mesh mesh in meshes) {
    		mesh.Clear();
    		DestroyImmediate(mesh);
    	}
    	MeshRenderer meshRendererCombine = combo.GetComponent<MeshFilter>().GetComponent<MeshRenderer>();
    	if (meshRendererCombine == null) meshRendererCombine = gameObject.AddComponent<MeshRenderer>();
    	Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
    	meshRendererCombine.materials = materialsArray;	
    	combined = combo.gameObject;
    	EnableRenderers(false);
    	combo.transform.parent = transform;
    	#if UNITY_EDITOR
    	if(generateLightmapUV){
    		Unwrapping.GenerateSecondaryUVSet(combo.GetComponent<MeshFilter>().sharedMesh);
    	}
    	#endif
    	vCount = combo.GetComponent<MeshFilter>().sharedMesh.vertexCount;
    	if(vCount > 65536) { 
    		Debug.LogWarning("Vertex Count: " +vCount + "- Vertex Count too high, please divide mesh combine into more groups. Max 65536 for each mesh" );
    		_canGenerateLightmapUV = false;
    	}else{
    		_canGenerateLightmapUV = true;
    	}
    	#if UNITY_EDITOR
    	EditorUtility.ClearProgressBar();
    	#endif
    }
    
    public int Contains(ArrayList l,Material n) {
    	for(int i = 0; i < l.Count; i++) {
    		if ((l[i] as Material) == n) {
    			return i;
    		}
    	}
    	return -1;
    }
}