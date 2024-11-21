namespace KartverketGroup20.Data.Enum
{
    using System.ComponentModel.DataAnnotations;

    public enum Status
    {
        [Display(Name = "Ikke Behandlet")]
        IkkeBehandlet,

        [Display(Name = "Under Behandling")]
        UnderBehandling,

        [Display(Name = "Ikke Godkjent")]
        IkkeGodkjent,

        [Display(Name = "Godkjent")]
        Godkjent
    }

}
