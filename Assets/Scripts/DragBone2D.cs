using UnityEngine;

public class DragBone2D : MonoBehaviour
{
    public GameObject character; // 你的角色物件，確保這裡設定正確
    private bool isDragging = false;
    private Transform boneTransform;

    private void Update()
    {
        if (isDragging && boneTransform != null)
        {
            // 獲取滑鼠在世界座標的位置
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // 更新骨骼的位置
            boneTransform.position = mousePosition;
        }

        // 如果玩家放開鼠標，停止拖曳
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(12323);
        // 在玩家點擊時尋找名為 "BoneName" 的骨骼
        boneTransform = character.transform.Find("bone_2");
        
        if(boneTransform != null)
        {
            Debug.Log(1231232342342134);
            isDragging = true; // 開始拖曳
        }
    }
}
