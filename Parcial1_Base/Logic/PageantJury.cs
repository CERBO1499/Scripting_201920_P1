using System.Collections.Generic;
using System.Linq;

namespace Parcial1_Base.Logic
{
    public class PageantJury
    {
        /// <summary>
        /// The contestants collection.
        /// </summary>
        private List<Doll> contestants = new List<Doll>();

        /// <summary>
        /// Returns the total contestants count for a pageant round.
        /// </summary>
        public int TotalContestants { get => contestants.Count; }

        /// <summary>
        /// Adds a contestant to the pageant.
        /// </summary>
        /// <param name="d"></param>
        /// <returns>True if the contestant could be added, False otherwise</returns>
        public bool AddContestant(Doll d)
        {
            bool result = false;
            if(TotalContestants < 4)
            {
                if (d.CanParticipate)
                {
                    result = true;
                    contestants.Add(d);
                }
            }
            return result;
        }

        /// <summary>
        /// Clears the contestants collection
        /// </summary>
        public void ClearContestants()
        {
            contestants.Clear();
        }

        /// <summary>
        /// Returns the winner of the pageant
        /// </summary>
        /// <returns>The winner Doll</returns>
        public Doll GetWinner()
        {
            Doll winner = null;

            switch (contestants.Count)
            {
                case 1:
                    // Single contestant is deemed winner, no matter its score.
                    winner = contestants[0];
                    break;

                default:
                    contestants.OrderBy(x => x.Style);
                    winner = contestants[TotalContestants - 1];
                    break;
            }

            return winner;
        }
    }
}