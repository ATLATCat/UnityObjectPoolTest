using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform = null;

    [SerializeField] Gun _gun = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookMouse();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _gun.Fire();
        }
    }

    void LookMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(_playerTransform.position);

        Vector3 direction = mousePosition - playerPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
