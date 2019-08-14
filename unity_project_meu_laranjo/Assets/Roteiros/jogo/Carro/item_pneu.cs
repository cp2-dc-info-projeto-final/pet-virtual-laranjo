using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "roda_", menuName = "Laranjo/Inventario/Item de Carro (Roda)")]
public class item_pneu : item_carro{
    public aroNumeros aro;

    public GameObject prefab_2;

    public enum aroNumeros{
        aro15 = 15,
        aro17 = 17,
        aro19 = 19,
        aro22 = 22
    }
}
