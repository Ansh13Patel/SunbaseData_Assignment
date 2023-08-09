using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class APICallManager
{
    private ApiResponse apiResponse;
    private const string API_URL = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public IEnumerator FetchJSONData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                apiResponse =  JsonConvert.DeserializeObject<ApiResponse>(jsonResponse.ToString());
            }
        }
    }

    public ApiResponse GetApiResponse()
    {
        return apiResponse;
    }
}
