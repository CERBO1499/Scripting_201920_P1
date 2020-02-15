namespace Parcial1_Base.Logic
{
    /// <summary>
    /// Dress is the base accessory for all dolls. Dolls can't participate without one.
    /// </summary>
    public class Dress : Accessory
    {
        /// <summary>
        /// Dress categories.
        /// </summary>
        public enum EDressCategory
        {
            None,
            Suit,
            Party,
            Casual
        }

        /// <summary>
        /// Dress colors
        /// </summary>
        public enum EColor
        {
            None,
            Black,
            White,
            Red,
            Blue,
            Yellow,
            Green,
            Pink
        }

        private EColor colorChikito;
        private EDressCategory categoryChikita;

        /// <summary>
        /// This dress' color
        /// </summary>
        public EColor Color { get => colorChikito; protected set => colorChikito = value; }

        /// <summary>
        /// This dress' category
        /// </summary>
        public EDressCategory Category { get => categoryChikita; protected set => categoryChikita = value; }

        public Dress(int style, EColor color, EDressCategory category) : base(style)
        {
            colorChikito = color;
            categoryChikita = category;
        }

        /// <summary>
        /// Copies this instance attributes to a new independant one
        /// </summary>
        /// <returns>A new Accessory object with the same values of this instance</returns>
        public override Accessory Copy()
        {
            return new Dress(style, Color, Category);
        }
    }
}