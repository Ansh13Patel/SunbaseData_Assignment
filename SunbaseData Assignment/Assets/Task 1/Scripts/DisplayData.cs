using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayData : MonoBehaviour
{
    public GameObject dataButtonPrefap;
    public Transform parentPanel;
    public TMP_Dropdown options;
    public GameObject detailsPanel;

    private APICallManager apiCallManager = new APICallManager();
    private ApiResponse apiResponse;
    private Dictionary<GameObject, string> dataMap = new Dictionary<GameObject, string>();
    private Option option;

    private void Awake()
    {
        StartCoroutine(WaitForAPIResponse());
        option = Option.ALL;
        options.SetValueWithoutNotify(0);
        detailsPanel.SetActive(false);
    }

    private IEnumerator WaitForAPIResponse()
    {
        yield return StartCoroutine(apiCallManager.FetchJSONData());
        
        apiResponse = apiCallManager.GetApiResponse();
        ShowAll();
    }

    private void ShowAll()
    {
        ClearAll();
        foreach (ClientInfo clientInfo in apiResponse.clients)
        {
            createButton(clientInfo);
        }
    }

    private void ShowNonManager()
    {
        ClearAll();
        foreach (ClientInfo clientInfo in apiResponse.clients)
        {
            if (!clientInfo.isManager)
            {
                createButton(clientInfo);
            }
        }
    }

    private void ShowManager()
    {
        ClearAll();
        foreach (ClientInfo clientInfo in apiResponse.clients)
        {
            if (clientInfo.isManager)
            {
                createButton(clientInfo);
            }
        }
    }

    private void ClearAll()
    {
        foreach(GameObject gameObject in dataMap.Keys)
        {
            Destroy(gameObject);
        }
        dataMap.Clear();
    }

    public void HandleInput(int val)
    {
        if(val == 0)
        {
            if (option.Equals(Option.ALL)) return;
            ShowAll();
            option = Option.ALL;
        }
        else if(val == 1)
        {
            if (option.Equals(Option.NON_MANAGER)) return;
            ShowNonManager();
            option = Option.NON_MANAGER;
        }
        else if(val == 2)
        {
            if (option.Equals(Option.MANAGER)) return;
            ShowManager();
            option = Option.MANAGER;
        }
    }

    private void ShowDetails()
    {
        GameObject buttonPress = EventSystem.current.currentSelectedGameObject;
        string id = dataMap[buttonPress];
        string details = "";

        if (apiResponse.data.ContainsKey(id))
        {
            ClientData data = apiResponse.data[id];

            details += "Name : " + data.name + "\n";
            details += "Address : " + data.address + "\n";
            details += "Points : " + data.points;
        }
        else
        {
            details += "No Data Available";
        }

        detailsPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = details;
        detailsPanel.SetActive(true);
    }

    private void createButton(ClientInfo clientInfo)
    {
        GameObject newData;
        string dataToDisplay = "";
        dataToDisplay += "Label : " + clientInfo.label + "\n" + "Points : ";
        if (apiResponse.data.ContainsKey(clientInfo.id))
            dataToDisplay += apiResponse.data[clientInfo.id.ToString()].points.ToString();
        else
            dataToDisplay += "NA";

        newData = Instantiate(dataButtonPrefap, parentPanel);
        newData.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dataToDisplay;
        newData.GetComponent<Button>().onClick.AddListener(delegate () { this.ShowDetails(); });

        dataMap.Add(newData, clientInfo.id);
    }
}