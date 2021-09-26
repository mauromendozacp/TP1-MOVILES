using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] RectTransform stick;
    [SerializeField] Image background;
    public float joystickLimit;

    public enum Lado
    {
        Izq,
        Der
    }

    public Lado lado;

    Vector2 ConvertToLocalPos(PointerEventData eventData)
    {
        Vector2 newPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.enterEventCamera, out newPos);
        return newPos;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 stickPos = ConvertToLocalPos(eventData);
        if (stickPos.magnitude > joystickLimit)
        {
            stickPos = stickPos.normalized * joystickLimit;
        }
        if (stick != null)
            stick.anchoredPosition = stickPos;

        float x = stickPos.x / joystickLimit;
        float y = stickPos.y / joystickLimit;

        if (lado == Lado.Izq)
            InputManager.Instance.SetAxis("Horizontal1", x);
        else if (lado == Lado.Der)
            InputManager.Instance.SetAxis("Horizontal2", x);
        InputManager.Instance.SetAxis("Vertical", y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (stick != null && background != null)
        {
            background.color = new Color(0f, 0f, 1f, 0.059f);
            stick.anchoredPosition = ConvertToLocalPos(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (stick != null && background != null)
        {
            background.color = new Color(1f, 1f, 1f, 0.059f);

            stick.anchoredPosition = Vector2.zero;
        }
        if (lado == Lado.Izq)
            InputManager.Instance.SetAxis("Horizontal1", 0);
        else if (lado == Lado.Der)
            InputManager.Instance.SetAxis("Horizontal2", 0);
        InputManager.Instance.SetAxis("Vertical", 0);
    }

    void OnDisable()
    {
        if (lado == Lado.Izq)
            InputManager.Instance.SetAxis("Horizontal1", 0);
        else if (lado == Lado.Der)
            InputManager.Instance.SetAxis("Horizontal2", 0);
        InputManager.Instance.SetAxis("Vertical", 0);
    }
}