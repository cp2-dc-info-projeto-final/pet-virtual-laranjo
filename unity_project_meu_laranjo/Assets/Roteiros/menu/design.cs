using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class design : MonoBehaviour
{
    public GameObject[] partes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void mudaroupa(item item_){
    
    CopyComponent(item_.prefab.GetComponent<SkinnedMeshRenderer>(),partes[item_.posicao]);
    
}

T CopyComponent<T>(T original, GameObject destination) where T : Component
{
    System.Type type = original.GetType();
    var dst = destination.GetComponent(type) as T;
    if (!dst) dst = destination.AddComponent(type) as T;
    var fields = type.GetFields();
    foreach (var field in fields)
    {
        if (field.IsStatic) continue;
        field.SetValue(dst, field.GetValue(original));
    }
    var props = type.GetProperties();
    foreach (var prop in props)
    {
        if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
        prop.SetValue(dst, prop.GetValue(original, null), null);
    }
        return dst as T;
}

public void MudarMesh(item item_)
{

    if(item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMesh != null){
        Mesh meshInstance = Instantiate(item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMesh) as Mesh;
        partes[item_.posicao].GetComponent<SkinnedMeshRenderer>().sharedMesh = meshInstance;
    }else
    {
        partes[item_.posicao].GetComponent<SkinnedMeshRenderer>().sharedMesh = null;

    }
    partes[item_.posicao].GetComponent<SkinnedMeshRenderer>().sharedMaterials = item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
}

}
