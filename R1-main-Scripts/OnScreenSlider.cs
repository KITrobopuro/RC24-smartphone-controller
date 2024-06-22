using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class OnScreenSlider : OnScreenControl
{
    // 参照先のスライダー
    private Slider _slider;

    private void Awake()
    {
        // 初期化時に、スライダー操作のイベントリスナ登録
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    // スライダーが操作されたときに呼ばれるイベント
    private void OnValueChanged(float value)
    {
        // ここでControl Pathの入力値を変更する
        SendValueToControl(value);
    }

    // Control Pathの文字列を格納するフィールド
    [InputControl(layout = "Button"), SerializeField]
    private string _controlPath;

    // Control Pathを返すプロパティ
    // ここで返すパスに入力値が上書きされる
    protected override string controlPathInternal
    {
        get => _controlPath;
        set => _controlPath = value;
    }
}