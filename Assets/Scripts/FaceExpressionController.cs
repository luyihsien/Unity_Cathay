using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceExpressionController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Dictionary<string, Sprite> expressions = new Dictionary<string, Sprite>();
    public InputField inputField;
    public float shakeDuration = 1f;  // 搖頭的持續時間
    public float shakeInterval = 0.1f;  // 搖頭的間隔時間
    public float speakDuration = 2f;  // 說話的持續時間
    public float speakInterval = 0.2f;  // 開口和閉口的間隔時間

    private void Start()
    {
        expressions.Add("無", Resources.Load<Sprite>("Faces/無表情"));
        expressions.Add("左", Resources.Load<Sprite>("Faces/左擺"));
        expressions.Add("右", Resources.Load<Sprite>("Faces/頭往右"));
        expressions.Add("開口", Resources.Load<Sprite>("Faces/開口"));
        expressions.Add("閉口", Resources.Load<Sprite>("Faces/閉口"));
    }

    public void ChangeExpression()
    {
        string input = inputField.text;

        if (expressions.ContainsKey(input))
        {
            spriteRenderer.sprite = expressions[input];
        }
        else if(input == "搖頭")
        {
            StartCoroutine(ShakeHead());
        }
        else if(input == "說話")
        {
            StartCoroutine(Speak());
        }
        else
        {
            Debug.LogError("No expression or action found for input: " + input);
        }
    }

    private IEnumerator ShakeHead()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            spriteRenderer.sprite = expressions["左"];
            yield return new WaitForSeconds(shakeInterval);
            spriteRenderer.sprite = expressions["右"];
            yield return new WaitForSeconds(shakeInterval);
            elapsedTime += 2 * shakeInterval;
        }
        spriteRenderer.sprite = expressions["無"];
    }

    private IEnumerator Speak()
    {
        float elapsedTime = 0f;
        while (elapsedTime < speakDuration)
        {
            spriteRenderer.sprite = expressions["開口"];
            yield return new WaitForSeconds(speakInterval);
            spriteRenderer.sprite = expressions["閉口"];
            yield return new WaitForSeconds(speakInterval);
            elapsedTime += 2 * speakInterval;
        }
        spriteRenderer.sprite = expressions["無"];
    }
}
