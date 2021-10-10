using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomClassLevelUpWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D classNameSectionTexture;
    Texture2D customClassStatsTexture;
    Texture2D customClassStatIncreaseChanceTexture;
    Texture2D bottomSectionTexture;

    Color headerSectionColor = new Color(125f / 255f, 125f / 255f, 125f / 255f, 1f);
    Color classNameSectionColor = new Color(200f / 255f, 200f / 255f, 200f / 255f, 1f);
    Color bottomSectionColor = new Color(125f / 255f, 125f / 255f, 125f / 255f, 1f);

    Rect headerSection;
    Rect classNameSection;
    Rect customClassStatsSection;
    Rect customClassStatIncreaseSection;
    Rect bottomSection;

    GUISkin skin;

    static CustomClassData customclassData;

    public static CustomClassData customclassInfo { get { return customclassData; } }

    [MenuItem("Window/Custom Class LevelUp Designer")]
    static void OpenWindow()
    {
        CustomClassLevelUpWindow window = (CustomClassLevelUpWindow)GetWindow(typeof(CustomClassLevelUpWindow));
        window.minSize = new Vector2(600, 300);
        window.maxSize = new Vector2(600, 300);
        window.Show();
    }

    static void CloseWindow()
    {
        CustomClassLevelUpWindow window = (CustomClassLevelUpWindow)GetWindow(typeof(CustomClassLevelUpWindow));
        window.Close();
    }

    private void OnEnable()
    {
        InitTextures();
        InitData();
        skin = Resources.Load<GUISkin>("GUIStyles/CustomClassLevelUP_Skin");
    }

    void InitData()
    {
        customclassData = (CustomClassData)ScriptableObject.CreateInstance(typeof(CustomClassData));
    }

    void InitTextures()
    {
        //sets a pixel 1 by 1
        headerSectionTexture = new Texture2D(1, 1);
        //sets pixel color
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        //classNameSectionTexture = new Texture2D(1, 1);
        //classNameSectionTexture.SetPixel(0, 400, classNameSectionColor);
        //classNameSectionTexture.Apply();
        classNameSectionTexture = Resources.Load<Texture2D>("ArtForWindow/CustomClassBodyBackground2");

        //TODO either add a color for custom class stats and stat increase chance or add custom background
        customClassStatsTexture = Resources.Load<Texture2D>("ArtForWindow/CustomClassBodyBackground");
        customClassStatIncreaseChanceTexture = Resources.Load<Texture2D>("ArtForWindow/CustomClassBodyBackground");

        bottomSectionTexture = new Texture2D(1, 1);
        bottomSectionTexture.SetPixel(0, 400, bottomSectionColor);
        bottomSectionTexture.Apply();        
    }

    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawClassName();
        DrawCustomClassSettings();
        DrawCustomClassStatIncreaseChanceSettings();
        DrawBottom();
    }

    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = position.width;
        headerSection.height = 50;

        classNameSection.x = 0;
        classNameSection.y = 50;
        classNameSection.width = position.width;
        classNameSection.height = 50;

        customClassStatsSection.x = 0;
        customClassStatsSection.y = 100;
        customClassStatsSection.width = position.width / 2f;
        customClassStatsSection.height = position.width - 50;

        customClassStatIncreaseSection.x = position.width / 2f;
        customClassStatIncreaseSection.y = 100;
        customClassStatIncreaseSection.width = position.width / 2f;
        customClassStatIncreaseSection.height = position.width - 50;

        bottomSection.x = 0;
        bottomSection.y = 255;
        bottomSection.width = position.width;
        bottomSection.height = 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(classNameSection, classNameSectionTexture);
        GUI.DrawTexture(customClassStatsSection, customClassStatsTexture);
        GUI.DrawTexture(customClassStatIncreaseSection, customClassStatIncreaseChanceTexture);
        GUI.DrawTexture(bottomSection, bottomSectionTexture);

    }

    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Space(8);
        GUILayout.Label("Custom Class LevelUp Designer", skin.GetStyle("Header"));

        GUILayout.EndArea();
    }

    void DrawClassName()
    {
        GUILayout.BeginArea(classNameSection);
        GUILayout.Space(20);
        //TODO add text field for Class Name
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(100);
        GUILayout.Label("Class Name");
        customclassData.className = EditorGUILayout.TextField(customclassData.className);
        GUILayout.Space(200);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void DrawCustomClassSettings()
    {
        GUILayout.BeginArea(customClassStatsSection);

        GUILayout.Label(" Stats", skin.GetStyle("StatsStyle"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("HP", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        customclassData.healthStat = EditorGUILayout.IntField(customclassData.healthStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ATK", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        customclassData.attackStat = EditorGUILayout.IntField(customclassData.attackStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("DEF", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        customclassData.defenseStat = EditorGUILayout.IntField(customclassData.defenseStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("SPD", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        customclassData.speedStat = EditorGUILayout.IntField(customclassData.speedStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
    
    void DrawCustomClassStatIncreaseChanceSettings()
    {
        GUILayout.BeginArea(customClassStatIncreaseSection);

        GUILayout.Label(" % Chance to Increase Stat", skin.GetStyle("StatsStyle"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("HP% ", GUILayout.Width(40));
        GUILayout.FlexibleSpace();
        customclassData.chanceToIncreaseHealth = EditorGUILayout.IntField(customclassData.chanceToIncreaseHealth);
        GUILayout.Space(50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ATK%", GUILayout.Width(40));
        GUILayout.FlexibleSpace();
        customclassData.chanceToIncreaseAttack = EditorGUILayout.IntField(customclassData.chanceToIncreaseAttack);
        GUILayout.Space(50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("DEF%", GUILayout.Width(40));
        GUILayout.FlexibleSpace();
        customclassData.chanceToIncreseDefense = EditorGUILayout.IntField(customclassData.chanceToIncreseDefense);
        GUILayout.Space(50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("SPD%", GUILayout.Width(40));
        GUILayout.FlexibleSpace();
        customclassData.chanceToIncreaseSpeed = EditorGUILayout.IntField(customclassData.chanceToIncreaseSpeed);
        GUILayout.Space(50);
        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void DrawBottom()
    {
        GUILayout.BeginArea(bottomSection);

        if (customclassData.className == null)
        {
            EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created.", MessageType.Warning);
        }
        //TODO add more else if functions that check if the inputs are null
        //TODO add an else if function that checks if className == an existing Data name
        //else if (/*Find CustomClassData file's name*/ = customclassData.className)
        //{
        //    EditorGUILayout.HelpBox("This [Name] already exists.", MessageType.Warning);
        //}
        else if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            //save class data?
            SaveClassData();
            //once saved, open window with data to test level up system
            TestLevelUpSystem.OpenWindow();
            CloseWindow();
            OpenWindow();            
        }

        GUILayout.EndArea();
    }

    void SaveClassData()
    {
        string dataPath = "Assets/Custom_Class_LevelUp_Tool/resources/classesData/";

        //currently replaces asset file if it is named the same
        //create the asset file
        dataPath += CustomClassLevelUpWindow.customclassInfo.className + ".asset";
        AssetDatabase.CreateAsset(CustomClassLevelUpWindow.customclassInfo, dataPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

public class TestLevelUpSystem : EditorWindow
{
    static TestLevelUpSystem window;

    public static void OpenWindow()
    {
        TestLevelUpSystem window = (TestLevelUpSystem)GetWindow(typeof(TestLevelUpSystem));
        window.minSize = new Vector2(250, 200);
        window.maxSize = new Vector2(250, 200);
        window.Show();
    }

    private void OnGUI()
    {
        DrawClassLevel((CustomClassData)CustomClassLevelUpWindow.customclassInfo);
        DrawClassStats((CustomClassData)CustomClassLevelUpWindow.customclassInfo);
        DrawLevelUpButton();
    }

    void DrawClassName(CustomClassData classData)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Space(8);
        GUILayout.Label(classData.className);

        GUILayout.EndHorizontal();
    }

    void DrawClassLevel(CustomClassData classData)
    {
        GUILayout.BeginHorizontal();

        GUILayout.Space(8);
        GUILayout.Label("Level");
        classData.level = EditorGUILayout.IntField(classData.level);

        GUILayout.EndHorizontal();
    }

    void DrawClassStats(CustomClassData classData)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(" Stats");
        GUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("HP", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        classData.healthStat = EditorGUILayout.IntField(classData.healthStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ATK", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        classData.attackStat = EditorGUILayout.IntField(classData.attackStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("DEF", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        classData.defenseStat = EditorGUILayout.IntField(classData.defenseStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("SPD", GUILayout.Width(30));
        GUILayout.FlexibleSpace();
        classData.speedStat = EditorGUILayout.IntField(classData.speedStat);
        GUILayout.Space(60);
        EditorGUILayout.EndHorizontal();       
    }
    
    void DrawLevelUpButton()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Level Up!", GUILayout.Height(40)))
        {
            //Increase level then stats based on their percentages 
        }

        GUILayout.EndHorizontal();
    }
}
