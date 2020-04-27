using UnityEngine;

public class TextController : MonoBehaviour
{
    public void OnEndAnimation()
    {
        // 何番目の爆発かを表示するTextのアニメーションが完了したら
        // Textを削除します
        GameObject.Destroy(gameObject);
    }
}
