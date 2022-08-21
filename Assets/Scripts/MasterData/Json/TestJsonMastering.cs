using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJsonMastering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Network.WebRequest.Request<Network.WebRequest.GetString>("<https://script.google.com/macros/s/AKfycbyc6WmX57vj8_V5tRL7eN4QCWMcLUQx8Jtu_B_JyqnMRGxH0Uk/exec?sheet=Cube>", Network.WebRequest.ResultType.String, (string json) =>
        {
            var data = JsonUtility.FromJson<MasterData.MasterDataClass<MasterData.Cube>>(json);
            Debug.Log(data.Data[0].Id);
        });
    }
}
