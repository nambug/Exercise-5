using System;
using UnityEngine;

namespace Code
{
    /// <summary>
    /// Keeps track of the scores of the players.
    /// </summary>
    public class ScoreManager : MonoBehaviour {
        /// <summary>
        /// Singleton class
        /// </summary>
        private static ScoreManager _theScoreScript;
        
        /// <summary>
        /// GameObjects array for players
        /// </summary>
        public GameObject[] Players;
        
        /// <summary>
        /// UI elements to display the players' scores.
        /// </summary>
        public UnityEngine.UI.Text[] ScoreFields;
    
        /// <summary>
        /// Scores for the players
        /// </summary>
        private int[] _scores;
    
        /// <summary>
        /// Initialize component
        /// </summary>
        internal void Start(){
            _theScoreScript = this;
            _scores = new int[Players.Length];
            UpdateText();
        }
    
        /// <summary>
        /// Gets the position in the Players array of a given player
        /// </summary>
        /// <param name="player">Player to find</param>
        /// <returns>Index into the arrays</returns>
        static int PlayerNumber(GameObject player)
        {
            var playerNumber = Array.IndexOf(_theScoreScript.Players, player);
            if (playerNumber < 0)
                Debug.Log("Unknown player: "+player.name);
            return playerNumber;
        }
    
        /// <summary>
        /// Increase the score for a given player
        /// </summary>
        public static void IncreaseScore(GameObject player, int val)
        {
            var playerNumber = PlayerNumber(player);
            if (playerNumber>=0)
                _theScoreScript._scores[playerNumber] += val;
            _theScoreScript.UpdateText();
        }
    
        /// <summary>
        /// Update all the score text fields
        /// </summary>
        private void UpdateText(){
            for (int i=0; i<Players.Length; i++)
                ScoreFields[i].text = string.Format("{0}: {1}", Players[i].name, _scores[i]);
        }
    }

}
