using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading;


namespace ServerMapResponses {
    public class GetNewUsers {
        public string[] NewConnectedUsers;
        public string[] NewDiconnectedUsers;
    }
}

public class ServerMap : MonoBehaviour {
    private readonly HttpClient client = new HttpClient();
    private readonly Uri serverURI = new Uri("http://127.0.0.1:8000");
    public GameObject spawnPoint;
    public GameObject playerPrefab;
    private List<GameObject> players;
    private string CSRFtoken;
    CancellationTokenSource checkNewUsers_cts = new CancellationTokenSource();

    public void addPlayer(string username) {
        print("INSTANTIATE");
        players.Add(Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation));
    }

    public async void checkNewUsers(CancellationToken token) {
        for (;;) {
            var response = await client.GetAsync(serverURI + "map/getNewUsersInfo", token);
            var responseString = await response.Content.ReadAsStringAsync();
            ServerMapResponses.GetNewUsers json;
            try {
                json = JsonUtility.FromJson<ServerMapResponses.GetNewUsers>(responseString);
                print(json + " " + json.NewConnectedUsers.Length);
                foreach (var username in json.NewConnectedUsers)
                    addPlayer(username);
            } catch (Exception) {}
            await Task.Delay(2000);
        }
    }

    // Start is called before the first frame update
    async void Start() {
        var values = new Dictionary<string, string> {
            { "username", "admin" },
            { "password", "admin" }
        };
        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(serverURI + "signin", content);
        var responseString = await response.Content.ReadAsStringAsync();
        ClientManagerResponses.SignInResponse json = JsonUtility.FromJson<ClientManagerResponses.SignInResponse>(responseString);
        json.statusCode = response.StatusCode;
        if (json.statusCode != System.Net.HttpStatusCode.OK) {
            foreach (var error in json.errors)
                print(error.text);
            Destroy(gameObject);
            return;
        } else {
            print("SUCCESSFUL CONNECTION");
            //PlayerPrefs.SetString("Server CSRF token", json.csrfToken);
            checkNewUsers(checkNewUsers_cts.Token);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnDestroy() {
        checkNewUsers_cts.Cancel();
        print("DESTROY");
    }
}
