using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace JuiceKit
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
    class ConfigSO : BaseConfigSO<Config>
    {
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
                    FieldInfo field = configType.GetField(_configRaw[i][0], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                    if (field.FieldType.IsArray)
                    {
                        List<object> objList = new List<object>();
                        
                        List<string> fieldList = new List<string>();
                        for (int itr = 1; itr < _configRaw[i].Length; itr++)
                        {
                            if (_configRaw[i][itr].Contains(' '))
                            {
                                fieldList.Add(String.Concat(_configRaw[i][itr].Split(' ')));
                            }
                            else
                            {
                                fieldList.Add(_configRaw[i][itr]);
                            }
                            
                        }

                        for (int itr = i + 1; itr < _configRaw.Length; itr++)
                        {
                            if (_configRaw[itr][0] != "")
                            {
                                break;
                            }

                            object objInstance = Activator.CreateInstance(field.FieldType.GetElementType());
                            
                            for (int j = 1; j < _configRaw[itr].Length;j++)
                            {
                                if (objInstance.GetType().GetField(fieldList[j-1], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase).FieldType.IsEquivalentTo(typeof(UnityEngine.Color)))
                                {
                                    objInstance.GetType().GetField(fieldList[j - 1],
                                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase)
                                        .SetValue(objInstance, _configRaw[itr][j].StringToColor());

                                }
                                else
                                {
                                    objInstance.GetType().GetField(fieldList[j - 1],
                                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase)
                                        .SetValue(objInstance,
                                            Convert.ChangeType(_configRaw[itr][j],
                                                objInstance.GetType().GetField(fieldList[j - 1],
                                                    BindingFlags.Instance | BindingFlags.NonPublic |
                                                    BindingFlags.IgnoreCase).FieldType));
                                }
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
                            if (_configRaw[i][j] == " ")
                            {
                                break;
                            }

                            bufferString += _configRaw[i][j];
                        }

                        if (field.FieldType.IsEquivalentTo(typeof(UnityEngine.Color)))
                        {
                            field.SetValue(config, bufferString.StringToColor());
                        }
                        else
                        {
                            field.SetValue(config, Convert.ChangeType(bufferString, field.FieldType));
                        }

                       
                        
                    }
                }
            }

             SetConfig(config);

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
