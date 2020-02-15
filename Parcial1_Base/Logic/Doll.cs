using System.Collections.Generic;
using System.Linq;
using System;

namespace Parcial1_Base.Logic
{
    /// <summary>
    /// Definition for the player's avatar. Players dress up a doll to win the contest.
    /// </summary>
    public class Doll : IClonable<Doll>
    {
        /// <summary>
        /// The accessories collection.
        /// </summary>
        private List<Accessory> accessories = new List<Accessory>();
        private List<Doll> dolls = new List<Doll>();

        private Dress dress;
        private List<Bracelet> bracelets = new List<Bracelet>();

        private string name = "";



        /// <summary>
        /// The doll's name
        /// </summary>
        public string Name { get => name; protected set => name = value; }

        
        /// <summary>
        /// Whether the doll can b  e included in the contest.
        /// </summary>
        public bool CanParticipate { get => FindTypeInAccessories(typeof(Dress)); }

        /// <summary>
        /// The total accessories count worn by the doll.
        /// </summary>
        public int TotalAccessories { get => accessories.Count; }

        public int BraceletCount { get => 0; }

        /// <summary>
        /// The total style score, affected by each worn accessory.
        /// </summary>
        public int Style
        {
            get
            {
                int sum = 0;
                foreach(var a in accessories)
                {
                    sum += a.Style;
                }
                return sum;
            }
        }

        public Doll(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Removes the given accessory.
        /// </summary>
        /// <param name="a">The accessory to be removed</param>
        /// <returns>True if the accessory was being worn, then removed. False otherwise</returns>
        public bool Remove(Accessory a)
        {
            bool result = false;
            if (accessories.Contains(a))
            {
                if(a is Dress)
                {
                    accessories.Clear();
                    result = true;
                }
                else
                {
                    accessories.Remove(a);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Makes the doll wear the given accessory
        /// </summary>
        /// <param name="a">The accessory to be worn by the doll</param>
        /// <returns>True if the doll successfully wore the accessory. False otherwise</returns>
        public bool Wear(Accessory a)
        {
            if (a!=null)
            {
                switch (a)
                {
                    #region Dress
                    case Dress d:
                        if (!FindTypeInAccessories(d))
                        {
                            accessories.Add(d);
                            dress = d;
                            goto SuccessCase;
                        }
                        goto FailedCase;
                    #endregion
                    #region Necklace
                    case Necklace n:
                        if (!FindTypeInAccessories(n) && CanParticipate)
                        {
                            if (dress != null)
                            {
                                switch (dress.Color)
                                {
                                    case Dress.EColor.Red:
                                    case Dress.EColor.Black:
                                    case Dress.EColor.White:
                                        if (dress.Category != Dress.EDressCategory.Suit)
                                        {
                                            accessories.Add(n);
                                            goto SuccessCase;
                                        }
                                        goto FailedCase;
                                    default:
                                        goto FailedCase;
                                }
                            }
                        }
                        goto FailedCase;
                    #endregion
                    #region Purse
                    case Purse p:
                        if (!FindTypeInAccessories(p) && CanParticipate)
                        {
                            if (dress != null)
                            {
                                switch (dress.Color)
                                {
                                    case Dress.EColor.Black:
                                    case Dress.EColor.White:
                                    case Dress.EColor.None:
                                        p.StyleMod = 0;
                                        break;
                                    default:
                                        p.StyleMod = 0.5F;
                                        break;
                                }
                                accessories.Add(p);
                                goto SuccessCase;
                            }
                        }
                        goto FailedCase;
                    #endregion
                    #region Bracelet
                    case Bracelet b:
                        if(bracelets.Count < 5 && CanParticipate)
                        {
                            int allowedCount = 5;
                            switch (dress.Category)
                            {
                                case Dress.EDressCategory.Suit:
                                    allowedCount = 1;
                                    break;
                                case Dress.EDressCategory.Party:
                                    allowedCount = 3;
                                    break;
                                case Dress.EDressCategory.None:
                                    goto FailedCase;
                            }
                            if(bracelets.Count > allowedCount)
                            {
                                b.StyleMod = -0.75F;
                            }
                            else
                            {
                                foreach(var br in bracelets)
                                {
                                    br.StyleMod = 0;
                                }
                            }
                            accessories.Add(b);
                            bracelets.Add(b);
                            goto SuccessCase;
                        }
                        goto FailedCase;
                    #endregion
                }
            }
            else
            {
                goto FailedCase;
            }
           
            FailedCase:
            return false;

            SuccessCase:
            return true;
        }

        /// <summary>
        /// Searches for a specific type in the accesories list
        /// </summary>
        /// <param name="a">reference object</param>
        /// <returns>true if finds an element in the list of the same type, false if not</returns>
        bool FindTypeInAccessories(Accessory a)
        {
            foreach (var ac in accessories)
            {
                if(a.GetType() == ac.GetType())
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Searches for a specific type in the accesories list
        /// </summary>
        /// <param name="a">reference type</param>
        /// <returns>true if finds an element in the list of the same type, false if not</returns>
        bool FindTypeInAccessories(Type a)
        {
            foreach (var ac in accessories)
            {
                if(a == ac.GetType())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Doll object with the same values of this instance</returns>
        public Doll Copy()
        {
            if (dolls.Count < 4)
            {
                return new Doll(Name);
            }
            return null; 
        }
    }
}