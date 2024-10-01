using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnim : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] MaterialPropertyBlock materialPropertyBlock;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
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
        renderer.GetPropertyBlock(materialPropertyBlock);
        return materialPropertyBlock.GetFloat(propertyID);
    }


    private void SetFloatProperty(int propertyID, float value)
    {
        renderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat(propertyID, value);
        renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
