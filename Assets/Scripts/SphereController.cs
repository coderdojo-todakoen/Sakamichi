using UnityEngine;

public class SphereController : MonoBehaviour
{
    // 球がカメラに表示されたかどうか
    // カメラに表示されない球が画面下に落ちた時に
    // ミスとしてカウントしないようにします
    public bool visible;

    // 爆発するまでに必要なクリック回数(デフォルトは1回)
    // MainControllerで、Sphereを生成した時に設定します
    public int count = 1;

    void Update()
    {
        if (transform.position.y < -30)
        {
            // 画面上の坂道以外の場所に落ちた場合に
            // 球を削除します
            GameObject.Destroy(gameObject);
        }
    }

    // 球がカメラに表示されている間、呼び出されます
    void OnWillRenderObject()
    {
#if UNITY_EDITOR
    	if (Camera.current.name != "SceneCamera"  && Camera.current.name != "Preview Camera")
#endif
        {
            if (!visible)
            {
                // カメラに表示されたことを記録します
                visible = true;
            }
        }
    }

    // 球がクリックされた場合に
    // Event Triggerのコンポーネントから呼び出されます
    public void OnClick()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer.enabled)
        {
            // 非表示なら何もしません
            return;
        }

        GameObject plane = GameObject.Find("Plane");

        // 爆発までのクリック回数をチェックします
        switch (--count)
        {
            case 2:
            case 1:
                // 上向きの力を加えます
                GetComponent<Rigidbody>().AddForce(plane.transform.forward * 10, ForceMode.Impulse);
                return;
            default:
                break;
        }

        // 球を爆発させます

        // 球を非表示にします
        meshRenderer.enabled = false;

        // 爆発の音を鳴らします
        GetComponent<AudioSource>().Play();

        // ParticleSystemを開始します
        transform.GetChild(0).gameObject.transform.forward = plane.transform.up;
        transform.GetChild(0).gameObject.SetActive(true);

        // 何番目の数字を表示します
        Vector3 position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        GameObject.Find("Main").GetComponent<MainController>().ShowCount(position);
    }
}
