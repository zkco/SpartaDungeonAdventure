using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject _itemDataUIPanel;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI ItemDescription;

    private Camera _camera;
    public LayerMask Interactable;
    public GameObject InteractObject;

    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        CharacterManager.Instance.Player.Controller.OnUseItem += OnInteract;
    }

    private void Update()
    {
        ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        OnLook();
    }

    private void OnLook()
    {
        if (Physics.Raycast(ray, out hit, 1f, Interactable))
        {
            if (hit.collider.gameObject.TryGetComponent<Item>(out Item item))
            {
                InteractObject = hit.collider.gameObject;
                ItemData itemData = InteractObject.GetComponent<Item>().ItemData;
                ItemName.text = itemData.ItemName;
                ItemDescription.text = itemData.ItemDescription;
                CharacterManager.Instance.Player.Item = InteractObject;
                _itemDataUIPanel.SetActive(true);
                return;
            }
        }
        CharacterManager.Instance.Player.Item = null;
        _itemDataUIPanel.SetActive(false);
        return;
    }
    private void OnInteract()
    {
        Debug.Log("interact");
        if (hit.collider.gameObject != null)
        {
            Destroy(hit.collider.gameObject, 0.01f);
        }
    }
}
