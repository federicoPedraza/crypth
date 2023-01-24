using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Pixelation.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[RequireComponent(typeof(MetafabController))]
public class MetafabMainMenuController : MonoBehaviour
{
    public GameObject metafabUI;
    public GameObject mainUI;
    public GameObject player;
    public GameObject enviroment;

    private Pixelation _pixelation;
    private MetafabController _controller;
    
    
    [Header("Log in")] 
    public TMP_InputField login_username;
    public TMP_InputField login_password;
    public Button login_button;
    
    [Header("Sign in")]
    public TMP_InputField signin_username;
    public TMP_InputField signin_password;
    public TMP_InputField signin_password_confirm;
    public Button signin_button;

    private void Start()
    {
        _pixelation = FindObjectOfType<Pixelation>();
        _controller = GetComponent<MetafabController>();
        _pixelation.BlockCount = 512;
    }

    private void Update()
    {
        float _pIntensity = _controller.IsLogged ? 220 : 512;
        _pixelation.BlockCount = Mathf.Lerp(_pixelation.BlockCount, _pIntensity, Time.deltaTime * 25f);

        Vector3 enviromentStart = new Vector3(0, -20, 0);
        Vector3 enviromentEnd = new Vector3(0, -3, 0);
        enviroment.transform.position = Vector3.Lerp(enviroment.transform.position, _controller.IsLogged ? enviromentEnd : enviromentStart, Time.deltaTime * 5);
        player.SetActive(_controller.IsLogged);
        metafabUI.SetActive(!_controller.IsLogged && _controller.validatedGame);
        mainUI.SetActive(_controller.IsLogged && _controller.validatedGame);
        
        login_button.interactable = login_password.text.Length != 0 && login_username.text.Length != 0;
        signin_button.interactable = signin_password.text.Length != 0 && signin_password_confirm.text == signin_password.text && signin_username.text.Length != 0;
    }

    public void TryLogin()
    {
        string username = login_username.text;
        string password = login_password.text;

        _controller.Login(username, password);
    }
    
    public void TrySignIn()
    {
        string username = signin_username.text;
        string password = signin_password.text;

        _controller.SignIn(username, password);
    }
}
