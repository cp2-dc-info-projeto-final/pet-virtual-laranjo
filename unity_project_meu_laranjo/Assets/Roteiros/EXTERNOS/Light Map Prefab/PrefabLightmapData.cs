#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
#endif
using UnityEngine;
using System.Collections.Generic;

public class PrefabLightmapData : MonoBehaviour
{
    [System.Serializable]
    struct RendererInfo
    {
        public Renderer renderer;
        public int lightmapIndex;
        public Vector4 lightmapOffsetScale;
    }

    [SerializeField]
    RendererInfo[] m_RendererInfo;
    [SerializeField]
    Texture2D[] m_Lightmaps;
    [SerializeField]
    Texture2D[] m_Lightmaps2;

    public const string LIGHTMAP_RESOURCE_PATH = "Assets/Scenes/Resources/Lightmaps/";

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

    static void ApplyLightmaps(RendererInfo[] rendererInfo, Texture2D[] lightmaps, Texture2D[] lightmaps2)
    {
        bool existsAlready = false;
        int counter = 0;
        int[] lightmapArrayOffsetIndex;

        if (rendererInfo == null || rendererInfo.Length == 0)
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

        var combinedLightmaps2 = new LightmapData[settingslightmaps.Length + counter];
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

    static void ApplyRendererInfo(RendererInfo[] infos, int[] arrayOffsetIndex)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            var info = infos[i];
            info.renderer.lightmapIndex = arrayOffsetIndex[info.lightmapIndex];
            info.renderer.lightmapScaleOffset = info.lightmapOffsetScale;
        }
    }

#if UNITY_EDITOR
    [MenuItem("VR_Rehearsal_app/Update Scene with Prefab Lightmaps")]
    static void UpdateLightmaps()
    {
        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();

        foreach (var instance in prefabs)
        {
            ApplyLightmaps(instance.m_RendererInfo, instance.m_Lightmaps, instance.m_Lightmaps2);
        }

        Debug.Log("Prefab lightmaps updated");
    }

    [MenuItem("VR_Rehearsal_app/Bake Prefab Lightmaps")]
    static void GenerateLightmapInfo()
    {
        Debug.ClearDeveloperConsole();

        if (Lightmapping.giWorkflowMode != Lightmapping.GIWorkflowMode.OnDemand)
        {
            Debug.LogError("ExtractLightmapData requires that you have baked you lightmaps and Auto mode is disabled.");
            return;
        }

        Lightmapping.Bake();

        sceneLightmaps = new List<Texture2D_Remap>();

        PrefabLightmapData[] prefabs = FindObjectsOfType<PrefabLightmapData>();

        foreach (var instance in prefabs)
        {
            var gameObject = instance.gameObject;
            var rendererInfos = new List<RendererInfo>();
            var lightmaps = new List<Texture2D>();
            var lightmaps2 = new List<Texture2D>();

            GenerateLightmapInfo(gameObject, rendererInfos, lightmaps, lightmaps2);

            instance.m_RendererInfo = rendererInfos.ToArray();
            instance.m_Lightmaps = lightmaps.ToArray();
            instance.m_Lightmaps2 = lightmaps2.ToArray();

            var targetPrefab = PrefabUtility.GetPrefabParent(gameObject) as GameObject;
            if (targetPrefab != null)
            {
                //Prefab
                PrefabUtility.ReplacePrefab(gameObject, targetPrefab);
                PrefabUtility.RevertPrefabInstance(gameObject);
            }

            ApplyLightmaps(instance.m_RendererInfo, instance.m_Lightmaps, instance.m_Lightmaps2);
        }

        Debug.Log("Update to prefab lightmaps finished");
    }

    static void GenerateLightmapInfo(GameObject root, List<RendererInfo> rendererInfos, List<Texture2D> lightmaps, List<Texture2D> lightmaps2)
    {
        var renderers = root.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.lightmapIndex != -1)
            {
                RendererInfo info = new RendererInfo();
                info.renderer = renderer;
                info.lightmapOffsetScale = renderer.lightmapScaleOffset;

                Texture2D lightmap = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapColor;
                Texture2D lightmap2 = LightmapSettings.lightmaps[renderer.lightmapIndex].lightmapDir;
                //int sceneLightmapIndex = AddLightmap(scenePath, resourcePath, renderer.lightmapIndex, lightmap, lightmap2);

                info.lightmapIndex = lightmaps.IndexOf(lightmap);
                if (info.lightmapIndex == -1)
                {
                    info.lightmapIndex = lightmaps.Count;
                    lightmaps.Add(lightmap);
                    lightmaps2.Add(lightmap2);
                }

                rendererInfos.Add(info);
            }
        }
    }
#endif

}