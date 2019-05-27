#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class PrefabLightmapData : MonoBehaviour
{
    [System.Serializable]
    struct RendererInfo
    {
        public List<Renderer> renderer;
        public int lightmapIndex;
        public Vector4 lightmapOffsetScale;
    }
    [SerializeField]
    List<RendererInfo> m_RendererInfo;
    [SerializeField]
    Texture2D[] m_Lightmaps;
    [SerializeField]
    Texture2D[] m_Lightmaps2;
    [SerializeField]
  public  static string LIGHTMAP_RESOURCE_PATH = "Assets/Resources/Lightmaps/";
    [System.Serializable]
    struct Texture2D_Remap
    {
        public int originalLightmapIndex;
        public Texture2D originalLightmap;
        public Texture2D lightmap;
        public Texture2D lightmap2;
    }
    static List<Texture2D_Remap> sceneLightmaps = new List<Texture2D_Remap>();
    void Awake()
    {
        ApplyLightmaps(m_RendererInfo, m_Lightmaps, m_Lightmaps2);
    }
    // Use this for initialization
    void Start()
    {

        StaticBatchingUtility.Combine(this.gameObject);
    }
    static void ApplyLightmaps(List<RendererInfo> rendererInfo, Texture2D[] lightmaps, Texture2D[] lightmaps2)
    {
        bool existsAlready = false;
        int counter = 0;
        int[] lightmapArrayOffsetIndex;
        if (rendererInfo == null || rendererInfo.Count == 0)
            return;
        var settingslightmaps = LightmapSettings.lightmaps;
        var combinedLightmaps = new List<LightmapData>();
        lightmapArrayOffsetIndex = new int[lightmaps.Length];
        for (int i = 0; i < lightmaps.Length; i++)
        {
            existsAlready = false;
            for (int j = 0; j < settingslightmaps.Length; j++)
            {
                if (lightmaps[i] == settingslightmaps[j].lightmapColor)
                {
                    lightmapArrayOffsetIndex[i] = j;
                    existsAlready = true;
                }
            }
            if (!existsAlready)
            {
                lightmapArrayOffsetIndex[i] = counter + settingslightmaps.Length;
                var newLightmapData = new LightmapData();
                newLightmapData.lightmapColor = lightmaps[i];
                newLightmapData.lightmapDir = lightmaps2[i];
                combinedLightmaps.Add(newLightmapData);
                ++counter;
            }
        }
        var combinedLightmaps2 = new LightmapData[settingslightmaps.Length + combinedLightmaps.Count];
        settingslightmaps.CopyTo(combinedLightmaps2, 0);
        if (counter > 0)
        {
            for (int i = 0; i < combinedLightmaps.Count; i++)
            {
                combinedLightmaps2[i + settingslightmaps.Length] = new LightmapData();
                combinedLightmaps2[i + settingslightmaps.Length].lightmapColor = combinedLightmaps[i].lightmapColor;
                combinedLightmaps2[i + settingslightmaps.Length].lightmapDir = combinedLightmaps[i].lightmapDir;
            }
        }
        ApplyRendererInfo(rendererInfo, lightmapArrayOffsetIndex);
        LightmapSettings.lightmaps = combinedLightmaps2;
    }
    static void ApplyRendererInfo(List<RendererInfo> infos, int[] arrayOffsetIndex)
    {
        for (int i = 0; i < infos.Count; i++)
        {
            var info = infos[i];
            foreach (Renderer rer in info.renderer)
            {
                rer.lightmapIndex = arrayOffsetIndex[info.lightmapIndex];
                rer.lightmapScaleOffset = info.lightmapOffsetScale;
            }
        }
    }
#if UNITY_EDITOR
   // [MenuItem("Assets/Assign lightmaps to LOD")]
   public static void UseMeBeforeUpdatint(string lightMapFix = "LOD")
    {
        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();
        foreach (var instance in prefabs)
        {
            List<RendererInfo> tempListOfRender = instance.m_RendererInfo;
            foreach (RendererInfo ren in tempListOfRender)
            {
                foreach (Renderer re in ren.renderer.ToList())
                {
                    Transform myParentForSib = re.transform.parent;
                    foreach (var chil in myParentForSib.GetComponentsInChildren<Transform>())
                    {
                        if (chil != ren.renderer[0].transform)
                        {
                            if (chil.gameObject.name.Contains(lightMapFix))
                            {
                                ren.renderer.Add(chil.GetComponent<MeshRenderer>());
                            }
                        }
                    }
                }
            }
        }
    }
  //  [MenuItem("Assets/Update Scene with Prefab Lightmaps")]
  public  static void UpdateLightmaps()
    {
        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();
        foreach (var instance in prefabs)
        {
            ApplyLightmaps(instance.m_RendererInfo, instance.m_Lightmaps, instance.m_Lightmaps2);
        }
        Debug.Log("Prefab lightmaps updated");
    }
 //   [MenuItem("Assets/Bake Prefab Lightmaps")]
  public  static void GenerateLightmapInfo(string folderPath,TextureImporterFormat textureFormat,int textureMaxSize)
    {
        LIGHTMAP_RESOURCE_PATH = folderPath;
        tf = textureFormat;
        sizeOfTexture = textureMaxSize;

        Debug.ClearDeveloperConsole();
        if (Lightmapping.giWorkflowMode != Lightmapping.GIWorkflowMode.OnDemand)
        {
            Debug.LogError("ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.");
            return;
        }
        Lightmapping.Bake();
        sceneLightmaps = new List<Texture2D_Remap>();
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        

        var resourcePath = LIGHTMAP_RESOURCE_PATH + scene.name;
        var scenePath = System.IO.Path.GetDirectoryName(scene.path) + "/" + scene.name + "/";
        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();
        foreach (var instance in prefabs)
        {
            var gameObject = instance.gameObject;
            var rendererInfos = new List<RendererInfo>();
            var lightmaps = new List<Texture2D>();
            var lightmaps2 = new List<Texture2D>();
            GenerateLightmapInfo(scenePath, resourcePath, gameObject, rendererInfos, lightmaps, lightmaps2);
            instance.m_RendererInfo = rendererInfos;
            instance.m_Lightmaps = lightmaps.ToArray();
            instance.m_Lightmaps2 = lightmaps2.ToArray();
            var targetPrefab = PrefabUtility.GetPrefabParent(gameObject) as GameObject;
            if (targetPrefab != null)
            {
                //Prefab
                PrefabUtility.ReplacePrefab(gameObject, targetPrefab);
            }
            ApplyLightmaps(instance.m_RendererInfo, instance.m_Lightmaps, instance.m_Lightmaps2);
        }
        Debug.Log("Update to prefab lightmaps finished");
    }
    static void GenerateLightmapInfo(string scenePath, string resourcePath, GameObject root, List<RendererInfo> rendererInfos, List<Texture2D> lightmaps, List<Texture2D> lightmaps2)
    {
        var renderers = root.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.lightmapIndex != -1 && renderer.enabled)
            {
                RendererInfo info = new RendererInfo();
                info.renderer = new List<Renderer>();
                info.renderer.Add(renderer);
                info.lightmapOffsetScale = renderer.lightmapScaleOffset;
                Texture2D lightmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapColor;
                Texture2D lightmap2 = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapDir;
                int sceneLightmapIndex = AddLightmap(scenePath, resourcePath, renderer.lightmapIndex, lightmap, lightmap2);
                info.lightmapIndex = lightmaps.IndexOf(sceneLightmaps[sceneLightmapIndex].lightmap);
                if (info.lightmapIndex == -1)
                {
                    info.lightmapIndex = lightmaps.Count;
                    lightmaps.Add(sceneLightmaps[sceneLightmapIndex].lightmap);
                    lightmaps2.Add(sceneLightmaps[sceneLightmapIndex].lightmap2);
                }
                rendererInfos.Add(info);
            }
        }
    }
    static int AddLightmap(string scenePath, string resourcePath, int originalLightmapIndex, Texture2D lightmap, Texture2D lightmap2)
    {
        int newIndex = -1;
        for (int i = 0; i < sceneLightmaps.Count; i++)
        {
            if (sceneLightmaps[i].originalLightmapIndex == originalLightmapIndex)
            {
                return i;
            }
        }
        if (newIndex == -1)
        {
            var lightmap_Remap = new Texture2D_Remap();
            lightmap_Remap.originalLightmapIndex = originalLightmapIndex;
            lightmap_Remap.originalLightmap = lightmap;
            var filename = scenePath + "Lightmap-" + originalLightmapIndex;
            lightmap_Remap.lightmap = GetLightmapAsset(filename + "_comp_light.exr", resourcePath + "_light", originalLightmapIndex, lightmap);
            if (lightmap2 != null)
            {
                lightmap_Remap.lightmap2 = GetLightmapAsset(filename + "_comp_dir.exr", resourcePath + "_dir", originalLightmapIndex, lightmap2);
            }
            sceneLightmaps.Add(lightmap_Remap);
            newIndex = sceneLightmaps.Count - 1;
        }
        return newIndex;
    }

    public static int sizeOfTexture;
    public static TextureImporterFormat tf;

    static Texture2D GetLightmapAsset(string filename, string resourcePath, int originalLightmapIndex, Texture2D lightmap)
    {
        AssetDatabase.ImportAsset(filename, ImportAssetOptions.ForceUpdate);
        var importer = AssetImporter.GetAtPath(filename) as TextureImporter;
        importer.isReadable = true;
        importer.textureFormat = tf;
        importer.maxTextureSize = sizeOfTexture;



        AssetDatabase.ImportAsset(filename, ImportAssetOptions.ForceUpdate);
        var assetLightmap = AssetDatabase.LoadAssetAtPath<Texture2D>(filename);
        var assetPath = resourcePath + "-" + originalLightmapIndex + ".asset";
        var newLightmap = Instantiate<Texture2D>(assetLightmap);
        AssetDatabase.CreateAsset(newLightmap, assetPath);
        newLightmap = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
        importer.isReadable = false;
        AssetDatabase.ImportAsset(filename, ImportAssetOptions.ForceUpdate);
        return newLightmap;
    }
#endif
}