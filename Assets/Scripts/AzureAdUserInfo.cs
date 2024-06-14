using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace i5.Toolkit.Core.OpenIDConnectClient
{
    /// <summary>
    /// Description of the user information data for the Git Hub client
    /// </summary>
    public class AzureAdUserInfo : AbstractUserInfo
    {

        [SerializeField] protected string family_name;
        [SerializeField] protected string given_name;
        [SerializeField] protected string sub;
        [SerializeField] protected string unique_name;
        [SerializeField] protected string upn;

        public virtual string Upn { get => upn; }
        public virtual string Sub { get => sub; }
        public virtual string Unique_name { get => Unique_name; }
        public override string Email { get => upn; }
    }
}
