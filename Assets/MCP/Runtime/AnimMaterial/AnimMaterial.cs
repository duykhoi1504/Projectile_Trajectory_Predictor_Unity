
namespace MCP.Runtime
{

    using UnityEngine;
    using DG.Tweening;
    public class AnimMaterial : MonoBehaviour
    {
        private Renderer red;
        private MaterialPropertyBlock materialPropertyBlock;

        void Start()
        {
            red = GetComponent<Renderer>();
            materialPropertyBlock = new MaterialPropertyBlock();

        }

        // Update is called once per frame



        public void ImpactAnim()
        {




            // Tạo hiệu ứng offset Y UV
            DOTween.To(() => GetFloatProperty(CONSTANTSHADER.offsetUvYID),
                       x => SetFloatProperty(CONSTANTSHADER.offsetUvYID, x),
                       -0.08f,
                        0.2f)
                   .SetEase(Ease.Unset)
                   .OnComplete(() =>
                   {
                       DOTween.To(() => GetFloatProperty(CONSTANTSHADER.offsetUvYID),
                                 x => SetFloatProperty(CONSTANTSHADER.offsetUvYID, x),
                                 0, 0.2f)
                             .SetEase(Ease.Unset);
                   });



            // Tạo hiệu ứng hit effect
            DOTween.To(() => GetFloatProperty(CONSTANTSHADER.hitEffectBlendID),
                        x => SetFloatProperty(CONSTANTSHADER.hitEffectBlendID, x),
                        1f, .1f)
                    .OnComplete(() =>
                    {
                        DOTween.To(() => GetFloatProperty(CONSTANTSHADER.hitEffectBlendID),
                        x => SetFloatProperty(CONSTANTSHADER.hitEffectBlendID, x),
                        0, .1f);
                    }).SetLoops(2);

        }

        private float GetFloatProperty(int propertyID)
        {

            //sao chép các thuộc tính hiện tại từ red vào biến materialPropertyBlock
            red.GetPropertyBlock(materialPropertyBlock);

            return materialPropertyBlock.GetFloat(propertyID);
        }


        private void SetFloatProperty(int propertyID, float value)
        {

            red.GetPropertyBlock(materialPropertyBlock);

            materialPropertyBlock.SetFloat(propertyID, value);

            // áp dụng các thay đổi đã thực hiện trong materialPropertyBlock lại vào red
            red.SetPropertyBlock(materialPropertyBlock);
        }

    }
}