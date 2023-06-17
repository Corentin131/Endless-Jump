using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifier : MonoBehaviour
{
    public GameObject notificationObject;

    static Transform objectTransform;

    public Sprite sprite1;

    void Start()
    {
        print(Game.app.score);
        objectTransform = transform;
    }

    public void Notify(string tittle , Sprite sprite)
    {
        StartCoroutine(Game.app.notifier.MakeNotification(tittle,sprite));
    }

    public IEnumerator MakeNotification(string tittle,Sprite sprite)
    {
        GameObject notification = Instantiate(notificationObject,objectTransform.position,objectTransform.rotation,transform);

        GameObject textGameObject = notification.transform.Find("Text").gameObject;
        GameObject logoGameObject = notification.transform.Find("Logo").gameObject;

        TextMeshProUGUI text = textGameObject.GetComponent<TextMeshProUGUI>();
        Image image = logoGameObject.GetComponent<Image>();

        text.text = tittle;
        image.sprite = sprite;

        LeanTween.moveLocalY(notification,-48,0.8f).setEaseOutExpo();
        yield return new WaitForSeconds(3);
        LeanTween.moveLocalY(notification,0,0.8f).setEaseOutExpo();
    }
}
