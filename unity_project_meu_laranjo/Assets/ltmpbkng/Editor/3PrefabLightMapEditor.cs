using UnityEngine;
using System.Collections;
using UnityEditor;

public class PrefabLightMapEditor : EditorWindow {

   
    bool checkToggle;

 

    [MenuItem("Window/Prefabbed Lightmap")]
    public static void FirstTest()
    {
         EditorWindow.GetWindow(typeof(PrefabLightMapEditor));
    }


public    string pathToFolder = "Assets/Resources/Lightmaps/";

    int selected = 0;


    TextureImporterFormat[] options = new TextureImporterFormat[]
{
    TextureImporterFormat.Alpha8,
      TextureImporterFormat.ARGB16,
          TextureImporterFormat.ARGB32,
      TextureImporterFormat.Automatic16bit,   TextureImporterFormat.AutomaticCompressed,
      TextureImporterFormat.AutomaticCrunched  , TextureImporterFormat.AutomaticTruecolor,
      TextureImporterFormat.DXT1,    TextureImporterFormat.DXT5,
      TextureImporterFormat.ETC2_RGB4,    TextureImporterFormat.ETC2_RGB4_PUNCHTHROUGH_ALPHA,
      TextureImporterFormat.ETC2_RGBA8,    TextureImporterFormat.ETC_RGB4,
      TextureImporterFormat.PVRTC_RGB2,    TextureImporterFormat.RGB16,
      TextureImporterFormat.RGBA32,

            TextureImporterFormat.RGBA16,    TextureImporterFormat.RGB24,
      TextureImporterFormat.PVRTC_RGBA4,    TextureImporterFormat.PVRTC_RGBA2,
      TextureImporterFormat.PVRTC_RGB4,

};

   

    string[] options_string = new string[]
{
    "TextureImporterFormat.Alpha8",
      "TextureImporterFormat.ARGB16",
          "TextureImporterFormat.ARGB32",
      "TextureImporterFormat.Automatic16bit",   "TextureImporterFormat.AutomaticCompressed",
      "TextureImporterFormat.AutomaticCrunched"  , "TextureImporterFormat.AutomaticTruecolor",
      "TextureImporterFormat.DXT1",    "TextureImporterFormat.DXT5",
      "TextureImporterFormat.ETC2_RGB4 bits",    "TextureImporterFormat.ETC2_RGB4_PUNCHTHROUGH_ALPHA",
      "TextureImporterFormat.ETC2_RGBA 8 bits",    "TextureImporterFormat.ETC_RGB 4 Bit",
      "TextureImporterFormat.PVRTC_RGB2",    "TextureImporterFormat.RGB16",
      "TextureImporterFormat.RGBA32",

            "TextureImporterFormat.RGBA16",    "TextureImporterFormat.RGB24",
      "TextureImporterFormat.PVRTC_RGBA4",    "TextureImporterFormat.PVRTC_RGBA2",
      "TextureImporterFormat.PVRTC_RGB4"

};

    int textureSizeIndex = 0;


    int[] textureSize = new int[]
    {

        512,1024,2048,4096,8192


    };
    string[] textureSting = new string[]
 {

        "512","1024","2048","4096","8192"


 };

    public string lightMapFix = "LOD";

    void OnGUI()
    {
        GUILayout.Label("Bake lightmap into prefabs", EditorStyles.boldLabel);
        GUILayout.Space(20);
        GUILayout.Label("Add path to folder and make sure its exists");

        pathToFolder = GUILayout.TextField(pathToFolder, 150);
        GUILayout.Space(5);


        selected = EditorGUILayout.Popup("Select Texture format for generated lightmaps", selected, options_string);
        GUILayout.Space(5);

        textureSizeIndex = EditorGUILayout.Popup("Select Size for generated lightmaps", textureSizeIndex, textureSting);
        GUILayout.Space(5);



        if (GUILayout.Button("Step 1 : Bake prefab lightmap"))
        {


            if (AssetDatabase.IsValidFolder(pathToFolder.Remove(pathToFolder.Length-1)))
            {

                

                PrefabLightmapData.GenerateLightmapInfo(pathToFolder, options[selected],textureSize[textureSizeIndex]);

            }
            else {
                Debug.Log("Please create valid path");


            }

        }

        GUILayout.Space(15);

        if (GUILayout.Button("(OPTIONAL)Step 2 : Update Scene with Prefab Lightmaps"))
        {


            if (AssetDatabase.IsValidFolder(pathToFolder.Remove(pathToFolder.Length - 1)))
            {

                PrefabLightmapData.UpdateLightmaps();


            }
            else {
                Debug.LogError("Please create valid path");


            }

        }

        GUILayout.Space(20);

        checkToggle = EditorGUILayout.BeginToggleGroup("Enable LOD", checkToggle);

        GUILayout.Label("Add lightmap prefix", EditorStyles.boldLabel);


        lightMapFix = GUILayout.TextField(lightMapFix, 150);

        if (GUILayout.Button("Step 3 : Assign lightmaps to LOD"))
        {


            if (AssetDatabase.IsValidFolder(pathToFolder.Remove(pathToFolder.Length - 1)))
            {

                PrefabLightmapData.UseMeBeforeUpdatint(lightMapFix);

                //PrefabLightmapData.GenerateLightmapInfo(pathToFolder, options[selected], textureSize[textureSizeIndex]);

            }
            else {
                Debug.Log("Please create valid path");


            }

        }


        EditorGUILayout.EndToggleGroup();


    }
}
