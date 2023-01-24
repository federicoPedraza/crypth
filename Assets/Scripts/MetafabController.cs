using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using MetafabSdk;
using TMPro;

public class MetafabController : MonoBehaviour
{
    [HideInInspector] public bool validatedGame;
    public User user;
    public bool IsLogged => user != null;
    public TextMeshProUGUI authMessage;

    [HideInInspector] public bool working;

    private async UniTask Start()
    {
        AuthGame();
    }

    public async UniTaskVoid AuthGame()
    {
        working = true;
        var request = await Metafab.GamesApi.AuthGame(Config.Email, Config.Password);
        Config.PublishedKey = request.publishedKey;
        Metafab.PublishedKey = Config.PublishedKey;
        Metafab.SecretKey = request.secretKey;
        Metafab.Password = Config.Password;

        validatedGame = true;
    }

    public async void Login(string username, string password)
    {
        try
        {
            working = true;
            var request = await Metafab.PlayersApi.AuthPlayer(username, password);
            user = new User(request);
        }
        catch (Exception exception)
        {
            authMessage.text = exception.Message;
        }
    }
    
    public async void SignIn(string username, string password)
    {
        try
        {
            working = true;
            var request = new CreatePlayerRequest(username, password);
            var response = await Metafab.PlayersApi.CreatePlayer(request);
            
            Login(username, password);
        }
        catch (Exception exception)
        {
            authMessage.text = exception.Message;
        }
    }
}
