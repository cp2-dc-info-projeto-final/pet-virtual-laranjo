using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class gerCompras : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string id_produto_99_dolares =    "99dolares";   
    public static string id_produto_499_dolares = "499dolares";
    public static string id_produto_999_dolares =  "999dolares"; 

    public GameObject menu_dolares, menu_moedas;
    public GameObject erro_;


    // Google Play Store-specific product identifier subscription product.
    //private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
    }

    public void InitializePurchasing() 
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(id_produto_99_dolares, ProductType.Consumable);
        builder.AddProduct(id_produto_499_dolares, ProductType.Consumable);
        builder.AddProduct(id_produto_999_dolares, ProductType.Consumable);



        // Continue adding the non-consumable product.

        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);

        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 

        /*builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){

            { kProductNameGooglePlaySubscription, GooglePlay.Name },
            
        });*/

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void comprar99Dolares()
    {
        ComprarProdutoID(id_produto_99_dolares);
    }

    public void comprar499Dolares()
    {
        ComprarProdutoID(id_produto_499_dolares);
    }

    public void comprar999Dolares()
    {
        ComprarProdutoID(id_produto_999_dolares);
    }


    public void comprar999Moedas(){
        if(gerDados.instancia.dados_.dolares >= 1){

        }
        gerDados.instancia.dados_.dolares -= 1;
            menu_moedas.GetComponent<menuReceberDolares>().receberMoeda(999);

        gerDados.instancia.salvar(true);
    }

    public void comprar9999Moedas(){
        if(gerDados.instancia.dados_.dolares >= 1){
            gerDados.instancia.dados_.dolares -= 10;
            menu_moedas.GetComponent<menuReceberDolares>().receberMoeda(9999);

            gerDados.instancia.salvar(true);
        }
        
    }

    public void comprar99999Moedas(){
        if(gerDados.instancia.dados_.dolares >= 1){
            gerDados.instancia.dados_.dolares -= 100;
            menu_moedas.GetComponent<menuReceberDolares>().receberMoeda(99999);

            gerDados.instancia.salvar(true);
        }
        
    }


    void ComprarProdutoID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
    {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, id_produto_99_dolares, StringComparison.Ordinal))
        {
            menu_dolares.GetComponentInParent<menuReceberDolares>().receber(99);
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, id_produto_499_dolares, StringComparison.Ordinal))
        {
            menu_dolares.GetComponentInParent<menuReceberDolares>().receber(499);
        }
        // Or ... a subscription product has been purchased by this user.
        else if (String.Equals(args.purchasedProduct.definition.id, id_produto_999_dolares, StringComparison.Ordinal))
        {
            menu_dolares.GetComponentInParent<menuReceberDolares>().receber(999);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else 
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            erro_.SetActive(true);
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.

        erro_.SetActive(true);
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

}
