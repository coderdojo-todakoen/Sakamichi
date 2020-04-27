using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    // 球のプレハブ
    private GameObject sphere;
    // 何番目の爆発かを表示するためのTextの親となるCanvas
    private GameObject canvas;
    // 何番目の爆発かを表示するためのTextのプレハブ
    private GameObject prefab;

    // 経過時間
    private float time;
    // 何番目の爆発かをカウントします
    private int count;

    void Start()
    {
        // ランダムに球を生成するためのプレハブを取得します
        sphere = Resources.Load<GameObject>("Prefabs/Sphere");

        // 球が爆発した時に、何番目かを表示するためのCanvasと
        // Textのプレハブを取得します
        canvas = GameObject.Find("Canvas");
        prefab = Resources.Load<GameObject>("Prefabs/Text");
    }

    void Update()
    {
        // 経過時間をカウントします
        time += Time.deltaTime;

        if (time > 1) {
            // 1秒間に1回球を生成します
            // 表示上の左右(-5〜5)と、落とす高さ(10〜30)をランダムに決定します
            Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(10, 30), 10);
            GameObject obj = GameObject.Instantiate(sphere, position, Quaternion.identity);

            if (Random.value < 0.15F)
            {
                // 生成した球のうち15%の割合で、3回クリックしなければ
                // 爆発しないようにします
                SphereController s = obj.GetComponent<SphereController>();
                s.count = 3;
                MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                mr.material.color = Color.red;
            }

            // 経過時間を0に戻します
            time = 0;
        }
    }

    // 球がクリックされて爆発した時に呼び出されます
    // 何番目の爆発かを表示するTextを生成します
    public void ShowCount(Vector3 position)
    {
        GameObject obj = GameObject.Instantiate(prefab, position, Quaternion.identity, canvas.transform);
        obj.GetComponent<Text>().text = (++count).ToString();
    }
}
