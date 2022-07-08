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
            List<Items> itemsList = new List<Items>();
            List<OtherItems> otherItemsList= new List<OtherItems>();
            string min_age = String.Empty;
            string default_color = String.Empty;
            for (int i = 0;i< _configRaw.Length;i++)
            {
                if (CheckEmptyLine(_configRaw[i]))
                {
                    continue;
                }

                if (_configRaw[i][0] == "items")
                {
                    
                    for (int j = i+1; j < _configRaw.Length; j++)
                    {
                        List<string> lineList = new List<string>();
                        if (_configRaw[j][0] != "")
                        {
                            break;
                        }

                        for (int value = 1; value < _configRaw[j].Length; value++)
                        {
                            lineList.Add(_configRaw[j][value]);
                        }

                        Type type = typeof(Items);
                        ConstructorInfo[] constructorsInfo = type.GetConstructors();
                        ConstructorInfo constructor = constructorsInfo[0];
                        Items item = (Items)constructor.Invoke(lineList.ToArray());
                        itemsList.Add(item);
                    }
                }
                
                if (_configRaw[i][0] == "other_items")
                {

                    for (int j = i + 1; j < _configRaw.Length; j++)
                    {
                        List<string> lineList = new List<string>();
                        if (_configRaw[j][0] != "")
                        {
                            break;
                        }

                        for (int value = 1; value < _configRaw[j].Length; value++)
                        {
                            lineList.Add(_configRaw[j][value]);
                        }

                        Type type = typeof(OtherItems);
                        ConstructorInfo[] constructorsInfo = type.GetConstructors();
                        ConstructorInfo constructor = constructorsInfo[0];
                        OtherItems otherItem = (OtherItems)constructor.Invoke(lineList.ToArray());
                        otherItemsList.Add(otherItem);
                    }

                }

                if (_configRaw[i][0] == "min_age")
                {

                    for (int j = i; j < _configRaw.Length; j++)
                    {
                        if (_configRaw[j][0] == "default_color")
                        {
                            break;
                        }

                        List<string> lineList = new List<string>();
                        
                        for (int value = 1; value < _configRaw[j].Length; value++)
                        {
                            lineList.Add(_configRaw[j][value]);
                        }

                        min_age = lineList[0];
                    }
                }

                if (_configRaw[i][0] == "default_color")
                {

                    for (int j = i; j < _configRaw.Length; j++)
                    {
                        List<string> lineList = new List<string>();

                        for (int value = 1; value < _configRaw[j].Length; value++)
                        {
                            lineList.Add(_configRaw[j][value]);
                        }

                        default_color = lineList[0];
                    }
                }


            }
            
            Type configType = typeof(Config);
            ConstructorInfo[] configConstructorsInfo = configType.GetConstructors();
            ConstructorInfo configConstructor = configConstructorsInfo[0];
            Config config = (Config)configConstructor.Invoke(new object[]{itemsList.ToArray(),otherItemsList.ToArray(), min_age, default_color});
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
