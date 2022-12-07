using MultiversXUnityTools;
using UnityEngine;
using UnityEngine.UI;

namespace MultiversXUnityExamples
{
    /// <summary>
    /// This class is added to NFT Holder GameObject to display an NFT
    /// </summary>
    public class NFTHolder : MonoBehaviour
    {
        public Image image;
        public Text nftName;
        public string collectionIdentifier;
        public ulong nonce;

        private NftsScreen demoScript;
        private string txHash;


        /// <summary>
        /// Initialize properties with values from the NFT
        /// </summary>
        /// <param name="demoScript">Main script</param>
        /// <param name="name">the display name for NFT</param>
        /// <param name="collectionIdentifier">collection identifier</param>
        /// <param name="nonce">nonce of the NFT (the characters after the last -(dash) from the NFT identifier)</param>
        public void Initialize(NftsScreen demoScript, string name, string collectionIdentifier, ulong nonce)
        {
            this.demoScript = demoScript;
            this.nftName.text = name;
            this.collectionIdentifier = collectionIdentifier;
            this.nonce = nonce;
        }


        //linked to the send button on screen
        public void SendNFT()
        {
            Manager.SendNFT(demoScript.nftDestination.text, collectionIdentifier, nonce, 1, CompleteListener);
        }


        /// <summary>
        /// Track the status of the send NFT transaction
        /// </summary>
        /// <param name="operationStatus">Completed, In progress or Error</param>
        /// <param name="message">if the operation status is complete, the message is the txHash</param>
        private void CompleteListener(OperationStatus operationStatus, string message)
        {
            demoScript.status.text = operationStatus + " " + message;
            if (operationStatus == OperationStatus.Complete)
            {
                txHash = message;
                Debug.Log("Tx Hash: " + txHash);
                Manager.CheckTransactionStatus(txHash, BlockchainTransactionListener, 1);
            }
            if (operationStatus == OperationStatus.Error)
            {
                //do something
            }
        }


        /// <summary>
        /// Listener for the transaction status response
        /// </summary>
        /// <param name="operationStatus">Completed, In progress or Error</param>
        /// <param name="message">additional message</param>
        private void BlockchainTransactionListener(OperationStatus operationStatus, string message)
        {
            demoScript.status.text = operationStatus + " " + message;
            if (operationStatus == OperationStatus.Complete)
            {
                demoScript.RefreshNFTs(collectionIdentifier, nonce);
                Destroy(gameObject);
            }
        }
    }
}