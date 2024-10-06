using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    // 경로에서 해당 리소스를 찾아서 불러옵니다
    public T Load<T>(string path) where T : Object
    {
        T resource = Resources.Load<T>(path);

        if (resource == null)
        {
            Debug.Log($"{path}에 해당 리소스가 존재하지 않습니다");
        }

        return resource;
    }

    // 경로를 입력하면 해당 리소스를 불러와 게임 오브젝트로 생성합니다
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>(path);

        if (original == null)
        {
            return null;
        }

        GameObject go = Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    // 게임 오브젝트를 제거합니다
    public void Destroy(GameObject go, float t = 0f)
    {
        if (go == null)
        {
            return;
        }

        // 유니티 엔진의 Destroy() 함수를 호출합니다
        UnityEngine.Object.Destroy(go, t);
    }
}
