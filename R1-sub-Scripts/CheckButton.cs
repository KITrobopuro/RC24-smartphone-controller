using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    //ボタンをキャッシュする変数
    Button btn;
	bool btnChangeFlag = true;

    //ここでカラーを設定
    static readonly Color btnColor1 = Color.red;
    static readonly Color btnColor2 = Color.blue;

	void Awake()
	{
		//何度もアクセスするのでこの変数にキャッシュ
        btn = gameObject.GetComponent<Button>();
        btn.image.color = btnColor1;
	}

    void Start()
    {
        btn.onClick.AddListener( OnClick );
    }

    public void OnClick()
	{
        btnChangeFlag = !btnChangeFlag;
        btn.image.color = btnChangeFlag ? btnColor1 : btnColor2;
	}
}