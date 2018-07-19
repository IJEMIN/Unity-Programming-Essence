using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleMeshCombineMaster : MonoBehaviour {
	public bool generateLightmapUV;
}

#if UNITY_EDITOR
[CustomEditor(typeof(SimpleMeshCombineMaster))]
public class SimpleMeshCombineMasterEditor : Editor {
	SimpleMeshCombineMaster masterTarget;

	void AttachCombineScriptToChildren() {
		for (int i = 0; i < masterTarget.transform.childCount; i++) {
			Transform t = masterTarget.transform.GetChild(i);
			SimpleMeshCombine smc = t.GetComponent<SimpleMeshCombine>();
			if (!smc) {
				t.gameObject.AddComponent<SimpleMeshCombine>();
			}
		}
	}

	void CombineAll(bool combine, SimpleMeshCombine[] arr) {
		for (int i = 0; i < arr.Length; i++) {
			SimpleMeshCombine smc = arr[i];
			if (combine && !smc.combined) {
				if (masterTarget.generateLightmapUV) smc.generateLightmapUV = true;
				smc.CombineMeshes();
			} else if (!combine && smc.combined) {
				smc.EnableRenderers(true);
				if (smc.combined != null) DestroyImmediate(smc.combined);
				smc.combinedGameOjects = null;
			}
		}
	}

	void OnEnable() {
		masterTarget = target as SimpleMeshCombineMaster;
	}

	public override void OnInspectorGUI() {
		if (GUILayout.Button("Attach SMC to Children")) AttachCombineScriptToChildren();
		GUILayout.Space(25);
		masterTarget.generateLightmapUV = EditorGUILayout.Toggle("Create Lightmap UV", masterTarget.generateLightmapUV);
		if (GUILayout.Button("Combine All Children"))
			CombineAll(true, masterTarget.transform.GetComponentsInChildren<SimpleMeshCombine>());
		if (GUILayout.Button("Release All Children"))
			CombineAll(false, masterTarget.transform.GetComponentsInChildren<SimpleMeshCombine>());
		GUILayout.Space(25);
		if (GUILayout.Button("Combine All in Scene"))
			CombineAll(true, FindObjectsOfType<SimpleMeshCombine>());
		if (GUILayout.Button("Release All in Scene"))
			CombineAll(false, FindObjectsOfType<SimpleMeshCombine>());
	}
}
#endif