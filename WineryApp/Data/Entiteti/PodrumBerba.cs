namespace WineryApp.Data.Entiteti
{
    public partial class PodrumBerba
    {
        public int PodrumId { get; set; }
        public int BerbaId { get; set; }

        public virtual Berba Berba { get; set; }
        public virtual Podrum Podrum { get; set; }
    }
}
