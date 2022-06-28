using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JuiceKit
{
    public abstract class BaseConfigSO : ScriptableObject
    {
#if UNITY_EDITOR
        bool hasError;

        protected abstract void ParseRaw(string[][] _configRaw);
        protected abstract void Validate();

        public void ImportConfig(string[][] _configRaw)
        {
            Debug.Log($"Import {name}.");

            ParseRaw(_configRaw);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);

            Debug.Log($"{name} imported.");
        }

        public void ValidateConfig()
        {
            Debug.Log($"Validate {name}.");

            hasError = false;
            Validate();

            if (hasError)
                throw new Exception($"Validation error in {name}.");

            Debug.Log($"{name} validated.");
        }

        protected void LogError(string _errorStr)
        {
            hasError = true;
            Debug.LogError($"{name}: {_errorStr}");
        }
#endif

        public abstract BaseConfig Config { get; }
    }

    public abstract class BaseConfigSO<Config_T> : BaseConfigSO
        where Config_T : BaseConfig
    {
        [SerializeField]
        protected Config_T config;

        public void SetConfig(Config_T _config)
        {
            config = _config;
        }

        public override BaseConfig Config => config;
        public Config_T TypedConfig => Config as Config_T;
    }
}
