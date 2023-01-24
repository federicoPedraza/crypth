using System.Collections;
using System.Collections.Generic;
using MetafabSdk;
using UnityEngine;

public class User
{
    public string id;
    public string gameId;
    public string walletId;
    public string connectedWalletId;
    public string profileId;
    public string profileAuthorizationId;
    public string username;
    public string accessToken;
    public string updatedAt;
    public string createdAt;
    public WalletModel wallet;

    public User(AuthPlayer200Response data)
    {
        id = data.id;
        gameId = data.gameId;
        walletId = data.walletId;
        connectedWalletId = data.connectedWalletId;
        profileId = data.profileId;
        profileAuthorizationId = data.profileAuthorizationId;
        username = data.username;
        accessToken = data.accessToken;
        updatedAt = data.updatedAt;
        createdAt = data.createdAt;
        wallet = data.wallet;
    }
}
