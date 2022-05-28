using LitJson;
using UnityEngine;

namespace gamejam
{
    public class Util
    {
        public static JsonData LoadJson(string filePath)
        {
            TextAsset bindata = Resources.Load(filePath) as TextAsset;
            return JsonMapper.ToObject(bindata.ToString());
        }
    }
}
