using UnityEngine;

public class RunTimeCombineAndRelease:MonoBehaviour{
    
    public SimpleMeshCombine simpleMeshCombine;
    
    public float combineTime = 0.5f;
    public float releaseTime = 2.0f;
    
    public void Awake(){
    	simpleMeshCombine = GetComponent<SimpleMeshCombine>();
    	//simpleMeshCombine.CombineMeshes();
    }
    
    public void Start() {
    	
    	if(simpleMeshCombine == null){
    		Debug.Log("Couldn't find SMC, aborting");
    		return;
    	}
    	Invoke("Combine", combineTime);
    	Invoke("Release", releaseTime);
    }
    
    public void Combine() {
    	simpleMeshCombine.CombineMeshes();
    	Debug.Log("Combined");
    }
    
    public void Release() {
    	simpleMeshCombine.EnableRenderers(true);
    	if(simpleMeshCombine.combined == null) return;
    	Destroy(simpleMeshCombine.combined);
    	simpleMeshCombine.combinedGameOjects = null;
    	Debug.Log("Released");
    }
}