using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace JuiceKit
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
    class ConfigSO : BaseConfigSO<Config>
    {
        private BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase;
        protected override void ParseRaw(string[][] _configRaw)
        {
            Config config = new Config();
            Type configType = config.GetType();
            for (int i = 0; i < _configRaw.Length; i++)
            {
                if (CheckEmptyLine(_configRaw[i]))
                {
                    continue;
                }

                if (_configRaw[i][0] != "")
                {
                    FieldInfo field = configType.GetField(_configRaw[i][0], BINDING_FLAGS);
                    if (field is null) 
                    {
                        Debug.LogError($"Config does not contain a field {_configRaw[i][0]}");
                        continue;
                    }
                    if (field.FieldType.IsArray)
                    {
                        List<object> objList = new List<object>();
                        
                        List<string> fieldList = GetParametersList(_configRaw[i]);
                        

                        for (int itr = i + 1; itr < _configRaw.Length; itr++)
                        {
                            if (_configRaw[itr][0] != "")
                            {
                                break;
                            }

                            object objInstance = Activator.CreateInstance(field.FieldType.GetElementType());
                            
                            for (int j = 1; j < _configRaw[itr].Length;j++)
                            {
                                if (_configRaw[itr][j] == "")
                                {
                                    continue;
                                }

                                if (objInstance.GetType().GetField(fieldList[j - 1], BINDING_FLAGS) is null)
                                {
                                    Debug.LogError($"{objInstance.GetType().Name} list does not contain a field {fieldList[j-1]}");
                                    continue;
                                }
                                objInstance.GetType().GetField(fieldList[j - 1], BINDING_FLAGS)
                                        .SetValue(objInstance,ConvertType(_configRaw[itr][j],
                                                objInstance.GetType().GetField(fieldList[j - 1], BINDING_FLAGS).FieldType));

                            }
                            objList.Add(objInstance);
                        }
                        
                        Array objectArray = Array.CreateInstance(field.FieldType.GetElementType(),objList.Count);
                        for (int j = 0; j < objList.Count; j++)
                        {
                            objectArray.SetValue(objList[j],j);
                        }

                        field.SetValue(config,objectArray);

                    }
                    else
                    {
                        string bufferString = String.Empty;
                        for (int j = 1; j < _configRaw[i].Length; j++)
                        {
                            if (_configRaw[i][j] == "")
                            {
                                continue;
                            }

                            if (_configRaw[i][j] == " ")
                            {
                                break;
                            }

                            bufferString = _configRaw[i][j];
                        }

                        field.SetValue(config, ConvertType(bufferString, field.FieldType));

                    }
                }
            }

             SetConfig(config);

        }

        private object ConvertType(string value,Type type) 
        {
            if (type.IsEquivalentTo(typeof(UnityEngine.Color)))
            {
                return value.StringToColor();
            }
            else 
            {
                return Convert.ChangeType(value, type);
            }
        }

        private List<string> GetParametersList(string[] tableRaw) 
        {
            List<string> list = new List<string>();
            for (int itr = 1; itr < tableRaw.Length; itr++)
            {
                if (tableRaw[itr] == "")
                {
                    continue;
                }
              
                list.Add(String.Concat(tableRaw[itr].Split(' ')));              

            }

            return list;
        }
        protected override void Validate()
        {
           
        }

        private bool CheckEmptyLine(string[] line)
        {
            int countEmptyStrings = 0;
            foreach (string iterator in line)
            {
                if (iterator == "")
                {
                    countEmptyStrings++;
                }
            }

            if (countEmptyStrings.Equals(line.Length))
            {
                return true;
            }

            return false;
        }

    }


}
