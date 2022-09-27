using Erdcsharp.Domain;
using Erdcsharp.Provider.Dtos;
using UnityEngine.UI;

namespace ElrondUnityExamples
{
    public class ConnectedScreen : GenericUIScreen
    {
        public Text address;
        public Text ht;
        public Text egldValue;

        public override void Init(params object[] args)
        {
            base.Init(args);
            RefreshAccount(DemoScript.Instance.GetConnectedAccount());
        }

        /// <summary>
        /// Refresh the address and the amount of tokens of the connected wallet 
        /// </summary>
        /// <param name="connectedAccount"></param>
        private void RefreshAccount(AccountDto connectedAccount)
        {
            var amount = TokenAmount.From(connectedAccount.Balance);
            address.text = connectedAccount.Address;
            egldValue.text = "EGLD: " + amount.ToDenominated();
            ht.text = "HT: " + connectedAccount.Username;
        }


        //linked to the UI button to open the Transactions screen
        public void ShowTransactions()
        {
            DemoScript.Instance.LoadScreen(Screens.Transactions);
        }

        //linked to the UI button to open the SC screen
        public void ShowSCScreen()
        {
            DemoScript.Instance.LoadScreen(Screens.SC);
        }

        //linked to the UI button to open the NFT screen
        public void ShowNFTScreen()
        {
            DemoScript.Instance.LoadScreen(Screens.NFT);
        }

        //linked to the disconnect button in editor
        public void Disconnect()
        {
            ElrondUnityTools.Manager.Disconnect();
        }
    }
}