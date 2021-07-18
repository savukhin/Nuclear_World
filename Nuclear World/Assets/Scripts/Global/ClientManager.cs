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

namespace ClientManagerResponses {
    [Serializable]
    public class Error {
        public string subject;
        public string text;
        public Error(string subject, string text) {
            this.subject = subject;
            this.text = text;
        }
    }

    [Serializable]
    public class SignInResponse {
        public HttpStatusCode statusCode;
        public Error[] errors;
        public string csrfToken;
        public string sessionID;
        
        public SignInResponse(HttpStatusCode code, Error[] errors, string token) {
            statusCode = code;
            this.errors = errors;
            csrfToken = token;
        }
    }
}

public static class ClientManager {
    private static readonly HttpClient client = new HttpClient();
    private static readonly Uri serverURI = new Uri("http://127.0.0.1:8000");

    private static string parseCSRF(string response) {
        return response.Split('"')[5];
    }

    // returns CSRF token
    public static async Task<ClientManagerResponses.SignInResponse> SignIn(string username, string password) {
        var values = new Dictionary<string, string> {
            { "username", username },
            { "password", password }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await client.PostAsync(serverURI + "signin", content);
        var responseString = await response.Content.ReadAsStringAsync();
        ClientManagerResponses.SignInResponse json = JsonUtility.FromJson<ClientManagerResponses.SignInResponse>(responseString);
        json.statusCode = response.StatusCode;
        return json;
    }

    public static async Task SendTransform(Transform transform) {
        var json = JSONConvertation.TransformToJSON(transform);
        var content = new FormUrlEncodedContent(json);
        var response = await client.PostAsync(serverURI + "map/updateTransform", content);
    }
}
