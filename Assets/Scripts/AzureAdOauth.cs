using i5.Toolkit.Core.OpenIDConnectClient;
using i5.Toolkit.Core.ServiceCore;
using UnityEngine;

using CandyCoded.env;

public class AzureAdOauth : BaseServiceBootstrapper
{
    [SerializeField] private ClientDataObject azureAdClientDataObject;
    [SerializeField] private ClientDataObject azureAdClientDataObjectEditorOnly;

    protected override void RegisterServices()
    {
        OpenIDConnectService oidc = new OpenIDConnectService();

        string tenant_id = "";
        if (env.TryParseEnvironmentVariable("TENANT_ID", out string TENANT_ID))
        {
            tenant_id = TENANT_ID;
        } else
        {
            throw new System.Exception("Add TENANT_ID to enviroment variables file");

        }

        oidc.OidcProvider = new AzureAdOidcProvider(tenant_id);

#if !UNITY_EDITOR
        oidc.OidcProvider.ClientData = azureAdClientDataObject.clientData;
        oidc.RedirectURI = "a";
        oidc.ServerListener.ListeningUri = "http://localhost:52229/";
#else
        oidc.OidcProvider.ClientData = azureAdClientDataObject.clientData;
        oidc.ServerListener.ListeningUri = "http://localhost:52229/";
#endif

        ServiceManager.RegisterService(oidc);

    }

    protected override void UnRegisterServices()
    {
        
    }



}
