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
    
    CopyComponent(item_.prefab.GetComponent<SkinnedMeshRenderer>(),partes[(int)item_.posicao - 1]);
    
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

public void MudarMesh(item item_, float nivel_)
{

    if(item_.prefab != null){
        if((int)item_.posicao != 11){
            Mesh meshInstance = Instantiate(item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMesh) as Mesh;
            partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMesh = meshInstance;

            partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMaterials = item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
        }else
        {
            Mesh meshInstance = Instantiate(item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMesh) as Mesh;
            partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMesh = meshInstance;

            Material[] mats_ = item_.prefab.GetComponent<SkinnedMeshRenderer>().sharedMaterials;

            item_skin skin_ = item_ as item_skin;

            mats_[3] = skin_.materialResultado(nivel_);

            partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMaterials = mats_;
        }
        
    }else
    {
        //Debug.Log("---------item: " + item_.id + ", posicao: " + ((int)item_.posicao-1));
        partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMesh = null;
        partes[(int)item_.posicao-1].GetComponent<SkinnedMeshRenderer>().sharedMaterials = new Material[]{};

    }
    
}

}
