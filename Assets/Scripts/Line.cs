using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Line : MonoBehaviour {
    [SerializeField] private LineRenderer _renderer;
    [SerializeField] private EdgeCollider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject object1; // 物体1
    [SerializeField] private GameObject object2; // 物体2

    private readonly List<Vector2> _points = new List<Vector2>();
    private bool drawing = true;

    void Start() {
        _collider.transform.position -= transform.position;
        _rb.isKinematic = true;
    }

    void Update() {
        if (drawing && Input.GetKeyUp(KeyCode.Space)) {
            FinishDrawing();
        }
    }
        public void SetPosition(Vector2 pos) {
        if (!CanAppend(pos)) return;

        if (_points.Count == 0) {  // 如果是第一次畫點
            StartCoroutine(DelayedDestroy());
        }

        _points.Add(pos);
        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);
        _collider.points = _points.ToArray();
    }
	  private IEnumerator DelayedDestroy() {
        yield return new WaitForSeconds(5f);  // 等待五秒

        // 方法1: 直接銷毀物件
        Destroy(gameObject);

        // 方法2: 如果你只是想要"隱藏"線條而不銷毀它，可以禁用渲染器和碰撞器：
        // _renderer.enabled = false;
        // _collider.enabled = false;
    }


    private bool CanAppend(Vector2 pos) {
        if (_renderer.positionCount == 0) return true;
        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    private void FinishDrawing() {
        drawing = false;
        _rb.isKinematic = false;

        // 检测线的两端是否与物体碰撞
        CheckEndsCollision();
    }

    private void CheckEndsCollision() {
        // 对线条的起始和结束点使用小的OverlapCircle进行检测
        Collider2D startCollider = Physics2D.OverlapCircle(_points[0], 0.1f);
        Collider2D endCollider = Physics2D.OverlapCircle(_points[_points.Count - 1], 0.1f);

        if (startCollider != null && endCollider != null) {
            Debug.Log("123");
            if (startCollider.gameObject == object1 && endCollider.gameObject == object2) {
                // 播放物体1的动画
                Animator animator = object1.GetComponent<Animator>();
                if (animator != null) {
                    animator.Play("YourAnimationName");  // 替换为你的动画名
                }

                // 销毁物体2
                Destroy(object2);
            }
        }
    }
}

