
namespace MCPMovement.Runtime
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public static class CONSTANTSHADER
    {



        public static int offsetUvYID = Shader.PropertyToID("_OffsetUvY");
        public static string offsetUvYID_ON = "OFFSETUV_ON";


        public static int hitEffectBlendID = Shader.PropertyToID("_HitEffectBlend");
        public static string hitEffectBlendID_ON = "HITEFFECT_ON";

    }
}