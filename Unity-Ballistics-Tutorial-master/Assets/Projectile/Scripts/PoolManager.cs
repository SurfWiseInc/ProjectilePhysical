using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager SharedInstance;
    [SerializeField]
    private GameObject _bulletContainer;
    [SerializeField]
    private GameObject _bulletPrefab;

    private List<GameObject> _bulletPool = new List<GameObject>();

    private void Awake()
    {
        SharedInstance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        _bulletPool = GeneratePool(10);    
    }

    List<GameObject> GeneratePool(int poolLenghet)
    {
        for(int i = 0; i < poolLenghet; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.transform.parent = _bulletContainer.transform;
            bullet.SetActive(false);
            _bulletPool.Add(bullet);
        }
        return _bulletPool;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RequestBullet()
    {
        foreach (var bullet in _bulletPool)
        {
            if(bullet.activeInHierarchy == false)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(_bulletPrefab);
        newBullet.transform.parent = _bulletContainer.transform;
        _bulletPool.Add(newBullet);
        return newBullet;
    }


}
