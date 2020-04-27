using UnityEngine;

public class CubeController : MonoBehaviour
{
    private int miss;

    void OnTriggerEnter(Collider other)
    {
        // 画面下のCubeに球がぶつかったら呼び出されます
        MeshRenderer meshRenderer = other.gameObject.GetComponent<MeshRenderer>();
        SphereController sphere = other.gameObject.GetComponent<SphereController>();
        if (meshRenderer.enabled && sphere.visible)
        {
            // カメラに表示された球が、表示されたまま(クリックされずに)
            // 画面下まで落ちてしまった場合の処理をおこないます。
            // 現在の実装では、ミスした数をカウントするだけで何もしません
            ++miss;
            Debug.Log(miss);
        }

        // 球を削除します
        GameObject.Destroy(other.gameObject);
    }
}
