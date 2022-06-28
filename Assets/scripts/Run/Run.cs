using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JuiceKit.Editor
{
    public class Run
    {
        [MenuItem("Utilities/Generate")]
        public static void Import()
        {
            ConfigsActions.ImportConfig();
        }
    }

}
