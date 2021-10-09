using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mageSectionTexture;
    Texture2D warriorSectionTexture;
    Texture2D rogueSectionTexture;
    Texture2D mageTexture;
    Texture2D warriorTexture;
    Texture2D rogueTexture;

    Color headerSectionColor = new Color(125f/255f, 125f/255f, 125f/255f, 1f);

    Rect headerSection;
    Rect mageSection;
    Rect warriorSection;
    Rect rogueSection;
    Rect mageIconSection;
    Rect warriorIconSection;
    Rect rogueIconSection;

    GUISkin skin;

    static MageData mageData;
    static WarriorData warriorData;
    static RogueData rogueData;

    public static MageData MageInfo { get { return mageData; } }
    public static WarriorData WarriorInfo { get { return warriorData; } }
    public static RogueData RogueInfo { get { return rogueData; } }

    float iconSize = 80;

    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyDesignerWindow window = 
            (EnemyDesignerWindow)GetWindow(typeof(EnemyDesignerWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    private void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("guiStyles/EnemyDesignerSkin");
    }

    public static void InitData()
    {
        mageData = (MageData)ScriptableObject.CreateInstance(typeof(MageData));
        warriorData = (WarriorData)ScriptableObject.CreateInstance(typeof(WarriorData));
        rogueData = (RogueData)ScriptableObject.CreateInstance(typeof(RogueData));
    }
    //Initialize 2d Textures
    void InitTextures()
    {
        //sets a pixel 1 by 1
        headerSectionTexture = new Texture2D(1, 1);
        //sets pixel color
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        //adding a texture based on custom texture
        mageSectionTexture = Resources.Load<Texture2D>("icons/MageIcon");
        warriorSectionTexture = Resources.Load<Texture2D>("icons/WarriorIcon");
        rogueSectionTexture = Resources.Load<Texture2D>("icons/RogueIcon");

        //adding icons
        mageTexture = Resources.Load<Texture2D>("icons/MageSymbol");
        warriorTexture = Resources.Load<Texture2D>("icons/WarriorSymbol");
        rogueTexture = Resources.Load<Texture2D>("icons/RogueSymbol");
    }
    //Similar to Update function but called 1 or more times per interaction
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }
    //Defines Rect values and paints textures based on Rects
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = position.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = position.width/3f;
        mageSection.height = position.width - 50;

        mageIconSection.x = (mageSection.x + mageSection.width / 2f) - iconSize / 2f;
        mageIconSection.y = mageSection.y + 8;
        mageIconSection.width = iconSize;
        mageIconSection.height = iconSize;

        warriorSection.x = position.width/3f;
        warriorSection.y = 50;
        warriorSection.width = position.width / 3f;
        warriorSection.height = position.width - 50;

        warriorIconSection.x = (warriorSection.x + warriorSection.width / 2f) - iconSize / 2f;
        warriorIconSection.y = warriorSection.y + 8;
        warriorIconSection.width = iconSize;
        warriorIconSection.height = iconSize;

        rogueSection.x = (position.width / 3f) * 2;
        rogueSection.y = 50;
        rogueSection.width = position.width / 3f;
        rogueSection.height = position.width - 50;

        rogueIconSection.x = (rogueSection.x + rogueSection.width / 2f) - iconSize / 2f;
        rogueIconSection.y = rogueSection.y + 8;
        rogueIconSection.width = iconSize;
        rogueIconSection.height = iconSize;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
        GUI.DrawTexture(mageIconSection, mageTexture);
        GUI.DrawTexture(warriorIconSection, warriorTexture);
        GUI.DrawTexture(rogueIconSection, rogueTexture);
    }
    //Draw contents in a region
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Enemy Designer", skin.GetStyle("Header1"));

        GUILayout.EndArea();
    }
    void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Mage", skin.GetStyle("MageHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage", skin.GetStyle("MageField"));
        mageData.dmgType = (Types.MageDmgType)EditorGUILayout.EnumPopup(mageData.dmgType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("MageField"));
        mageData.wpnType = (Types.MageWpnType)EditorGUILayout.EnumPopup(mageData.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }
    void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Warrior", skin.GetStyle("WarriorHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class", skin.GetStyle("WarriorField"));
        warriorData.classType = (Types.WarriorClassType)EditorGUILayout.EnumPopup(warriorData.classType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("WarriorField"));
        warriorData.wpnType = (Types.WarriorWpnType)EditorGUILayout.EnumPopup(warriorData.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }
    void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);

        GUILayout.Space(iconSize + 8);

        GUILayout.Label("Rogue", skin.GetStyle("RogueHeader"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy", skin.GetStyle("RogueField"));
        rogueData.stategyType = (Types.RogueStategyType)EditorGUILayout.EnumPopup(rogueData.stategyType);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon", skin.GetStyle("RogueField"));
        rogueData.wpnType = (Types.RogueWpnType)EditorGUILayout.EnumPopup(rogueData.wpnType);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }
}

public class GeneralSettings : EditorWindow
{
    public enum SettingsType
    {
        MAGE,
        WARRIOR,
        ROGUE
    }
    static SettingsType dataSetting;
    static GeneralSettings window;

    public static void OpenWindow(SettingsType setting)
    {
        dataSetting = setting;
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        switch (dataSetting)
        {
            case SettingsType.MAGE:
                DrawSettings((CharacterData)EnemyDesignerWindow.MageInfo);
                break;
            case SettingsType.WARRIOR:
                DrawSettings((CharacterData)EnemyDesignerWindow.WarriorInfo);
                break;
            case SettingsType.ROGUE:
                DrawSettings((CharacterData)EnemyDesignerWindow.RogueInfo);
                break;
        }
    }

    void DrawSettings(CharacterData charData)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Prefab");
        //the false at the end is allow scene assets (essentially only allowing prefabs if false)
        charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Health");
        charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Max Energy");
        charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Power");
        charData.power = EditorGUILayout.Slider(charData.power, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("% Crit Chance");
        charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Name");
        charData.name = EditorGUILayout.TextField(charData.name);
        EditorGUILayout.EndHorizontal();

        if (charData.prefab == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created.", MessageType.Warning);
        }
        else if (charData.name == null || charData.name.Length < 1)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
        {
            SaveCharacterData();
            window.Close();
        }
    }

    void SaveCharacterData()
    {
        string prefabPath; //path to the base prefab
        string newPrefabPath = "Assets/RPG_Enemy_Classes_Tool/prefabs/characters/";
        string dataPath = "Assets/RPG_Enemy_Classes_Tool/resources/characterData/data/";

        switch (dataSetting)
        {
            case SettingsType.MAGE:

                //create the asset file
                dataPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.MageInfo, dataPath);

                newPrefabPath += "mage/" + EnemyDesignerWindow.MageInfo.name + ".prefab";
                //get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.MageInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!magePrefab.GetComponent<Mage>())
                {
                    magePrefab.AddComponent(typeof(Mage));
                }
                magePrefab.GetComponent<Mage>().mageData = EnemyDesignerWindow.MageInfo;

                break;

            case SettingsType.WARRIOR:

                //create the asset file
                dataPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.WarriorInfo, dataPath);

                newPrefabPath += "warrior/" + EnemyDesignerWindow.WarriorInfo.name + ".prefab";
                //get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.WarriorInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!warriorPrefab.GetComponent<Warrior>())
                {
                    warriorPrefab.AddComponent(typeof(Warrior));
                }
                warriorPrefab.GetComponent<Warrior>().warriorData = EnemyDesignerWindow.WarriorInfo;

                break;

            case SettingsType.ROGUE:

                //create the asset file
                dataPath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".asset";
                AssetDatabase.CreateAsset(EnemyDesignerWindow.RogueInfo, dataPath);

                newPrefabPath += "rogue/" + EnemyDesignerWindow.RogueInfo.name + ".prefab";
                //get prefab path
                prefabPath = AssetDatabase.GetAssetPath(EnemyDesignerWindow.RogueInfo.prefab);
                AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                if (!roguePrefab.GetComponent<Rogue>())
                {
                    roguePrefab.AddComponent(typeof(Rogue));
                }
                roguePrefab.GetComponent<Rogue>().rogueData = EnemyDesignerWindow.RogueInfo;

                break;
        }
    }
}
