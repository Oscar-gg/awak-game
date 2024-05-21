using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzureAdOauth : BaseServiceBootstrapper
{
    [SerializeField] private ClientDataObject azureAdClientDataObject;
    [SerializeField] private ClientDataObject azureAdClientDataObjectEditorOnly;

    protected override void RegisterServices()
    {
        OpenIDConnectService oidc = new OpenIDConnectService();
        oidc.OidcProvider = new AzureAdOidcProvider("");
        //oidc.OidcProvider = new GoogleOidcProvider();
        //oidc.OidcProvider = new GitHubOidcProvider();

#if !UNITY_EDITOR
        oidc.OidcProvider.ClientData = azureAdClientDataObject.clientData;
        oidc.RedirectURI = "a";
        oidc.ServerListener.ListeningUri = "http://localhost:52229/";
#else
        oidc.OidcProvider.ClientData = azureAdClientDataObject.clientData;
        oidc.RedirectURI = "https://www.tec.mx/";
        oidc.ServerListener.ListeningUri = "http://localhost:52229/";
#endif

        ServiceManager.RegisterService(oidc);

    }

    protected override void UnRegisterServices()
    {
        throw new System.NotImplementedException();
    }



}
