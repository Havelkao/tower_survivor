using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public UIDocument document;

    public void Hide()
    {
        document.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void Show()
    {
        document.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}