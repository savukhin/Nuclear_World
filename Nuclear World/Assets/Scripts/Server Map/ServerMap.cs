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

    [Serializable]
    public class GetNewTranforms {
        [Serializable]
        public class Item {
            public string username;
            //public Transform transform;
            public JSONtypes.Transform transform;
        }
        public Item[] items;
    }
}

public class ServerMap : MonoBehaviour {
    private readonly HttpClient client = new HttpClient();
    private readonly Uri serverURI = new Uri("http://127.0.0.1:8000");
    public GameObject spawnPoint;
    public GameObject playerPrefab;
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private string CSRFtoken;
    CancellationTokenSource checkNewUsers_cts = new CancellationTokenSource();

    public void addPlayer(string username) {
        if (players.ContainsKey(username))
            return;
        players.Add(username, Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation));
        print("PLAYER " + username + " CONNECTED");
    }

    public void changeTransform(string username, JSONtypes.Transform JSONtransform) {
        GameObject player = players[username];
        print("username" + players);
        if (player == null)
            return;
        print("START TRANSFORM " + player.transform);
        player.transform.position = new Vector3(float.Parse(JSONtransform.positionX), 
                                                        float.Parse(JSONtransform.positionY), float.Parse(JSONtransform.positionZ));
        player.transform.rotation = new Quaternion(float.Parse(JSONtransform.rotationX), float.Parse(JSONtransform.rotationY), 
                                                        float.Parse(JSONtransform.rotationZ), float.Parse(JSONtransform.rotationW));
        print("END TRANSFORM " + player.transform);
    }

    public async Task checkNewUsers(int checkRate, CancellationToken token) {
        for (;;) {
            var response = await client.GetAsync(serverURI + "map/getNewUsersInfo", token);
            var responseString = await response.Content.ReadAsStringAsync();
            ServerMapResponses.GetNewUsers json;
            try {
                json = JsonUtility.FromJson<ServerMapResponses.GetNewUsers>(responseString);
                //print(json + " " + json.NewConnectedUsers.Length);
                foreach (var username in json.NewConnectedUsers)
                    addPlayer(username);
            } catch (Exception) {}
            await Task.Delay(checkRate);
        }
    }

    public async Task checkNewTransforms(int checkRate, CancellationToken token) {
        for (;;) {
            var response = await client.GetAsync(serverURI + "map/getNewTransformsInfo", token);
            var responseString = await response.Content.ReadAsStringAsync();
            ServerMapResponses.GetNewTranforms json;
            try {
                json = JsonUtility.FromJson<ServerMapResponses.GetNewTranforms>(responseString);
                foreach (var user in json.items) {
                    changeTransform(user.username, user.transform);
                }
            } catch (Exception) {}
            await Task.Delay(checkRate);
        }
    }

    public void checkEverything(CancellationToken token) {
        checkNewUsers(5000, token);
        checkNewTransforms(1000, token);
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
            PlayerPrefs.SetString("Server CSRF token", json.csrfToken);
            PlayerPrefs.SetString("Server Session ID", json.sessionID);
            checkEverything(checkNewUsers_cts.Token);
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
