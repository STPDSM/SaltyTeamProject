public class Cellule
{
    #region Attributs
    public bool _Ouest, _Nord, _Est, _Sud;    // Mur Ouest, Nord, Est et Sud.
    public bool _visite;                       // Booleen qui indique si la cellule a deja ete visitee, utile pour la fonction de generation

    public int x, z;                      // Position en X et en Z.
    #endregion

    #region Propriétés
    #endregion

    #region Enums
    #endregion

    #region Constructeur
    // Constructeur.
    public Cellule(bool ouest, bool nord, bool est, bool sud, bool visite)
    {
        this._Ouest = ouest;
        this._Nord = nord;
        this._Est = est;
        this._Sud = sud;
        this._visite = visite;
    }
    #endregion

}