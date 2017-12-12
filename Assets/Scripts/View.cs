using UnityEngine;

public class View {
    public RectTransform transform { get; private set; }
    public GameObject gameObject { get; private set; }
    public NodeBind rootNodeBind { get; private set; }

    public View() {
        rootNodeBind = new NodeBind(null);
    }

    public void SetTransform(RectTransform transform) {
        this.transform = transform;
        gameObject = transform.gameObject;
        rootNodeBind.SetTransform(transform);
    }

    public void Show() {
        transform.gameObject.SetActive(true);
    }

    public void Hide() {
        transform.gameObject.SetActive(false);
    }
}