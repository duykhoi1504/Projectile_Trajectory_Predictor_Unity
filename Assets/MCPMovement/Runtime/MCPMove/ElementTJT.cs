namespace MCPMovement.Runtime.MCPMove.LogicElement
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElementTJT : MonoBehaviour
{
    private Transform headHolder;
    private Transform trailHolder;
    private List<TrailRenderer> trails;
    private List<SpriteRenderer> sprites;
    private float duration;
    public void SetUp(Transform headHolder, Transform trailHolder, List<TrailRenderer> trails, List<SpriteRenderer> sprites)
    {

        this.headHolder = headHolder;
        this.trailHolder = trailHolder;
        this.trails = trails;
        this.sprites = sprites;
    }
    public void Init(float duration)
    {
        this.duration = duration;
    }
    public void OnUpdate(float time)
    {

        if (time > duration)
        {
            SetActiveParent(headHolder, false);
        }

    }



    public void ResetElement(Vector3 start)
    {
        if (trails != null)
        {
            foreach (var trail in trails)
            {

                if (trail != null) // Kiểm tra null trước khi gọi phương thức
                {
                    trail.enabled = false;
                    trail.Clear(); // Xóa toàn bộ trail
                }
            }
        }
        transform.position = start;
        SetActiveParent(headHolder, false);

        // // Đợi 1 frame trước khi bật lại
        StartCoroutine(EnableTrailAndSprite());

    }

    private IEnumerator EnableTrailAndSprite()
    {
        yield return null; // Đợi 1 frame

        if (trails != null)
        {
            foreach (var trail in trails)
            {
                if (trail != null)
                {
                    trail.enabled = true; 
                }
            }
        }
        SetActiveParent(headHolder, true);
        SetActiveParent(trailHolder, true);
    }
    private void SetActiveParent(Transform parent, bool isActive)
    {
        if (parent != null)
        {
            parent.gameObject.SetActive(isActive);
        }
    }
}
}