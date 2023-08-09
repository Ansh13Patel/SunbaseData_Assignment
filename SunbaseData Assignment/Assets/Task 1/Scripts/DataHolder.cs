using System.Collections.Generic;

[System.Serializable]
public class ClientInfo
{
    public bool isManager;
    public string id;
    public string label;
}

[System.Serializable]
public class ClientData
{
    public string address;
    public string name;
    public int points;
}

[System.Serializable]
public class ApiResponse
{
    public ClientInfo[] clients;
    public Dictionary<string, ClientData> data = new Dictionary<string, ClientData>();
    public string label;
}
