using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public static class Ext
{
    public static string[][] SplitStrings(this string _string, char _splitCh)
    {
            _string = _string.Replace("\r\n", "\n");
            string[] _lines = _string.Split('\n');
            string[][] _splitStrings = new string[_lines.Length][];
            for (int i = 0; i < _lines.Length; i++)
            {
                _splitStrings[i] = _lines[i].Split(_splitCh);
            }

            return _splitStrings;
    }

}

namespace JuiceKit.Editor
{
    public class ConfigsActions
    {
        static readonly string JK_ExportConfig = "JK_ExportConfig";

        public static void ImportConfig()
        {
            Debug.Log("Selected object: " + Selection.activeObject);

            var _configSO = Selection.activeObject as BaseConfigSO;
            if (_configSO)
            {
                string _sourceFilePath = EditorUtility.OpenFilePanel("Choose tsv config", "/..", "");
                //string _sourceFilePath = Application.dataPath.Replace("/Assets", "/Assets/Scripts/Config");
                //if (!_sourceFilePath.IsEmptyOrWhitespace())
                //{
                    var _configData = File.ReadAllText(_sourceFilePath).SplitStrings('\t');
                    _configSO.ImportConfig(_configData);
                //}
            }
            else
            {
                Debug.LogError("Select config asset at first.");
            }
        }

        public static void ValidateConfigs()
        {
            FindAndValidateConfigs();
        }

        public static void ExportConfigs()
        {
            var _configs = FindAndValidateConfigs();

            string _targetPath = ExportPath;

            EditorUtility.DisplayProgressBar("ConfigsExporter", "Export configs", 0.75f);

            try
            {
                if (!Directory.Exists(_targetPath))
                    Directory.CreateDirectory(_targetPath);

                _configs.ForEach(_config =>
                {
                    var _labels = new List<string>(AssetDatabase.GetLabels(_config));
                    if (_labels.Contains(JK_ExportConfig))
                    {
                        string _configJson = JsonUtility.ToJson(_config.Config);
                        File.WriteAllText(Path.Combine(_targetPath, $"{_config.name}.txt"), _configJson);
                    }
                });

                EditorUtility.RevealInFinder(_targetPath);
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        static List<BaseConfigSO> FindAndValidateConfigs()
        {
            EditorUtility.DisplayProgressBar("ConfigsExporter", "Find Configs", 0.25f);

            var _configs = new List<BaseConfigSO>();
            var _configsGUIDs = AssetDatabase.FindAssets("t:BaseConfigSO");

            EditorUtility.DisplayProgressBar("ConfigsExporter", "Validate Configs", 0.75f);

            try
            {
                foreach (var _guid in _configsGUIDs)
                {
                    var _path = AssetDatabase.GUIDToAssetPath(_guid);
                    var _config = AssetDatabase.LoadAssetAtPath<BaseConfigSO>(_path);

                    if (_configs.Find(_cfg => _cfg.name == _config.name))
                    {
                        throw new Exception($"Config's name already exists: {_config.name}. Use unique names for all configs!");
                    }
                    else
                    {
                        _config.ValidateConfig();
                        _configs.Add(_config);

                        EditorUtility.SetDirty(_config);
                    }
                }
            }
            finally
            {
                AssetDatabase.SaveAssets();
                EditorUtility.ClearProgressBar();
            }

            return _configs;
        }

        static string ExportPath => Path.Combine(Application.dataPath, "../Configs");
    }
}
