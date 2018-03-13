using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CelluleEtPosition
{
    #region Attributs
    public Cellule cell;           // Cellule
    public Direction direction; // Direction permettant de savoir quel mur detruire
    #endregion

    #region Propriétés
    #endregion

    #region Enums
    public enum Direction
    {
        Nord,
        Sud,
        Est,
        Ouest
    }
    #endregion

    #region Constructeur
    public CelluleEtPosition(Cellule cell, Direction direction)
    {
        this.cell = cell;
        this.direction = direction;
    }
    #endregion
}
