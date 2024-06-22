using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour
{
    private Color originalColor;
    private Button lastPressedButton;

    void Start()
    {
        // 最初のボタンの色を取得
        originalColor = GetComponent<Button>().colors.normalColor;
    }

    // ボタンがクリックされたときに呼び出されるメソッド
    public void OnButtonClick(Button button)
    {
        // 最初に押されたボタンがまだないか、直前に押されたボタンと異なる場合
        if (lastPressedButton == null || lastPressedButton != button)
        {
            // 直前に押されたボタンがあれば、そのボタンの色を元の色に戻す
            if (lastPressedButton != null)
            {
                ColorBlock lastButtonColors = lastPressedButton.colors;
                lastButtonColors.normalColor = originalColor;
                lastPressedButton.colors = lastButtonColors;
            }

            // 押されたボタンの色を赤色に変更
            ColorBlock buttonColors = button.colors;
            buttonColors.normalColor = Color.red;
            button.colors = buttonColors;

            // 直前に押されたボタンを更新
            lastPressedButton = button;
        }
    }
}
