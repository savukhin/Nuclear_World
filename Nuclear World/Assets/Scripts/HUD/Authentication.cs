using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour {
    public InputField usernameField;
    public InputField passwordField;
    public Text errorText;

    public async void SignIn() {
        string username = usernameField.text;
        string password = passwordField.text;
        ClientManagerResponses.SignInResponse t = await ClientManager.SignIn(username, password);
        if (t.statusCode != System.Net.HttpStatusCode.OK) {
            foreach (var error in t.errors)
                errorText.text = error.text;
        } else {
            errorText.text = "SUCCESS";
            PlayerPrefs.SetString("CSRF token", t.csrfToken);
            SceneManager.LoadScene("Client");
        }
    }
}
