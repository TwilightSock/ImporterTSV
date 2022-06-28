using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JuiceKit
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config")]
    class ConfigSO : BaseConfigSO<Config>
    {
        protected override void ParseRaw(string[][] _configRaw)
        {
            Items items = new Items(new List<string>(){_configRaw[2][1],_configRaw[3][1],_configRaw[4][1]}, 
                new List<int>(){ int.Parse(_configRaw[2][2]), int.Parse(_configRaw[3][2]), int.Parse(_configRaw[4][2]) },
                new List<string>() { _configRaw[2][3], _configRaw[3][3], _configRaw[4][3] });
            
            OtherItems otherItems = new OtherItems(new List<string>(){_configRaw[6][1],_configRaw[7][1],_configRaw[8][1]},
                new List<bool>(){ bool.Parse(_configRaw[6][2]), bool.Parse(_configRaw[7][2]), bool.Parse(_configRaw[8][2]) },
                new List<string>(){ _configRaw[6][3], _configRaw[7][3], _configRaw[8][3] });

            config = new Config(items, otherItems, int.Parse(_configRaw[9][1]), _configRaw[10][1]);
        }

        protected override void Validate()
        {
           
        }
    }


}
