using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerationLabyrinthe : MonoBehaviour
{
    #region Attributs
    public int _largeur, _hauteur; //Largeur et hauteur du labyrinthe.

    public VisualisationCellule visualisationCellulePrefab; // Prefab qui sert de modèle à l'instanciation.

    public Cellule[,] Cellules; //Tableau de cellules a deux dimensions.

    private Vector2 _PositionAleatoireCellule; //Position de la cellule ou commence la generation
    private VisualisationCellule visualCelluleInst; // Copie du prefab

    private List<CelluleEtPosition> voisins; //Liste des cellules voisines
    #endregion

    #region Propriétés
    #endregion

    #region Enums
    #endregion

    #region Méthodes
    void Start()
    {
        Cellules = new Cellule[_largeur, _hauteur]; //Initialisation du tableau de cellules
        Init(); //Lance la fonction Init
    }

    void Init()
    {
        for (int i = 0; i < _largeur; i++)
        {

            for (int j = 0; j < _hauteur;  j++)
            {

                Cellules[i, j] = new Cellule(false, false, false, false, false);
                Cellules[i, j].x = i;
                Cellules[i, j].z = j;
            }
        }
        RandomCellule(); //Lance la fonction RandomCellule

        InitVisualCell(); //Lance l'instantiation du visuel des cellules
    }

    void RandomCellule()
    {
        //Recupere une position aleatoire
        _PositionAleatoireCellule = new Vector2((int)UnityEngine.Random.Range(0, _largeur), (int)UnityEngine.Random.Range(0, _hauteur));

        //Lance la fonction GenerationLabyrinthe avec la positions aleatoire.
        GeneratationLabyrinthe((int)_PositionAleatoireCellule.x, (int)_PositionAleatoireCellule.y);
    }

    void GeneratationLabyrinthe(int x, int y)
    {
        Cellule currentCellule = Cellules[x, y]; //Definit la cellule courante
        voisins = new List<CelluleEtPosition>(); //Initialise la liste
        if (currentCellule._visite == true) return;
        currentCellule._visite = true;

        if (x + 1 < _largeur && Cellules[x + 1, y]._visite == false)
        { //Si la cellule de droite existe et n'est pas visitee par l'algorithme
            voisins.Add(new CelluleEtPosition(Cellules[x + 1, y], CelluleEtPosition.Direction.Est)); //Ajoute la cellule de droite dans la liste des voisins
        }

        if (y + 1 < _hauteur && Cellules[x, y + 1]._visite == false)
        { //Si la cellule du bas existe et n'est pas visitee par l'algorithme
            voisins.Add(new CelluleEtPosition(Cellules[x, y + 1], CelluleEtPosition.Direction.Sud)); //Ajoute la cellule du bas dans liste des voisins
        }

        if (x - 1 >= 0 && Cellules[x - 1, y]._visite == false)
        { //Si la cellule de gauche existe et n'est pas visitee par l'algorithme
            voisins.Add(new CelluleEtPosition(Cellules[x - 1, y], CelluleEtPosition.Direction.Ouest)); //Ajoute la cellule de gauche dans la liste des voisins
        }

        if (y - 1 >= 0 && Cellules[x, y - 1]._visite == false)
        { //Si la cellule du haut existe et n'est pas visitee par l'algorithme
            voisins.Add(new CelluleEtPosition(Cellules[x, y - 1], CelluleEtPosition.Direction.Nord)); //Ajoute la cellule du haut dans la liste des voisins
        }

        if (voisins.Count == 0) // Si il y a 0 voisins dans la liste on sort de la méthode.
            return;  

        voisins.Shuffle(); // Melange la liste de voisins

        foreach (CelluleEtPosition ActualCellule in voisins)
        {
            if (ActualCellule.direction == CelluleEtPosition.Direction.Est)
            {
                if (ActualCellule.cell._visite) continue;
                currentCellule._Est = true; //Enleve le mur de droite de la cellule acutelle
                ActualCellule.cell._Ouest = true; //Enleve le mur de gauche de la cellule voisine de droite
                GeneratationLabyrinthe(x + 1, y); //Relance la fonction sur la cellule voisine
            }

            else if (ActualCellule.direction == CelluleEtPosition.Direction.Sud)
            {
                if (ActualCellule.cell._visite) continue;
                currentCellule._Sud = true;//Enleve le mur de droite de la cellule acutelle
                ActualCellule.cell._Nord = true; //Enleve le mur du haut de la cellule voisine du bas
                GeneratationLabyrinthe(x, y + 1); //Relance la fonction sur la cellule voisine
            }
            else if (ActualCellule.direction == CelluleEtPosition.Direction.Ouest)
            {
                if (ActualCellule.cell._visite) continue;
                currentCellule._Ouest = true; //Enleve le mur de droite de la cellule acutelle
                ActualCellule.cell._Est = true; //Enleve le mur de droite de la cellule voisine de gauche
                GeneratationLabyrinthe(x - 1, y); //Relance la fonction sur la cellule voisine
            }
            else if (ActualCellule.direction == CelluleEtPosition.Direction.Nord)
            {
                if (ActualCellule.cell._visite) continue;
                currentCellule._Nord = true; //Enleve le mur de droite de la cellule acutelle
                ActualCellule.cell._Sud = true; //Enleve le mur du bas de la cellule voisine du haut
                GeneratationLabyrinthe(x, y - 1); //Relance la fonction sur la cellule voisine
            }
        }


    }

    void InitVisualCell()
    {
        // Initialise le visuel des cellules et enleve les murs en fonction des cellules virtuelles
        foreach (Cellule cellule in Cellules)
        {

            visualCelluleInst = Instantiate(visualisationCellulePrefab, new Vector3(cellule.x * 3, 0, _hauteur * 3f - cellule.z * 3), Quaternion.identity) as VisualisationCellule;
            visualCelluleInst.transform.parent = transform;
            visualCelluleInst._Nord.gameObject.SetActive(!cellule._Nord);
            visualCelluleInst._Sud.gameObject.SetActive(!cellule._Sud);
            visualCelluleInst._Est.gameObject.SetActive(!cellule._Est);
            visualCelluleInst._Ouest.gameObject.SetActive(!cellule._Ouest);

            visualCelluleInst.transform.name = cellule.x.ToString() + "_" + cellule.z.ToString();
        }

    }
    #endregion
}