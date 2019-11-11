﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class gerAnuncios : MonoBehaviour
{
    public int ultimo_anuncio = 0;
    public Button botaoVideoDolares, botaoVideoGasolina;
    public GameObject menu_recompensa;
    private bool eventos1setados = false, eventos2setados = true;
    public static gerAnuncios instancia;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    private RewardBasedVideoAd anuncio_video_dolares, anuncio_video_gasolina;
    private string id_anuncio_video_dolares = "ca-app-pub-3940256099942544/5224354917", id_anuncio_video_gasolina = "ca-app-pub-3940256099942544/5224354917";

    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    public void Awake() {
        instancia = this;
    }

    public void Start()
    {

#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544~3347511713";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif

        MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        anuncio_video_dolares = RewardBasedVideoAd.Instance;
        anuncio_video_gasolina = RewardBasedVideoAd.Instance;

        prepararAnuncioDolares();
        prepararAnuncioGasolina();
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;

    }

    public void prepararAnuncioDolares(){
        AdRequest request_ = new AdRequest.Builder().Build();

        if(!eventos1setados){
            anuncio_video_dolares.OnAdLoaded += HandleRewardBasedVideoLoaded;
            anuncio_video_dolares.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            anuncio_video_dolares.OnAdRewarded += HandleRewardBasedVideoRewarded;
            anuncio_video_dolares.OnAdClosed += HandleRewardBasedVideoClosed;

            eventos1setados = true;
        }

        anuncio_video_dolares.LoadAd(request_,id_anuncio_video_dolares);
    }

    public void prepararAnuncioGasolina(){
        AdRequest request_ = new AdRequest.Builder().Build();

        if(!eventos2setados){
            anuncio_video_gasolina.OnAdLoaded += HandleRewardBasedVideoLoaded;
            anuncio_video_gasolina.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            anuncio_video_gasolina.OnAdRewarded += HandleRewardBasedVideoRewarded;
            anuncio_video_gasolina.OnAdClosed += HandleRewardBasedVideoClosed;

            eventos2setados = true;
        }

        anuncio_video_gasolina.LoadAd(request_,id_anuncio_video_gasolina);
    }

    public void mostrarAnuncioDolares(){
        if(anuncio_video_gasolina.IsLoaded()){
            ultimo_anuncio = 1;
            anuncio_video_gasolina.Show();
        }else
        {
            Debug.Log("anunucio dos dolares nao esta pronto");
        }
    }

    public void mostrarAnuncioGasolina(){
        if(anuncio_video_gasolina.IsLoaded()){
            ultimo_anuncio = 2;
            anuncio_video_gasolina.Show();
        }else
        {
            Debug.Log("anunucio da gasolina nao esta pronto");
        }
    }




    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        Debug.Log("Rewarded Video ad loaded successfully");

        botaoVideoDolares.interactable = anuncio_video_dolares.IsLoaded();

        botaoVideoGasolina.interactable = anuncio_video_gasolina.IsLoaded();

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Failed to load rewarded video ad : " + args.Message);


    }



    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        Debug.Log("You have been rewarded with  " + amount.ToString() + " " + type);

        if(ultimo_anuncio == 1){
            gerGames.instancia.recompensaVideo();
        }

        if(ultimo_anuncio == 2){
            menu_recompensa.GetComponent<menuReceberDolares>().receber(3);
        }


    }


    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        if(ultimo_anuncio == 1){
            prepararAnuncioDolares();
        }

        if(ultimo_anuncio == 2){
            prepararAnuncioGasolina();
        }

    }
}
