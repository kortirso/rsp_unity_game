using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(CheckServer());
  }

  IEnumerator CheckServer() {
    UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/api/v1/status.json");
    yield return www.SendWebRequest();

    if (www.result != UnityWebRequest.Result.Success) {
      Debug.Log(www.error);
    }
    else {
      print(www.downloadHandler.data);
      print(www.downloadHandler.text);
    }
  }
}
